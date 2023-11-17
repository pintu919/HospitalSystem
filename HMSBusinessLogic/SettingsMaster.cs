using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{


    public sealed class Settings_Master : BLayer
    {
        public Settings_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        #region "Expense Center"
        public DataSet AddExpense( int id, string ItemName, string PurchaseDate, string PurchasedBy, string Paidby, decimal Amount, int Status, string hoscode, string usercode)
        {
            var param = new SqlParameter[12];
            param[0] = new SqlParameter("@action", "addexpense");
            param[1] = new SqlParameter("@expenseid", id);
            param[2] = new SqlParameter("@itemname", ItemName);
            param[3] = new SqlParameter("@purchasedate", PurchaseDate);
            param[4] = new SqlParameter("@purchasedby", PurchasedBy);
            param[5] = new SqlParameter("@paidby", Paidby);
            param[6] = new SqlParameter("@amount", Amount);
            param[7] = new SqlParameter("@status", Status);
            param[8] = new SqlParameter("@hoscode", hoscode);
            param[9] = new SqlParameter("@usercode", usercode);
            param[10] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[11].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("hos_account_expense_master", param);
            return rs;
        }
        public DataSet GetExpenseData(string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getexpense");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_account_expense_master", param);
            return rs;
        }

        #endregion

        #region "Profit Center"
        public DataSet Addprofitcenter(int id, string centerName, int Status, string hoscode, string usercode)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "addprofitcenter");
            param[1] = new SqlParameter("@centerid", id);
            param[2] = new SqlParameter("@centername", centerName);
            param[3] = new SqlParameter("@status", Status);
            param[4] = new SqlParameter("@hoscode", hoscode);
            param[5] = new SqlParameter("@usercode", usercode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("hos_account_profitcenter_master", param);
            return rs;
        }

        public DataSet Getprofitcenter(string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getprofitcenter");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_account_profitcenter_master", param);
            return rs;
        }
        #endregion

        #region MappedExpenseAndProfit
        public DataSet GetExpenseandProfit(string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getexpenseandprofit");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_account_mapped_expenseAndprofit", param);
            return rs;
        }

        public DataTable GetAllProfitCenterData(string HosCode, int expid)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getprofitmapped");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@centerid", expid);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_account_mapped_expenseAndprofit", param);
            return rs.Tables[0];
        }

        public bool MappedExpandProfit(DataTable dt, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_account_mapped_expenseAndprofit", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }

        //public bool CheckPercentage(int profitcenterid, int expid, decimal percent, string hoscode)
        //{
        //    var param = new SqlParameter[7];
        //    param[0] = new SqlParameter("@action", "checkpercent");
        //    param[1] = new SqlParameter("@centerid", profitcenterid);
        //    param[2] = new SqlParameter("@exp_id", expid);
        //    param[3] = new SqlParameter("@percent", percent);
        //    param[4] = new SqlParameter("@hoscode", hoscode);
        //    param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[5].Direction = ParameterDirection.Output;
        //    param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
        //    param[6].Direction = ParameterDirection.ReturnValue;
        //    ApplyChanges("hos_account_mapped_expenseAndprofit", param.ToArray(), Actions.Add);
        //    return param[6].Value.ToInt() > 0 ? true : false;

        //}

        #endregion

        #region MappedProfitAndIncome
        public DataTable GetServiceMappedUnmapped(string HosCode, int profitcenterid, string Type)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "getprofitmappedincome");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@centerid", profitcenterid);
            param[3] = new SqlParameter("@type", Type);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_account_mapped_ProfitAndIncome", param);
            return rs.Tables[0];
        }

        public bool MappedIncomeAndService(DataTable dt, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_account_mapped_ProfitAndIncome", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }
        #endregion

        #region "Mapped Employee With Expense"
        public DataTable GetAllMappEmployee(string HosCode, int expid)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getemppmapped");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@expenseid", expid);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_employee_mapp_expense", param);
            return rs.Tables[0];
        }
        public int EmployeeMappedExpand(DataTable dt, string hoscode, string usercode, int expid)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@empdt", dt);
            param[2] = new SqlParameter("@expenseid", expid);
            param[3] = new SqlParameter("@hoscode", hoscode);
            param[4] = new SqlParameter("@usercode", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_employee_mapp_expense", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt();
        }
        #endregion

        public DataSet Getexpensewiseprofit( int expense_center_id, string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getexpwiseprofit");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@expense_center_id", expense_center_id);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_ExpenseWise_Profit", param);
            return rs;
        }

    }
}
