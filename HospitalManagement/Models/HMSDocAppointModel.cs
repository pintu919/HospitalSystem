using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;
using HMS.BizLogic;
namespace HospitalManagement.Models
{

    public class AppointmentModel
    {
        public HMSDocAppointModel RegPatAppointment { get; set; }
        public HMSDocNonRegpatAppointModel NonRegPatAppointment { get; set; }

        //Bellow Class is common class for all
        public List<HospitalDepartment_Entity> HosDepartmentList { get; set; }
        public List<Patient_Entity> PatientList { get; set; }
        public List<Doctor_Entity> DoctorList { get; set; }
        public List<DoctorTimeSlot> DoctorTimeSlotList { get; set; }
        public List<DocAppintment_Entity> Doctorappointlist { get; set; }
        public List<Country_Entity> CountryLst { get; set; }
        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }

        public List<ComissionAgent_Entity> Comission_Agent_List { get; set; }
    }

    


    public class HMSDocAppointModel
    {
        [Key]

        [Required(ErrorMessage = "*")]
        public string AppointmentType { get; set; }
        public string AppointmentCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentFromTime { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDepCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDocCode { get; set; }

        public string HosCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string patient_code { get; set; }

        [Required(ErrorMessage = "*")]
        public int Ctrl { get; set; }

        public HMSDocAppointModel()
        {
            Ctrl = 1;
        }
        public bool IsAppointment { get; set; }
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }

        public int PatientAge { get; set; }

        public string PatientProfile { get; set; }

        public string DoctorName { get; set; }

        public string HosDepName { get; set; }

        public string AppointmentTime { get; set; }
        public string uniqrow { get; set; }

        public string cancelreason { get; set; }


        public string Comission_Agent_Id { get; set; } = "0";

      



    }

    public class HMSDocNonRegpatAppointModel
    {
        [Required(ErrorMessage = "*")]
        public string AppointmentType { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string AppointmentFromTime { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDepCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDocCode { get; set; }
        public string HosCode { get; set; }
        [Required(ErrorMessage = "*")]
        public int Ctrl { get; set; }

        public HMSDocNonRegpatAppointModel()
        {
            Ctrl = 1;
        }
        public bool IsAppointment { get; set; }
      
        [Required(ErrorMessage = "*")]
        public int PatientAge { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientLastName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientDOB { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string MaritalStatus { get; set; }

        public string PatientEmail { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientMobile { get; set; }
        public int IsNonRegPat { get; set; }
        public string Relation { get; set; }
        public string SpouseName { get; set; }
        public string PatientProfessions { get; set; }
        public string Comission_Agent_Id { get; set; } = "0";

        [Required(ErrorMessage = "*")]
        public string PatientMotherName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFatherName { get; set; }
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string DistrictCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityCode { get; set; }

        public string PatVerify { get; set; }

        public string NID_Birthregno { get; set; }

        public string ZipCode { get; set; }

        public string Photo { get; set; }
    }

    public class WalkingAppointmodel
    {
        public HMSDocAppointWalkModel RegPatwalkingAppointment { get; set; }
        public HMSDocNonregPatWalkAppointModel NonregPatWalkAppoint { get; set; }
        public List<Doctor_Entity> DoctorList { get; set; }
        public List<HospitalDepartment_Entity> HosDepartmentList { get; set; }
        public List<Country_Entity> CountryLst { get; set; }

        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }
        public List<ComissionAgent_Entity> Comission_Agent_List { get; set; }
    }
    public class HMSDocAppointWalkModel
    {
        public string AppointmentType { get; set; }
        public string AppointmentCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentDate { get; set; }
        public string AppointmentFromTime { get; set; }
        [Required(ErrorMessage = "*")]
        public string HosDepCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string HosDocCode { get; set; }
        public string HosCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string patient_code { get; set; }
        [Required(ErrorMessage = "*")]
        public int Ctrl { get; set; }
        public HMSDocAppointWalkModel()
        {
            Ctrl = 1;
        }
        public bool IsAppointment { get; set; }

        public string PatientName { get; set; }

        public string Comission_Agent_Id { get; set; } = "0";
    }

    public class HMSDocNonregPatWalkAppointModel
    {
        public string AppointmentType { get; set; }
        public string AppointmentCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentDate { get; set; }
        public string AppointmentFromTime { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDepCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDocCode { get; set; }
        public string HosCode { get; set; }


        public int Ctrl { get; set; }
        public HMSDocNonregPatWalkAppointModel()
        {
            Ctrl = 1;
        }
        public bool IsAppointment { get; set; }

        [Required(ErrorMessage = "*")]
        public int PatientAge { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientLastName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientMotherName { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFatherName { get; set; }
        public string BloodGroup { get; set; }


        [Required(ErrorMessage = "*")]
        public string PatientDOB { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string DistrictCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityCode { get; set; }


        [Required(ErrorMessage = "*")]
        public string PatientEmail { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientMobile { get; set; }
        public int IsNonRegPat { get; set; }
        public string Relation { get; set; }
        public string SpouseName { get; set; }
        public string PatientProfessions { get; set; }
        public string Comission_Agent_Id { get; set; } = "0";
        public string PatVerify { get; set; }
        public string NID_Birthregno { get; set; }
        public string ZipCode { get; set; }
        public string Photo { get; set; }

    }

    public class RegPatient
    {
        [SqlKey(Display = true)]
        public string status { get; set; }
        [SqlKey(Display = true)]
        public string name { get; set; }
    }


    public class AppointModel
    {
        public List<AppointList> Patient_AppList { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }


    }
    public class AppointList
    {
        public string AppoinmentCode { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public int IsVisited { get; set; }
        public string HospitalCode { get; set; }
        public string Mobile { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Profile { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointDate { get; set; }
        public string UniqRow { get; set; }
        public bool IsView { get; set; }
        public DateTime AppointDatetime { get; set; }
        public string status { get; set; }
        public string paymenttype { get; set; }
        public string patuniqrow { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }


    }




}