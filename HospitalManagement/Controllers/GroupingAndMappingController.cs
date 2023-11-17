using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Helper;
using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class GroupingAndMappingController : _Base
    {
        readonly GroupingAndMapping_Master Group;
        public GroupingAndMappingController()
        {
            Group = new GroupingAndMapping_Master(bsInfo);
        }
        // GET: GroupingAndMapping
        #region Grouping
        public ActionResult Grouping()
        {
            GroupingModel Model = new GroupingModel();
            return View(Model);
        }
        public JsonResult Add_UpdateGroupName(GroupingModel M = null)
        {
            DataTable dt = new DataTable(); string Status = "";
            if (M.GroupName == null)
                dt = Group.GetGroupData(Convert.ToString(Session["ClinicCode"]));
            else
                dt = Group.Add_UpdateGroup(Convert.ToString(Session["UserCode"]), Convert.ToString(Session["ClinicCode"]), M.GroupId, M.GroupName, M.Ctrl, out Status);
            return ManageGroupData(dt, Status);
        }
        public JsonResult ManageGroupData(DataTable dt, string Status)
        {
            List<GroupList> lst = Extend.ToList<GroupList>(dt);
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/GroupingAndMapping/_GroupGrid.cshtml", lst, true);
            return Json(new { Status = Status, GroupGrid = strhtml }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteGroup(int Groupid)
        {
            string Status;
            DataTable dt = Group.DeleteGroup(Convert.ToString(Session["UserCode"]), Convert.ToString(Session["ClinicCode"]), Groupid, out Status);
            return ManageGroupData(dt, Status);
        }
        public JsonResult GetMappedData(int Groupid, string Type = null)
        {
            DataSet ds = Group.GetMappedData(Convert.ToString(Session["ClinicCode"]), Groupid, Type);
            return ManageGroupMappedData(ds, null, null);
        }
        public JsonResult ManageGroupMappedData(DataSet ds, string Status, string ActionType)
        {
            MappingModel Model = new MappingModel();
            Model.groupName = Convert.ToString(ds.Tables[0].Rows[0]["group_name"]);
            Model.group_id = Convert.ToInt16(ds.Tables[0].Rows[0]["group_id"]);
            //Bind Mapped Data
            Model.MappList = Extend.ToList<Mapp_UnMappList>(ds.Tables[1]);
            //Bind UnMapped Data
            Model.UnMappList = Extend.ToList<Mapp_UnMappList>(ds.Tables[2]);
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/GroupingAndMapping/_GroupMapping.cshtml", Model, true);
            return Json(new { GroupMapped = strhtml, ActionType = ActionType, Status = Status }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add_Mapping(MappingModel Model, string Click)
        {
            string ActionType = ""; string Status = "fail"; DataTable dt_Mapped = new DataTable(); DataTable dt_UnMapped = new DataTable();
            DataSet ds = new DataSet();
            if (Model.AddrightValues != null || Model.AddLeftValues != null)
            {
                if (Click == "add_right" && Model.AddrightValues!=null)
                {                    
                    dt_UnMapped = CreateTable_WithData(Model.AddrightValues);
                    if (dt_UnMapped.Rows.Count > 0)
                        ds = Group.Mapp_UnMappData(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), Model.group_id, Model.Type, "mapp_data", dt_UnMapped, out Status);
                    ActionType = "Mapp";
                }
                else if (Click == "add_left" && Model.AddLeftValues!=null)
                {                    
                    dt_Mapped = CreateTable_WithData(Model.AddLeftValues);
                    if (dt_Mapped.Rows.Count > 0)
                        ds = Group.Mapp_UnMappData(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), Model.group_id, Model.Type, "unmapp_data", dt_Mapped, out Status);
                    ActionType = "UnMapp";
                }
                else
                {
                    Status = "wrong";
                    return Json(new { GroupMapped = "", ActionType = ActionType, Status = Status }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Status = "add";
                return Json(new { GroupMapped = "", ActionType = ActionType, Status = Status }, JsonRequestBehavior.AllowGet);
            }
            return ManageGroupMappedData(ds, Status, ActionType);
        }
        public DataTable CreateTable_WithData(string value)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("IsCheck", typeof(int));
            dt.Columns.Add("Type", typeof(string));
            //Set Value in datatable
            foreach (var item in value.Split(','))
            {
                if (item != "")
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = Convert.ToInt32(item.Split('-')[0]);
                    dr["Name"] = "";
                    dr["IsCheck"] = 1;
                    dr["Type"] = Convert.ToString(item.Split('-')[1]);
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion
    }
}