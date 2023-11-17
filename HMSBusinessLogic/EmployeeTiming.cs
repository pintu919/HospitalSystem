using System;
using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class EmployeeTiming_Entity1
    {

        [SqlKey(Name = "@emp_avialdays")]
        public string emp_avialdays { get; set; }

        [SqlKey(Name = "@emp_strattime")]
        public string emp_strattime { get; set; }

        [SqlKey(Name = "@emp_endtime")]
        public string emp_endtime { get; set; }

        [SqlKey(Name = "@emp_code")]
        public string emp_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int emp_timingid { get; set; }
    }

        public sealed class employee_Timing : BLayer
        {
            public employee_Timing(BsInfo info, DConnection cn = null) : base(info, cn) { }

            public string Add(EmployeeTiming_Entity entity)
            {
                var param = ToSqlParams(entity);
                param.Add(new SqlParameter("@action", "insert"));
                try
                {
                    var rs = ApplyChanges("Hos_EmployeeTiming", param.ToArray(), Actions.Add);
                    var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                    return result;
                }
                catch (DException e) { return "0"; }
            }
            public bool Update(EmployeeTiming_Entity entity)
            {
                var param = ToSqlParams(entity);
                param.Add(new SqlParameter("@action", "update"));
                try
                {
                    ApplyChanges("Hos_EmployeeTiming", param.ToArray(), Actions.Update);
                }
                catch (DException) { return false; }
                return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
            }
            public string Delete1(string EMPcode)
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", "delete");
                param[1] = new SqlParameter("@emp_code", EMPcode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
                param[4].Direction = ParameterDirection.ReturnValue;
                try
                {
                    var rs = ApplyChanges("Hos_EmployeeTiming", param.ToArray(), Actions.Delete);
                    return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
                }
                catch (DException) { return "0"; }
            }
            public List<EmployeeTiming_Entity> GetAll(string Hoscode = "")
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", Hoscode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                var rs = SP_Read<EmployeeTiming_Entity>("Hos_EmployeeTiming", param);
                return rs;
            }
            public DataSet GetByCode(string EMPcode)
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "get");
                param[1] = new SqlParameter("@emp_code", EMPcode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                var rs = SP_ResultSet("Hos_EmployeeTiming", param);
                return rs;
            }

            public DataSet ExistingDoctor(string searchpara, string depcode, string Role)
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", "search");
                param[1] = new SqlParameter("@doctor_code", searchpara);
                param[2] = new SqlParameter("@hosde_code", depcode);
                param[3] = new SqlParameter("@role", Role);
                param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;
                var rs = SP_ResultSet("Hos_Employee", param);
                return rs;
            }
        }

    }

