using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Helper;
using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class LabSupplierController : _Base
    {
        readonly LabSupplier_Master lab_master;
        public LabSupplierController()
        {
            lab_master = new LabSupplier_Master(bsInfo);
        }

        #region "LabSupplier Add Master"
        // GET: LabSupplier
        public ActionResult LabMaster()
        {
            LabSupplierModel Model = new LabSupplierModel();
            DataSet ds = lab_master.GetAllLab(Convert.ToString(Session["ClinicCode"]));
            Model.LabList = Extend.ToList<LabSupplierList>(ds.Tables[0]);
            return View(Model);
        }
        public PartialViewResult AddLab(LabSupplierModel M)
        {
            var ds = lab_master.SaveLabSupplier(Convert.ToString(Session["ClinicCode"]), M.SupplierId, M.LabName, M.RegisterNo, M.Mobile, M.Address, M.ctrl, Convert.ToString(Session["UserCode"]));
            LabSupplierModel Model = new LabSupplierModel();
            Model.Status = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            Model.LabList = Extend.ToList<LabSupplierList>(ds.Tables[1]);
            return PartialView("LabMaster", Model);
        }
        public JsonResult GetEditData(int Id)
        {
            LabSupplierModel M = new LabSupplierModel();
            var dt = lab_master.Getdata(Convert.ToString(Session["ClinicCode"]), Id);
            M.LabList = Extend.ToList<LabSupplierList>(dt);
            return Json(M.LabList, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Delete(int Id)
        {
            LabSupplierModel M = new LabSupplierModel();
            var usercode = Convert.ToString(Session["UserCode"]);
            var ds = lab_master.DeleteData(Convert.ToString(Session["ClinicCode"]), Id, usercode);
            M.Status = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            M.LabList = Extend.ToList<LabSupplierList>(ds.Tables[1]);
            return PartialView("LabMaster", M);
        }
        #endregion

        #region "LabSupplier Price"
        public ActionResult LabSupplierPrice()
        {
            LabSupplierPrice model = new LabSupplierPrice();
            DataSet ds = lab_master.GetAllGroupName(Convert.ToString(Session["ClinicCode"]));
            model.InvGroupList = Extend.ToList<InvGroup>(ds.Tables[0]);
            model.Sup_LabList = Extend.ToList<LabList>(ds.Tables[1]);
            return View("LabSupplierPrice", model);
        }
        public ActionResult GetSupInvestigationPara(string categorycode, int Supplier_Id)
        {
            LabSupplierPrice model = new LabSupplierPrice();
            model.InvGroupList = new List<InvGroup>();
            model.Sup_LabList = new List<LabList>();
            model.InvpriceList = new List<InvestigationPrice>();
            var data = lab_master.GetAllData(categorycode, Convert.ToString(Session["ClinicCode"]), Supplier_Id);
            DataRow[] dr = data.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new InvestigationPrice
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
                model.InvpriceList.Add(list);
            }
            return View("LabSupplierPrice", model);
        }
        public ActionResult AddSupplierInvestigationPrice(LabSupplierPrice model)
        {
            string status = "";
            DataTable dt = CreateTable();
            if (model.InvpriceList != null)
            {
                var lst = model.InvpriceList.Where(a => a.IsSelected == true).ToList();
                if (lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        DataRow dr = dt.NewRow();
                        dr["id"] = Convert.ToInt32(item.id);
                        dr["investigationmaster_id"] = Convert.ToInt32(item.investigationmaster_id);
                        dr["investigation_proceddure"] = Convert.ToString(item.invstigationmaster_Procedure);
                        dr["inv_cost"] = 0;//Convert.ToDecimal(item.inv_cost);
                        dr["price"] = Convert.ToDecimal(item.Amount);
                        dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
                        dr["ctrl"] = Convert.ToInt32(item.ctrl);
                        dt.Rows.Add(dr);
                    }
                    bool add = lab_master.AddSupplierInvestigationPrice(dt, model.supplier_id, Convert.ToString(Session["UserCode"]));
                    if (add)
                    {
                        status = "Record Added Successfully!";
                    }
                    else
                        status = "Record Not Saved!";
                }
                else
                    status = "Select atleast one record";
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
        #endregion
    }
}