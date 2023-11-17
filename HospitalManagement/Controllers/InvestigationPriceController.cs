using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using System.Data;
using System.IO;
using System.Globalization;
using iTextSharp.text.pdf.qrcode;
using System.Drawing;
using System.Configuration;
using ImageResizer;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class InvestigationPriceController : _Base
    {
        // GET: InvestigationPrice
        readonly Investigation_Entity entity;
        readonly Common_Master COM;
        readonly InvestigationPrice_Master IPM;
        public InvestigationPriceController()
        {
            COM = new Common_Master(bsInfo);
            IPM = new InvestigationPrice_Master(bsInfo);
            entity = new Investigation_Entity();

        }

        #region "Investigation Price"
        public ActionResult InvestigationPrice()
        {
            InvestigationPriceModel model = new InvestigationPriceModel();
            model.Investigationgrouplst = COM.GetAllGroupName();
            return View("InvestigationPrice", model);
        }
        public ActionResult GetInvestigationPara(string categorycode)
        {
            InvestigationPriceModel model = new InvestigationPriceModel();
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.PriceListData = new List<PriceListData>();
            var data = IPM.GetAll(categorycode, Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = data.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new PriceListData
                {
                    id = Convert.ToInt32(item[0]),
                    investigationmaster_id = Convert.ToInt32(item[1]),
                    invstigationmaster_Procedure = Convert.ToString(item[2]),
                    IsSelected = Convert.ToBoolean(item[3]),
                    invstigationmaster_name = Convert.ToString(item[4]),
                    inv_cost = Convert.ToDecimal(item[5]),
                    Amount = Convert.ToDecimal(item[6]),
                    ctrl = Convert.ToBoolean(item[7])
                };
                model.PriceListData.Add(list);
            }
            return View("InvestigationPrice", model);
        }
        public ActionResult AddInvestigationPrice(InvestigationPriceModel model)
        {
            string status = "";
            DataTable dt = CreateTable();
            var lst = model.PriceListData.Where(a => a.IsSelected == true).ToList();
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = Convert.ToInt32(item.id);
                    dr["investigationmaster_id"] = Convert.ToInt32(item.investigationmaster_id);
                    dr["investigation_proceddure"] = Convert.ToString(item.invstigationmaster_Procedure);
                    dr["inv_cost"] = Convert.ToDecimal(item.inv_cost);
                    dr["price"] = Convert.ToDecimal(item.Amount);
                    dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
                    dr["ctrl"] = Convert.ToInt32(item.ctrl);
                    dt.Rows.Add(dr);
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                bool add = IPM.AddInvestigationPrice(dt, hos_code, user_code);
                if (add)
                {
                    status = "Record Added Successfully!";
                }
                else
                    status = "Record Not Saved!";
            }
            else
                status = "Select atleast one record";

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("investigationmaster_id", typeof(int));
            dt.Columns.Add("investigation_proceddure", typeof(string));
            dt.Columns.Add("inv_cost", typeof(decimal));
            dt.Columns.Add("price", typeof(decimal));
            dt.Columns.Add("hos_code", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            return dt;
        }

        public DataTable CreateTableSign()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DoctorCode", typeof(string));
            dt.Columns.Add("SignaturePath", typeof(string));
            return dt;
        }

        #endregion

        #region "Investigation Mapp with Doctor For investigation Approval provide"
        public ActionResult LabDoctorSeting()
        {
            LabDoctorModel Model = new LabDoctorModel();
            Model.InvestigationGroupLst = COM.GetAllGroupName();
            return View(Model);
        }
        public ActionResult GetLabDoctorList(string categorycode)
        {
            LabDoctorModel model = new LabDoctorModel();
            model.InvestigationGroupLst = new List<Investigationgroup_Entity>();
            model.DoctorList = new List<LabDoctorList>();
            var data = IPM.GetAll_LabDoctor(categorycode, Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = data.Tables[0].Select().OrderByDescending(u => u["IsSelected"]).ToArray(); 
            foreach (var item in dr)
            {
                if (Convert.ToBoolean(item[2]))
                {
                    model.DoctorCodes += Convert.ToString(item[0]) + ",";
                }
                var list = new LabDoctorList
                {
                    doctor_code = Convert.ToString(item[0]),
                    doctor_name = Convert.ToString(item[1]),
                    IsSelected = Convert.ToBoolean(item[2]),
                    Signature =Convert.ToString(item[3])
                };
                model.DoctorList.Add(list);
            }
            if (model.DoctorCodes != "" && model.DoctorCodes != null)
                model.DoctorCodes = model.DoctorCodes.Substring(0, model.DoctorCodes.Length - 1);
            return View("LabDoctorSeting", model);
        }
        public ActionResult AddLabDoctor(LabDoctorModel model)
        {
            string status = "";
            string myfile = "";
            DataTable dt = CreateTableSign();
            var lst = model.DoctorList.Where(a => a.IsSelected == true).ToList();
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    var doctorcode = item.doctor_code;
                    if (item.signaturepath != null)
                    {
                        HttpPostedFileBase imgfile = item.signaturepath;
                        var folderpath = ConfigurationManager.AppSettings["addDoctorSignature"];
                        myfile = doctorcode + Path.GetExtension(imgfile.FileName);
                        string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                        if (System.IO.File.Exists(folderpath + myfile))
                        {
                            System.IO.File.Delete(folderpath + myfile);
                        }
                       imgfile.SaveAs(fname);
                        ResizeSettings resizeSetting = new ResizeSettings
                        {
                            Width = 150,
                            Height = 75,
                            Format = "jpg"
                        };
                        ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    }
                    else
                    {
                        myfile = item.Signature;
                    }
                    DataRow dr = dt.NewRow();
                    dr["DoctorCode"] = Convert.ToString(item.doctor_code);
                    dr["SignaturePath"] = Convert.ToString(myfile);
                    dt.Rows.Add(dr);
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
               bool add = IPM.AddLabDoctorList(dt, hos_code, user_code,model.CategoryCode);
                if (add)
                    status = "1";
                else
                    status = "0";

            }
            else {
                status = "2";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pathology Employee Map with Investigation
        public ActionResult LabtechnicianMapping()
        {
            LabtechnicianModal Modal = new LabtechnicianModal();
            var ds = IPM.GetAllMapdata(Convert.ToString(Session["ClinicCode"]));
            Modal.EmpList = ds.Tables[0].ToList<Employes>();
            Modal.InvGroupList = GetetLists(ds.Tables[1]);
            Modal.LabTechList = LabList(ds.Tables[2]);
            return View(Modal);
        }
        private static List<SelectListItem> GetetLists(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(dt.Rows[i]["Text"]),
                    Value = Convert.ToString(dt.Rows[i]["Value"])
                });
            }
            return items;
        }
        public PartialViewResult MappTechnician(LabtechnicianModal Modal)
        {
            LabtechnicianModal Data = new LabtechnicianModal();
            string status;
            if (Modal.CategoryCode != null)
            {
                string CategoryCodes = "";
                foreach (var item in Modal.CategoryCode)
                {
                    CategoryCodes += item + ",";
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                var dt = IPM.MappingTechnician(CategoryCodes.TrimEnd(','), Modal.EmpCode, hos_code, user_code, out status);
                Data.LabTechList = LabList(dt);
            }
            else
                status = "2";
            Data.Status = status;
            return PartialView("LabtechnicianMapping", Data);
        }
        public List<LabTachnicianList> LabList(DataTable dt)
        {
            List<LabTachnicianList> Lst = new List<LabTachnicianList>();
            DataRow[] dr = dt.Select();
            foreach (var item in dr)
            {
                var list = new LabTachnicianList
                {
                    Id = Convert.ToInt16(item[0]),
                    EmployeeName = Convert.ToString(item[1]),
                    InvestigationGroupName = Convert.ToString(item[2]),
                    SignaturePath = Convert.ToString(item[3]),
                    EmployeeCode = Convert.ToString(item[4])
                };
                Lst.Add(list);
            }
            return Lst;
        }
        public JsonResult UploadImage(LabtechnicianModal Modal)
        {
            string Status;
            var Datalist = Modal.LabTechList.FindAll(m => m.Signature != null).ToList();
            DataTable dt = UploadEmployeImageDt();
            foreach (var item in Datalist)
            {
                if (item.Signature != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmployeeCode"] = Convert.ToString(item.EmployeeCode);
                    dr["SignaturePath"] = SaveImage(Convert.ToString(item.EmployeeCode), item.Signature);
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                bool a = IPM.EmployeeSignatureUpload(dt, hos_code, user_code);
                Status = a == true ? "1" : "0";
            }
            else
                Status = "2";
            return Json(new { status = Status }, JsonRequestBehavior.AllowGet);
        }
        public string SaveImage(string EmpCode, HttpPostedFileBase filesImg)
        {
            string myfile;
            HttpPostedFileBase imgfile = filesImg;
            var folderpath = ConfigurationManager.AppSettings["addEmployeeSignature"];
            myfile = EmpCode + Path.GetExtension(imgfile.FileName);
            string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
            if (System.IO.File.Exists(folderpath + myfile))
            {
                System.IO.File.Delete(folderpath + myfile);
            }
            imgfile.SaveAs(fname);
            ResizeSettings resizeSetting = new ResizeSettings
            {
                Width = 150,
                Height = 75,
                Format = "jpg"
            };
            ImageBuilder.Current.Build(fname, fname, resizeSetting);
            return myfile;
        }
        public DataTable UploadEmployeImageDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeCode", typeof(string));
            dt.Columns.Add("SignaturePath", typeof(string));
            return dt;
        }
        public JsonResult DeleteLabtechnican(string EmployeCode)
        {
            string Status;
            bool a = IPM.UnMappingLabtechnican(EmployeCode, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            Status = a == true ? "1" : "0";
            return Json(new { status = Status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region"Investigation Collection Room"
        public ActionResult InvestigationCollectionRoom()
        {
            InvestigationCollectionRoomModel model = new InvestigationCollectionRoomModel();
            model.Inv_Collection_Room_Data = new List<InvestigationCollectionRoomList>();
            var data = IPM.GetAllInvCollectionRoom(Convert.ToString(Session["ClinicCode"]));

            DataRow[] dr = data.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new InvestigationCollectionRoomList
                {
                    Id = Convert.ToInt32(item[0]),
                    investigationgroup_id = Convert.ToInt32(item[1]),
                    investigationgroup_code = Convert.ToString(item[2]),
                    investigationgroup_name = Convert.ToString(item[3]),
                    collection_room_no = Convert.ToString(item[4]),
                    ctrl = Convert.ToBoolean(item[5]),
                    IsSelected = Convert.ToBoolean(item[6]),
                };
                model.Inv_Collection_Room_Data.Add(list);
            }
            return View("InvestigationCollectionRoom", model);
        }

        public DataTable CreateTableCollectionRoom()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("investigationgroup_id", typeof(int));
            dt.Columns.Add("investigationgroup_code", typeof(string));
            dt.Columns.Add("collection_room_no", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            return dt;
        }

        public ActionResult AddInvCollectionRoom(InvestigationCollectionRoomModel model)
        {
            string status = "";
            DataTable dt = CreateTableCollectionRoom();
            var lst = model.Inv_Collection_Room_Data.Where(a => a.IsSelected == true).ToList();
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = Convert.ToInt32(item.Id);
                    dr["investigationgroup_id"] = Convert.ToInt32(item.investigationgroup_id);
                    dr["investigationgroup_code"] = Convert.ToString(item.investigationgroup_code);
                    dr["collection_room_no"] = Convert.ToString(item.collection_room_no);
                    dr["ctrl"] = Convert.ToInt32(item.ctrl);
                    dt.Rows.Add(dr);
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                bool add = IPM.AddInvestigationCollectionRoom(dt, hos_code, user_code);
                if (add)
                {
                    status = "Record Added Successfully!";
                }
                else
                    status = "Record Not Saved!";
            }
            else
                status = "Select atleast one record";

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}