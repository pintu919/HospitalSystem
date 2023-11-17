using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using HMS.BizLogic;
using System.Data;
using HMS.Data;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class PaymentController : _Base
    {

        readonly Payment_Master PM;
        DataSet ds = new DataSet();

        public PaymentController()
        {

            PM = new Payment_Master(bsInfo);
        }
        // GET: Payment

        #region "PaymentType Master"
        public ActionResult PaymentMaster()
        {
            PaymentTypeModel model = new PaymentTypeModel();
            ds = PM.GetPaymentTypeRecord(Convert.ToString(Session["ClinicCode"]));
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ctrl = 1;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add_Update_Payment_Type(PaymentTypeModel Data)
        {
            if (Data.Id == 0)
            {
                ds = PM.Insert_update_PaymentType(Data.Id, Data.PaymentType, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = PM.Insert_update_PaymentType(Data.Id, Data.PaymentType, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new paymentStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
            }
            return View("PaymentMaster", Data);
        }

        public ActionResult DeletePaymentType(int PaymentTypeId)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = PM.DeletePaymentType(PaymentTypeId, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PaymentType Channel Master

        public ActionResult PaymentTypeChannelMaster()
        {
            PaymentTypeChannelModel model = new PaymentTypeChannelModel();
            ds = PM.GetChannelTypeRecord(Convert.ToString(Session["ClinicCode"]));
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.HeadTypeList = Extend.ToList<AccountHeadType_Entity>(ds.Tables[1]);
            model.LedgerAccountLst = new List<LedgerAccountList>();
            model.PaymentTypeChannel_List = new List<PaymentTypeChanelListData>();
            model.ctrl = 1;
            return View(model);
        }

        public PartialViewResult GetLedgerAccountList(int HeadID)
        {
            PaymentTypeChannelModel Data = new PaymentTypeChannelModel();
            var ds = PM.GetSelectedLedgerAccountList(Convert.ToString(Session["ClinicCode"]), HeadID);
            Data.PaymentType_List = new List<PaymentTypeList>();
            Data.HeadTypeList = new List<AccountHeadType_Entity>();
            Data.PaymentTypeChannel_List = new List<PaymentTypeChanelListData>();
            Data.LedgerAccountLst = Extend.ToList<LedgerAccountList>(ds.Tables[0]);
            return PartialView("PaymentTypeChannelMaster", Data);
        }

        public ActionResult AddPaymentTypeChannel(PaymentTypeChannelModel ch)
        {
            string Status = "";
            if (ModelState.IsValid)
            {
                ds = PM.AddChannelData(ch.PaymentTypeId, ch.ChannelName, ch.AccountHeadId, ch.AccountId, ch.ctrl, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
                string st = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                if (st == "1")
                {
                    Status = "Record Added Successfully!";
                }
                else if (st == "2")
                {
                    Status = "Record Allrady Exists!";
                }
                else
                {
                    Status = "Record Not Saved!";
                }
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentTypeChannelListData()
        {
            PaymentTypeChannelModel model = new PaymentTypeChannelModel();
            model.PaymentType_List = new List<PaymentTypeList>();
            model.HeadTypeList = new List<AccountHeadType_Entity>();
            model.LedgerAccountLst = new List<LedgerAccountList>();
            ds =  PM.GetPaymentTypeChannelList(Convert.ToString(Session["ClinicCode"]));
            model.PaymentTypeChannel_List = Extend.ToList<PaymentTypeChanelListData>(ds.Tables[0]);
            return View(model);
        }

        #endregion
    }
}