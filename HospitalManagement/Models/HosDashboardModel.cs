using HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class HMSDashboardData
    {
        [SqlKey(Display = true)]
        public int totaldoc { get; set; }
        [SqlKey(Display = true)]
        public int totalpat { get; set; }
        [SqlKey(Display = true)]
        public int totalvisited { get; set; }
        [SqlKey(Display = true)]
        public int totalpending { get; set; }

        [SqlKey(Display = true)]
        public int today_appoint { get; set; }

        [SqlKey(Display = true)]
        public int today_appoint_online { get; set; }

        [SqlKey(Display = true)]
        public int todayappoint_offline { get; set; }

        [SqlKey(Display = true)]
        public int todayregister { get; set; }

        [SqlKey(Display = true)]
        public int totalinpatient { get; set; }

        [SqlKey(Display = true)]
        public int todayinpatient { get; set; }

        [SqlKey(Display = true)]
        public int totalOT { get; set; }

        [SqlKey(Display = true)]
        public int totalIPOT { get; set; }

        [SqlKey(Display = true)]
        public int totalOPDOT { get; set; }

        [SqlKey(Display = true)]
        public int totalrealsepatient { get; set; }

        [SqlKey(Display = true)]
        public int todayrealsepatient { get; set; }

        [SqlKey(Display = true)]
        public decimal todayearning { get; set; }

        public List<TopPatient> TopPatientList { get; set; }
        public List<TopDoctor> TopDoctorList { get; set; }
        public List<TopAppointment> TopAppointmentList { get; set; }
        public List<patientration> patientrationlist { get; set; }
        public List<billing> billinglist { get; set; }
        public List<HOSEmployee> employeelst { get; set; }

    }

   
    public class TopPatient
    {
        [SqlKey(Display = true)]
        public string patientname { get; set; }
        [SqlKey(Display = true)]
        public string email { get; set; }
        [SqlKey(Display = true)]
        public string phone { get; set; }
        [SqlKey(Display = true)]
        public int ctrl { get; set; }

        [SqlKey(Display = true)]
        public string patient_profile { get; set; }
        
    }

    public class patientration
    {
        [SqlKey(Display = true)]
        public int totalop { get; set; }
        [SqlKey(Display = true)]
        public int totalip { get; set; }
      

    }

    public class HOSEmployee
    {
        [SqlKey(Display = true)]
        public int totalemp { get; set; }

        [SqlKey(Display = true)]
        public int admin { get; set; }
        
        [SqlKey(Display = true)]
        public int staff { get; set; }
        
        [SqlKey(Display = true)]
        public int doctor { get; set; }
       
        [SqlKey(Display = true)]
        public int nurse { get; set; }
      
        //[SqlKey(Display = true)]
        //public int laboratorist { get; set; }

        [SqlKey(Display = true)]
        public int pharmacist { get; set; }

        [SqlKey(Display = true)]
        public int accountant { get; set; }

        [SqlKey(Display = true)]
        public int Pathologist { get; set; }


    }

    public class billing
    {
        [SqlKey(Display = true)]
        public decimal totaldirectbilling { get; set; }
        [SqlKey(Display = true)]
        public decimal totalamountip { get; set; }
        [SqlKey(Display = true)]
        public decimal totalamountop { get; set; }
      

    }
    public class TopDoctor
    {
        [SqlKey(Display = true)]
        public string doctor_name { get; set; }
        [SqlKey(Display = true)]
        public string doctor_designation { get; set; }

        [SqlKey(Display = true)]
        public string doctor_image { get; set; }

        [SqlKey(Display = true)]
        public string uniqrow { get; set; }
    }

    public class TopAppointment
    {
        [SqlKey(Display = true)]
        public int appointment_id { get; set; }
        [SqlKey(Display = true)]
        public string appointment_code { get; set; }
        [SqlKey(Display = true)]
        public string patient_name { get; set; }

        [SqlKey(Display = true)]
        public string patient_profile { get; set; }

        [SqlKey(Display = true)]
        public string patient_address { get; set; }
        [SqlKey(Display = true)]
        public string doctor_name { get; set; }
        [SqlKey(Display = true)]
        public string department_name { get; set; }
        [SqlKey(Display = true)]
        public string appointment_date { get; set; }
        [SqlKey(Display = true)]
        public string appointment_time { get; set; }
        [SqlKey(Display = true)]
        public int ctrl { get; set; }
        [SqlKey(Display = true)]
        public int is_appointment { get; set; }
        [SqlKey(Display = true)]
        public string uniqrow { get; set; }
    }
}