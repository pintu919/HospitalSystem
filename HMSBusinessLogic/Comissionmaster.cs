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
    public sealed class Comission_Master : BLayer
    {
        public Comission_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();

        public DataSet Insert(string AgentName,string hos_code,string AgentMobile, int CategoryId,  int ctrl,string user_code)
        {
            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@action", "insert");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@AgentName", AgentName);
                param[3] = new SqlParameter("@AgentMobile", AgentMobile);
                param[4] = new SqlParameter("@CategoryId", CategoryId);
                //param[5] = new SqlParameter("@ComissionType", ComissionType);
                //param[6] = new SqlParameter("@Comission", Comission);
                param[5] = new SqlParameter("@ctrl", ctrl);
                param[6] = new SqlParameter("@user_code", user_code);
                param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[7].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hms_comission_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet Update(int id, string AgentName, string hos_code, string AgentMobile, int CategoryId,  int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[9];
                param[0] = new SqlParameter("@action", "update");
                param[1] = new SqlParameter("@id", id);
                param[2] = new SqlParameter("@hos_code", hos_code);
                param[3] = new SqlParameter("@AgentName", AgentName);
                param[4] = new SqlParameter("@AgentMobile", AgentMobile);
                param[5] = new SqlParameter("@CategoryId", CategoryId);
                //param[6] = new SqlParameter("@ComissionType", ComissionType);
                //param[7] = new SqlParameter("@Comission", Comission);
                param[6] = new SqlParameter("@ctrl", ctrl);
                param[7] = new SqlParameter("@user_code", user_code);
                param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[8].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hms_comission_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet Delete(int id, string hos_code, string user_code)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", "delete");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@id", id);
                param[3] = new SqlParameter("@user_code", user_code);
                param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hms_comission_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetAll(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "getall");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hms_comission_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataTable GetServices()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "get_services");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            return rs.Tables[0];
        }
        public DataTable GetCategoryData(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_allcategory");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            return rs.Tables[0];
        }
        public DataSet GetCategoryWiseServices(string HosCode, int CategoryId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_categorywise_services");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@category_id", CategoryId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            return rs;
           
        }

        public DataTable GetCategorywiseComission(int  catid, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_comission");
            param[1] = new SqlParameter("@category_id", catid);
            param[2] = new SqlParameter("@hos_code", HosCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            return rs.Tables[0];
        }

        public DataSet GetAgentCategorywiseInvestigation(string hos_code, int categoryid)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "get_agent_inv_comission_services");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@category_id", categoryid);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("Hos_Comission_Category", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataTable Add_UpdateCategory( DataTable dt, string usercode, string HosCode, int categoryid, string categoryname,   bool ctrl, string Type, DataTable dt2, out string Status)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "insert_update");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@category_id", categoryid);
            param[3] = new SqlParameter("@category_name", categoryname);
            param[4] = new SqlParameter("@dt_services", dt);
            param[5] = new SqlParameter("@usercode", usercode);
            param[6] = new SqlParameter("@ctrl", Convert.ToInt16(ctrl));
            param[7] = new SqlParameter("@Type",Type);
            param[8] = new SqlParameter("@dt_agent_comission", dt2);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@result", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            Status = param[10].Value == DBNull.Value ? "fail" : Convert.ToInt16(param[10].Value) > 0 ? Convert.ToInt16(param[10].Value) == 2 ? "added" : "sucess" : "fail";
            return rs.Tables[0];
        }

        public DataTable DeleteComissionCategory(string usercode, string HosCode, int categoryid, out string Status)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete_category");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@category_id", categoryid);
            param[3] = new SqlParameter("@usercode", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@result", SqlDbType.NVarChar);
            param[5].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Comission_Category", param);
            Status = param[5].Value == DBNull.Value ? "fail" : Convert.ToInt16(param[5].Value) > 0 ? "sucess" : "fail";
            return rs.Tables[0];
        }

        public DataSet GetAgentComissionData(string HosCode,string fromdate,string todate,string comission_agentid)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_agent_comission_data");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", fromdate);
            param[3] = new SqlParameter("@to_date", todate);
            param[4] = new SqlParameter("@agent_id", Convert.ToInt32(comission_agentid));
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Agent_Comission_Data", param);
            return rs;

        }

        public DataSet Save_Agent_ComissionData(string HosCode, string FromDate, string ToDate, string UserCode, DataTable dt, int PaymentTypeId, int ChannelId, string comission_agentid, out int Status)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "save_agent_comissiondata");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@UserCode", UserCode);
            param[3] = new SqlParameter("@from_date", FromDate);
            param[4] = new SqlParameter("@to_date", ToDate);
            param[5] = new SqlParameter("@dt_comissiondata", dt);
            param[6] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[7] = new SqlParameter("@ChannelId", ChannelId);
            param[8] = new SqlParameter("@agent_id", Convert.ToInt32(comission_agentid));
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Agent_Comission_Data", param);
            Status = Convert.ToInt16(param[10].Value);
            return rs;
        }

        public DataSet GetAllDoctorComissionCategory(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "getall");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_doctor_comission", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InsertDoctorCategory(string ComissionName, string hos_code,  int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[6];
                param[0] = new SqlParameter("@action", "insert");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@ComissionName", ComissionName);
                param[3] = new SqlParameter("@ctrl", ctrl);
                param[4] = new SqlParameter("@user_code", user_code);
                param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[5].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_doctor_comission", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet UpdateDoctorCategory(int Id, string ComissionName, string hos_code, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[7];
                param[0] = new SqlParameter("@action", "update");
                param[1] = new SqlParameter("@id", Id);
                param[2] = new SqlParameter("@hos_code", hos_code);
                param[3] = new SqlParameter("@ComissionName", ComissionName);
                param[4] = new SqlParameter("@ctrl", ctrl);
                param[5] = new SqlParameter("@user_code", user_code);
                param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[6].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_doctor_comission", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetDoctorCategorywiseInvestigation(string hos_code, int Id)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "categorywiseservices");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@Id", Id);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_doctor_comission", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet InsertDoctorComissionServices(string hos_code, int ID, DataTable dt, string user_code)
        {
            try
            {
                var param = new SqlParameter[6];
                param[0] = new SqlParameter("@action", "insert_comission_services");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@Id", ID);
                param[3] = new SqlParameter("@dt_doctor_comission", dt);
                param[4] = new SqlParameter("@user_code", user_code);
                param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[5].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_doctor_comission", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetDoctorComissionData(string HosCode, string fromdate, string todate, string doctor_code)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_doctor_comission_data");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", fromdate);
            param[3] = new SqlParameter("@to_date", todate);
            param[4] = new SqlParameter("@doctor_code", doctor_code);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Doctor_Comission_Data", param);
            return rs;

        }
        public DataSet Save_ComissionData(string HosCode, string FromDate, string ToDate, string UserCode, int acc_id, DataTable dt, int PaymentTypeId, int ChannelId, string Doctor_Code, out int Status)
        {
            var param = new SqlParameter[12];
            param[0] = new SqlParameter("@action", "save_comissiondata");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@UserCode", UserCode);
            param[3] = new SqlParameter("@from_date", FromDate);
            param[4] = new SqlParameter("@to_date", ToDate);
            param[5] = new SqlParameter("@acc_id", acc_id);
            param[6] = new SqlParameter("@dt_comissiondata", dt);
            param[7] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[8] = new SqlParameter("@ChannelId", ChannelId);
            param[9] = new SqlParameter("@doctor_code", Doctor_Code);
            param[10] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[11].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Doctor_Comission_Data", param);
            Status = Convert.ToInt16(param[11].Value);
            return rs;
        }
        #region "Comission Payment Slip"
        public DataTable ComissionPaymentSlipPageLoad(string HosCode, string Type)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_comission_pay_slip_page");
            param[1] = new SqlParameter("@Type", Type);
            param[2] = new SqlParameter("@hos_code", HosCode);
            var rs = SP_ResultSet("Hos_Comission_PaymentSlip", param);
            return rs.Tables[0];
        }
        public DataTable GetComissionPaymentSlip(string ActType, string HosCode, string Type, string Fromdate,
            string ToDate, int ComissionId, string doctorCode)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", ActType);
            param[1] = new SqlParameter("@Type", Type);
            param[2] = new SqlParameter("@hos_code", HosCode);
            param[3] = new SqlParameter("@comission_agent_id", ComissionId);
            param[4] = new SqlParameter("@fromdate", Fromdate);
            param[5] = new SqlParameter("@todate", ToDate);
            param[6] = new SqlParameter("@doctor_code", doctorCode);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Comission_PaymentSlip", param);
            return rs.Tables[0];
        }
        #endregion
    }
}
