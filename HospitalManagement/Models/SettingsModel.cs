using HMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{

    public class Expense
    {
        [Required(ErrorMessage = "*")]
        public string item_name { get; set; }
        public string purchase_date { get; set; }
        public string purchased_by { get; set; }
        public decimal? amount { get; set; }
        public string paid_by { get; set; }
        public int status { get; set; }
        public List<Expensedata> explst { get; set; }
        public int msg { get; set; }
        public int id { get; set; }
    }
    public class ProfitCenter
    {
        public int id { get; set; }

        [Required(ErrorMessage = "*")]
        public string center_name { get; set; }
        public int status { get; set; }
        public int msg { get; set; }
        public List<ProfitCenterlist> profitcenterlst { get; set; }
    }
    public class ProfitCenterlist
    {
        [SqlKey(Display = true)]
        public int id { get; set; }
        [SqlKey(Display = true)]
        public string center_name { get; set; }

        [SqlKey(Display = true)]
        public int status { get; set; }

        [SqlKey(Display = true)]
        public int msg { get; set; }

        [SqlKey(Display = true)]
        public bool IsSelected { get; set; }

        [SqlKey(Display = true)]
        public decimal profitPercent { get; set; }
    }

    public class Expensedata
    {
        [SqlKey(Display = true)]
        public int id { get; set; }

        [SqlKey(Display = true)]
        public string item_name { get; set; }

        [SqlKey(Display = true)]
        public string purchase_date { get; set; }

        [SqlKey(Display = true)]
        public string purchased_by { get; set; }

        [SqlKey(Display = true)]
        public decimal amount { get; set; }

        [SqlKey(Display = true)]
        public string paid_by { get; set; }

        [SqlKey(Display = true)]
        public int status { get; set; }
    }

    public class MappedExpenseAndProfit
    {
        public int expenseid { get; set; }
        public int profitcenterid { get; set; }
        public string Type { get; set; }
        //public bool IsAction { get; set; }
        public List<Expensedata> explst { get; set; }
        public List<ProfitCenterlist> profitcenterlst { get; set; }
        public List<ServiceListdata> servicelst { get; set; }
        public List<Mapp_EmployeeLists> emp_mapp_lists { get; set; }

    }

    public class ServiceListdata
    {
        [SqlKey(Display = true)]
        public int Id { get; set; }

        [SqlKey(Display = true)]
        public string Name { get; set; }

        [SqlKey(Display = true)]
        public bool IsCheck { get; set; }

        [SqlKey(Display = true)]
        public string Type { get; set; }
    }

    public class Mapp_EmployeeLists
    {
        [SqlKey(Display = true)]
        public string employee_code { get; set; }
        [SqlKey(Display = true)]
        public string EmployeeName { get; set; }
        [SqlKey(Display = true)]
        public bool IsSelected { get; set; }
    }


    public class ExpenseWiseProfitData
    {
        public int expenseid { get; set; }
        public List<Expense_Wise_Profit> exp_wise_profit { get; set; }
        public List<Expensedata> explst { get; set; }
    }


    public class Expense_Wise_Profit
    {
        [SqlKey(Display = true)]
        public decimal Profit { get; set; }

        [SqlKey(Display = true)]
        public int profit_center_id { get; set; }

        [SqlKey(Display = true)]
        public string center_name { get; set; }

        [SqlKey(Display = true)]
        public decimal profit_percent { get; set; }

        [SqlKey(Display = true)]
        public decimal total_profit { get; set; }


    }
}