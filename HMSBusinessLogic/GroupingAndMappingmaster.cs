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
    public sealed class GroupingAndMapping_Master : BLayer
    {
        public GroupingAndMapping_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        public DataTable GetGroupData(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_allgroup");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_grouping_mapping", param);
            return rs.Tables[0];
        }
        public DataTable Add_UpdateGroup(string usercode, string HosCode, int grpid, string grpname, bool ctrl, out string Status)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "insert_update");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@group_id", grpid);
            param[3] = new SqlParameter("@groupname", grpname);
            param[4] = new SqlParameter("@usercode", usercode);
            param[5] = new SqlParameter("@ctrl", Convert.ToInt16(ctrl));
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@result", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_grouping_mapping", param);
            Status = param[7].Value == DBNull.Value ? "fail" : Convert.ToInt16(param[7].Value) > 0 ? Convert.ToInt16(param[7].Value) == 2 ? "added" : "sucess" : "fail";
            return rs.Tables[0];
        }
        public DataTable DeleteGroup(string usercode, string HosCode, int Grpid, out string Status)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete_group");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@group_id", Grpid);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@result", SqlDbType.NVarChar);
            param[5].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_grouping_mapping", param);
            Status = param[5].Value == DBNull.Value ? "fail" : Convert.ToInt16(param[5].Value) > 0 ? "sucess" : "fail";
            return rs.Tables[0];
        }
        public DataSet GetMappedData(string HosCode, int Grpid,string Type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_mapped_data");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@group_id", Grpid);
            param[3] = new SqlParameter("@Type", Type);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@result", SqlDbType.NVarChar);
            param[5].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_grouping_mapping", param);
            return rs;
        }
        public DataSet Mapp_UnMappData(string HosCode,string UserCode, int Grpid, string Type,string action,DataTable dt,out string Status)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", action);
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@usercode", UserCode);
            param[3] = new SqlParameter("@group_id", Grpid);
            param[4] = new SqlParameter("@Type", Type);
            param[5] = new SqlParameter("@dt", dt);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@result", SqlDbType.NVarChar);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_grouping_mapping", param);
            Status= param[7].Value == DBNull.Value ? "fail" : Convert.ToInt16(param[7].Value) > 0 ? "sucess" : "fail";
            return rs;
        }
    }
}
