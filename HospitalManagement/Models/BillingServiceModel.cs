using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;
using HMS.BizLogic;
namespace HospitalManagement.Models
{
    public partial class BillingServiceModel
    {
     
        public string PatientCode { get; set; }
        public string ReferalDoctorName { get; set; }
        public string ServiceType { get; set; }
        public string CategoryCode { get; set; }
        public int Service_id { get; set; }
        public int investingation_id { get; set; }
        public decimal Discount { get; set; }
        public int is_realtime { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public decimal MinSellingCost { get; set; }
        public decimal SellingCost { get; set; }
        public string Remark { get; set; }
        public List<Investigationgroup_Entity> Investigationgrouplst { get; set; }
        public List<SubInvListData> SubInvgrouplst { get; set; }
        public List<BillingService_Entity> BillingServicelst { get; set; }
        public List<BillingServiceItem_Entity> BillingServiceItemlst { get; set; }
        public string PatientName { get; set; }
        public string InvoiceCode { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string photo { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public List<ComissionAgent_Entity> Comission_Agent_List { get; set; }
        public string Comission_Agent_Id { get; set; } = "0";


    }




    public class SubInvListData
    {
        public int id { get; set; }
        public int investigationmaster_id { get; set; }
        public string invstigationmaster_name { get; set; }
        public decimal inv_cost { get; set; }
        public decimal Amount { get; set; }

    }

    public class BillingServiceItemModel
    {
        public string hos_code { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "*")]
        public string service_name { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal cost { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal selling_price { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal minimum_selling_price { get; set; }
        public string type { get; set; } = "All";
        public bool is_realtime { get; set; }

        public bool is_admission { get; set; }
        public int ctrl { get; set; }
        public List<ServiceList> ALlData { get; set; }
        public Status ResponseMsg { get; set; }
    }
    public class ServiceList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string hos_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string service_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal cost { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal selling_price { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal minimum_selling_price { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_realtime { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_admission { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }
    public class Status
    {
        public string StatusId { get; set; }
    }

    public partial class PatientAdmissionModel
    {

        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string photo { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public List<RoomTypeList> RoomTypeList { get; set; }
        public List<wardType_Entity> WardTypeList { get; set; }
        public List<RegDoctor_list> doctor_list { get; set; }
        public List<RegDepartment_list> department_list { get; set; }
        public List<Room_Entity> RoomList { get; set; }
        public List<Bed_Entity> BedList { get; set; }

        [Required(ErrorMessage = "*")]
        public string Roomfacility { get; set; }
        public int ward_id { get; set; }

        [Required(ErrorMessage = "*")]
        public string room_no { get; set; }

        [Required(ErrorMessage = "*")]
        public int bed_no { get; set; }

        [Required(ErrorMessage = "*")]
        public string departmentcode { get; set; }

        [Required(ErrorMessage = "*")]
        public string doctorcode { get; set; }

        public string admissionnote { get; set; }

        public string documentinclusion { get; set; }

        [Required(ErrorMessage = "*")]
        public string gardianName { get; set; }

        [Required(ErrorMessage = "*")]
        public string gardianmobile { get; set; }
        public int ctrl { get; set; }
        public List<ComissionAgent_Entity> Comission_Agent_List { get; set; }
        public string Comission_Agent_Id { get; set; } = "0";

    }

    public class RegDoctor_list
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }


    }

    public class RegDepartment_list
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string department_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string department_name { get; set; }


    }

}