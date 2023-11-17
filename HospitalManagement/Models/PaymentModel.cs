using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class PaymentTypeModel
    {
        [Required(ErrorMessage = "*")]
        public string PaymentType { get; set; }

        public int Id { get; set; }
        public int ctrl { get; set; }
        public List<PaymentTypeList> PaymentType_List { get; set; }
        public paymentStatus ResponseMsg { get; set; }
    }
    public class paymentStatus
    {
        public string StatusId { get; set; }
    }
    public class PaymentTypeList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PaymentType { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }
    public class PaymentTypeChannelModel
    {
        [Required(ErrorMessage = "*")]
        public int PaymentTypeId { get; set; }
        public List<PaymentTypeList> PaymentType_List { get; set; }
        public int ChannelId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ChannelName { get; set; }
        public List<AccountHeadType_Entity> HeadTypeList { get; set; }

        [Required(ErrorMessage = "*")]
        public int AccountHeadId { get; set; }

        [Required(ErrorMessage = "*")]
        public int AccountId { get; set; }
        public List<LedgerAccountList> LedgerAccountLst { get; set; }
        public int ctrl { get; set; }
        public List<PaymentTypeChanelListData> PaymentTypeChannel_List { get; set; }

    }
    public class LedgerAccountList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int acc_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string acc_name { get; set; }
    }

    public class PaymentTypeChanelListData
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string PaymentType { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ChannelName { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string head_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string acc_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }


}