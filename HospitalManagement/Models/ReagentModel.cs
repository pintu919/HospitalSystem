using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    #region "Add New Reagent"
    public class AddReagent
    {
        [Required(ErrorMessage = "*")]
        public int InvestigationId { get; set; }
        public int reagent_id { get; set; }
        [Required(ErrorMessage = "*")]
        public string reagent_name { get; set; }
        [Required(ErrorMessage = "*")]
        public string uses_unit { get; set; }
        [Required(ErrorMessage = "*")]
        public bool is_usiesvalidity { get; set; }
        [Required(ErrorMessage = "*")]
        public string purchase_unit { get; set; }
        public string validity_date { get; set; }
        public bool Ctrl { get; set; } = true;
        public List<investigationLists> Inv_Lists { get; set; }
    }
    public class investigationLists
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationmaster_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationmaster_name { get; set; }
    }
    #endregion

    #region "Reagent Lists"
    public class Reagent_ModelList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int reagent_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigation_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string inv_type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigation_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string reagent_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string validity_date { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string use_unit { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_usevalidity { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string unit_of_purchase { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_para_added { get; set; }
    }
    public class List_regent
    {
        public int rows_count { get; set; }
        public string ParamaterStatus { get; set; }
        public string SearchText { get; set; }
        public List<Reagent_ModelList> lst_regent { get; set; }

        public string investigation_name { get; set; }
        public List<SubGroup_Inv> ParaLists { get; set; }
    }
    #endregion

    #region "Reagent Master"
    public class InvestigationModel
    {
        public int InvestigationID { get; set; }       
        public string InvestigationName { get; set; }
        public string InvestigationType { get; set; }
        public int ReagentId { get; set; }
        //public List<Investigationgroup_Entity> InveGroupList { get; set; }
        public InvestigationSubGroup SubGroup { get; set; }
        public InvestigationSubGroupPara SubGroupPara { get; set; }
        //public InvestigationReagent ReagentFields { get; set; }
    }
    //public sealed class Investigationgroup_Entity
    //{       
    //    public string investigationgroup_code { get; set; }
    //    public string investigationgroup_name { get; set; }
    //    public string grouptype { get; set; }
    //    public int Ctrl { get; set; }
    //    public string row { get; set; }
    //    private string Return { get; set; }
    //    private string status { get; set; }
    //    private string Error { get; set; }
    //    public DataTable dtlogtable { get; set; }
    //    public int investigationgroup_id { get; set; }
    //    public int no_of_test { get; set; }
    //}
    public class InvestigationSubGroup
    {
        [Key]
        public int InvestigationSubGroupId { get; set; }

        [AllowHtml, Required(ErrorMessage = "*")]
        public string InvestigationSubGroupName { get; set; }
        public string RefreanceValue { get; set; }
        public string Unit { get; set; }
        public string InvestigationName { get; set; }
        public int InvestigationID { get; set; }
        public bool Ctrl { get; set; } = true;
        public List<Investigationsubgroup_Entity> InvSubGroupList { get; set; }
        public string INVType { get; set; }
    }
    public class InvestigationSubGroupPara
    {
        public int InvestigationSubGroupId { get; set; }
        public int InvestigationID { get; set; }
        public string InvestigationSubGroupName { get; set; }
        public string Type { get; set; }
        [Key]
        public int InvestigationSubgroupParaID { get; set; }

        [StringLength(1000), Required(ErrorMessage = "*")]
        public string ParaName { get; set; }

        [StringLength(1000), Required(ErrorMessage = "*")]
        public string Units { get; set; }

        [StringLength(1000), Required(ErrorMessage = "*")]
        public string Reference { get; set; }
        public bool Ctrl { get; set; } = true;
        public List<Investigationsubgrouppara_Entity> InvSubGroupParaList { get; set; }
    }
    #endregion

    #region "Investigation Para View"
    public class SubGroup_Inv
    {
        public string SubgroupName { get; set; }
        public List<InvestigationPara_Inv> invParaList { get; set; }
    }
    public class InvestigationPara_Inv
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationmaster_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationsubgrop_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationmaster_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationpara_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationpara_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationpara_reference { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string find_ip_reference { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; } = 1;

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigation_path { get; set; }

    }
    #endregion
}