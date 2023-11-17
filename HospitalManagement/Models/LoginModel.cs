using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        public string HosCode { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "User Name")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        public string Pwd { get; set; }

        [Required(ErrorMessage = "*")]
        public string Capcha { get; set; }

        public bool RememberMe { get; set; }


    }

    public class ChangePasswordmodel
    {

        [StringLength(20), Required(ErrorMessage = "*"), DataType(DataType.Password),  MinLength(6,ErrorMessage ="Minimum lenght of 6")]
        public string oldPwd { get; set; }

        [StringLength(20), Required(ErrorMessage = "*"), DataType(DataType.Password), MinLength(6, ErrorMessage = "Minimum lenght of 6")]
        public string newPwd { get; set; }

        [StringLength(20), Required(ErrorMessage = "*"), DataType(DataType.Password),  MinLength(6, ErrorMessage = "Minimum lenght of 6")]
        [Compare("newPwd", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfPwd { get; set; }

    }

    public class Forgetpass
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }
    }
}