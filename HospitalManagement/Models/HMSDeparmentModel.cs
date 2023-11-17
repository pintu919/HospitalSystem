using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS;

namespace HospitalManagement.Models
{
    public class HMSDeparmentModel
    {
        [Key]
        public int HosDepartmentId { get; set; }

        public string HosDepartmentCode { get; set; }

        [StringLength(500), Required(ErrorMessage = "*")]
        public string HosDepartmentName { get; set; }
        public bool Ctrl  { get; set; }
        public HMSDeparmentModel()
        {
            Ctrl = true;
        }
        public string HosiptalCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string DepartmentCode { get; set; }

        public List<Department_Entity> DepartmentLst { get; set; }
    }
}