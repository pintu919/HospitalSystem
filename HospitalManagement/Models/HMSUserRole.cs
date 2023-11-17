using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagement.Models
{
    public class UserRole
    {
        public int UserGroupId { get; set; }
        public bool Ctrl { get; set; }
        public UserRole()
        {
            Ctrl = true;
        }
        public List<Usergroup_Entity> UserGroupList { get; set; }
        public List<HpMenu_Entity> MenuList { get; set; }
    }

    public class MenuList
    {
        public int UserGroupId { get; set; }
        public int Hp_MenuId { get; set; }
    }
}