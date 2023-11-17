using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{


    public class HMSPatientModel
    {
        [Key]
        public int PatientId { get; set; }


        public string PatientCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientLasttName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFatherName { get; set; }

        public string NID_Birthregno { get; set; }

        public string Age { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}|[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string PatientMotherName { get; set; }
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string DistrictCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityCode { get; set; }

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "*")]
        public string DateOfBirth { get; set; }

        public string MaritalStatus { get; set; }

        public string hospitalcode { get; set; }

        public string HosDocCode { get; set; }

        public int Ctrl { get; set; }

        public string Relation { get; set; }

        public HMSPatientModel()
        {
            Ctrl = 1;
        }

        public List<Country_Entity> CountryLst { get; set; }
        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }
        public List<Doctor_Entity> DoctorList { get; set; }
        public List<Patient_Entity> visitpatientlist { get; set; }
        public PatientStatus PatientStatusData { get; set; }
        public string PatVerify { get; set; }
        public int isverify { get; set; }
        //Added By Dhaval For Patient Search
        public OtherPatientSearch PatientSearch { get; set; }

        public List<opdhistory> opd_history { get; set; }

        public List<iphistory> ip_history { get; set; }
        public List<invhistory> inv_history { get; set; }

        public string uniqrow { get; set; }

        public string SpouseName { get; set; }
        public string PatientProfessions { get; set; }

        public List<SubGroupPatient> invSubgroupList { get; set; }
        public string appointment_code { get; set; }
        public string investigationname { get; set; }

        public string invtype { get; set; }
        public string invoice_code { get; set; }

        public int service_id { get; set; }

        //Added By Dhaval For patient Search

    }

    public class SubGroupPatient
    {
        public string SubgroupName { get; set; }
        public List<HosInvestigationParapatient> invParaList { get; set; }
    }
    public class HosInvestigationParapatient
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

    //Added By Dhaval For Patient Search
    public class OtherPatientSearch
    {
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public string PatientFatherName { get; set; }
        public string PatientBirthYear { get; set; }
    }
    //Added By Dhaval For patient Search

    public class VisitedPatientHistory
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int totaldirectbilling { get; set; }
        public int totalippatient { get; set; }
        public int totalopdpatient { get; set; }
        public List<Patient_Entity> directilling_lst { get; set; }
        public List<Patient_Entity> ip_patient_lst { get; set; }
        public List<Patient_Entity> opd_patient_lst { get; set; }

    }

}
public class PatientStatus
{
    public Surgery SurgeryData { get; set; }
    public Disease DiseaseData { get; set; }
    public ChronicMedicine ChronicMedicineData { get; set; }
    public OtherMed Other_Med { get; set; }
    public List<patient_status_Surgery> SurgeryList { get; set; }
    public List<patient_status_Disease> DiseaseList { get; set; }
    public List<patient_status_Organs> OrgansList { get; set; }
    public List<FormulationList> Chronic_Formulation_List { get; set; }
    public List<SelectListItem> Freq_List { get; set; }
    public FamilyHistory family_history { get; set; }
    public string Type { get; set; }

}
public class Surgery
{
    public int StatusSurgeryTypeId { get; set; }

    [Required(ErrorMessage = "*")]
    public int StatusSurgeryNameId { get; set; }

    [Required(ErrorMessage = "*")]
    public string sur_organ_name { get; set; }

    [Required(ErrorMessage = "*")]
    public string Surgeryfromdate { get; set; }
    public string Surgeryremarks { get; set; }
    public List<ForntendVital_Entity> vitalsurgerylst { get; set; }
}
public class Disease
{

    public int StatusDiseaseTypeId { get; set; }
    [Required(ErrorMessage = "*")]
    public int StatusDiseaseNameId { get; set; }
    [Required(ErrorMessage = "*")]
    public string des_organ_name { get; set; }

    [Required(ErrorMessage = "*")]
    public string Diseasefromdate { get; set; }
    public string Diseaseremarks { get; set; }
    public List<ForntendVital_Entity> vitaldiseaselst { get; set; }
}
public class ChronicMedicine
{
    public int formula_id { get; set; }
    [Required(ErrorMessage = "*")]
    public string formulation_code { get; set; }
    [Required(ErrorMessage = "*")]
    public int Mapping_id { get; set; }
    public string brand_code { get; set; }
    [Required(ErrorMessage = "*")]
    public string ChronicMedicine_Name { get; set; }
    [Required(ErrorMessage = "*")]
    public string Frequence { get; set; }
    [Required(ErrorMessage = "*")]
    public string Med_StartDate { get; set; }
    public string Remarks { get; set; }
    public List<ForntendchronicVital_Entity> vitalchroniclst { get; set; }
}
public class OtherMed
{
    [Required(ErrorMessage = "*")]
    public string AlergyMedicine { get; set; }
    public string warning { get; set; }
    public string Other { get; set; }
    public bool IsPregnent { get; set; }
    public string DeleviryDate { get; set; }
    public string Msg { get; set; }
    public List<ForntendotherVital_Entity> vitalotherlst { get; set; }

}

public class FamilyHistory
{
    public string father_Medical_history { get; set; }
    public string mother_medical_history { get; set; }
    public string history_date { get; set; }
    public List<Family_History_Entity> familyhistorylst { get; set; }


}

public class opdhistory
{
    [SqlKey(Display = true)]
    public string visit_date { get; set; }

    [SqlKey(Display = true)]
    public string doctor_name { get; set; }

    [SqlKey(Display = true)]
    public string complain { get; set; }

    [SqlKey(Display = true)]
    public decimal due_amount { get; set; }

    [SqlKey(Display = true)]
    public string invoice_code { get; set; }
    [SqlKey(Display = true)]
    public Guid uniqrow { get; set; }

}

public class iphistory
{
    [SqlKey(Display = true)]
    public string doctor_name { get; set; }

    [SqlKey(Display = true)]
    public string admited_date { get; set; }

    [SqlKey(Display = true)]
    public string release_date { get; set; }

    [SqlKey(Display = true)]
    public int AdmitDays { get; set; }

    [SqlKey(Display = true)]
    public string appoinment_code { get; set; }

    [SqlKey(Display = true)]
    public int otcomplete { get; set; }

    [SqlKey(Display = true)]
    public decimal due_amount { get; set; }

    [SqlKey(Display = true)]
    public string invoice_code { get; set; }

    [SqlKey(Display = true)]
    public Guid uniqrow { get; set; }

}

public class invhistory
{
    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int service_id { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int investigationmaster_id { get; set; }
    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string investigationmaster_name { get; set; }
    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public decimal Amount { get; set; }
    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string invoice_code { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string appointment_code { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int IsOwnCheckUp { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int pending_sample { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public Int64 investigation_investigationid { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int invpriceid { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int invcollection { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int supplierid { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int invcollectionitem { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int isapprove { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string invstigation_date { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string doctor_name { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public int IsDr_visit { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string invtype { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string reportpath { get; set; }

    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    public string SupplierName { get; set; }

}











