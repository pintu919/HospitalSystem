using HMS.BizLogic;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    public partial class CliniqModel
    {
        [Key]
        public int CliniqID { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string DistrictCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string CityCode { get; set; }

        public string CliniqCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string CliniqName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Cliniq_Addr { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}||[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string Cliniq_phone { get; set; }

        public string Cliniq_tollfree { get; set; }

        public string Cliniq_emergency_no { get; set; }

        public string Telephone { get; set; }

        [Required(ErrorMessage = "*")]
        public string Cliniq_landmark { get; set; }

        public string Cliniq_lat { get; set; }

        public string Cliniq_lang { get; set; }

        public string Cliniq_off_day { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string Cliniq_email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Cliniq_RegNo { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string GSTNO { get; set; }

        [Required(ErrorMessage = "*")]
        public string zip { get; set; }

        public string cliniclogo { get; set; }

        [Required(ErrorMessage = "*")]
        public string hosting_url { get; set; }

        public bool Ctrl { get; set; }

        public CliniqModel()
        {
            Ctrl = true;
        }

        public bool Daily { get; set; }

        public List<Country_Entity> CountryLst { get; set; }
        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }

        //Day 1
        public bool Day1 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day1_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day1_Cliniq_close_time { get; set; }
        //Day 2
        public bool Day2 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day2_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day2_Cliniq_close_time { get; set; }
        //Day 3
        public bool Day3 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        public TimeSpan Day3_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        public TimeSpan Day3_Cliniq_close_time { get; set; }
        //Day 4
        public bool Day4 { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Day4_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]
        public TimeSpan Day4_Cliniq_close_time { get; set; }
        //Day 5
        public bool Day5 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day5_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day5_Cliniq_close_time { get; set; }
        //Day 6
        public bool Day6 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day6_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm:ss tt", ApplyFormatInEditMode = true)]
        public TimeSpan Day6_Cliniq_close_time { get; set; }
        //Day 7
        public bool Day7 { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]    
        public TimeSpan Day7_Cliniq_open_time { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "HH:mm:ss", ApplyFormatInEditMode = true)]       
        public TimeSpan Day7_Cliniq_close_time { get; set; }

        // public List<TypeList> TypeLst { get; set; }
    }

    public class ClinicProfile
    {
        public Cliniclogo clklogo { get; set; }
        public InvoiceContent inv_content { get; set; }
        public HospitalControl Hos_Control { get; set; }
    }
    public class HospitalControl
    {
        public bool AllowDischargeWithoutPayment { get; set; }
        public bool DoctorVisitFeesAddOPDInvoice { get; set; }
        public bool AllowTestReportEmailWithoutFullPayment { get; set; }
        public bool Showpatientiamgeinprescription { get; set; }
        public bool Showpatientdistrictinprescription { get; set; }
        public bool Showeyeinprescription { get; set; }
    }
    public partial class InvoiceContent
    {
        public string invcontent { get; set; }
        public string CliniqCode { get; set; }
        [AllowHtml]
        public string AdmissionFormTerms_Condition { get; set; }
    }

    public partial class Cliniclogo
    {

        public string CliniqCode { get; set; }

        //[Required(ErrorMessage = "*")]
        public string cliniclogo { get; set; }

        //[Required(ErrorMessage = "*")]
        public string clinicfevicon { get; set; }

        public string ClinicDoclogo { get; set; }

    }

    public class Prescriptionsetting
    {
        public ClinicPrescriptionsetting ClinicPre { get; set; }
        public List<default_prescription> DefaultPescription { get; set; }
    }

    public class ClinicPrescriptionsetting
    {

        public string CliniqCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string HeaderImage { get; set; }

        [Required(ErrorMessage = "*")]
        public string FooterImage { get; set; }


    }

    public class HRSetting
    {

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinc_hr_basic { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinc_hr_hra { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinic_hr_medical { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinic_hr_conveyallowance { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinic_hr_otherallowance { get; set; }

        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinic_hr_basicpf { get; set; }

        [Range(0, 100, ErrorMessage = "percentile must be between 0 to 100")]
        public decimal clinic_hr_maxbasicpf { get; set; }

        public string clinic_code { get; set; }
        public bool Ctrl { get; set; }

        public HRSetting()
        {
            Ctrl = true;
        }

    }

    public class HosNewsData
    {
        public int id { get; set; }

        [Required(ErrorMessage = "*")]
        public string news_title { get; set; }

        [Required(ErrorMessage = "*")]
        public string news_Date { get; set; }

        [AllowHtml, Required(ErrorMessage = "*")]
        public string news_content { get; set; }

        [Required(ErrorMessage = "*")]
        public string news_img { get; set; }
        public int ctrl { get; set; }
        public string user_code { get; set; }
        public string hos_code { get; set; }
        public List<hospital_new_entity> hos_news_data { get; set; }
        public Statusmsg msg { get; set; }
    }
    public class Statusmsg
    {
        public string StatusId { get; set; }
    }




}


