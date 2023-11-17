using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    public class EmployeeData
    {
        public List<HMSEmployeeModel> EmpList { get; set; }


        public HMSEmployeeTime EmpTime { get; set; }
        public List<HMSEmployeeSalary> EmpSalary { get; set; }
        public string ActionType { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeFirstName { get; set; }

        public int EmployeeroleId { get; set; }

        public List<Usergroup_Entity> Rolelistdata { get; set; }

    }
    public class HMSEmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string EmployeeFirstname { get; set; }

        [Required(ErrorMessage = "*")]
        public string EmployeeLastname { get; set; }
        public int Age { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{10}|[0-9]{11}|[0-9]{12}|[0-9]{13})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Confirmpass { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string DistrictCode { get; set; }
        public string CityCode { get; set; }
        public string ZipCode { get; set; }
        public string PresentAddress { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "*")]
        public string JoinigDate { get; set; }

        [Required(ErrorMessage = "*")]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

      
        public string DoctorCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string HosDepartmentcode { get; set; }

        public string HosCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string Designation { get; set; }

        public bool Ctrl { get; set; }

        public HMSEmployeeModel()
        {
            Ctrl = true;
        }

        public int EmployeeType { get; set; }

        public List<Country_Entity> CountryLst { get; set; }
        public List<State_Entity> StateLst { get; set; }
        public List<District_Entity> DistrictLst { get; set; }
        public List<City_Entity> CityLst { get; set; }
        public List<HospitalDepartment_Entity> HosDepartmentLst { get; set; }

        public List<EmployeeTiming_Entity> Employeetiming { get; set; }

        public List<Usergroup_Entity> Rolelst { get; set; }

        public string DeparmentName { get; set; }

        public string DMBCNO { get; set; }

        public int status { get; set; }

    }

    public class HMSEmployeeTime
    {
        public string EffectiveDate { get; set; }

        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string SunIn { get; set; }
        public string SunOut { get; set; }
        public string MonIn { get; set; }
        public string MonOut { get; set; }
        public string TueIn { get; set; }
        public string TueOut { get; set; }
        public string WedIn { get; set; }
        public string WedOut { get; set; }
        public string ThuIn { get; set; }
        public string ThuOut { get; set; }
        public string FriIn { get; set; }
        public string FriOut { get; set; }
        public string SatIn { get; set; }
        public string SatOut { get; set; }
        public bool IsCustom { get; set; } = false;
        public string EmployeeCode { get; set; }
        public Int32 EmpTimeId { get; set; }
        public List<CheckBoxModel> DayesCheckBoxItems { get; set; }

        public string Status { get; set; }
    }
    public class CheckBoxModel
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class HMSEmployeeSalary
    {

        [Required(ErrorMessage = "*")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal EmployeeNetSalary { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal EmployeeBasicSlary { get; set; }
        public decimal EmployeeTDS { get; set; }
        public string EmpDearnessAllowance { get; set; }
        public decimal EmpStateInsurance { get; set; }
        public string EmpHouserentAllowance { get; set; }
        public decimal EmpProvidentFund { get; set; }
        public decimal EmpConveyance { get; set; }
        public decimal EmpAllowance { get; set; }
        public decimal EmpProfTax { get; set; }
        public decimal EmpMedicalAllowance { get; set; }
        public decimal EmpLabourWelfare { get; set; }
        public decimal EmpFund { get; set; }
        public string EmpOther { get; set; }
        public string EmployeeCode { get; set; }
        public bool Ctrl { get; set; }

        public HMSEmployeeSalary()
        {
            Ctrl = true;
        }

    }

}