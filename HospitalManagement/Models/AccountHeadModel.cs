using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;
using HMS.BizLogic;

namespace HospitalManagement.Models
{
    public partial class AccountHeadModel
    {
        [Required(ErrorMessage = "*")]
        public string HeadName { get; set; }

        [Required(ErrorMessage = "*")]
        public int ParentId { get; set; }
        public int HeadId { get; set; }
        public int Show { get; set; }
        public string HeadType { get; set; }
        public int IsEditable { get; set; }
        public int DoNotShowToAll { get; set; }
        public int ctrl { get; set; }
        public string HosCode { get; set; }
        public List<AccountHeadType_Entity> HeadTypeList { get; set; }
        public List<AccountHeadTypeDto> HeadTypeLst { get; set; }

    }
    public class AccountHeadTypeDto
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string title { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int? parentId { get; set; }
        public IList<AccountHeadTypeDto> subs { get; set; }
    }
    public partial class AccountLedgerModel
    {
        [Required(ErrorMessage = "*")]
        public int AccountHeadId { get; set; }

        [Required(ErrorMessage = "*")]
        public string AccountName { get; set; }
        public string SpecialAccount { get; set; }
        public string ContactPerson { get; set; }
        public string Telephone { get; set; }

        [Required(ErrorMessage = "*")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "*")]
        public string Email { get; set; }
        public int ISShow { get; set; }
        public string HosCode { get; set; }
        public int ctrl { get; set; }
        public List<AccountHeadType_Entity> HeadTypeList { get; set; }
        public List<AccountLedger_Entity> LedgerAccountList { get; set; }
        public int acc_id { get; set; }
    }
    public class GeneralAccountEntry
    {
        [Required(ErrorMessage = "*")]
        public int ParentIdFrom { get; set; }

        [Required(ErrorMessage = "*")]
        public int LedgerIdFrom { get; set; }

        [Required(ErrorMessage = "*")]
        public int ParentIdTo { get; set; }

        [Required(ErrorMessage = "*")]
        public int LedgerIdTo { get; set; }

        public decimal Amount { get; set; }

        public decimal Particulars { get; set; }

        //public int ctrl { get; set; }
        public List<AccountHeadTypeDto> HeadTypeLst { get; set; }
        public List<AccountLedger_Entity> LedgerAccountList { get; set; }
        public List<Journal_data> Journal_entry { get; set; }
        public int Status { get; set; }
    }
    public class Journal_data
    {
        [SqlKey(Name = "@journal_ref")]
        public string journal_ref { get; set; }

        [SqlKey(Name = "@journal_date")]
        public string journal_date { get; set; }

        [SqlKey(Name = "@payment_type")]
        public string payment_type { get; set; }

        [SqlKey(Name = "@cr_amount")]
        public decimal cr_amount { get; set; }

        [SqlKey(Name = "@dr_amount")]
        public decimal dr_amount { get; set; }

        [SqlKey(Name = "@acc_name")]
        public string acc_name { get; set; }

    }
}