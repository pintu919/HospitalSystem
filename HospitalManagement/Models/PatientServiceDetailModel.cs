using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    public class PatientServiceDetailModel
    {
        //public List<Investigation_Entity> Inv_List { get; set; }
        public List<SelectListItem> Inv_List { get; set; }
        public int[] investigationmaster_id { get; set; }

        [Required(ErrorMessage = "*")]
        public string PatientFirstName { get; set; }
        [Required(ErrorMessage = "*")]
        public string PatientLasttName { get; set; }
        [Required(ErrorMessage = "*")]
        public string DateOfBirth { get; set; }

        public int Age { get; set; }
        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}|[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*")]
        public string PresentAddress { get; set; }

    }
}