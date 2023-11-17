using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;
using HMS.BizLogic;

namespace HospitalManagement.Models
{

    public class discharge_profile
    {
        public ProgressMedicine Prog_Med { get; set; }
        public List<DischargeProfilemed_entity> med_list { get; set; }
        public List<ProfileSection_Entity> ProfileSectionlist { get; set; }
        public ProfileSectionModel pfsection { get; set; }

        public List<MedicineRemark_List> RemarkList { get; set; }

    }
    public class MedicineRemark_List
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string remark_name { get; set; }
    }
    public partial class ProgressMedicine
    {

        [Required(ErrorMessage = "*")]
        public int SectionId { get; set; }

        [Required(ErrorMessage = "*")]
        public string genericname { get; set; }

        [Required(ErrorMessage = "*")]
        public int mappingid { get; set; }
        public string brand_code { get; set; }

        [Required(ErrorMessage = "*")]
        public string Dosages { get; set; }

        [Required(ErrorMessage = "*")]
        public int Used { get; set; }
        [Required(ErrorMessage = "*")]
        public int Duration { get; set; }
        public string Remark { get; set; }
        public string MedicineIds { get; set; }
        public string RealMedicineName { get; set; }
    }

    public sealed class Med_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string medicine_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string brand_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int formula_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string title { get; set; }
    }


    public class ProfileSectionModel
    {
        public int SectionId { get; set; }

        [Required(ErrorMessage = "*")]
        public string SectionName { get; set; }
        public string GeneralAdvice { get; set; }
        public string HosCode { get; set; }
        public List<ProfileSection_Entity> ProfileSection { get; set; }
        public int Ctrl { get; set; }

        public ProfileSectionModel()
        {
            Ctrl = 1;
        }

    }

    public class doctor_Assgin_list
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string DoctorName { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string DoctorCode { get; set; }

    }
    public class ChangeIPDoctorModel
    {
        [Required(ErrorMessage = "*")]
        public string AppointmentCode { get; set; }
        public List<doctor_Assgin_list> Doctor_Assign_List_Data { get; set; }
        public string Doctor_Code { get; set; }
        public string Message { get; set; }
        public string DoctorName { get; set; }

    }

}