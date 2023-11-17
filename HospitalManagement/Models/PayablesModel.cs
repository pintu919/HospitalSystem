using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    #region "Lab Payable"
    public class LabPayableModel
    {
        public string IDs { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int LabSupplierId { get; set; } = 0;
        public List<LabList> SupplierLabList { get; set; }
        //[Required]
        //public int AccountHeadId { get; set; }
        //public List<AccountHeadType_Entity> HeadTypeList { get; set; }

        //[Required]
        //public int AccountId { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        //public List<AccountList> AccountLst { get; set; }
        public List<LabWisePayableList> LabPayablelst { get; set; }
        public List<MonthWisePayableList> MonthPayablelst { get; set; }
        public List<DateWisePayableList> DatePayablelst { get; set; }
        public List<InvoiceWisePayableList> InvoicePayablelst { get; set; }
        public string Status { get; set; }
        public string IsPaid { get; set; } = "Pending";

        public List<PaymentTypeList> PaymentType_List { get; set; }
        
        [Required]
        public int PaymentTypeId { get; set; }

        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }
        [Required]
        public int ChannelId { get; set; }

    }
    public sealed class LabWisePayableList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string LabName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }
    public sealed class MonthWisePayableList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }
    public sealed class DateWisePayableList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }
    public sealed class InvoiceWisePayableList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ServiceName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string LabName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string journal_ref { get; set; }
    }
    public class AccountList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int acc_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string acc_name { get; set; }
    }
    #endregion

    #region "Lab Receivable"
    public class LabReceivableModel
    {
        [Required]
        public int invoice_id { get; set; }
        //public int AccountHeadId { get; set; }
      
        //[Required]
        //public int AccountId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }

        //public List<AccountHeadType_Entity> HeadTypeList { get; set; }
        //public List<AccountList> AccountLst { get; set; }
        public List<MonthWiseReceivablelist> MonthReceivablelst { get; set; }
        public List<DateWiseReceivablelist> DateReceivablelst { get; set; }
        public List<invoiceReceivablelist> InvoiceReceivablelst { get; set; }
        public string Status { get; set; }
        public int Ctrl { get; set; }
        public int filterby { get; set; }

        public List<PaymentTypeList> PaymentType_List { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }
        [Required]
        public int ChannelId { get; set; }

    }
    public sealed class MonthWiseReceivablelist
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal total { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal paidamount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal balance { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }
    public sealed class DateWiseReceivablelist
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string inv_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal total { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal paidamount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal balance { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }
    public sealed class invoiceReceivablelist
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int invoice_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string cdate { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal total { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal paid_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal balance { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; }
    }

    #endregion

    #region "Refund IP Patient Investigation"

    public class RefundPatientDetails
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string AppointmentCode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string InvoiceCode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PatientName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]

        public decimal TotalAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PendingAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }

    }

    public class RefundInvestigationList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string AppointmentCode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string InvoiceCode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PendingAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int InvestigationMasterId { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string InvestigationName { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int dr_inpatient_id { get; set; }

    }

    public class investigationId_lst
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string appointment_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigation_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string dr_inpatient_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string amount { get; set; }

    }

    public class InPatientInvestigationRefund
    {

        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PaymentTypeList> PaymentType_List { get; set; }

        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }

        public List<RefundPatientDetails> ref_patient_details { get; set; }

        public List<RefundInvestigationList> ref_investigation_list { get; set; }

        public List<investigationId_lst> investigationId_lst { get; set; }

        public string Status { get; set; }

        public string PatientName { get; set; }

        [Required]
        public string PatientCode { get; set; }

    }

    #endregion

    #region "Refund OPD Patient Investigation"
    public class OPDPatientInvestigationRefund
    {

        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PaymentTypeList> PaymentType_List { get; set; }

        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }

        public List<RefundPatientDetails> ref_patient_details { get; set; }

        public List<RefundInvestigationList> ref_investigation_list { get; set; }

        public List<investigationId_lst> investigationId_lst { get; set; }

        public string Status { get; set; }

        public string PatientName { get; set; }

        [Required]
        public string PatientCode { get; set; }

    }

    #endregion
}