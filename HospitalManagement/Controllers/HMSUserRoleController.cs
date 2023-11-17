using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using HMS.BizLogic;
using HMS.Data;
using System.Data;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSUserRoleController : _Base
    {
        // GET: HMSUserRole
        readonly Common_Master comMaster;
        public HMSUserRoleController()
        {
            comMaster = new Common_Master(bsInfo);
        }
        public ActionResult HMSRolePermission()
        {
            UserRole Model = new UserRole();
            var rs = comMaster.GetHospitalMenuDataSet(0,Convert.ToString(Session["ClinicCode"]));
            Model.UserGroupList = Extend.ToList<Usergroup_Entity>(rs.Tables[0]).ToList();
            Model.MenuList = Extend.ToList<HpMenu_Entity>(rs.Tables[1]).ToList();
            Model.UserGroupId = Convert.ToInt16(rs.Tables[2].Rows[0]["UsergroupId"]);
            return View(Model);
        }

        [HttpPost]
        public PartialViewResult GetHospitalMenuList(int Id)
        {
            UserRole modal = new UserRole();
            modal.UserGroupList = new List<Usergroup_Entity>();
            var Data = comMaster.GetHospitalMenuDataSet(Id, Convert.ToString(Session["ClinicCode"]));
            modal.MenuList = Extend.ToList<HpMenu_Entity>(Data.Tables[1]).ToList();
            return PartialView("HMSRolePermission", modal);
        }

        [HttpPost]
        public ActionResult RoleAssign (List<MenuList> Data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserGroupId", typeof(int));
            dt.Columns.Add("HPMenuId", typeof(int));
            dt.Columns.Add("HospitalCode", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            if(Data!=null)
            {
                foreach (var item in Data)
                {
                    DataRow dr = dt.NewRow();
                    dr["UserGroupId"] = item.UserGroupId;
                    dr["HPMenuId"] = item.Hp_MenuId;
                    dr["HospitalCode"] = Convert.ToString(Session["ClinicCode"]);
                    dr["ctrl"] = 1;
                    dt.Rows.Add(dr);
                }
            }            
            var result = comMaster.SaveRoleAcess(dt);
            return Json(result== true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
    }
}