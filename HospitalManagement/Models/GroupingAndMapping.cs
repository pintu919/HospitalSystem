using HMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class GroupingModel
    {
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Please enter groupname")]
        public string GroupName { get; set; }
        public bool Ctrl { get; set; } = true;
    }
    public class GroupList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int GroupId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string GroupName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CreatedDate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }
    public class MappingModel
    {
        public int group_id { get; set; }
        public string groupName { get; set; }
        public string Type { get; set; }
        public string AddLeftValues { get; set; }
        public string AddrightValues { get; set; }
        //belowe Class For All UnMapp Data
        public List<Mapp_UnMappList> UnMappList { get; set; }
        //belowe Class For All Mapp Data
        public List<Mapp_UnMappList> MappList { get; set; }        
    }
    public class Mapp_UnMappList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsCheck { get; set; } = false;
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Type { get; set; }
    }
}