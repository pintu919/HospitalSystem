using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Models;
using HospitalManagement.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using static HospitalManagement.Helper.CustomHelper;
using System.Reflection;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class ComissionController : _Base
    {
        readonly Comission_Master COM;
        //readonly Payable_Master Payable;
        readonly Invoice_Master INVMaster;
        DataSet ds = new DataSet();
        public ComissionController()
        {
            COM = new Comission_Master(bsInfo);
            INVMaster = new Invoice_Master(bsInfo);
        }
        // GET: Comission
        public ActionResult AgentComissionMaster()
        {
            ComissionModel Model = new ComissionModel();
            ds = COM.GetAll(Convert.ToString(Session["ClinicCode"]));
            Model.ALlData = Extend.ToList<AgentList>(ds.Tables[0]);
            Model.comission_Category = Extend.ToList<ComissionCategoryList>(ds.Tables[1]);
            Model.ctrl = 1;
            return View("AgentComissionMaster", Model);
        }
        [HttpPost]
        public ActionResult Add_Update(ComissionModel Data)
        {
            if (Data.id == 0)
            {
                ds = COM.Insert(Data.AgentName, Convert.ToString(Session["ClinicCode"]), Data.AgentMobile, Data.CategoryId, Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = COM.Update(Data.id, Data.AgentName, Convert.ToString(Session["ClinicCode"]), Data.AgentMobile, Data.CategoryId, Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new AgentStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.ALlData = Extend.ToList<AgentList>(ds.Tables[1]);
                Data.comission_Category = Extend.ToList<ComissionCategoryList>(ds.Tables[2]);
            }
            return View("AgentComissionMaster", Data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ComissionModel Model = new ComissionModel();
            ds = COM.Delete(id, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            if (ds.Tables.Count > 0)
            {
                Model.ResponseMsg = new AgentStatus();
                Model.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Model.ALlData = Extend.ToList<AgentList>(ds.Tables[1]);
                Model.comission_Category = Extend.ToList<ComissionCategoryList>(ds.Tables[2]);
            }
            return View("AgentComissionMaster", Model);
        }

        public ActionResult ComissionCategoryMaster()
        {
            Session["lst_comission"] = null;
            ComissionCategoryModel model = new ComissionCategoryModel();
            DataTable dt = COM.GetServices();
            model.hos_service = Extend.ToList<Hos_Services>(dt);
            return View(model);
        }

        public JsonResult Add_UpdateComissionCategory(ComissionCategoryModel M = null)
        {
            DataTable dt = new DataTable(); string Status = "";
            string Type = "";
            if (M.hos_service != null)
            {
                if (M.hos_service.All(c => c.ischecked == false))
                {
                    dt = COM.GetCategoryData(Convert.ToString(Session["ClinicCode"]));
                    Status = "atleastone";
                }
                else
                {
                    DataTable dt1 = CreateTable();
                    DataTable dt2 = new DataTable();
                    if (M.hos_service != null)
                    {
                        foreach (var item in M.hos_service)
                        {
                            if (item.ischecked == true)
                            {
                                DataRow dr = dt1.NewRow();
                                dr["ServiceId"] = Convert.ToInt32(item.Id);
                                dr["ServiceName"] = Convert.ToString(item.servicename);
                                dr["Comission"] = Convert.ToDecimal(item.comission);
                                dr["ComissionType"] = Convert.ToString(item.comission_type);
                                dt1.Rows.Add(dr);

                                if (item.servicename == "Investigation")
                                {
                                    List<InvestigationService> lst_comission = (List<InvestigationService>)Session["lst_comission"];
                                    ListtoDataTableConverter converter = new ListtoDataTableConverter();
                                    dt2 = converter.ToDataTable(lst_comission);
                                    Type = item.servicename;
                                }
                                else
                                {
                                    List<InvestigationService> lst_comission = new List<InvestigationService>();
                                    ListtoDataTableConverter converter = new ListtoDataTableConverter();
                                    dt2 = converter.ToDataTable(lst_comission);
                                }

                            }
                        }
                    }

                    if (M.CategoryName == null)
                        dt = COM.GetCategoryData(Convert.ToString(Session["ClinicCode"]));
                    else
                        dt = COM.Add_UpdateCategory(dt1, Convert.ToString(Session["UserCode"]), Convert.ToString(Session["ClinicCode"]), M.CategoryId, M.CategoryName, M.Ctrl, Type, dt2, out Status);
                }
            }
            else
                dt = COM.GetCategoryData(Convert.ToString(Session["ClinicCode"]));

            return ManageCategoryData(dt, Status);
        }

        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }

        public ActionResult Get_CategorywiseServices(int CategoryId)
        {
            DataSet ds = new DataSet();
            ComissionCategoryModel model = new ComissionCategoryModel();

            ds = COM.GetCategoryWiseServices(Convert.ToString(Session["ClinicCode"]), CategoryId);

            if (ds.Tables.Count > 0)
            {
                model.CategoryName = Convert.ToString(ds.Tables[0].Rows[0]["category_name"]);
                model.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["category_id"]);
                model.hos_service = Extend.ToList<Hos_Services>(ds.Tables[1]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                model.Investigation_Group_List = new List<InviestigationGroupServices>();
                var ser_list = Extend.ToList<InvestigationService>(ds.Tables[3]);
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    string inv_group_code = Convert.ToString(ds.Tables[2].Rows[i]["investigationgroup_code"]);
                    model.Investigation_Group_List.Add(new InviestigationGroupServices
                    {
                        investigationgroup_code = inv_group_code,
                        grouptype = Convert.ToString(ds.Tables[2].Rows[i]["grouptype"]),
                        investigationgroup_name = Convert.ToString(ds.Tables[2].Rows[i]["investigationgroup_name"]),
                        Investigation_Service_List = ser_list.FindAll(d => d.investigationgroup_code == inv_group_code).ToList()
                    });

                }

                DataTable dt = new DataTable();
                int j = 0;
                foreach (var item in model.Investigation_Group_List)
                {
                    if (item.Investigation_Service_List != null)
                    {
                        if (j == 0)
                            dt = item.Investigation_Service_List.ToDataTable();
                        else
                            dt.Merge(item.Investigation_Service_List.ToDataTable());
                        j++;
                    }
                }
                Session["lst_comission"] = dt.ToList<InvestigationService>();
            }
            

           

            //model.Investigation_Group_List = new List<InviestigationGroupServices>();
            //var ser_list = Extend.ToList<InvestigationService>(ds.Tables[3]);
            //for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            //{
            //    string inv_group_code = Convert.ToString(ds.Tables[2].Rows[i]["investigationgroup_code"]);
            //    model.Investigation_Group_List.Add(new InviestigationGroupServices
            //    {
            //        investigationgroup_code = inv_group_code,
            //        grouptype = Convert.ToString(ds.Tables[2].Rows[i]["grouptype"]),
            //        investigationgroup_name = Convert.ToString(ds.Tables[2].Rows[i]["investigationgroup_name"]),
            //        Investigation_Service_List = ser_list.FindAll(d => d.investigationgroup_code == inv_group_code).ToList()
            //    });

            //}
            return View("ComissionCategoryMaster", model);

        }

        public ActionResult Get_AgentCategorywiseInvestigation(int CategoryId)
        {
            ComissionCategoryModel model = new ComissionCategoryModel();
            List<InvestigationService> lst_comission = (List<InvestigationService>)Session["lst_comission"];
            if (lst_comission == null)
            {

                ds = COM.GetAgentCategorywiseInvestigation(Convert.ToString(Session["ClinicCode"]), CategoryId);
                model.CategoryName = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]);
                model.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryId"]);
                model.hos_service = new List<Hos_Services>();
                model.Investigation_Group_List = new List<InviestigationGroupServices>();
                var ser_list = Extend.ToList<InvestigationService>(ds.Tables[2]);
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    string inv_group_code = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_code"]);
                    model.Investigation_Group_List.Add(new InviestigationGroupServices
                    {
                        investigationgroup_code = inv_group_code,
                        grouptype = Convert.ToString(ds.Tables[1].Rows[i]["grouptype"]),
                        investigationgroup_name = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_name"]),
                        Investigation_Service_List = ser_list.FindAll(d => d.investigationgroup_code == inv_group_code).ToList()
                    });

                }
            }
            else
            {
                

                ds = COM.GetAgentCategorywiseInvestigation(Convert.ToString(Session["ClinicCode"]), CategoryId);
                model.CategoryName = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]); ;
                model.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryId"]);
                model.hos_service = new List<Hos_Services>();
                model.Investigation_Group_List = new List<InviestigationGroupServices>();
              //  var ser_list = Extend.ToList<InvestigationService>(dt);
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    string inv_group_code1 = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_code"]);
                    model.Investigation_Group_List.Add(new InviestigationGroupServices
                    {
                        investigationgroup_code = inv_group_code1,
                        grouptype = Convert.ToString(ds.Tables[1].Rows[i]["grouptype"]),
                        investigationgroup_name = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_name"]),
                        Investigation_Service_List = lst_comission.FindAll(d => d.investigationgroup_code == inv_group_code1).ToList()
                    });

                }
                

            }

            //model.Investigation_Group_List = Extend.ToList<InviestigationGroupServices>(ds.Tables[1]);
            //model.Investigation_Service_List = Extend.ToList<InvestigationService>(ds.Tables[2]);
            return PartialView("_Agent_Inv_Comission_Services", model);
        }

        public ActionResult AddHosAgentDoctorComission(ComissionCategoryModel data)
        {
            DataTable dt = new DataTable(); int i = 0;
            foreach (var item in data.Investigation_Group_List)
            {
                if (item.Investigation_Service_List != null)
                {
                    if (i == 0)
                        dt = item.Investigation_Service_List.ToDataTable();
                    else
                        dt.Merge(item.Investigation_Service_List.ToDataTable());
                    i++;
                }
            }

           
            Session["lst_comission"] = dt.ToList<InvestigationService>();
            //ds = COM.InsertDoctorComissionServices(Convert.ToString(Session["ClinicCode"]), data.Id, dt, Convert.ToString(Session["UserCode"]));
            //if (ds.Tables.Count > 0)
            //{
               data.ResponseMsg = new AgentStatus();
               data.ResponseMsg.StatusId = "1";
            //}
            return PartialView("_Agent_Inv_Comission_Services", data);
        }


        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ServiceId", typeof(int));
            dt.Columns.Add("ServiceName", typeof(string));
            dt.Columns.Add("Comission", typeof(decimal));
            dt.Columns.Add("ComissionType", typeof(string));
            return dt;
        }

        public JsonResult ManageCategoryData(DataTable dt, string Status)
        {
            List<ComissionCategoryList> lst = Extend.ToList<ComissionCategoryList>(dt);
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Comission/_CategoryGrid.cshtml", lst, true);
            return Json(new { Status = Status, CategoryGrid = strhtml }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategorywiseComission(int CategoryId)
        {
            decimal comission = 0;
            string comtype = "";

            DataTable dt = new DataTable();
            dt = COM.GetCategorywiseComission(CategoryId, Convert.ToString(Session["ClinicCode"]));
            comission = Convert.ToDecimal(dt.Rows[0]["Total_Comission"]);
            comtype = Convert.ToString(dt.Rows[0]["comission_type"]);
            return Json(new { categoryid = comission, comissiontype = comtype }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCategory(int CategoryId)
        {
            string Status;
            DataTable dt = COM.DeleteComissionCategory(Convert.ToString(Session["UserCode"]), Convert.ToString(Session["ClinicCode"]), CategoryId, out Status);
            return ManageCategoryData(dt, Status);
        }

       

        public ActionResult DoctorComissionMaster()
        {
            DoctorComissionModel Model = new DoctorComissionModel();
            //Model.Investigation_Group_List = new List<InviestigationGroupServices>();
            //Model.Investigation_Service_List = new List<InvestigationService>();

            ds = COM.GetAllDoctorComissionCategory(Convert.ToString(Session["ClinicCode"]));
            Model.ALlComissionData = Extend.ToList<DoctorAgentList>(ds.Tables[0]);
            Model.ctrl = 1;
            return View("DoctorComissionMaster", Model);
        }

        [HttpPost]
        public ActionResult Add_Update_Doctor_Comission(DoctorComissionModel Data)
        {
            if (Data.Id == 0)
            {
                ds = COM.InsertDoctorCategory(Data.ComissionName, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = COM.UpdateDoctorCategory(Data.Id, Data.ComissionName, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new AgentStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.ALlComissionData = Extend.ToList<DoctorAgentList>(ds.Tables[1]);
                //Data.Investigation_Group_List = new List<InviestigationGroupServices>();
                //Data.Investigation_Group_List[0].Investigation_Service_List = new List<InvestigationService>();

            }
            return View("DoctorComissionMaster", Data);
        }

        public ActionResult Get_DoctorCategorywiseInvestigation(int Id)
        {
            DoctorComissionModel model = new DoctorComissionModel();
            ds = COM.GetDoctorCategorywiseInvestigation(Convert.ToString(Session["ClinicCode"]), Id);
            model.ComissionName = Convert.ToString(ds.Tables[0].Rows[0]["ComissionName"]);
            model.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);
            model.ALlComissionData = new List<DoctorAgentList>();
            model.Investigation_Group_List = new List<InviestigationGroupServices>();
            var ser_list = Extend.ToList<InvestigationService>(ds.Tables[2]);
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                string inv_group_code = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_code"]);
                model.Investigation_Group_List.Add(new InviestigationGroupServices
                {
                    investigationgroup_code = inv_group_code,
                    grouptype = Convert.ToString(ds.Tables[1].Rows[i]["grouptype"]),
                    investigationgroup_name = Convert.ToString(ds.Tables[1].Rows[i]["investigationgroup_name"]),
                    Investigation_Service_List = ser_list.FindAll(d => d.investigationgroup_code == inv_group_code).ToList()
                });

            }
            //model.Investigation_Group_List = Extend.ToList<InviestigationGroupServices>(ds.Tables[1]);
            //model.Investigation_Service_List = Extend.ToList<InvestigationService>(ds.Tables[2]);
            return PartialView("_DoctorComissionService", model);
        }

        public ActionResult AddHosDoctorComission(DoctorComissionModel data)
        {
            DataTable dt = new DataTable(); int i = 0;
            foreach (var item in data.Investigation_Group_List)
            {
                if (item.Investigation_Service_List != null)
                {
                    if (i == 0)
                        dt = item.Investigation_Service_List.ToDataTable();
                    else
                        dt.Merge(item.Investigation_Service_List.ToDataTable());
                    i++;
                }
            }

            ds = COM.InsertDoctorComissionServices(Convert.ToString(Session["ClinicCode"]), data.Id, dt, Convert.ToString(Session["UserCode"]));
            if (ds.Tables.Count > 0)
            {
                data.ResponseMsg = new AgentStatus();
                data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            return PartialView("_DoctorComissionService", data);
        }

        #region "Comission Agent Report"
        public ActionResult ComissionAgentReport()
        {
            AgentComissionReportModel model = new AgentComissionReportModel();
            DataSet ds = new DataSet();
            ds = COM.GetAgentComissionData(Convert.ToString(Session["ClinicCode"]), "", "", "0");
            model.comission_agent_list = Extend.ToList<ComissionAgent>(ds.Tables[0]);
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
            model.agent_comission_data = new List<AgentComissionData>();
            model.agent_comission_service = new List<AgentComissionService>();
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            return View(model);
        }

        public ActionResult GetComissionAgentReportfilter(AgentComissionReportModel RM)
        {
            AgentComissionReportModel model = new AgentComissionReportModel();
            DataSet ds = new DataSet();
            ds = COM.GetAgentComissionData(Convert.ToString(Session["ClinicCode"]), RM.FromDate, RM.ToDate, RM.comission_agentid);
            //model.comission_agent_list = Extend.ToList<ComissionAgent>(ds.Tables[0]);
            model.agent_comission_data = Extend.ToList<AgentComissionData>(ds.Tables[2]);
            model.agent_comission_service = Extend.ToList<AgentComissionService>(ds.Tables[3]);
            return PartialView("ComissionAgentReport", model);
            // return PartialView("SummaryReport", model);
        }

        public PartialViewResult getPaymentTypeChannel_agentcomission(int PaymentTypeId)
        {
            AgentComissionReportModel model = new AgentComissionReportModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.agent_comission_data = new List<AgentComissionData>();
            model.agent_comission_service = new List<AgentComissionService>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("ComissionAgentReport", model);
        }

        public PartialViewResult SaveAgentComission(AgentComissionReportModel D)
        {
            int val;
            AgentComissionReportModel Data = new AgentComissionReportModel();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(D.invoiceId_lst);

            if (dt.Rows.Count > 0)
            {
                var ds = COM.Save_Agent_ComissionData(Convert.ToString(Session["ClinicCode"]), D.FromDate, D.ToDate, Convert.ToString(Session["UserCode"]), dt, D.PaymentTypeId, D.ChannelId, D.comission_agentid, out val);
                Data.Status = val > 0 ? "sucess" : "fail";
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
                Data.agent_comission_data = Extend.ToList<AgentComissionData>(ds.Tables[2]);
                Data.agent_comission_service = Extend.ToList<AgentComissionService>(ds.Tables[3]);
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();

            }
            else
            {
                Data.Status = "no datas elected";
                Data.agent_comission_data = new List<AgentComissionData>();
                Data.agent_comission_service = new List<AgentComissionService>();
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            }
            return PartialView("ComissionAgentReport", Data);
        }

        #endregion

        #region "Comission Doctor Report"


        public ActionResult ComissionDoctorReport()
        {
            DoctorComissionReportModal modal = new DoctorComissionReportModal();
            DataSet ds = new DataSet();
            ds = COM.GetDoctorComissionData(Convert.ToString(Session["ClinicCode"]), "", "", "");
            modal.comission_doctor_list = Extend.ToList<ComissionDoctor>(ds.Tables[0]);
            modal.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
            modal.doctor_comission_data = new List<DoctorComissionData>();
            modal.doctor_comission_service = new List<DoctorComissionServices>();
            modal.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            return View(modal);
        }

        public ActionResult GetComissiondocotrReportfilter(DoctorComissionReportModal RM)
        {
            DoctorComissionReportModal model = new DoctorComissionReportModal();
            DataSet ds = new DataSet();
            ds = COM.GetDoctorComissionData(Convert.ToString(Session["ClinicCode"]), RM.FromDate, RM.ToDate, RM.doctor_code);
            model.doctor_comission_data = Extend.ToList<DoctorComissionData>(ds.Tables[2]);
            model.doctor_comission_monthwise = Extend.ToList<DoctorComissionMonthwise>(ds.Tables[3]);
            model.doctor_comission_service = Extend.ToList<DoctorComissionServices>(ds.Tables[4]);
           
            return PartialView("ComissionDoctorReport", model);
            // return PartialView("SummaryReport", model);
        }

        public PartialViewResult getPaymentTypeChannel_Payable(int PaymentTypeId)
        {
            DoctorComissionReportModal model = new DoctorComissionReportModal();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.comission_doctor_list = new List<ComissionDoctor>();
            model.doctor_comission_data = new List<DoctorComissionData>();
            model.doctor_comission_monthwise = new List<DoctorComissionMonthwise>();
            model.doctor_comission_service = new List<DoctorComissionServices>();
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("ComissionDoctorReport", model);
        }

        public PartialViewResult SaveDoctorComission(DoctorComissionReportModal D)
        {
            int val;
            DoctorComissionReportModal Data = new DoctorComissionReportModal();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(D.invoiceId_lst);

            if (dt.Rows.Count > 0)
            {
                var ds = COM.Save_ComissionData(Convert.ToString(Session["ClinicCode"]), D.FromDate, D.ToDate, Convert.ToString(Session["UserCode"]), D.PaymentTypeId, dt, D.PaymentTypeId,D.ChannelId,D.doctor_code, out val);
                Data.Status = val > 0 ? "sucess" : "fail";
                Data.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[1]);
                Data.doctor_comission_data = Extend.ToList<DoctorComissionData>(ds.Tables[2]);
                Data.doctor_comission_monthwise = Extend.ToList<DoctorComissionMonthwise>(ds.Tables[3]);
                Data.doctor_comission_service = Extend.ToList<DoctorComissionServices>(ds.Tables[4]);
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();

            }
            else {
                Data.Status = "no datas elected";
                Data.doctor_comission_data = new List<DoctorComissionData>();
                Data.doctor_comission_monthwise = new List<DoctorComissionMonthwise>();
                Data.doctor_comission_service = new List<DoctorComissionServices>();
                Data.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            }
            return PartialView("ComissionDoctorReport", Data);
        }

        #endregion

        #region "Comission Payment Slip"
        public ActionResult ComissionPaymentSlip()
        {
            ComissionPaymentSlipModel Modal = GetData("agentwise");
            return View(Modal);
        }
        public PartialViewResult OnChangeEvent(string Type)
        {
            ComissionPaymentSlipModel Modal = GetData(Type);
            return PartialView("ComissionPaymentSlip", Modal);
        }
        public ComissionPaymentSlipModel GetData(string Type)
        {
            ComissionPaymentSlipModel Modal = new ComissionPaymentSlipModel();
            Modal.DropDownData = new List<Drpdown_Data>();
            Modal.ComissionPaySlipLists = new List<Com_PaySlipList>();
            DataTable dt = COM.ComissionPaymentSlipPageLoad(Convert.ToString(Session["ClinicCode"]), Type);
            Modal.DropDownData = Extend.ToList<Drpdown_Data>(dt);
            return Modal;
        }
        public PartialViewResult GetComissionPaymentSlip(ComissionPaymentSlipModel m)
        {
            ComissionPaymentSlipModel Modal = new ComissionPaymentSlipModel();
            Modal.DropDownData = new List<Drpdown_Data>();
            Modal.ComissionPaySlipLists = new List<Com_PaySlipList>();
            DataTable dt = COM.GetComissionPaymentSlip(m.ActType, Convert.ToString(Session["ClinicCode"]),
                m.Type, m.FromDate, m.ToDate, m.ActType == "agentwise" ? Convert.ToInt32(m.Id) : 0,
                m.ActType == "agentwise" ? "" : m.Id);
            Modal.ComissionPaySlipLists = Extend.ToList<Com_PaySlipList>(dt);
            return PartialView("ComissionPaymentSlip", Modal);
        }
        #endregion
    }
}