using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using ImageResizer;
using System.IO;
using System.Data;
using System.Globalization;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class HMSLabController : _Base
    {
        // GET: HMSLab
        readonly Lab_Master LabMaster;
        readonly Hos_Purchase_Entity HE;

        public HMSLabController()
        {
            LabMaster = new Lab_Master(bsInfo);
            HE = new Hos_Purchase_Entity();
        }

        public ActionResult AddHosPurchaseItemNew()
        {
            List<MainPurchase> main = new List<MainPurchase>();
            List<HMSPurchase> sub = new List<HMSPurchase>();
            var res = LabMaster.GetAllRegent();
            List<SelectListItem> EM = new List<SelectListItem>();
            foreach (DataRow dr in res.Tables[0].Rows)
            {
                var ListPro = new SelectListItem
                {
                    Text = (dr["reagent_name"]).ToString(),
                    Value = (dr["reagent_id"]).ToString(),
                };
                EM.Add(ListPro);
            }
            sub = new List<HMSPurchase> { new HMSPurchase { ReagentId = 0, Qty = 0, ProductExpiry = "", QtyPerUnit = 0, Amount = 0, ValidityDays =0, Reg_List = EM } };
            main = new List<MainPurchase> { new MainPurchase { purchaseItem = sub, supplier = "", InvoiceRefNo = "", PurchaseDate = "", PICode = "" } };
            return View(main);
        }

        public JsonResult AddHosPurchase(List<MainPurchase> MainPurchaseList)
        {
            string Message ="";
            if (MainPurchaseList[0].purchaseItem.Count > 0)
            {
                DataTable dt = Extend.ToDataTable(MainPurchaseList[0].purchaseItem);
                Message = LabMaster.SaveHosPurchase(dt, MainPurchaseList[0].supplier, MainPurchaseList[0].InvoiceRefNo, MainPurchaseList[0].PurchaseDate, MainPurchaseList[0].PICode, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            }
            else
            {
                Message = "0";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPurchaseItem()
        {
            List<MainPurchase> main = new List<MainPurchase>();
            List<HMSPurchase> sub = new List<HMSPurchase>();
            string code = Convert.ToString(Session["UniqRow"]);
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = LabMaster.getPurchasedataEdit(hoscode, code);
            List<SelectListItem> EM = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var ListPro = new SelectListItem
                {
                    Text = (dr["reagent_name"]).ToString(),
                    Value = (dr["reagent_id"]).ToString(),
                };
                EM.Add(ListPro);
            }
            DataTable dt = ds.Tables[1];
            DataTable dt1 = ds.Tables[2];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sub.Add( new HMSPurchase { ReagentId =Convert.ToInt32(dt.Rows[i]["reagent_id"]), Qty = Convert.ToInt32(dt.Rows[i]["qty"]), ProductExpiry = Convert.ToString(dt.Rows[i]["product_expiry"]), QtyPerUnit = Convert.ToInt32(dt.Rows[i]["qty_per_unit"]), Amount = Convert.ToDecimal(dt.Rows[i]["amount"]), ValidityDays= Convert.ToInt32(dt.Rows[i]["validity_days"]), Reg_List = EM });
                }
            }
            main.Add( new MainPurchase { purchaseItem = sub, supplier = Convert.ToString(dt1.Rows[0]["supplier"]), InvoiceRefNo = Convert.ToString(dt1.Rows[0]["invoice_ref_no"]), PurchaseDate = Convert.ToString(dt1.Rows[0]["purchase_date"]),PICode= Convert.ToString(dt1.Rows[0]["pi_code"])});
            return View(main);
        }

        public ActionResult HosPurchaseItemInvoiceList()
        {
            HosPurchaseItemInovice model = new HosPurchaseItemInovice();
            DataSet ds = LabMaster.GetAllPurchaseItemInv(Convert.ToString(Session["ClinicCode"]));
            model.hos_purchaseItem_Invoice = Extend.ToList<hos_purchaseItem_Inv>(ds.Tables[0]);
            model.hos_purchaseItem = new List<Hos_Purchase_Entity>();
            return View("HosPurchaseItemInvoiceList",model);
        }

        public ActionResult InvoiceDelete(string InvCode)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = LabMaster.Delete(InvCode, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewPurchaseItemInvoice()
        {
            HosPurchaseItemInovice model = new HosPurchaseItemInovice();
            string code = Convert.ToString(Session["UniqRow"]);
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = LabMaster.getPurchasedatacode(hoscode, code);
            model.hos_purchaseItem = Extend.ToList<Hos_Purchase_Entity>(ds.Tables[0]);
            model.hos_purchaseItem_Invoice = Extend.ToList<hos_purchaseItem_Inv>(ds.Tables[1]);
            return View("ViewPurchaseItemInvoice",model);
        }

        public ActionResult HosCollactionOperation()
        {
            HosCollectionOprModel model = new HosCollectionOprModel();
            DataSet ds = LabMaster.GetAllTestInv();
            model.testlist = Extend.ToList<TestItem_Entity>(ds.Tables[0]);
            model.Reg_List = new List<SelectListItem>();

          
            //model.status = 1;
            return View("HosCollactionOperation",model);

        }
        public PartialViewResult GetDatabyCode(int testid)
        {
            HosCollectionOprModel model = new HosCollectionOprModel();
            model.testlist = new List<TestItem_Entity>();
            model.Reg_List = new List<SelectListItem>();
            var res = LabMaster.GetRegentItemBytest(testid);
            List<SelectListItem> EM = new List<SelectListItem>();
            foreach (DataRow dr in res.Tables[0].Rows)
            {
                var ListPro = new SelectListItem
                {
                    Text = (dr["reagent_name"]).ToString(),
                    Value = (dr["reagent_id"]).ToString(),
                };
                EM.Add(ListPro);
            }
            model.Reg_List = EM;

            return PartialView("HosCollactionOperation", model);
        }

    }
}