using System;
using HMS.BizLogic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;

namespace HospitalManagement.Models
{

    public class HMSDoctorData
    {
        public List<HMSDocotContract> DocContractList { get; set; }
        public List<DoctorSchedule> DocSchedleList { get; set; }
        public List<DoctorLeaves> DocLeavesList { get; set; }

        public string ActionType { get; set; }
        public int ContractId { get; set; }
    }
    public class HMSDocotContract
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Please Enter valid Visit Fees")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Please enter a valid Visit Fees")]

        public decimal? VisitFees { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Please Enter valid Followup Fees")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Please enter a valid Followup Fees")]

        public decimal? FollowupFees { get; set; }
        [Required(ErrorMessage = "*")]
        public int FollowPolicy { get; set; }
        public bool OnlineApointAllow { get; set; }
        [Range(0, 60, ErrorMessage = "Please enter Valid Time")]
        public int AppointmentSlot { get; set; }

        [Required(ErrorMessage = "*")]

        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        public string Prefix { get; set; }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "*")]
        public string EmployeeCode { get; set; }
        public string HosCode { get; set; }
        public int Ctrl { get; set; }

        public HMSDocotContract()
        {
            Ctrl = 1;
        }
        public int ContactId { get; set; }

        public string DoctorName { get; set; }

        public string DepartmentName { get; set; }

        public string Uniqrow { get; set; }
        public List<DoctorContractlist_Entity> DoctorContractlist { get; set; }

        public List<Doctor_Entity> DoctorList { get; set; }

        public List<DoctorEducation_Entity> DoctorEduList { get; set; }

        public List<DoctorExperience_Entity> DoctorExepList { get; set; }

        public string doctor_comission_id { get; set; } = "0";
        public List<DoctorComissionCategory> doctor_Comission_Category { get; set; }

        //public List<SelectListItem> Availabldayeslst { get; set; }
    }

    public class DoctorComissionCategory
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ComissionName { get; set; }

    }

    public class DoctorSchedule
    {


        //public bool Day1 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day1_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day1_Cliniq_close_time { get; set; }
        ////Day 2
        //public bool Day2 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day2_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day2_Cliniq_close_time { get; set; }
        ////Day 3
        //public bool Day3 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        //public TimeSpan Day3_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        //public TimeSpan Day3_Cliniq_close_time { get; set; }
        ////Day 4
        //public bool Day4 { get; set; }
        //[DataType(DataType.Time)]
        //public TimeSpan Day4_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        //public TimeSpan Day4_Cliniq_close_time { get; set; }
        ////Day 5
        //public bool Day5 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day5_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day5_Cliniq_close_time { get; set; }
        ////Day 6
        //public bool Day6 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day6_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        //public TimeSpan Day6_Cliniq_close_time { get; set; }
        ////Day 7
        //public bool Day7 { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]    
        //public TimeSpan Day7_Cliniq_open_time { get; set; }
        //[DataType(DataType.Time)]
        ////[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]       
        //public TimeSpan Day7_Cliniq_close_time { get; set; }

        public string Day { get; set; }

        [DataType(DataType.Time)]

        public TimeSpan TimeFrom { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan TimeTo { get; set; }

        public int ContractId { get; set; }

        public int scheduleId { get; set; }

    }

    public class DoctorLeaves
    {
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string LeaveType { get; set; }
        public int LeaveDays { get; set; }
        public Int64 ContractId { get; set; }
    }

    public class doctordetail
    {
        [SqlKey(Display = true)]
        public int Id { get; set; }
        [SqlKey(Display = true)]
        public string doctor_code { get; set; }
        [SqlKey(Display = true)]
        public string doctor_name { get; set; }
        [SqlKey(Display = true)]
        public string department_name { get; set; }
        [SqlKey(Display = true)]
        public string doctor_image { get; set; }
        [SqlKey(Display = true)]
        public int ctrl { get; set; }
    }
    public class doctorAcessAdmitpateint
    {
        public string doctor_code { get; set; }
        public List<RegDoctor_list> Doctorlst { get; set; }
        public List<doctordetail> doctordetaillst { get; set; }
        public int Status { get; set; }
    }

    public class DoctorModel
    {

        [Required(ErrorMessage = "*")]
        public string DoctorName { get; set; }
        public string DoctorCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string DoctorDesignation { get; set; }
        public string Address { get; set; }
        public string Languages { get; set; }
        [Required(ErrorMessage = "*")]
        public string SpecialityCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string statecode { get; set; }

        public int is_bmdc_verify { get; set; }

        [Required(ErrorMessage = "*")]
        public string districtcode { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityCode { get; set; }
        public string DoctorImage { get; set; }
        public string ImagePath { get; set; }
        public int is_online_amader_doctor { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}|[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string DoctorMobile { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string VerificationCode { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string WorkEmail { get; set; }

        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "*")]
        public string DoctrorbmdcRegno { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "*")]
        public string DoctorDepartmentCode { get; set; }

        public string DescriptionProfessionalStatement { get; set; }
        public string DegreeotherSpecification { get; set; }
        public int Ctrl { get; set; }
        public bool Is_Private_Appointment { get; set; }
        public List<Specialize_Entity> SpecializeLst { get; set; }
        public List<Country_Entity> CountryLst { get; set; }
        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }
        public List<Department_Entity> DeptLst { get; set; }
    }
}