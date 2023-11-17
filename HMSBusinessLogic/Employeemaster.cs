using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class Employee_Entity
    {
        [SqlKey(Name = "@employee_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 500)]
        public string employee_code { get; set; }

        [SqlKey(Name = "@employee_firstname")]
        public string employee_firstname { get; set; }

        [SqlKey(Name = "@employee_lastname")]
        public string employee_lastname { get; set; }

        [SqlKey(Name = "@age")]
        public int age { get; set; }

        [SqlKey(Name = "@phone")]
        public string phone { get; set; }

        [SqlKey(Name = "@email")]
        public string email { get; set; }

        [SqlKey(Name = "@username")]
        public string username { get; set; }

        [SqlKey(Name = "@password")]
        public string password { get; set; }

        [SqlKey(Name = "@confirmpass")]
        public string confirmpass { get; set; }

        [SqlKey(Name = "@country_code")]
        public string country_code { get; set; }

        [SqlKey(Name = "@city_code")]
        public string city_code { get; set; }

        [SqlKey(Name = "@zip")]
        public string zip { get; set; }

        [SqlKey(Name = "@present_address")]
        public string present_address { get; set; }

        [SqlKey(Name = "@dist_code")]
        public string dist_code { get; set; }

        [SqlKey(Name = "@state_code")]
        public string state_code { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@dob")]
        public string dob { get; set; }

        [SqlKey(Name = "@marital_status")]
        public string marital_status { get; set; }

        [SqlKey(Name = "@joinig_date")]
        public string joinig_date { get; set; }

        [SqlKey(Name = "@roleid")]
        public int roleid { get; set; }

        [SqlKey(Name = "@doctor_code")]
        public string doctor_code { get; set; }

        [SqlKey(Name = "@hosde_code")]
        public string hosde_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@employee_type")]
        public int employee_type { get; set; }

        [SqlKey(Name = "@photo")]
        public string photo { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@employee_id")]
        public int employee_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string rolename { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@designation")]
        public string designation { get; set; }

    }

    public sealed class EmployeeTiming_Entity
    {

        [SqlKey(Name = "@dtEmployeetime", SqlType = SqlDbType.Structured)]
        public DataTable Employeetimetable { get; set; }

        [SqlKey(Name = "@employee_code")]
        public string employee_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }



    }
    //public sealed class EmployeeTiming_Entity
    //{

    //    [SqlKey(Name = "@emp_strattime")]
    //    public string emp_strattime { get; set; }

    //    [SqlKey(Name = "@emp_endtime")]
    //    public string emp_endtime { get; set; }

    //    [SqlKey(Name = "@emp_code")]
    //    public string emp_code { get; set; }

    //    [SqlKey(Name = "@ctrl")]
    //    public int Ctrl { get; set; }

    //    [SqlKey(Name = "@sunday")]
    //    public bool Sunday { get; set; }

    //    [SqlKey(Name = "@monday")]
    //    public bool Monday { get; set; }

    //    [SqlKey(Name = "@tuesday")]
    //    public bool Tuesday { get; set; }

    //    [SqlKey(Name = "@wednesday")]
    //    public bool Wednesday { get; set; }

    //    [SqlKey(Name = "@thursday")]
    //    public bool Thursday { get; set; }

    //    [SqlKey(Name = "@friday")]
    //    public bool Friday { get; set; }

    //    [SqlKey(Name = "@saturday")]
    //    public bool Saturday { get; set; }

    //    [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
    //    private string Return { get; set; }

    //    [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
    //    private string Error { get; set; }

    //    [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
    //    public string uniqrow { get; set; }

    //    [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
    //    public Int64 emp_timingid { get; set; }
    //}

    public sealed class EmployeeSalary_Entity
    {
        [SqlKey(Name = "@emp_netsalary")]
        public decimal emp_netsalary { get; set; }

        [SqlKey(Name = "@emp_basicsalary")]
        public decimal emp_basicsalary { get; set; }

        [SqlKey(Name = "@emp_tds")]
        public decimal emp_tds { get; set; }

        [SqlKey(Name = "@emp_da")]
        public string emp_da { get; set; }

        [SqlKey(Name = "@emp_esi")]
        public decimal emp_esi { get; set; }

        [SqlKey(Name = "@emp_hra")]
        public string emp_hra { get; set; }

        [SqlKey(Name = "@emp_pf")]
        public decimal emp_pf { get; set; }

        [SqlKey(Name = "@emp_Conveyance")]
        public decimal emp_Conveyance { get; set; }

        [SqlKey(Name = "@emp_allowance")]
        public decimal emp_allowance { get; set; }

        [SqlKey(Name = "@emp_prof_tax")]
        public decimal emp_prof_tax { get; set; }

        [SqlKey(Name = "@emp_medical_allowance")]
        public decimal emp_medical_allowance { get; set; }

        [SqlKey(Name = "@emp_labour_Welfare")]
        public decimal emp_labour_Welfare { get; set; }

        [SqlKey(Name = "@emp_fund")]
        public decimal emp_fund { get; set; }

        [SqlKey(Name = "@emp_other")]
        public string emp_other { get; set; }

        [SqlKey(Name = "@emp_code")]
        public string emp_code { get; set; }
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int emp_salaryid { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }



    }

    public sealed class employee_Master : BLayer
    {
        public employee_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        #region"Employee"
        public string Add(Employee_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Employee", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException e) { return "0"; }
        }
        public bool Update(Employee_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_Employee", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public bool Delete(int EmployeeId, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@employee_id", EmployeeId);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Employee", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet GetAll(string Hoscode = "")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Employee", param);
            return rs;
        }

        public DataSet GetAllFilter(string Hoscode = "", string Employeecode = "", string Employeename = "", int RoleId = 0)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "recordfilter");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@employee_code", Employeecode);
            param[3] = new SqlParameter("@employee_firstname", Employeename);
            param[4] = new SqlParameter("@roleid", RoleId);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Employee", param);
            return rs;
        }

        public DataSet GetByCode(string EMPcode, string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@employee_code", EMPcode);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Employee", param);
            return rs;
        }



        public DataSet ExistingDoctor(string searchpara, string depcode, int RoleId)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "search");
            param[1] = new SqlParameter("@doctor_code", searchpara);
            param[2] = new SqlParameter("@hosde_code", depcode);
            param[3] = new SqlParameter("@roleId", RoleId);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Employee", param);
            return rs;
        }

        public string GetRolName(int RoleId)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getrolname");
            param[1] = new SqlParameter("@roleid", RoleId);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Employee", param);
            return Convert.ToString(rs.Tables[0].Rows[0]["RoleName"]);
        }

        public bool ResetEmployeePassword(Employee_Entity entity)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "resetpass");
            param[1] = new SqlParameter("@employee_code", entity.employee_code);
            param[2] = new SqlParameter("@password", entity.password);
            param[3] = new SqlParameter("@confirmpass", entity.confirmpass);
            param[4] = new SqlParameter("@user_code", entity.user_code);
            param[5] = new SqlParameter("@hos_code", entity.hos_code);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            try
            {
                ApplyChanges("Hos_Employee", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param[7].Value.ToInt() > 0) ? true : false;
        }

        public Email_Entity getforgetpass(string empcode, string password)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getforgetpass");
            param[1] = new SqlParameter("@employee_code", empcode);
            param[2] = new SqlParameter("@password", password);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Email_Entity>("Hos_Employee", param.ToArray()).SingleOrDefault();
            return rs;

        }
        public Email_Entity GetEmployeeEmailDetails(int EmployeeId, string password)
        {

            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getmaildetail");
            param[1] = new SqlParameter("@employee_id", EmployeeId);
            param[2] = new SqlParameter("@password", password);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Email_Entity>("Hos_Employee", param.ToArray()).SingleOrDefault();
            return rs;
        }

        #endregion

        #region"Employee Timing"
        public DataSet AddEmpTiming(EmployeeTiming_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = SP_ResultSet("Hos_EmployeeTiming_v1", param.ToArray());

                return rs;
            }
            catch (DException e) { return new DataSet(); }
        }


        //public bool UpdateTiming(EmployeeTiming_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "update"));
        //    try
        //    {
        //        ApplyChanges("Hos_EmployeeTiming", param.ToArray(), Actions.Update);
        //    }
        //    catch (DException) { return false; }
        //    return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        //}
        //public string DeleteTiming(string EMPcode)
        //{
        //    var param = new SqlParameter[5];
        //    param[0] = new SqlParameter("@action", "delete");
        //    param[1] = new SqlParameter("@emp_code", EMPcode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
        //    param[3].Direction = ParameterDirection.Output;
        //    param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
        //    param[4].Direction = ParameterDirection.ReturnValue;
        //    try
        //    {
        //        var rs = ApplyChanges("Hos_EmployeeTiming", param.ToArray(), Actions.Delete);
        //        return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
        //    }
        //    catch (DException) { return "0"; }
        //}
        public DataSet GetAllTiming(string Hoscode = "", string Emp_Code = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@employee_code", Emp_Code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_EmployeeTiming_v1", param);
            return rs;
        }
        //public DataSet GetByCodeTiming(string EMPcode)
        //{
        //    var param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@action", "get");
        //    param[1] = new SqlParameter("@emp_code", EMPcode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    var rs = SP_ResultSet("Hos_EmployeeTiming", param);
        //    return rs;
        //}


        #endregion


        #region"Employee Salary"
        public bool AddEmpSalary(EmployeeSalary_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_EmployeeSalary", param.ToArray(), Actions.Add);
                var result = (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
                return result;
            }
            catch (DException e) { return false; }
        }
        public bool UpdateEmpSlary(EmployeeSalary_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_EmployeeSalary", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public EmployeeSalary_Entity GetAllEmpSlary(string Hoscode = "", string Emp_Code = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@emp_code", Emp_Code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<EmployeeSalary_Entity>("Hos_EmployeeSalary", param).SingleOrDefault();
            return rs;
        }

        public EmployeeSalary_Entity GetAllEmpSlaryCalculate(string HosCode, string Emp_Code, decimal Amount)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "getsalarycalculate");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@emp_code", Emp_Code);
            param[3] = new SqlParameter("@emp_netsalary", Amount);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_Read<EmployeeSalary_Entity>("Hos_EmployeeSalary", param).SingleOrDefault(); ;
            return rs;
        }


        #endregion



    }
}
