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
using NReco.PdfGenerator;
using HospitalManagement.Helper;
using System.Reflection.Emit;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class InvoiceController : _Base
    {
        readonly Invoice_Master INVMaster;
        readonly Invoice_Entity IE;
        // GET: Invoice
        public InvoiceController()
        {
            INVMaster = new Invoice_Master(bsInfo);
            IE = new Invoice_Entity();
        }
        #region OPD Patient Invoice"
        public ActionResult Invoice()
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetAll(Convert.ToString(Session["ClinicCode"]), "");
            model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            return View("Invoice", model);
        }
        public PartialViewResult PaidInvoice(string invoicecode, decimal currentamount, string paymenttype, string Date, int PaymentTypeId, int ChannelId, string TransactionId)
        {
            DateTime dt = Convert.ToDateTime(Date);
            int isvisited = 0;
            InvoiceModel model = new InvoiceModel();
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = INVMaster.UpdateStatus(invoicecode, currentamount, paymenttype, hoscode, usercode, 0, dt, PaymentTypeId, ChannelId,TransactionId,out isvisited);
            if (Convert.ToInt32(res) == 1)
            {
                var DataSet = INVMaster.GetPaidInvoicedata(invoicecode);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.RefundAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["refundamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
                model.TotalDiscount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_discount"]);
                model.InvoiceCode = invoicecode;
                model.InvoiceData = new List<Invoice_Entity>();
                model.isvisited = isvisited;
                //var DataSet = INVMaster.GetAll(Convert.ToString(Session["ClinicCode"]), "");
                //model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            }
            else if (Convert.ToInt32(res) == 2)
            {
                var DataSet = INVMaster.GetPaidInvoicedata(invoicecode);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.RefundAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["refundamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
                model.TotalDiscount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_discount"]);
                model.InvoiceCode = invoicecode;
                model.status = "2";
                model.InvoiceData = new List<Invoice_Entity>();
                model.isvisited = isvisited;
            }
            else
            {
                model.JournalData = new List<Journal_Entity>();
                model.InvoiceData = new List<Invoice_Entity>();
            }
            return PartialView("Invoice", model);
        }

        public PartialViewResult getPaidAmountdata(string invoicecode)
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetPaidInvoicedata(invoicecode, Convert.ToString(Session["ClinicCode"]));
            model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
            if (DataSet.Tables[1].Rows.Count > 0)
            {
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.RefundAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["refundamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
            }
            else
            {
                model.PaidAmount = 0;
            }
            model.InvoiceCode = invoicecode;
            if (DataSet.Tables[2].Rows.Count > 0)
            {
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
            }

            if (DataSet.Tables[3].Rows.Count > 0)
            {
                model.PaymentType_List = Extend.ToList<PaymentTypeList>(DataSet.Tables[3]);
            }

            return PartialView("Invoice", model);
        }

        public PartialViewResult getPaymentTypeChannel(int PaymentTypeId)
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("Invoice", model);
        }

        public PartialViewResult getPaymentTypeChannelForIP(int PaymentTypeId)
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("IPInvoice", model);
        }

        [HttpPost]
        public PartialViewResult FilterInvoice(InvoiceModel INV)
        {
            InvoiceModel model = new InvoiceModel();
            int ctrl = 2;
            string invcode = "";
            if (INV.InvoiceCode != "" && INV.InvoiceCode != null)
            {
                invcode = INV.InvoiceCode.Trim();
            }



            //if (INV.status == null && INV.FromDate == null && INV.Todate == null)
            //{
            //    var DataSet = INVMaster.GetAll(Convert.ToString(Session["ClinicCode"]), "");
            //    model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            //}
            //else
            //{

            if (INV.status == "Paid") { ctrl = 1; } else if (INV.status == "UnPaid") { ctrl = 0; }
            var dataset = INVMaster.GetAllfilter(Convert.ToString(Session["ClinicCode"]), ctrl, INV.FromDate, INV.Todate, invcode);
            model.InvoiceData = Extend.ToList<Invoice_Entity>(dataset.Tables[0]);
            //}
            return PartialView("Invoice", model);
        }

        public ActionResult InvoiceView()
        {

            DateTime d = System.DateTime.Now;
            //TimeZoneInfo localZone = TimeZoneInfo.Local;
            //string a = localZone.Id;

            //var zone = TimeZoneInfo.FindSystemTimeZoneById(a);
            //var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            InvoiceModel model = new InvoiceModel();
            model.InvoicePara = new Invoicepara();
            var code = Convert.ToString(Request.Path.Split('/')[3]);
            if (code != "")
            {
                var DataSet = INVMaster.GetAllInvoiceDetails(code);
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    model.invoiceRemark = Convert.ToString(DataSet.Tables[0].Rows[0]["InvoiceRemark"]);
                }
                model.InvoicePara.opd_doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.InvoicePara.opd_investigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.InvoicePara.opd_advanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.InvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["paid_amount"]);
                model.RefundAmount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["refundamount"]);
                model.ExtraDiscount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["ExtraDiscount"]);
                model.FooterHtml = Convert.ToString(DataSet.Tables[7].Rows[0]["Footer"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[8]);
                model.DueAmount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["DueAmount"]);

            }
            return View("InvoiceView", model);
        }
        [HttpPost]
        public JsonResult AddInvoceRemark(string invoicecode, string Remark)
        {
            bool result = INVMaster.AddInvoiceRemark(invoicecode, Remark, Convert.ToString(Session["ClinicCode"]));
            return Json(result==true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InvoicePrint(string PrintContent, string PrintFooter)
        {
            Session["Bodyhtml"] = PrintContent;
            Session["FooterHtml"] = PrintFooter;
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OPDInvoiceUpdateDiscount(string InvoiceCode, decimal Discount)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            var dataset = INVMaster.OPDInvoiceUpdateDiscount(InvoiceCode, Discount, hoscode, usercode);
            var data = CustomHelper.DataTableToJSONWithJavaScriptSerializer(dataset.Tables[0]);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvoiceNew()
        {
            InvoiceModel model = new InvoiceModel();
            return View("InvoiceNew", model);
        }
        #endregion

        #region InPatient Invoice"

        public ActionResult IPInvoice()
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetAllIPInvoice(Convert.ToString(Session["ClinicCode"]), "");
            model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            return View("IPInvoice", model);
        }

        public ActionResult IPInvoiceView()
        {
            InvoiceModel model = new InvoiceModel();
            model.IPInvoicePara = new IPInvoicepara();
            var code = Convert.ToString(Request.Path.Split('/')[3]);
            if (code != "")
            {
                var DataSet = INVMaster.GetIPInvoiceDetails(code);
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    model.invoiceRemark = Convert.ToString(DataSet.Tables[0].Rows[0]["InvoiceRemark"]);
                }
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.IPInvoicePara.doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.IPInvoicePara.ipinvestigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.IPInvoicePara.ipadvanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.IPInvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.IPInvoicePara.IPMedList = Extend.ToList<IPMedicineList>(DataSet.Tables[7]);
                model.IPInvoicePara.IPAdmitRoomList = Extend.ToList<IPAdmitRoom>(DataSet.Tables[8]);
                model.PaidAmount = Convert.ToString(DataSet.Tables[9].Rows[0]["paid_amount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["paid_amount"]) : 0;
                model.ExtraDiscount = Convert.ToString(DataSet.Tables[9].Rows[0]["extra_discount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["extra_discount"]) : 0;
                model.FooterHtml = Convert.ToString(DataSet.Tables[9].Rows[0]["Footer"]);
                model.DueAmount = Convert.ToDecimal(DataSet.Tables[9].Rows[0]["DueAmount"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[10]);

            }
            return View("IPInvoiceView", model);
        }

        public ActionResult ShortIPInvoiceView()
        {
            InvoiceModel model = new InvoiceModel();
            model.IPInvoicePara = new IPInvoicepara();
            var code = Convert.ToString(Request.Path.Split('/')[3]);
            if (code != "")
            {
                var DataSet = INVMaster.GetIPInvoiceDetails(code);
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    model.invoiceRemark = Convert.ToString(DataSet.Tables[0].Rows[0]["InvoiceRemark"]);
                }
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.IPInvoicePara.doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.IPInvoicePara.ipinvestigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.IPInvoicePara.ipadvanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.IPInvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                //model.IPInvoicePara.IPMedList = Extend.ToList<IPMedicineList>(DataSet.Tables[7]);
                model.IPInvoicePara.IPAdmitRoomList = Extend.ToList<IPAdmitRoom>(DataSet.Tables[8]);
                model.PaidAmount = Convert.ToString(DataSet.Tables[9].Rows[0]["paid_amount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["paid_amount"]) : 0;
                model.ExtraDiscount = Convert.ToString(DataSet.Tables[9].Rows[0]["extra_discount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["extra_discount"]) : 0;
                model.FooterHtml = Convert.ToString(DataSet.Tables[9].Rows[0]["Footer"]);
                model.DueAmount = Convert.ToDecimal(DataSet.Tables[9].Rows[0]["DueAmount"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[10]);

            }
            return View("ShortIPInvoiceView", model);
        }

        [HttpPost]
        public PartialViewResult FilterIPInvoice(InvoiceModel INV)
        {
            InvoiceModel model = new InvoiceModel();
            int ctrl = 2;
            string invcode = "";
            if (INV.InvoiceCode != "" && INV.InvoiceCode != null)
            {
                invcode = INV.InvoiceCode.Trim();
            }
            //if (INV.status == null && INV.FromDate == null && INV.Todate == null)
            //{
            //    var DataSet = INVMaster.GetAllIPInvoice(Convert.ToString(Session["ClinicCode"]), "");
            //    model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            //}
            //else
            //{

            if (INV.status == "Paid") { ctrl = 1; } else if (INV.status == "UnPaid") { ctrl = 0; }
            var dataset = INVMaster.GetAllIPfilter(Convert.ToString(Session["ClinicCode"]), ctrl, INV.FromDate, INV.Todate, invcode);
            model.InvoiceData = Extend.ToList<Invoice_Entity>(dataset.Tables[0]);
            //}
            return PartialView("IPInvoice", model);
        }

        public PartialViewResult IPgetPaidAmountdata(string invoicecode)
        {
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetIPPaidInvoicedata(invoicecode, Convert.ToString(Session["ClinicCode"]));
            model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
            if (DataSet.Tables[1].Rows.Count > 0)
            {
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
                model.AdvancePayment = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["advance_payment"]);
            }
            else
            {
                model.PaidAmount = 0;
            }
            //model.AdvancePaymentData = Extend.ToList<Advance_Payment_Entity>(DataSet.Tables[2]);
            model.InvoiceCode = invoicecode;
            if (DataSet.Tables[2].Rows.Count > 0)
            {
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
            }

            if (DataSet.Tables[3].Rows.Count > 0)
            {
                model.PaymentType_List = Extend.ToList<PaymentTypeList>(DataSet.Tables[3]);
            }

            return PartialView("IPInvoice", model);
        }

        public PartialViewResult IPPaidInvoice(string invoicecode, decimal currentamount, string paymenttype, int isadvanced, string Date, int PaymentTypeId, int ChannelId, string TransactionId)
        {
            DateTime dt = Convert.ToDateTime(Date);
            InvoiceModel model = new InvoiceModel();
            int isvisited = 0;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = INVMaster.UpdateStatus(invoicecode, currentamount, paymenttype, hoscode, usercode, isadvanced, dt, PaymentTypeId, ChannelId, TransactionId,out isvisited);
            if (Convert.ToInt32(res) == 1)
            {
                var DataSet = INVMaster.GetIPPaidInvoicedata(invoicecode);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
                model.TotalDiscount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_discount"]);
                model.AdvancePayment = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["advance_payment"]);
                model.InvoiceCode = invoicecode;
                model.InvoiceData = new List<Invoice_Entity>();
                //model.AdvancePaymentData = Extend.ToList<Advance_Payment_Entity>(DataSet.Tables[2]);
                //var DataSet = INVMaster.GetAll(Convert.ToString(Session["ClinicCode"]), "");
                //model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            }
            else if (Convert.ToInt32(res) == 2)
            {

                var DataSet = INVMaster.GetIPPaidInvoicedata(invoicecode);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[0]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["paidamount"]);
                model.TotalAmount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_amount"]);
                model.TotalDiscount = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["total_discount"]);
                model.AdvancePayment = Convert.ToDecimal(DataSet.Tables[1].Rows[0]["advance_payment"]);
                model.InvoiceCode = invoicecode;
                model.InvoiceData = new List<Invoice_Entity>();
                model.status = "2";
            }
            else
            {
                model.JournalData = new List<Journal_Entity>();
                model.InvoiceData = new List<Invoice_Entity>();
            }
            return PartialView("IPInvoice", model);
        }

        [HttpPost]
        public JsonResult IPInvoiceUpdateDiscount(int ServiceId, string ServiceType, string InvoiceCode, decimal Discount)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            var dataset = INVMaster.IPInvoiceUpdateDiscount(ServiceId, ServiceType, InvoiceCode, Discount, hoscode, usercode);
            var data = CustomHelper.DataTableToJSONWithJavaScriptSerializer(dataset.Tables[0]);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IPInvoiceGrossDiscount(string InvoiceCode, decimal Discount)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            var dataset = INVMaster.IPInvoiceGrossDiscount(InvoiceCode, Discount, hoscode, usercode);
            var data = CustomHelper.DataTableToJSONWithJavaScriptSerializer(dataset.Tables[0]);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Outer Invoice"
        public ActionResult OuterInvoice()
        {
            InvoiceModel model = new InvoiceModel();
            model.HospitalData = new List<Cliniq_Entity>();
            return View("OuterInvoice", model);
        }
        public PartialViewResult FindInvoice(string InvoiceCode)
        {
            string InvoiceType;
            InvoiceModel model = new InvoiceModel();
            var DataSet = INVMaster.GetINVDetails(InvoiceCode, out InvoiceType);
            if (InvoiceType == "opd_invoicedetails")
            {
                model.InvoicePara = new Invoicepara();
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.InvoicePara.opd_doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.InvoicePara.opd_investigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.InvoicePara.opd_advanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.InvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["paid_amount"]);
                model.ExtraDiscount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["ExtraDiscount"]);
                model.FooterHtml = Convert.ToString(DataSet.Tables[7].Rows[0]["Footer"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[8]);
            }
            else if (InvoiceType == "ip_invoicedetails")
            {
                model.IPInvoicePara = new IPInvoicepara();
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.IPInvoicePara.doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.IPInvoicePara.ipinvestigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.IPInvoicePara.ipadvanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.IPInvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.IPInvoicePara.IPMedList = Extend.ToList<IPMedicineList>(DataSet.Tables[7]);
                model.IPInvoicePara.IPAdmitRoomList = Extend.ToList<IPAdmitRoom>(DataSet.Tables[8]);
                model.PaidAmount = Convert.ToString(DataSet.Tables[9].Rows[0]["paid_amount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["paid_amount"]) : 0;
                model.ExtraDiscount = Convert.ToString(DataSet.Tables[9].Rows[0]["extra_discount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["extra_discount"]) : 0;
                model.FooterHtml = Convert.ToString(DataSet.Tables[9].Rows[0]["Footer"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[10]);
            }
            model.Type = InvoiceType;
            return PartialView("OuterInvoice", model);
        }
        #endregion
    }
}