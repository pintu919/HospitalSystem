using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    

    public class CancelAppointmentModel
    {
        public List<HMSCancelAppointModel> DoctorCanappointlist { get; set; }
        public List<Doctor_Entity> DoctorList { get; set; }

        [Required(ErrorMessage = "*")]
        public string cancelreason { get; set; }

        public string HosDocCode { get; set; }

    }

    public class HMSCancelAppointModel
    {
     
        public string AppointmentType { get; set; }
        public string AppointmentCode { get; set; }
        public string AppointmentDate { get; set; }
        public string HosDocCode { get; set; }
        public string patient_code { get; set; }
        public bool Ctrl { get; set; }
        public HMSCancelAppointModel()
        {
            Ctrl = true;
        }
        public bool IsAppointment { get; set; }

        [Required(ErrorMessage = "*")]
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientProfile { get; set; }
        public string DoctorName { get; set; }
        public string HosDepName { get; set; }
        public string AppointmentTime { get; set; }
        public string uniqrow { get; set; }
        public string Gender { get; set; }
        public int Isrefund { get; set; }
        public string cancelreason { get; set; }

    }
}