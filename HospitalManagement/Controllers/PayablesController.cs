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
using static HospitalManagement.Helper.CustomHelper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class PayablesController : _Base
    {
        readonly Invoice_Master INVMaster;
        readonly Payable_Master Payable;
        public PayablesController()
        {
            Payable = new Payable_Master(bsInfo);
            INVMaster = new Invoice_Master(bsInfo);
        }
        // GET: Payables
        #region "Lab Payable"
        public ActionResult LabPayable()
        {
            LabPayableModel Data = new LabPayableModel();
            var ds = Payable.LabPayabledata(Convert.ToString(Session["ClinicCode"]));
            Data.SupplierLabList = Extend.ToList<LabList>(ds.Tables[0]);
            Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
            Data.LabPayablelst = new List<LabWisePayableList>();
            Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            return View(Data);
        }
        public PartialViewResult SearchLabCost(LabPayableModel model)
        {
            LabPayableModel Data = new LabPayableModel();
            var ds = Payable.GetLabPayableData(Convert.ToString(Session["ClinicCode"]), model.FromDate, model.ToDate, model.LabSupplierId, model.IsPaid);
            Data.LabPayablelst = Extend.ToList<LabWisePayableList>(ds.Tables[0]);
            Data.MonthPayablelst = Extend.ToList<MonthWisePayableList>(ds.Tables[1]);
            Data.DatePayablelst = Extend.ToList<DateWisePayableList>(ds.Tables[2]);
            Data.InvoicePayablelst = Extend.ToList<InvoiceWisePayableList>(ds.Tables[3]);
            return PartialView("LabPayable", Data);
        }
        public PartialViewResult getPaymentTypeChannel_Payable(int PaymentTypeId)
        {
            LabPayableModel model = new LabPayableModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.LabPayablelst = new List<LabWisePayableList>();
            model.MonthPayablelst = new List<MonthWisePayableList>();
            model.DatePayablelst = new List<DateWisePayableList>();
            model.PaymentType_List = new List<PaymentTypeList>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("LabPayable", model);
        }

        //public PartialViewResult GetAccountList(int HeadID)
        //{
        //    LabPayableModel Data = new LabPayableModel();
        //    var ds = Payable.GetSelectedAccountList(Convert.ToString(Session["ClinicCode"]), HeadID);
        //    Data.AccountLst = Extend.ToList<AccountList>(ds.Tables[0]);
        //    Data.LabPayablelst = new List<LabWisePayableList>();
        //    return PartialView("LabPayable", Data);
        //}
        public PartialViewResult SaveCostPayables(LabPayableModel D)
        {
            int val;

            var ds = Payable.Save_LabPaybaleReport(Convert.ToString(Session["ClinicCode"]),
            D.IDs != null || D.IDs != "" ? D.IDs.TrimEnd(',') : null, D.PaymentTypeId, D.ChannelId,
            Convert.ToString(Session["UserCode"]), D.FromDate, D.ToDate, D.LabSupplierId, out val);

            LabPayableModel Data = new LabPayableModel();
            Data.Status = val > 0 ? "sucess" : "fail";
            Data.LabPayablelst = Extend.ToList<LabWisePayableList>(ds.Tables[0]);
            Data.MonthPayablelst = Extend.ToList<MonthWisePayableList>(ds.Tables[1]);
            Data.DatePayablelst = Extend.ToList<DateWisePayableList>(ds.Tables[2]);
            Data.InvoicePayablelst = Extend.ToList<InvoiceWisePayableList>(ds.Tables[3]);
            return PartialView("LabPayable", Data);
        }
        #endregion

        #region "Receivable"
        public ActionResult Receivable()
        {
            LabReceivableModel model = new LabReceivableModel();
            model.MonthReceivablelst = new List<MonthWiseReceivablelist>();
            model.DateReceivablelst = new List<DateWiseReceivablelist>();
            model.InvoiceReceivablelst = new List<invoiceReceivablelist>();
            model.PaymentType_List = new List<PaymentTypeList>();
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.Ctrl = 1;
            return View("Receivable", model);
        }

        public ActionResult Searchreceivabledata(LabReceivableModel JT)
        {
            LabReceivableModel model = new LabReceivableModel();
            var DataSet = Payable.GetLabReceivableData(Convert.ToString(Session["ClinicCode"]), JT.FromDate, JT.ToDate, JT.filterby);
            model.MonthReceivablelst = Extend.ToList<MonthWiseReceivablelist>(DataSet.Tables[0]);
            model.DateReceivablelst = Extend.ToList<DateWiseReceivablelist>(DataSet.Tables[1]);
            model.InvoiceReceivablelst = Extend.ToList<invoiceReceivablelist>(DataSet.Tables[2]);
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(DataSet.Tables[3]);
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.Ctrl = 1;
            return View("Receivable", model);
        }

        public PartialViewResult getPaymentTypeChannel(int PaymentTypeId)
        {
            LabReceivableModel model = new LabReceivableModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.MonthReceivablelst = new List<MonthWiseReceivablelist>();
            model.DateReceivablelst = new List<DateWiseReceivablelist>();
            model.InvoiceReceivablelst = new List<invoiceReceivablelist>();
            model.PaymentType_List = new List<PaymentTypeList>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("Receivable", model);
        }

        public PartialViewResult SaveInvoiceReceivable(LabReceivableModel D)
        {
            int val;
            //string paytype;
            //if (D.Ctrl == 1)
            //{
            //    paytype = "cash";
            //}
            //else
            //{
            //    paytype = "card";
            //}
            var ds = Payable.Save_receivableReport(Convert.ToString(Session["ClinicCode"]), D.invoice_id, D.FromDate, D.ToDate, Convert.ToString(Session["UserCode"]), D.TotalAmount,D.filterby, D.PaymentTypeId, D.ChannelId, out val);
            LabReceivableModel Data = new LabReceivableModel();
            Data.Status = val > 0 ? "sucess" : "fail";
            Data.MonthReceivablelst = Extend.ToList<MonthWiseReceivablelist>(ds.Tables[0]);
            Data.DateReceivablelst = Extend.ToList<DateWiseReceivablelist>(ds.Tables[1]);
            Data.InvoiceReceivablelst = Extend.ToList<invoiceReceivablelist>(ds.Tables[2]);
            Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[3]);
            Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            Data.Ctrl = 1;
            return PartialView("Receivable", Data);
        }

        //public PartialViewResult RecivableAccountList(int HeadID)
        //{
        //    LabReceivableModel Data = new LabReceivableModel();
        //    var ds = Payable.GetSelectedAccountList(Convert.ToString(Session["ClinicCode"]), HeadID);
        //    Data.paymentType_Channel_list = Extend.ToList<AccountList>(ds.Tables[0]);
        //    Data.MonthReceivablelst = new List<MonthWiseReceivablelist>();
        //    Data.DateReceivablelst = new List<DateWiseReceivablelist>();
        //    Data.InvoiceReceivablelst = new List<invoiceReceivablelist>();
        //    return PartialView("Receivable", Data);
        //}

        #endregion

        #region "IP Investigation Refund"
        public ActionResult IPInvestigationRefund()
        {
            InPatientInvestigationRefund model = new InPatientInvestigationRefund();
            var ds = Payable.GetIpRefundInvestigation(Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("IPInvestigationRefund", model);

        }

        public ActionResult filterIPInvestigationRefund(InPatientInvestigationRefund I)
        {
            InPatientInvestigationRefund model = new InPatientInvestigationRefund();
            var ds = Payable.GetIpRefundInvestigation(Convert.ToString(Session["ClinicCode"]), I.PatientCode);
            // model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            //  model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("IPInvestigationRefund", model);

        }

        public PartialViewResult getPaymentTypeChannel_refund(int PaymentTypeId)
        {
            InPatientInvestigationRefund model = new InPatientInvestigationRefund();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.ref_patient_details = new List<RefundPatientDetails>();
            model.ref_investigation_list = new List<RefundInvestigationList>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("IPInvestigationRefund", model);
        }

        public PartialViewResult SaveIPRefundable(InPatientInvestigationRefund D)
        {
            int val;
            InPatientInvestigationRefund Data = new InPatientInvestigationRefund();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(D.investigationId_lst);

            if (dt.Rows.Count > 0)
            {
                var ds = Payable.Save_IP_RefundData(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), dt, D.PaymentTypeId, D.ChannelId, out val);
                Data.Status = val > 0 ? "sucess" : "fail";
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
                Data.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
                Data.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);

            }
            else
            {
                Data.Status = "no data Selected";
                Data.ref_patient_details = new List<RefundPatientDetails>();
                Data.ref_investigation_list = new List<RefundInvestigationList>();
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            }
            return PartialView("IPInvestigationRefund", Data);
        }



        #endregion

        #region "OPD Investigation Refund"
        public ActionResult OPDInvestigationRefund()
        {
            OPDPatientInvestigationRefund model = new OPDPatientInvestigationRefund();
            var ds = Payable.GetOPDRefundInvestigation(Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("OPDInvestigationRefund", model);
        }

        public ActionResult filterOPDInvestigationRefund(OPDPatientInvestigationRefund O)
        {
            OPDPatientInvestigationRefund model = new OPDPatientInvestigationRefund();
            var ds = Payable.GetOPDRefundInvestigation(Convert.ToString(Session["ClinicCode"]), O.PatientCode);
            //model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            //model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("OPDInvestigationRefund", model);
        }

        public PartialViewResult getPaymentTypeChannel_OPDrefund(int PaymentTypeId)
        {
            OPDPatientInvestigationRefund model = new OPDPatientInvestigationRefund();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.ref_patient_details = new List<RefundPatientDetails>();
            model.ref_investigation_list = new List<RefundInvestigationList>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("OPDInvestigationRefund", model);
        }

        public PartialViewResult SaveOPDRefundable(OPDPatientInvestigationRefund D)
        {
            int val;
            OPDPatientInvestigationRefund Data = new OPDPatientInvestigationRefund();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(D.investigationId_lst);

            if (dt.Rows.Count > 0)
            {
                var ds = Payable.Save_OPD_RefundData(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), dt, D.PaymentTypeId, D.ChannelId, out val);
                Data.Status = val > 0 ? "sucess" : "fail";
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
                Data.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
                Data.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);

            }
            else
            {
                Data.Status = "no data Selected";
                Data.ref_patient_details = new List<RefundPatientDetails>();
                Data.ref_investigation_list = new List<RefundInvestigationList>();
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            }
            return PartialView("OPDInvestigationRefund", Data);
        }

        #endregion

        #region "Outer Patient Refund"
        public ActionResult OuterPatientRefund()
        {
            OPDPatientInvestigationRefund model = new OPDPatientInvestigationRefund();
            var ds = Payable.Getouterpatientrefund(Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("OuterPatientRefund", model);
        }
        public ActionResult filterOuterPatientRefund(OPDPatientInvestigationRefund O)
        {
            OPDPatientInvestigationRefund model = new OPDPatientInvestigationRefund();
            var ds = Payable.Getouterpatientrefund(Convert.ToString(Session["ClinicCode"]), O.PatientCode);
            //model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            //model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
            model.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);
            return View("OuterPatientRefund", model);
        }

        public PartialViewResult SaveouterPatientRefundable(OPDPatientInvestigationRefund D)
        {
            int val;
            OPDPatientInvestigationRefund Data = new OPDPatientInvestigationRefund();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(D.investigationId_lst);

            if (dt.Rows.Count > 0)
            {
                var ds = Payable.save_outerpatient_refund(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), dt, D.PaymentTypeId, D.ChannelId, out val);
                Data.Status = val > 0 ? "sucess" : "fail";
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
                Data.ref_patient_details = Extend.ToList<RefundPatientDetails>(ds.Tables[1]);
                Data.ref_investigation_list = Extend.ToList<RefundInvestigationList>(ds.Tables[2]);

            }
            else
            {
                Data.Status = "no data Selected";
                Data.ref_patient_details = new List<RefundPatientDetails>();
                Data.ref_investigation_list = new List<RefundInvestigationList>();
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            }
            return PartialView("OuterPatientRefund", Data);
        }


        #endregion
    }
}