using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{

    public partial class MainNurseModel
    {
        public NurseModel Nurse { get; set; }
        //public vitalpara vital { get; set; }
        public MainVitalPara MainVital { get; set; }
        public investigationparaamount parameter { get; set; }
        public List<investigationparaamount> investigationPriceList { get; set; }
        public AddOPDOTTransfer OPDOTTransfer { get; set; }
        public TodayVital VitalPara { get; set; }

    }

    public class TdyVital
    {
        [SqlKey(Display = true)]
        public string VitalCode { get; set; }
        [SqlKey(Display = true)]
        public string VitalName { get; set; }

        [SqlKey(Display = true)]
        public string VitalValue { get; set; }

        [SqlKey(Display = true)]
        public string VitalUnit { get; set; }
    }
    public class TodayVital
    {
        //public decimal BodyTemperature { get; set; }
        //public string BloodPressure { get; set; }
        //public decimal Weight { get; set; }
        //public decimal Sugar { get; set; }
        //public decimal OxygenLevel { get; set; }
        //public int Heartrate { get; set; }

        [SqlKey(Display = true)]
        public List<TdyVital> VitalList { get; set; }
        [SqlKey(Display = true)]
        public string Remarks { get; set; }
        [SqlKey(Display = true)]
        public string PatientCode { get; set; }
        [SqlKey(Display = true)]
        public string HOScode { get; set; }
        [SqlKey(Display = true)]
        public string Appointmentcode { get; set; }
        [SqlKey(Display = true)]
        public string Status { get; set; }
    }

    public class NurseModel
    {
        public int NurseSationId { get; set; }

        [Required(ErrorMessage = "*")]
        public string NurseSationName { get; set; }
        public string HosCode { get; set; }

        public string HosDocCode { get; set; }
        public List<Nurse_Entity> Nurselstation { get; set; }
        public List<HospitalDepartment_Entity> dpartmentlst { get; set; }
        public List<Doctor_Entity> DoctorList { get; set; }
        public int Ctrl { get; set; }

        public NurseModel()
        {
            Ctrl = 1;
        }
        public List<DocAppintment_Entity> RegisterAppointmentlst { get; set; }
        public string HosDepartmentcode { get; set; }    
    }
    public class investigationparaamount
    {
        [SqlKey(Display = true)]
        public int service_id { get; set; }

        [SqlKey(Display = true)]
        public int investigationmaster_id { get; set; }
        [SqlKey(Display = true)]
        public string investigationmaster_name { get; set; }
        [SqlKey(Display = true)]
        public decimal price { get; set; }
        [SqlKey(Display = true)]
        public string appoinment_code { get; set; }

        [SqlKey(Display = true)]
        public int IsOwnCheckUp { get; set; }

        [SqlKey(Display = true)]
        public int pending_sample { get; set; }

        [SqlKey(Display = true)]
        public Int64 investigation_investigationid { get; set; }

        [SqlKey(Display = true)]
        public int invpriceid { get; set; }

        [SqlKey(Display = true)]
        public int invcollection { get; set; }

        [SqlKey(Display = true)]
        public int supplierid { get; set; }

        [SqlKey(Display = true)]
        public int invcollectionitem { get; set; }

        [SqlKey(Display = true)]
        public List<SupplierList> sup_lst { get; set; }

        [SqlKey(Display = true)]
        public int isapprove { get; set; }

        [SqlKey(Display = true)]
        public string invstigation_date { get; set; }

        [SqlKey(Display = true)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true)]
        public int IsDr_visit { get; set; }

        [SqlKey(Display = true)]
        public string invtype { get; set; }

        [SqlKey(Display = true)]
        public string reportpath { get; set; }

        [SqlKey(Display = true)]
        public int reagent_id { get; set; }

        [SqlKey(Display = true)]
        public string reagent_name { get; set; }
        [SqlKey(Display = true)]
        public int is_added { get; set; }

        [SqlKey(Display = true)]
        public int canceled { get; set; }

        [SqlKey(Display = true)]
        public string cancel_reason { get; set; }


    }
    public class SupplierList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int SupplierId { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string SupplierName { get; set; }

        public bool Selected { get; set; }


    }

    //public class vitalpara
    //{
    //    public decimal BodyTemperature { get; set; }
    //    public string  BloodPressure { get; set; }
    //    public decimal Weight { get; set; }
    //    public decimal Sugar { get; set; }

    //    public decimal Oxygenlevel { get; set; }

    //    public int Heartrate { get; set; }
    //    public string Remarks { get; set; }
    //    public int ctrl { get; set; }
    //    public string PatientCode { get; set; }
    //    public string HOScode { get; set; }
    //    public string Appointmentcode { get; set; }
    //    public string status { get; set; }
    //}

    public class MainVitalPara {
      
        public string FatherMedicalHistory { get; set; }
        public string MotherMedicalHistory { get; set; }
        public string ctrl { get; set; }
        public string patientCode { get; set; }
        public string hoscode { get; set; }
        public string Appointmentcode { get; set; }

    }

    public class DepartmentMapping
    {
        public int StationId { get; set; }
        public string Hos_department_code { get; set; }
        public string Hos_code { get; set; }
    }

    public class AddOPDOTTransfer
    {
        [Required(ErrorMessage = "*")]
        public string operation_date { get; set; }
        //public string AppointmentCode { get; set; }
        public string operation_time { get; set; }
        public string operation_type { get; set; }
        public List<Doctor_list> OPDDoctor_lst { get; set; }
        public string doctor_code { get; set; }
        public string appointment_code { get; set; }

    }

    //public partial class OPD_OT_Transfer
    //{
    //    public string appointment_code { get; set; }
    //    public int TransferId { get; set; }
    //    public List<Inpatient_OT_List> OT_transfer_List { get; set; }
    //    public string statusmsg { get; set; }
    //    public OT_Detail OTdetail { get; set; }
    //    public OT_Service OTservice { get; set; }

    //}


    //public  class PateintCaseStudy
    //{
    //    public string FoodAllergies { get; set; }
    //    public string TendencyBleed { get; set; }
    //    public string HeartDisease { get; set; }
    //    public string HighBloodPressure { get; set; }
    //    public string Diabetic { get; set; }
    //    public string Surgery { get; set; }
    //    public string Accident { get; set; }
    //    public string Others { get; set; }
    //    public string FamilyMedicalHistory { get; set; }
    //    public string CurrentMedication { get; set; }
    //    public string FemalePregnancy { get; set; }
    //    public string BreastFeeding { get; set; }
    //    public string HealthInsurance { get; set; }
    //    public string LowIncom { get; set; }
    //    public string Reference { get; set; }
    //    public string HosCode { get; set; }
    //    public string EmployeeCode { get; set; }
    //    public string PatientCode { get; set; }
    //    public int Ctrl { get; set; }

    //}




}