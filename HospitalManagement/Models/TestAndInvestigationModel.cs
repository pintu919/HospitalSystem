using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class BarcodeData
    {
        public string PatientName { get; set; }
        public string TestUniqId { get; set; }
        public string InvName { get; set; }
    }
    public class TestAndInvestigationModel
    {
        [Required(ErrorMessage = "*")]
        public int reagent_id { get; set; }
        public List<ReagentList> reagent_list { get; set; }
        public addvalue_para parameter { get; set; }
        public List<DocAppintment_Entity> RegisterAppointmentlst { get; set; }
        public List<investigationparaamount> investigationPriceList { get; set; }
        public List<OP_investigation_Entity> OP_InvestigationCollection { get; set; }

        public List<OP_investigationItem> OP_InvestigationItems { get; set; }
        public string investigationname { get; set; }
        public string appointment_code { get; set; }
        public string invoice_code { get; set; }
        public int service_id { get; set; }
        public int SupplierId { get; set; }
        public List<SubGroup> invSubgroupList { get; set; }
        public List<DoctorContractlist_Entity> DoctorContractlist { get; set; }
        public List<Technician_Entity> Technicianlist { get; set; }
        public string Doctorcode { get; set; }
        public string EmployeeCode { get; set; }
        public List<HosCollection> hospitalcollection { get; set; }
        public string invtype { get; set; }
        public string status { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public DateTime? fromdateop { get; set; }
        public DateTime? todateop { get; set; }
        public string Id { get; set; }
    }
    public class addvalue_para
    {
        public int service_id { get; set; }
        public int Investigationid { get; set; }
        public string Appointmentcode { get; set; }
        public string unique_invstigation_id { get; set; }
        public string InvType { get; set; }
        public string invoice_code { get; set; }
    }
    public class ReagentList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int reagent_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string reagent_name { get; set; }
    }
    public class SubGroup
    {
        public int InvestigationSubGroupId {get; set; }
        public string SubgroupName { get; set; }
        public string Unit { get; set; }
        public string Reference { get; set; }
        public string FindReference { get; set; }
        public string para_type { get; set; }
        public string investigation_uniqcode { get; set; }
        public int investigationmaster_id { get; set; }
        public List<HosInvestigationPara> invParaList { get; set; }
    }
    public class HosInvestigationPara
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationmaster_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigationsubgrop_id { get; set; }
        [AllowHtml, SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigationsubgroup_name { get; set; }
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
        public string investigation_uniqcode { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; } = 1;

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigation_path { get; set; }

    }

    public class OP_investigationItem
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int service_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int investigation_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string investigation_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsOwnCheckUp { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int invcollection { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int supplierid { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int invcollectionitem { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public List<SupplierList> sup_lst { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isapprove { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invstigation_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string test_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsDr_visit { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invtype { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string reportpath { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int reagent_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string reagent_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_added { get; set; }
    }

    public class HosCollection
    {
        //public int testid { get; set; }

        //public string testname { get; set; }

        //public int[] regentid { get; set; }

        //public int status { get; set; }
        //public string Hoscode { get; set; }
        //public int ctrl { get; set; }
        //public List<TestItem_Entity> testlist { get; set; }

        //public List<SelectListItem> Reg_List { get; set; }

        //public string appointment_code { get; set; }

        //public string appointment_id { get; set; }

        //public List<RegentItem_Entity> RegentList { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Status { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Message { get; set; }
    }



}