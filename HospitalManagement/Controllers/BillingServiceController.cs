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
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class BillingServiceController : _Base
    {
        readonly Common_Master COM;
        readonly InvestigationPrice_Master IPM;
        readonly DocAppointment_Master DocApp;
        readonly Service_Master Ser;
        readonly RoomType_Master RoomMaster;
        readonly BillingServiceItem_Entity BE;
        readonly Patient_admission_entity pat;
        // GET: BillingService
        public BillingServiceController()
        {
            COM = new Common_Master(bsInfo);
            IPM = new InvestigationPrice_Master(bsInfo);
            DocApp = new DocAppointment_Master(bsInfo);
            Ser = new Service_Master(bsInfo);
            BE = new BillingServiceItem_Entity();
            RoomMaster = new RoomType_Master(bsInfo);
            pat = new Patient_admission_entity();
        }
        DataSet ds = new DataSet();
        public ActionResult BillingService()
        {
            BillingServiceModel model = new BillingServiceModel();
            ds = Ser.GetAllServices(Convert.ToString(Session["ClinicCode"]), "");
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.BillingServicelst = Extend.ToList<BillingService_Entity>(ds.Tables[1]);
            model.SubInvgrouplst = new List<SubInvListData>();
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            //var data = ds.Tables[0];
            DataRow[] dr = ds.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new SubInvListData
                {
                    id = Convert.ToInt32(item[0]),
                    investigationmaster_id = Convert.ToInt32(item[1]),
                    invstigationmaster_name = Convert.ToString(item[3]),
                    inv_cost = Convert.ToDecimal(item[4]),
                    Amount = Convert.ToDecimal(item[5]),

                };
                model.SubInvgrouplst.Add(list);
            }
            return View("BillingService", model);
        }
        public ActionResult GetSubInvestigationPara(string categorycode, string patientcode)
        {
            BillingServiceModel model = new BillingServiceModel();
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.SubInvgrouplst = new List<SubInvListData>();
            model.BillingServicelst = new List<BillingService_Entity>();
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            var data = Ser.GetAllavialbaleINV(categorycode, Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = data.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new SubInvListData
                {
                    id = Convert.ToInt32(item[0]),
                    investigationmaster_id = Convert.ToInt32(item[1]),
                    invstigationmaster_name = Convert.ToString(item[3]),
                    inv_cost = Convert.ToDecimal(item[4]),
                    Amount = Convert.ToDecimal(item[5]),

                };
                model.SubInvgrouplst.Add(list);
                model.PatientCode = patientcode;
                model.ServiceType = "Investigation";
            }
            return View("BillingService", model);
        }
        [HttpPost]
        public JsonResult GatePatient(string Prefix)
        {
            //Note : you can bind same list from database  
            var dt = DocApp.GetAllPatient(Prefix);
            return Json(CustomHelper.DataTableToJSONWithJavaScriptSerializer(dt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetServiceCost(string ServiceId, string Type)
        {
            BillingServiceModel model = new BillingServiceModel();
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.SubInvgrouplst = new List<SubInvListData>();
            model.BillingServicelst = new List<BillingService_Entity>();
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            ds = Ser.GetServicecost(ServiceId, Type, Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.SellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["selling_price"]);
                model.MinSellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["minimum_selling_price"]);
                model.is_realtime = Convert.ToInt32(ds.Tables[0].Rows[0]["is_realtime"]);
                model.ServiceType = Convert.ToString(ds.Tables[0].Rows[0]["type"]);
                model.Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Cost"]);
            }
            return View("BillingService", model);
        }

        [HttpPost]
        public ActionResult AddServices(BillingServiceModel BS)
        {
            
            string Status = "";
            var entity = SetEntityData(BS);
            var res = Ser.Add(entity);
            if (res != "same")
            {
                if (res.ToInt() > 0 ? true : false)
                {
                    Status = "Record Added Successfully!";
                }
                else
                    Status = "Record Not Saved!";
            }
            else
                Status = "Record Allrady Exists!";
            ModelState.Clear();
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditAddServices(BillingServiceModel BS)
        {
            string Status = "";
            var entity = SetEntityData(BS);
            var res = Ser.EditAddServices(entity);
            if (res != "same")
            {
                if (res.ToInt() > 0 ? true : false)
                {
                    Status = "Record Added Successfully!";
                }
                else
                    Status = "Record Not Saved!";
            }
            else
                Status = "Record Allrady Exists!";
            ModelState.Clear();
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public BillingServiceItem_Entity SetEntityData(BillingServiceModel BSM)
        {

            BE.id = BSM.ServiceType == "Investigation" ? BSM.investingation_id : BSM.Service_id;
            BE.hos_code = Convert.ToString(Session["ClinicCode"]);
            BE.user_code = Convert.ToString(Session["UserCode"]);
            BE.service_name = BSM.ServiceName;
            BE.cost = BSM.ServiceType == "Investigation" ? BSM.MinSellingCost : BSM.Cost;
            BE.selling_price = BSM.SellingCost;
            BE.minimum_selling_price = BSM.MinSellingCost;
            BE.discount = BSM.Discount;
            BE.type = BSM.ServiceType;
            BE.is_realtime = BSM.is_realtime;
            BE.patient_code = BSM.PatientCode;
            BE.invoice_code = BSM.InvoiceCode;
            BE.referal_doctor = BSM.ReferalDoctorName;
            BE.remark = BSM.Remark;
            BE.ctrl = 1;
            return BE;
        }
        public ActionResult GetPatientBillingAddItem(string PatientCode, string Type, string InvoiceCode)
        {

            BillingServiceModel model = new BillingServiceModel();
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.BillingServicelst = new List<BillingService_Entity>();
            model.SubInvgrouplst = new List<SubInvListData>();
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            ds = Ser.GetPatientBillingItem(Convert.ToString(Session["ClinicCode"]), PatientCode, Type, InvoiceCode);
            model.BillingServiceItemlst = Extend.ToList<BillingServiceItem_Entity>(ds.Tables[0]);
            model.ReferalDoctorName = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["referal_doctor"]) : "";
            model.PatientCode = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["patient_code"]) : "";
            model.DOB = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["dob"]) : "";
            model.Address = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["address"]) : "";
            model.Gender = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["gender"]) : "";
            model.photo = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["photo"]) : "";
            model.FatherName = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["patient_fathername"]) : "";
            model.Mobile = ds.Tables[1].Rows.Count > 0 ? Convert.ToString(ds.Tables[1].Rows[0]["phone"]) : "";
            model.Comission_Agent_List = Extend.ToList<ComissionAgent_Entity>(ds.Tables[2]);
            return View("BillingService", model);
        }
        public ActionResult GetPatientBillingEditItem(string PatientCode, string Type, string InvoiceCode)
        {

            BillingServiceModel model = new BillingServiceModel();
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.BillingServicelst = new List<BillingService_Entity>();
            model.SubInvgrouplst = new List<SubInvListData>();
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            ds = Ser.GetPatientBillingItem(Convert.ToString(Session["ClinicCode"]), PatientCode, Type, InvoiceCode);
            model.BillingServiceItemlst = Extend.ToList<BillingServiceItem_Entity>(ds.Tables[0]);
            model.ReferalDoctorName = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["referal_doctor"]) : "";
            return View("EditBillingServiceInvoice", model);
        }
        [HttpPost]
        public ActionResult AddBillingServiceItemInvoice(BillingServiceModel BSM)
        {
            string status = "";
            DataTable dt = CreateTable();
            foreach (var item in BSM.BillingServiceItemlst)
            {
                if (item.id != 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = Convert.ToInt32(item.id);
                    dr["patient_code"] = Convert.ToString(item.patient_code);
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                var hoscode = Convert.ToString(Session["ClinicCode"]);
                var usercode = Convert.ToString(Session["UserCode"]);
                bool add = Ser.AddBillingServiceItemInvoice(dt, hoscode, usercode, Convert.ToInt32(BSM.Comission_Agent_Id));
                if (add)
                {
                    status = "1";
                }
                else
                    status = "0";
            }
            else
                status = "2";

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("patient_code", typeof(string));
            return dt;
        }
        public ActionResult EditBillingServiceInvoice(string enc)
        {
            BillingServiceModel model = new BillingServiceModel();
            ds = Ser.GetAllServices(Convert.ToString(Session["ClinicCode"]), enc);
            model.Investigationgrouplst = new List<Investigationgroup_Entity>();
            model.SubInvgrouplst = new List<SubInvListData>();
            DataRow[] dr = ds.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new SubInvListData
                {
                    id = Convert.ToInt32(item[0]),
                    investigationmaster_id = Convert.ToInt32(item[1]),
                    invstigationmaster_name = Convert.ToString(item[3]),
                    inv_cost = Convert.ToDecimal(item[4]),
                    Amount = Convert.ToDecimal(item[5]),

                };
                model.SubInvgrouplst.Add(list);
            }
            model.BillingServicelst = Extend.ToList<BillingService_Entity>(ds.Tables[1]);
            model.BillingServiceItemlst = new List<BillingServiceItem_Entity>();
            model.BillingServiceItemlst = Extend.ToList<BillingServiceItem_Entity>(ds.Tables[3]);
            model.ReferalDoctorName = ds.Tables[3].Rows.Count > 0 ? Convert.ToString(ds.Tables[3].Rows[0]["referal_doctor"]) : "";
            model.PatientCode = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["patient_code"]) : "";
            model.DOB = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["dob"]) : "";
            model.Address = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["address"]) : "";
            model.Gender = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["gender"]) : "";
            model.photo = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["photo"]) : "";
            model.PatientName = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["patient_name"]) : "";
            model.InvoiceCode = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["invoice_code"]) : "";
            model.FatherName = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["patient_fathername"]) : "";
            model.Mobile = ds.Tables[2].Rows.Count > 0 ? Convert.ToString(ds.Tables[2].Rows[0]["phone"]) : "";
            return View("EditBillingServiceInvoice", model);
        }
        [HttpPost]
        public ActionResult Delete_Service(int id)
        {
            bool i = Ser.DeleteBillingService(id, Convert.ToString(Session["ClinicCode"]), "", "delete_billservice", Convert.ToString(Session["UserCode"]));
            return Json(i == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Confirm_Insert_Service(string PatientCode, string ReferalDoctor, string Serviceid, string SellingCost, string MinsellingCost, string Discount, string Invoicecode)
        {
            bool i = Ser.confirmimsertservice(PatientCode, ReferalDoctor, Convert.ToInt32(Serviceid), Convert.ToDecimal(SellingCost), Convert.ToDecimal(MinsellingCost), Convert.ToDecimal(Discount), Convert.ToString(Session["ClinicCode"]), Invoicecode);
            return Json(i == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit_Delete_Service(int id, string invoice_code)
        {
            bool i = Ser.DeleteBillingService(id, Convert.ToString(Session["ClinicCode"]), invoice_code, "delete_editbillservice", Convert.ToString(Session["UserCode"]));
            return Json(i == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        // GET: BillingService
        public ActionResult AddBillingServiceItem()
        {
            BillingServiceItemModel Model = new BillingServiceItemModel();
            ds = Ser.GetAll(Convert.ToString(Session["ClinicCode"]));
            Model.ALlData = Extend.ToList<ServiceList>(ds.Tables[0]);
            Model.ctrl = 1;
            return View("AddBillingServiceItem", Model);
        }
        [HttpPost]
        public ActionResult Add_Update(BillingServiceItemModel Data)
        {
            if (Data.id == 0)
            {
                ds = Ser.Insert(Data.service_name, Convert.ToString(Session["ClinicCode"]), Data.cost, Data.selling_price, Data.minimum_selling_price, Data.type, Convert.ToInt16(Data.is_realtime), Convert.ToInt16(Data.is_admission), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = Ser.Update(Data.id, Data.service_name, Convert.ToString(Session["ClinicCode"]), Data.cost, Data.selling_price, Data.minimum_selling_price, Data.type, Convert.ToInt16(Data.is_realtime), Convert.ToInt16(Data.is_admission), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new Status();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.ALlData = Extend.ToList<ServiceList>(ds.Tables[1]);
            }
            return View("AddBillingServiceItem", Data);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            BillingServiceItemModel Model = new BillingServiceItemModel();
            ds = Ser.Delete(id, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            if (ds.Tables.Count > 0)
            {
                Model.ResponseMsg = new Status();
                Model.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Model.ALlData = Extend.ToList<ServiceList>(ds.Tables[1]);
            }
            return View("AddBillingServiceItem", Model);
        }

        public ActionResult PatientAdmission()
        {
            PatientAdmissionModel Model = new PatientAdmissionModel();

            var dataSet = Ser.GetAllService(Convert.ToString(Session["ClinicCode"]));
            Model.RoomTypeList = Extend.ToList<RoomTypeList>(dataSet.Tables[0]);
            Model.WardTypeList = Extend.ToList<wardType_Entity>(dataSet.Tables[1]);
            Model.department_list = Extend.ToList<RegDepartment_list>(dataSet.Tables[2]);
            Model.Comission_Agent_List = Extend.ToList<ComissionAgent_Entity>(dataSet.Tables[3]);
            Model.doctor_list = new List<RegDoctor_list>();
            Model.RoomList = new List<Room_Entity>();
            Model.BedList = new List<Bed_Entity>();
            return View("PatientAdmission", Model);
        }
        [HttpPost]
        public JsonResult GatePatientadmission(string Prefix)
        {
            //Note : you can bind same list from database  
            var dt = Ser.GetAllPatientAdmission(Prefix, Convert.ToString(Session["ClinicCode"]));
            return Json(CustomHelper.DataTableToJSONWithJavaScriptSerializer(dt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdmissionPatientdetail(string PatientCode)
        {

            PatientAdmissionModel model = new PatientAdmissionModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            model.department_list = new List<RegDepartment_list>();
            model.doctor_list = new List<RegDoctor_list>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            ds = Ser.GetAdmissionPatientdetail(Convert.ToString(Session["ClinicCode"]), PatientCode);
            model.PatientCode = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["patient_code"]) : "";
            model.DOB = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["dob"]) : "";
            model.Address = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["address"]) : "";
            model.Gender = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["gender"]) : "";
            model.photo = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["photo"]) : "";
            model.FatherName = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["patient_fathername"]) : "";
            model.Mobile = ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["phone"]) : "";
            return View("PatientAdmission", model);
        }
        public PartialViewResult GetReferalDoctor(string departmentcode)
        {
            //var admit_id = Convert.ToInt32(Session["UniqRow"]);
            PatientAdmissionModel model = new PatientAdmissionModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            model.department_list = new List<RegDepartment_list>();
            model.doctor_list = new List<RegDoctor_list>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            var dataset = Ser.GetRefDoctor(Convert.ToString(Session["ClinicCode"]), departmentcode);
            model.doctor_list = Extend.ToList<RegDoctor_list>(dataset.Tables[0]);
            return PartialView("PatientAdmission", model);
        }
        public PartialViewResult GetRooms(int roomtype_id, string Type, int roomno = 0, string roomfacility = "")
        {
            PatientAdmissionModel model = new PatientAdmissionModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            model.department_list = new List<RegDepartment_list>();
            model.doctor_list = new List<RegDoctor_list>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            if (Type == "rooms")
            {
                var dataset = RoomMaster.GetRooms(Convert.ToString(Session["ClinicCode"]), roomtype_id, roomfacility);
                model.RoomList = Extend.ToList<Room_Entity>(dataset.Tables[0]);

            }
            else if (Type == "beds")
            {
                var dataset = RoomMaster.GetBeds(Convert.ToString(Session["ClinicCode"]), roomtype_id, roomno);
                model.BedList = Extend.ToList<Bed_Entity>(dataset.Tables[0]);
            }
            return PartialView("PatientAdmission", model);
        }
        public PartialViewResult GetwardType(string roomfacility = "")
        {
            PatientAdmissionModel model = new PatientAdmissionModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            model.department_list = new List<RegDepartment_list>();
            model.doctor_list = new List<RegDoctor_list>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            var dataset = RoomMaster.GetWards(Convert.ToString(Session["ClinicCode"]), roomfacility);
            model.RoomTypeList = Extend.ToList<RoomTypeList>(dataset.Tables[0]);
            return PartialView("PatientAdmission", model);
        }
        [HttpPost]
        public ActionResult AddPatientAdmission(PatientAdmissionModel BS)
        {
            string Status = "";
            var entity = SetEntityDatapatientAdmission(BS);
            var res = Ser.AddPatientAdmission(entity);
            if (res != "same")
            {
                if (res.ToInt() > 0 ? true : false)
                {
                    Status = "Patient Admited Sucessfully!!";
                }
                else
                    Status = "Record Not Saved!";
            }
            else
                Status = "Patient Allrady Admited This Hospital!!";
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public Patient_admission_entity SetEntityDatapatientAdmission(PatientAdmissionModel BSM)
        {

            pat.patient_code = BSM.PatientCode;
            pat.hos_code = Convert.ToString(Session["ClinicCode"]);
            pat.user_code = Convert.ToString(Session["UserCode"]);
            pat.doctor_code = BSM.doctorcode;
            pat.department_code = BSM.departmentcode;
            pat.addmission_note = BSM.admissionnote;
            pat.document_inclusion = BSM.documentinclusion;
            pat.gardianname = BSM.gardianName;
            pat.mobile = BSM.gardianmobile;
            pat.roomtype_id = Convert.ToInt32(BSM.room_no);
            pat.no_of_bed = BSM.bed_no;
            pat.roomfacility = BSM.Roomfacility;
            pat.comission_agent_id = Convert.ToInt32(BSM.Comission_Agent_Id);
            return pat;
        }
    }
}