using HMS;
using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class HMSVitalParaModel
    {
        [Key]
        public int HosVitalParaId { get; set; }
        public string HosVitalParaCode { get; set; }
        [StringLength(500), Required(ErrorMessage = "*")]
        public string HosVitalParaName { get; set; }
        public string VitalUnit { get; set; }
        public bool Ctrl { get; set; }
        public HMSVitalParaModel()
        {
            Ctrl = true;
        }
        public string HosiptalCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string VitalParaCode { get; set; }
        public List<VitalLists> VitalLst { get; set; }
    }
    public class VitalLists
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int vital_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string vitalpar_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string vitalpara_name { get; set; }
    }
}