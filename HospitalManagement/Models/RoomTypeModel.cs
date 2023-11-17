using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{


    public class RoomTypeModel
    {
        public int RoomTypeId { get; set; }
        
        [Required(ErrorMessage = "*")]
        public int WardId { get; set; }

        [Required(ErrorMessage = "*")]
        public string NoofRoom { get; set; }

        [Required(ErrorMessage = "*")]
        public int NoofBed { get; set; }

        [Required(ErrorMessage = "*")]
        public int start_serial { get; set; } = 1;

        [Required(ErrorMessage = "*")]

        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        public string prefix { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Please Enter valid  Rent Amount")]
        //[Range(0.01, 9999999999999999.99, ErrorMessage = "Please enter a valid Rent Amount")]
        public decimal? PerDayRent { get; set; }
        public string Facility { get; set; }
        public string Hoscode { get; set; }

        [Required(ErrorMessage = "*")]
        public string Roomfacility { get; set; }
        public int Ctrl { get; set; }

        public RoomTypeModel()
        {
            Ctrl = 1;
        }
        public List<RoomTypeList> RoomTypeList { get; set; }
        public List<Patient_Allotment_Entity> PatientAllotmentList { get; set; }
        public List<wardType_Entity> WardTypeList { get; set; }

    }

    public class RoomTypeList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ward_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ward_type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_facility { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int no_of_bed { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal per_day_rent { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string facility { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string no_of_room { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string prefix { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int start_with_serial { get; set; }

    }

    public sealed class AllotmentModel
    {
       
        public int admit_id { get; set; }
      
        public string patientname { get; set; }
      
        public string doctorname { get; set; }

        public string Roomfacility { get; set; }

        public string roomtype { get; set; }
    
        public string patient_code { get; set; }
      
        public string hos_doctor_code { get; set; }

        [Required(ErrorMessage = "*")]
        public int roomtype_id { get; set; }

        [Required(ErrorMessage = "*")]
        public int bed_no { get; set; }

        [Required(ErrorMessage = "*")]
        public string room_no { get; set; }
     
        public bool isrelease { get; set; }
      
        public int ctrl { get; set; }
      
        public string message { get; set; }

        public List<RoomTypeList> RoomTypeList { get; set; }
        public List<Room_Entity> RoomList { get; set; }
        public List<Bed_Entity> BedList { get; set; }

        public int ward_id { get; set; }

    }

    public sealed class WardTypeModel
    {
        [Required(ErrorMessage = "*")]
        public string WardType { get; set; }
        public string HosCode { get; set; }
        public int ctrl { get; set; }

        public int WardId { get; set; }

        public List<wardType_Entity> WardTypeList { get; set; }

    }


}