using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class ComissionModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "*")]
        public string AgentName { get; set; }
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}||[0-9]{13})$", ErrorMessage = "Not a valid mobile number")]
        public string AgentMobile { get; set; }

        [Required(ErrorMessage = "*")]
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string ComissionType { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string Comission { get; set; }

        [Required(ErrorMessage = "*")]
        public int ctrl { get; set; }
        public List<AgentList> ALlData { get; set; }

        public List<ComissionCategoryList> comission_Category { get; set; }
        public AgentStatus ResponseMsg { get; set; }
    }
    public class AgentList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string AgentName { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string AgentMobile { get; set; }

        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string ComissionType { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Comission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int CategoryId { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CategoryName { get; set; }
    }
    public class AgentStatus
    {
        public string StatusId { get; set; }
    }

    public class ComissionCategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Category Name")]
        public string CategoryName { get; set; }
        public bool Ctrl { get; set; } = true;
        public AgentStatus ResponseMsg { get; set; }
        public List<Hos_Services> hos_service { get; set; }
        public List<InviestigationGroupServices> Investigation_Group_List { get; set; }

    }

    public class Hos_Services
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string servicename { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool ischecked { get; set; }


        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal comission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string comission_type { get; set; }

    }

    public class ComissionCategoryList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int CategoryId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CategoryName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CreatedDate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string comission_type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_admission { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal admission_commision { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_test { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal test_commision { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_bed { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal bed_commision { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Total_Comission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Services { get; set; }

    }

    public class ComissionAgent
    {
        [SqlKey(Display = true)]
        public string Comission_Agent_Id { get; set; }

        [SqlKey(Display = true)]
        public string Commision_Agent { get; set; }
    }
    public class AgentComissionReportModel
    {
        public string comission_agentid { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<AgentComissionData> agent_comission_data { get; set; }
        public List<AgentComissionService> agent_comission_service { get; set; }
        public List<ComissionAgent> comission_agent_list { get; set; }
        public List<PaymentTypeList> PaymentType_List { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }
        [Required]
        public int ChannelId { get; set; }
        public string Status { get; set; }
        public List<Invoice_idList> invoiceId_lst { get; set; }
        public decimal TotalComissionAmount { get; set; }

    }

    public class AgentComissionData
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string InvoiceCode { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PatientName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal TotalCommission { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PendingAmount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PaidAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }
       
    }

    public class AgentComissionService
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string InvoiceCode { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Service { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ComissionType { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Comission { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal ComissionAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ID { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int invoice_id { get; set; }
    }

    public class DoctorComissionModel
    {
        [Required(ErrorMessage = "*")]
        public string ComissionName { get; set; }
        public int Id { get; set; }
        public int ctrl { get; set; }

        public AgentStatus ResponseMsg { get; set; }
        public List<DoctorAgentList> ALlComissionData { get; set; }

        public List<InviestigationGroupServices> Investigation_Group_List { get; set; }

    }
    public class DoctorAgentList
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ComissionName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }

    public class InviestigationGroupServices
    {
        public string investigationgroup_code { get; set; }
        public string investigationgroup_name { get; set; }
        public string grouptype { get; set; }
        public List<InvestigationService> Investigation_Service_List { get; set; }
    }

    public class InvestigationService
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationmaster_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationgroup_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationmaster_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal comission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string comission_type { get; set; }

    }

    public class DoctorComissionReportModal
    {
        public List<Invoice_idList> invoiceId_lst { get; set; }
        //public string[,] invoiceId { get; set; }
        //public int AccountId { get; set; }
        //public int AccountHeadId { get; set; }
        public decimal TotalComissionAmount { get; set; }
        public string doctor_code { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public List<ComissionDoctor> comission_doctor_list { get; set; }
        public List<DoctorComissionData> doctor_comission_data { get; set; }
        public List<DoctorComissionMonthwise> doctor_comission_monthwise { get; set; }
        public List<DoctorComissionServices> doctor_comission_service { get; set; }
        //public List<AccountHeadType_Entity> HeadTypeList { get; set; }
        //public List<AccountList> AccountLst { get; set; }

        public List<PaymentTypeList> PaymentType_List { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }
        [Required]
        public int ChannelId { get; set; }


        public string Status { get; set; }

    }

    public class Invoice_idList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoicecode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoiceId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string itemid { get; set; }
    }

    public class ComissionDoctor
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }
    }

    public class DoctorComissionData
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal TotalAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal TotalComission { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PendingAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PaidAmount { get; set; }

        
    }

    public class DoctorComissionMonthwise
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal TotalAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal TotalComission { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PendingAmount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal PaidAmount { get; set; }

    }

    public class DoctorComissionServices
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ID { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string MonthYear { get; set; }


        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int invoice_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationmaster_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ComissionType { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Comission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal ComissionAmount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public DateTime cdate { get; set;}

    }
    #region "Comission Payment Slip"
    public class ComissionPaymentSlipModel
    {
        public string ActType { get; set; }
        public string Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Type { get; set; }
        [Required(ErrorMessage = "*")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string ToDate { get; set; }
        public List<Drpdown_Data> DropDownData { get; set; }
        public List<Com_PaySlipList> ComissionPaySlipLists { get; set; }
    }
    public class Drpdown_Data
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Value { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Text { get; set; }
    }
    public class Com_PaySlipList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Mobile { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PatientName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string journal_ref { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CreatedDate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string photo { get; set; }
    }
    #endregion

}