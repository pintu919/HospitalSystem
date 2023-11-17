using HMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    #region "Lab Supplier Added Class"
    public class LabSupplierModel
    {
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "*")]
        public string LabName { get; set; }
        public string RegisterNo { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}|[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int ctrl { get; set; } = 1;
        public string Status { get; set; }
        public List<LabSupplierList> LabList { get; set; }
    }
    public class LabSupplierList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int supplier_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string lab_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string reg_no { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string mobile { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string address { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }
    #endregion

    #region "Lab Supplier price"
    public class LabSupplierPrice
    {
        public string investigationgroup_code { get; set; }
        public int supplier_id { get; set; }
        public List<InvGroup> InvGroupList { get; set; }
        public List<LabList> Sup_LabList { get; set; }
        public List<InvestigationPrice> InvpriceList { get; set; }
    }
    public class InvestigationPrice
    {
        public int id { get; set; }
        public int investigationmaster_id { get; set; }
        public string invstigationmaster_Procedure { get; set; }
        public string invstigationmaster_name { get; set; }
        public decimal inv_cost { get; set; }
        public decimal Amount { get; set; }
        public bool ctrl { get; set; }
        public bool IsSelected { get; set; }
    }
    public class InvGroup
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationgroup_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationgroup_name { get; set; }
    }
    public class LabList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int supplier_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string lab_name { get; set; }
    }
    #endregion
}