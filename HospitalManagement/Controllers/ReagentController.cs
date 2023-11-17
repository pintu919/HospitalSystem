using HMS;
using HMS.Data;
using HospitalManagement.Helper;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using HospitalManagement.Models;
using HMS.BizLogic;
using System.Collections.Generic;
using static QRCoder.PayloadGenerator.SwissQrCode;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class ReagentController : _Base
    {
        readonly Reagent_Master reg;
        public ReagentController()
        {
            reg = new Reagent_Master(bsInfo);
        }
        // GET: Reagent
        #region "Add New Reagent"
        public ActionResult AddReagent(string Enc)
        {
            string para = "0";
            if (Enc != null)
                para = new QueryStringModule().Decrypt(Enc);
            AddReagent a = new AddReagent();
            DataSet ds = reg.GetAllInvestigation(para, Convert.ToString(Session["ClinicCode"]));
            //a = ds.Tables[1].ToList<AddReagent>().SingleOrDefault();
            a.Inv_Lists = ds.Tables[0].ToList<investigationLists>().ToList();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                a.Ctrl = Convert.ToBoolean(ds.Tables[1].Rows[i]["Ctrl"]);
                a.InvestigationId = Convert.ToInt32(ds.Tables[1].Rows[i]["InvestigationId"]);
                a.is_usiesvalidity = Convert.ToBoolean(ds.Tables[1].Rows[i]["is_usiesvalidity"]);
                a.purchase_unit = Convert.ToString(ds.Tables[1].Rows[i]["purchase_unit"]);
                a.reagent_id = Convert.ToInt32(ds.Tables[1].Rows[i]["reagent_id"]);
                a.reagent_name = Convert.ToString(ds.Tables[1].Rows[i]["reagent_name"]);
                a.uses_unit = Convert.ToString(ds.Tables[1].Rows[i]["uses_unit"]);
                a.validity_date = Convert.ToString(ds.Tables[1].Rows[i]["validity_date"]);
            }
            return View(a);
        }
        [HttpPost]
        public ActionResult SaveReagent(AddReagent M)
        {
            string Status = "";
            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Convert.ToInt16(M.reagent_id) == 0)
                    {
                        DataTable dt = reg.AddReagent(Convert.ToString(Session["ClinicCode"]), M.reagent_name, M.uses_unit, Convert.ToInt16(M.is_usiesvalidity),
                                                 M.purchase_unit, M.Ctrl.ToInt(), M.InvestigationId, Convert.ToString(Session["UserCode"]), M.validity_date);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string res = Convert.ToString(dt.Rows[0]["StatusMessage"]) == "same" ? Convert.ToString(dt.Rows[0]["StatusMessage"]) : Convert.ToString(dt.Rows[0]["returnvalue"]);
                            if (res != "same")
                            {
                                if (res.ToInt() > 0 ? true : false)
                                {
                                    //return RedirectToAction("ReagentMaster", "Reagent", new { Enc = new QueryStringModule().Encrypt("" + M.InvestigationId + "=" + Convert.ToString(dt.Rows[0]["Type"]) + "") });
                                    Status = "Record Added Successfully!";
                                }
                                else { Status = "Record Not Saved!"; }
                            }
                            else
                                Status = "Record Allrady Exists!";
                        }
                        else
                        {
                            Status = "Record Not Saved!";
                        }
                    }
                    else
                    {
                        DataTable dt = reg.UpdateReagent(M.reagent_name, M.uses_unit, Convert.ToInt16(M.is_usiesvalidity),
                                                 M.purchase_unit, M.Ctrl.ToInt(), M.InvestigationId, M.reagent_id,
                                                 Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), M.validity_date);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            bool res = Convert.ToInt32(dt.Rows[0]["returnvalue"]) > 0 ? true : false;
                            if (res)
                            {
                                Status = "Record Updated Successfully!";
                                //return RedirectToAction("ReagentMaster", "Reagent", new { Enc = new QueryStringModule().Encrypt("" + M.InvestigationId + "=" + Convert.ToString(dt.Rows[0]["Type"]) + "") });
                            }
                            else
                            {
                                Status = "Record Not Saved!";
                            }
                        }
                        else
                        {
                            Status = "Record Not Saved!";
                        }
                    }
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
            }
            else { return Json("Enter Field"); }
            return View("ReagentList");
        }
        [HttpPost]
        public JsonResult DeleteReagent(int id)
        {
            string Status = "";
            //For Activity log         
            // DataTable dtlogtable = comMaster.Loghistory(null, "hms_reagent_master", (Session["UserName"] as Userinfo_Entity).logno, Request.UserHostAddress, Request.Browser.Browser, "Delete", Convert.ToString(id));
            //end
            var res = reg.Delete(Convert.ToString(Session["ClinicCode"]), id);
            if (res != "notallow")
            {
                if (res.ToInt() > 0 ? true : false) { Status = "Record Deleted Successfully!"; }
                else { Status = "Record Not Delete!"; }
            }
            else
                Status = "It is not delete because of that investigation parameter is exists!";
            return Json(Status);
        }
        #endregion

        #region "Reagent Lists"
        public ActionResult ReagentList()
        {
            List_regent lst = new List_regent();
            DataSet ds = reg.GetAllReagent(0, 10, "", "all", Convert.ToString(Session["ClinicCode"]));
            lst.lst_regent = ds.Tables[0].ToList<Reagent_ModelList>();
            lst.rows_count = Convert.ToInt16(ds.Tables[1].Rows[0]["TotalCount"]) >= 10 ? (int)Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalCount"]) / 10)) : 0;
            return View(lst);
        }
        [HttpPost]
        public ActionResult FilterReagent(List_regent M)
        {
            DataSet ds = reg.GetAllReagent(0, 10, M.SearchText == null ? "" : M.SearchText, M.ParamaterStatus, Convert.ToString(Session["ClinicCode"]));
            var html_bind = "";
            List_regent lst = new List_regent();
            if (ds.Tables[0].Rows.Count > 0)
                lst.lst_regent = ds.Tables[0].ToList<Reagent_ModelList>();

            int rows_count = Convert.ToInt16(ds.Tables[1].Rows[0]["TotalCount"]) >= 10 ? (int)Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalCount"]) / 10)) : 0;

            html_bind = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Reagent/ReagentList.cshtml", lst, true);

            return Json(new
            {
                Cleared = html_bind,
                rows_count = rows_count
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPageWiseData(string Type, int PageNumber, string Search_Txt, string ParaStatus)
        {
            DataSet ds = reg.GetAllReagent(PageNumber - 1, 10, Search_Txt == null ? "" : Search_Txt, ParaStatus, Convert.ToString(Session["ClinicCode"]));
            var html_bind = "";
            List_regent lst = new List_regent();
            if (ds.Tables[0].Rows.Count > 0)
                lst.lst_regent = ds.Tables[0].ToList<Reagent_ModelList>();
            html_bind = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Reagent/ReagentList.cshtml", lst, true);

            return Json(new
            {
                Cleared = html_bind
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Reagent Master"
        public ActionResult ReagentMaster(string Enc)
        {
            InvestigationModel Modal = new InvestigationModel();
            Modal.SubGroup = new InvestigationSubGroup();
            Modal.SubGroupPara = new InvestigationSubGroupPara();
            string para = new QueryStringModule().Decrypt(Enc);
            Modal.InvestigationID = Convert.ToInt32(para.Split('=')[0]);
            Modal.InvestigationType = Convert.ToString(para.Split('=')[1]);
            Modal.ReagentId = Convert.ToInt32(para.Split('=')[2]);
            return View(Modal);
        }
        #region "Add Investigation Para"
        public PartialViewResult GetAllSubGroup(int inv_masterid, string InvType, int Reagentid)
        {
            InvestigationModel Modal = new InvestigationModel();
            Modal.SubGroupPara = new InvestigationSubGroupPara();
            Modal.SubGroup = new InvestigationSubGroup();
            DataSet Ds = reg.GetAllSubGroup(inv_masterid, Convert.ToString(Session["ClinicCode"]), Reagentid);
            Modal.SubGroup.INVType = InvType;
            Modal.ReagentId = Reagentid;
            if (Modal.SubGroup.INVType == "radiology")
            {
                Modal.SubGroup.Ctrl = Ds.Tables[1].Rows.Count > 0 ? Convert.ToBoolean(Ds.Tables[1].Rows[0]["ctrl"]) : true;
                Modal.SubGroup.InvestigationSubGroupId = Ds.Tables[1].Rows.Count > 0 ? Convert.ToInt16(Ds.Tables[1].Rows[0]["investigationsubgrop_id"]) : 0;
                Modal.SubGroup.InvestigationSubGroupName = Ds.Tables[1].Rows.Count > 0 ? Convert.ToString(Ds.Tables[1].Rows[0]["investigationsubgroup_name"]) : "";
            }
            else
                Modal.SubGroup.InvSubGroupList = Extend.ToList<Investigationsubgroup_Entity>(Ds.Tables[1]);

            Modal.SubGroup.InvestigationName = Convert.ToString(Ds.Tables[0].Rows[0]["investigationgroup_name"] + "$" + Ds.Tables[0].Rows[0]["investigationmaster_name"] + "$" + Ds.Tables[0].Rows[0]["reagent_name"]);
            Modal.SubGroup.InvestigationID = Convert.ToInt16(Ds.Tables[0].Rows[0]["investigationmaster_id"]);
            Modal.ReagentId = Reagentid;
            if (InvType == "radiology")
                //return PartialView("RadiologySubcategoryModel", Modal);
                return PartialView("ReagentMaster", Modal);
            else
                //return PartialView("SubcategoryModel", Modal);
                return PartialView("ReagentMaster", Modal);
        }
        [HttpPost]
        public PartialViewResult AddSubGroup(InvestigationModel model)
        {
            model.SubGroupPara = new InvestigationSubGroupPara();
            Investigationsubgroup_Entity ent = new Investigationsubgroup_Entity();
            if (model.SubGroup.InvestigationSubGroupName != null && model.SubGroup.InvestigationID != 0)
            {
                ent.investigationmaster_id = model.SubGroup.InvestigationID;
                ent.investigationsubgroup_name = model.SubGroup.InvestigationSubGroupName.Trim();
                ent.investigationsubgrop_id = model.SubGroup.InvestigationSubGroupId;
                ent.InvType = model.SubGroup.INVType;
                ent.Ctrl = Convert.ToInt16(model.SubGroup.Ctrl);
                ent.reagent_id = model.ReagentId;
                ent.hoscode = Convert.ToString(Session["ClinicCode"]);
                ent.usercode = Convert.ToString(Session["UserCode"]);
                ent.unit = model.SubGroup.Unit;
                ent.reference = model.SubGroup.RefreanceValue;
                ent.subgroup_id = model.SubGroup.InvestigationSubGroupId;
                if (Request.IsAjaxRequest())
                {
                    model.SubGroup.InvSubGroupList = reg.AddSubGroup(ent);
                    //if (Convert.ToInt16(ent.investigationsubgrop_id) == 0)
                    //{
                    //    model.SubGroup.InvSubGroupList = reg.AddSubGroup(ent);
                    //}
                    //else
                    //{
                    //    model.SubGroup.InvSubGroupList = reg.UpdateSubGroup(ent);
                    //}
                }
            }
            if (model.SubGroup.INVType == "radiology")
            {
                model.SubGroup.InvestigationSubGroupId = model.SubGroup.InvSubGroupList[0].investigationsubgrop_id;
                //return PartialView("RadiologySubcategoryModel", model);
                return PartialView("ReagentMaster", model);
            }
            else
                //return PartialView("SubcategoryModel", model);
                return PartialView("ReagentMaster", model);
        }
        public PartialViewResult SubGroup_AddUnit_Reference(int Reagentid, int INVSubgrop_id, string Units, string Reference, string ActType)
        {
            InvestigationModel model = new InvestigationModel();
            model.SubGroupPara = new InvestigationSubGroupPara();
            model.SubGroup = new InvestigationSubGroup();
            string Status_Result;
            model.ReagentId = Reagentid;
            DataTable dt_tbl = reg.AddSubGroup_Unit_Reference(INVSubgrop_id, Units, Reference, Reagentid, Convert.ToString(Session["UserCode"]), ActType, out Status_Result);
            model.SubGroup.InvSubGroupList = dt_tbl.ToList<Investigationsubgroup_Entity>().ToList();
            model.SubGroup.InvSubGroupList[0].status = Convert.ToInt16(Status_Result) > 0 ? "1" : "0";
            //var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Reagent/ReagentMaster.cshtml", model, true);
            //return Json(new { status = Convert.ToInt16(Status_Result), HtmlString = strhtml }, JsonRequestBehavior.AllowGet);
            return PartialView("ReagentMaster", model);
        }
        public PartialViewResult DeleteInveSubGroup(int ReagentId, int InvSubGroupId, int invmasterid, string InvType)
        {
            InvestigationModel Modal = new InvestigationModel();
            Modal.SubGroupPara = new InvestigationSubGroupPara();
            Modal.SubGroup = new InvestigationSubGroup();
            DataTable dt = reg.DeleteInveSubGroup(ReagentId, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), InvSubGroupId, invmasterid);
            Modal.SubGroup.InvSubGroupList = dt.ToList<Investigationsubgroup_Entity>().ToList();
            if (InvType == "radiology")
                //return PartialView("RadiologySubcategoryModel", Modal);
                return PartialView("ReagentMaster", Modal);
            else
                //return PartialView("SubcategoryModel", Modal);
                return PartialView("ReagentMaster", Modal);
        }
        public ActionResult SetSubGroupIndex(int subgroupid, int indexvalue)
        {
            bool IsStatus = reg.SetSubGroupIndex(subgroupid, indexvalue, Convert.ToString(Session["ClinicCode"]));
            string status = IsStatus == true ? "1" : "0";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Add Investigation Sub Para"
        public PartialViewResult GetAllSubGroupPara(int SubGroupId, string Type, int Reagentid)
        {
            InvestigationModel Modal = new InvestigationModel();
            Modal.SubGroup = new InvestigationSubGroup();
            Modal.SubGroupPara = new InvestigationSubGroupPara();
            DataSet Ds = reg.GetAllSubGroupPara(SubGroupId, Type, Convert.ToString(Session["ClinicCode"]), Reagentid);
            Modal.SubGroupPara.InvSubGroupParaList = Extend.ToList<Investigationsubgrouppara_Entity>(Type == "InvMaster" ? Ds.Tables[0] : Ds.Tables[1]);
            Modal.SubGroupPara.Type = Type;
            Modal.ReagentId = Reagentid;
            if (Type == "InvMaster")
                Modal.SubGroupPara.InvestigationID = SubGroupId;
            else
            {
                Modal.SubGroupPara.InvestigationSubGroupName = Convert.ToString(Ds.Tables[0].Rows[0]["investigationsubgroup_name"]);
                Modal.SubGroupPara.InvestigationSubGroupId = Convert.ToInt16(Ds.Tables[0].Rows[0]["investigationsubgrop_id"]);
            }
            // return PartialView("SubcategoryModel", Modal);
            return PartialView("ReagentMaster", Modal);
        }
        public PartialViewResult SubGroupPara_AddUnit_Reference(int SubGroupparaID, int INVSubgrop_id, int Reagentid, string Units, string Reference,
            string ActType, string Type)
        {
            InvestigationModel model = new InvestigationModel();
            model.SubGroupPara = new InvestigationSubGroupPara();
            model.SubGroup = new InvestigationSubGroup();
            model.ReagentId = Reagentid;
            DataTable dt_tbl = reg.AddSubGroupPara_Unit_Reference(SubGroupparaID, INVSubgrop_id, Units, Reference, Reagentid,
            Convert.ToString(Session["ClinicCode"]), ActType, Type, out string Status_Result);
            model.SubGroupPara.InvSubGroupParaList = dt_tbl.ToList<Investigationsubgrouppara_Entity>().ToList();
            model.SubGroupPara.InvSubGroupParaList[0].status = Convert.ToInt16(Status_Result) > 0 ? "1" : "0";
            return PartialView("ReagentMaster", model);
        }
        public PartialViewResult AddSubGroupPara(InvestigationModel model)
        {
            model.SubGroup = new InvestigationSubGroup();
            Investigationsubgrouppara_Entity enty = new Investigationsubgrouppara_Entity();
            if (model.SubGroupPara.InvestigationSubGroupId != 0 || model.SubGroupPara.InvestigationID != 0)
            {
                enty.investigationsubgrop_id = model.SubGroupPara.InvestigationSubGroupId;
                enty.investigationmaster_id = model.SubGroupPara.InvestigationID;
                enty.Ctrl = Convert.ToInt16(model.SubGroupPara.Ctrl);
                enty.investigationpara_id = model.SubGroupPara.InvestigationSubgroupParaID;
                enty.investigationpara_name = model.SubGroupPara.ParaName;
                enty.investigationpara_reference = model.SubGroupPara.Reference;
                enty.units = model.SubGroupPara.Units;
                enty.reagent_id = model.ReagentId;
                enty.hoscode = Convert.ToString(Session["ClinicCode"]);
                enty.usercode = Convert.ToString(Session["UserCode"]);
                if (Request.IsAjaxRequest())
                {
                    if (Convert.ToInt16(enty.investigationpara_id) == 0)
                    {
                        model.SubGroupPara.InvSubGroupParaList = reg.AddSubGroupPara(enty, model.SubGroupPara.Type);
                    }
                    else
                    {
                        model.SubGroupPara.InvSubGroupParaList = reg.UpdateSubGroupPara(enty);
                    }
                }
            }
            //return PartialView("SubcategoryModel", model);
            return PartialView("ReagentMaster", model);
        }
        public PartialViewResult DeleteInveSubGroupPara(int ParaId, int ReagentId, int SubGroupId, string Type, int InvMasterId)
        {
            InvestigationModel Modal = new InvestigationModel();
            Modal.SubGroupPara = new InvestigationSubGroupPara();
            Modal.SubGroup = new InvestigationSubGroup();
            var dt = reg.DeleteInveSubGroupPara(ParaId, SubGroupId == 0 ? InvMasterId : SubGroupId,
                Type, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), ReagentId);
            Modal.SubGroupPara.InvSubGroupParaList = dt.ToList<Investigationsubgrouppara_Entity>().ToList();
            //return PartialView("SubcategoryModel", Modal);
            return PartialView("ReagentMaster", Modal);
        }
        public ActionResult SetSubgroupParaIndex(int subgroupparaid, int indexvalue)
        {
            bool IsStatus = reg.SetSubgroupParaIndex(subgroupparaid, indexvalue, Convert.ToString(Session["ClinicCode"]));
            string status = IsStatus == true ? "1" : "0";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region "View Investigatiopn parameter"
        [HttpPost]
        public ActionResult ViewInvestigationPara(int reagent_id, int Investigationid)
        {
            List_regent model = new List_regent();
            model.ParaLists = new List<SubGroup_Inv>();
            var data = reg.ViewInvestigationPara(reagent_id, Investigationid, Convert.ToString(Session["ClinicCode"]));
            model.investigation_name = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);

            var pararesult = new List<InvestigationPara_Inv>();
            for (int i = 0; i < data.Tables[2].Rows.Count; i++)
            {
                var p_lst = new InvestigationPara_Inv
                {
                    investigationmaster_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationmaster_id"]),
                    investigationsubgrop_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationsubgrop_id"]),
                    investigationpara_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationpara_id"]),
                    investigationpara_name = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_name"]),
                    investigationpara_reference = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_reference"]),
                    find_ip_reference = "",
                };
                pararesult.Add(p_lst);
            }
            DataRow[] dr = data.Tables[1].Select();
            if (dr.Count() > 0)
            {
                foreach (var item in dr)
                {
                    var list = new SubGroup_Inv
                    {
                        SubgroupName = Convert.ToString(item[1]),
                        invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[0]))
                    };
                    model.ParaLists.Add(list);
                }
            }
            else
            {
                var list = new SubGroup_Inv
                {
                    SubgroupName = "",
                    invParaList = pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
                };
                model.ParaLists.Add(list);
            }
            return PartialView("_ViewInvestigationParaFormate", model);
        }
        #endregion
    }
}