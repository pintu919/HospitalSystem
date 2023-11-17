using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace HospitalManagement.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public string HosCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal ExtraDiscount { get; set; }
        public decimal PendingAmount { get; set; }
        public decimal AdvancePayment { get; set; }
        public string Paymentdate { get; set; }
        public string FooterHtml { get; set; }
        public List<Invoice_Entity> InvoiceData { get; set; }
        public List<Patient_Entity> PatientData { get; set; }
        public List<Doctor_Entity> Doctordata { get; set; }
        public List<Cliniq_Entity> HospitalData { get; set; }
        public Invoicepara InvoicePara { get; set; }
        public IPInvoicepara IPInvoicePara { get; set; }
        public List<Journal_Entity> JournalData { get; set; }
        public decimal DueAmount { get; set; }
        public int Ctrl { get; set; }
        public InvoiceModel()
        {
            Ctrl = 1;
        }
        public int isvisited { get; set; }
        public string status { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string Type { get; set; }
        public decimal TotalDiscount { get; set; }
        public List<PaymentTypeList> PaymentType_List { get; set; }
        public int PaymentTypeId { get; set; }
        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }
        public int ChannelId { get; set; }
        public string invoiceRemark { get; set; }
    }

    public class PaymentTypeChannelList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ChannelName { get; set; }
    }
}