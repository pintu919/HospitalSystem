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
    public class SettingsController : _Base
    {
        // GET: Settings

        readonly Settings_Master Se_Master;

        public SettingsController()
        {
            Se_Master = new Settings_Master(bsInfo);

        }
        #region "Expense Center"
        public ActionResult ExpenseCenter()
        {
            Expense model = new Expense();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = Se_Master.GetExpenseData(hoscode);
            model.explst = Extend.ToList<Expensedata>(ds.Tables[0]);
            model.status = 1;
            return View("ExpenseCenter", model);
        }
        public ActionResult AddExpense(Expensedata Ex)
        {
            Expense model = new Expense();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = Se_Master.AddExpense(Ex.id, Ex.item_name, Ex.purchase_date, Ex.purchased_by, Ex.paid_by, Convert.ToDecimal(Ex.amount), Ex.status, hoscode, usercode);
            model.explst = Extend.ToList<Expensedata>(ds.Tables[0]);
            model.msg = Convert.ToInt32(ds.Tables[1].Rows[0]["msg"]);
            return PartialView("ExpenseCenter", model);
        }
        #endregion
        #region "Profit Center"
        public ActionResult ProfitCenter()
        {
            ProfitCenter model = new ProfitCenter();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = Se_Master.Getprofitcenter(hoscode);
            model.profitcenterlst = Extend.ToList<ProfitCenterlist>(ds.Tables[0]);
            model.status = 1;
            return View("ProfitCenter", model);
        }
        public ActionResult AddProfitCenter(ProfitCenter PC)
        {
            ProfitCenter model = new ProfitCenter();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = Se_Master.Addprofitcenter(PC.id, PC.center_name, PC.status, hoscode, usercode);
            model.profitcenterlst = Extend.ToList<ProfitCenterlist>(ds.Tables[0]);
            model.msg = Convert.ToInt32(ds.Tables[1].Rows[0]["msg"]);
            return View("ProfitCenter", model);
        }
        #endregion
        #region "Mapping Expence and Profit Center"
        public ActionResult MapedExpenseAndProfit()
        {
            MappedExpenseAndProfit model = new MappedExpenseAndProfit();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = Se_Master.GetExpenseandProfit(hoscode);
            model.explst = Extend.ToList<Expensedata>(ds.Tables[0]);
            return View("MapedExpenseAndProfit", model);
        }

        [HttpPost]
        public JsonResult CheckPercent(MappedExpenseAndProfit MP)
        {

            //return Json(ISValidPercent(MP.profitcenterlst));

            return Json(true);

        }
        public bool ISValidPercent(decimal profitPercent)
        {



            bool status = true;
            //if (id != 0)
            //{
            //    //Already registered
            //    status = false;
            //}
            //else
            //{
            //    //Available to use
            //    status = true;
            //}


            return status;
        }

        public JsonResult GetProfitCenterData(int Expid)
        {
            MappedExpenseAndProfit model = new MappedExpenseAndProfit();
            model.explst = new List<Expensedata>();
            model.profitcenterlst = new List<ProfitCenterlist>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataTable dt = Se_Master.GetAllProfitCenterData(hoscode, Expid);
            DataRow[] dr = dt.Select();
            foreach (var item in dr)
            {
                var list = new ProfitCenterlist
                {
                    center_name = Convert.ToString(item[0]),
                    id = Convert.ToInt32(item[1]),
                    IsSelected = Convert.ToBoolean(item[2]),
                    profitPercent = Convert.ToDecimal(item[3])
                };
                model.profitcenterlst.Add(list);
            }
            model.expenseid = Expid;
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Settings/_MappedProfitAndExpense.cshtml", model, true);
            return Json(new { GroupGrid = strhtml }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MappedExp(MappedExpenseAndProfit model)
        {
            string status = "";
            if (IsCheckPercentage(model.profitcenterlst.Where(a => a.IsSelected == true).ToList()))
            {
                DataTable dt = CreateTable();
                var lst = model.profitcenterlst.Where(a => a.IsSelected == true).ToList();
                if (lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        DataRow dr = dt.NewRow();
                        dr["exp_id"] = Convert.ToInt32(model.expenseid);
                        dr["profit_id"] = Convert.ToInt32(item.id);
                        dr["profit_percent"] = Convert.ToDecimal(item.profitPercent);
                        dr["ctrl"] = Convert.ToInt32(1);
                        dt.Rows.Add(dr);
                    }
                    var hos_code = Convert.ToString(Session["ClinicCode"]);
                    var user_code = Convert.ToString(Session["UserCode"]);
                    bool add = Se_Master.MappedExpandProfit(dt, hos_code, user_code); //IPM.AddInvestigationPrice(dt, hos_code, user_code);
                    if (add)
                    {
                        status = "Record Added Successfully!";
                    }
                    else
                        status = "Record Not Saved!";
                }
                else
                    status = "Select atleast one record";
            }
            else
                status = "You must be set overall expense percentage 100%.";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public bool IsCheckPercentage(List<ProfitCenterlist> lst)
        {
            decimal percantage = 0;
            foreach (var d in lst)
            {
                percantage += d.profitPercent;
            }
            if (Convert.ToDecimal(percantage) == Convert.ToDecimal(100))
                return true;
            else
                return false;
        }
        //public ActionResult CheckPercentage(int profitcenterid, int expid, decimal percent)
        //{
        //    var hos_code = Convert.ToString(Session["ClinicCode"]);
        //    bool add = Se_Master.CheckPercentage(profitcenterid, expid, percent, hos_code);
        //    return Json(add, JsonRequestBehavior.AllowGet);
        //}

        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("exp_id", typeof(int));
            dt.Columns.Add("profit_id", typeof(int));
            dt.Columns.Add("profit_percent", typeof(decimal));
            dt.Columns.Add("ctrl", typeof(int));
            return dt;
        }

        #endregion
        #region "Income Mappoing"
        public ActionResult GetProfitCenter()
        {
            MappedExpenseAndProfit model = new MappedExpenseAndProfit();
            model.explst = new List<Expensedata>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = Se_Master.Getprofitcenter(hoscode);
            model.profitcenterlst = Extend.ToList<ProfitCenterlist>(ds.Tables[0]);
            return View("MapedExpenseAndProfit", model);
        }

        public JsonResult GetServiceMappedUnMappedData(int profitcenterid, string Type)
        {
            MappedExpenseAndProfit model = new MappedExpenseAndProfit();
            model.explst = new List<Expensedata>();
            model.profitcenterlst = new List<ProfitCenterlist>();
            model.servicelst = new List<ServiceListdata>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataTable dt = Se_Master.GetServiceMappedUnmapped(hoscode, profitcenterid, Type);
            DataRow[] dr = dt.Select();
            foreach (var item in dr)
            {
                var list = new ServiceListdata
                {
                    Id = Convert.ToInt32(item[0]),
                    Name = Convert.ToString(item[1]),
                    IsCheck = Convert.ToBoolean(item[2]),
                    Type = Convert.ToString(item[3]),


                };
                model.servicelst.Add(list);
            }
            model.profitcenterid = profitcenterid;
            model.Type = Type;
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Settings/_MappedProfitAndIncome.cshtml", model, true);
            return Json(new { GroupGrid = strhtml }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MappedIncome(MappedExpenseAndProfit model)
        {
            string status = "";
            DataTable dt = CreateTableIncome();
            var lst = model.servicelst.Where(a => a.IsCheck == true).ToList();
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr["profitcenter_id"] = Convert.ToInt32(model.profitcenterid);
                    dr["service_id"] = Convert.ToInt32(item.Id);
                    dr["type"] = Convert.ToString(item.Type);
                    dr["ctrl"] = Convert.ToInt32(1);
                    dt.Rows.Add(dr);
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                bool add = Se_Master.MappedIncomeAndService(dt, hos_code, user_code); //IPM.AddInvestigationPrice(dt, hos_code, user_code);
                if (add)
                {
                    status = "Record Added Successfully!";
                }
                else
                    status = "Record Not Saved!";
            }
            else
                status = "Select atleast one record";

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTableIncome()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("profitcenter_id", typeof(int));
            dt.Columns.Add("service_id", typeof(int));
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            return dt;
        }

        #endregion
        #region Employee Mapping with Expense for Salary
        public JsonResult Get_MappEmpList(int Expid)
        {
            MappedExpenseAndProfit model = new MappedExpenseAndProfit();
            model.explst = new List<Expensedata>();
            model.emp_mapp_lists = new List<Mapp_EmployeeLists>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataTable dt = Se_Master.GetAllMappEmployee(hoscode, Expid);
            DataRow[] dr = dt.Select();
            foreach (var item in dr)
            {
                var list = new Mapp_EmployeeLists
                {
                    employee_code = Convert.ToString(item[0]),
                    IsSelected = Convert.ToBoolean(item[1]),
                    EmployeeName = Convert.ToString(item[2])
                };
                model.emp_mapp_lists.Add(list);
            }
            model.expenseid = Expid;
            var strhtml = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Settings/_MappedEmployee.cshtml", model, true);
            return Json(new { GroupGrid = strhtml }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MappedEmployeeExp(MappedExpenseAndProfit model)
        {
            string status = "";
            DataTable dt = CreateEmpTable();
            var lst = model.emp_mapp_lists.Where(a => a.IsSelected == true).ToList();
            foreach (var item in lst)
            {
                DataRow dr = dt.NewRow();
                dr["exp_id"] = Convert.ToInt32(model.expenseid);
                dr["employee_code"] = Convert.ToString(item.employee_code);
                dt.Rows.Add(dr);
            }
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var user_code = Convert.ToString(Session["UserCode"]);
            int add = Se_Master.EmployeeMappedExpand(dt, hos_code, user_code, model.expenseid);
            if (add > 0)
            {
                if (add == 1)
                    status = "Record Added Successfully!";
                else
                    status = "Record Successfully UnMapped!";
            }
            else
                status = "Record Not Saved!";

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateEmpTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("exp_id", typeof(int));
            dt.Columns.Add("employee_code", typeof(string));
            return dt;
        }
        #endregion

        #region "ExpenseWise_Profit"
        public ActionResult ExpenseWiseProfit()
        {
            ExpenseWiseProfitData model = new ExpenseWiseProfitData();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = Se_Master.GetExpenseandProfit(hoscode);
            model.exp_wise_profit = new List<Expense_Wise_Profit>();
            model.explst = Extend.ToList<Expensedata>(ds.Tables[0]);
            return View("ExpenseWiseProfit", model);
        }

        public ActionResult GetExpensewiseProfit(int Expid)
        {
            ExpenseWiseProfitData model = new ExpenseWiseProfitData();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            DataSet ds = Se_Master.Getexpensewiseprofit(Expid, hoscode);
            model.explst = new List<Expensedata>();
            model.exp_wise_profit = Extend.ToList<Expense_Wise_Profit>(ds.Tables[0]);
            return View("ExpenseWiseProfit", model);
        }
        #endregion



    }
}