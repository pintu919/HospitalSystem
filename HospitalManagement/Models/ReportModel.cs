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
    public class ReportModel
    {
        [Required]
        public int AccountHeadId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Summary_Entity> SummaryData { get; set; }
        public List<Summary_accountwise_entity> SummaryAccountwiseData { get; set; }
        public List<AccountHeadType_Entity> HeadTypeList { get; set; }

    }

    public class JournalTransaction
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public bool op_balance { get; set; }
        public List<JournalTransaction_Entity> journal_transaction_data { get; set; }
        public List<AccountLedger_Entity> LedgerList { get; set; }
        public List<JournalTransaction_Entity> openingbalance { get; set; }
        public List<JournalTransaction_Entity> Monthwise { get; set; }
        public List<JournalTransaction_Entity> Invoicewise { get; set; }
        public int Acc_id { get; set; }
    }

    public class ReceiptModel
    {
        public string Type {get; set;}
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<ReceiptList> RecList { get; set; }
    }
    public class ReceiptList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int journal_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string journal_ref { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string journal_date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string payment_type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal dr_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string paymode { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string UniqRow { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsIP { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CollectBy { get; set; }
    }

    #region "Individualreport Prepared by Dhaval"
    public class IndividualReport
    {
        public string Durationtype { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }

        [Required]
        public string Type { get; set; }
        public string reportType { get; set; }
        //public List<ProfitReportList> profitreportList { get; set; }
        public List<ReportList> gridList { get; set; }

        public List<GroupWiseList> groupList { get; set; }
    }
    public class ReportList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Qty { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int group_id { get; set; }
        public List<SubReport> SubReportLists {get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string DataType {get; set; }

    }
    public class SubReport
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int group_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string DataType { get; set; }
    }

    public class GroupWiseList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string group_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int group_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Qty { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string GroupDataType { get; set; }
    }
    #endregion

    #region Settalment Invoice
    public class Set_Invoice
    {
        public string Type { get; set; }
        public List<Invoice_Entity> InvoiceList { get; set; }
    }
    #endregion

    #region "Profit Report"

    public class Profitdata
    {
        public List<ServiceGroup> servicegrouplst { get; set; }
        public List<ProfitReportList> profitlst { get; set; }

    }

    public class ServiceGroup
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int group_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string group_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal discount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal profit { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string GrossProfit { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Qty { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal comission { get; set; }
    }
    public class ProfitReportList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Qty { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Discount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Profit { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string GrossProfit { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int group_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal comission { get; set; }
    }
    public class ProfitGraph
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Value { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Type { get; set; }
    }
    #endregion

}