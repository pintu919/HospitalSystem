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
    public sealed class InvestigationPrice_Master : BLayer
    {
        public InvestigationPrice_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }


        public bool AddInvestigationPrice( DataTable dt, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_investigation_price", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }
       
        public DataSet GetAll(string CategoryId, string Hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@id", CategoryId);
            param[2] = new SqlParameter("@hoscode", Hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_investigation_price", param);
            return rs;
        }
        public DataSet GetAll_LabDoctor(string InvCatCode, string Hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_labdoctorlist");
            param[1] = new SqlParameter("@id", InvCatCode);
            param[2] = new SqlParameter("@hoscode", Hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_investigation_price", param);
            return rs;
        }
        //public bool AddLabDoctorList(string DoctorLists,string CategoryCode, string hoscode, string usercode)
        //{
        //    var param = new SqlParameter[7];
        //    param[0] = new SqlParameter("@action", "add_labdoctor");
        //    param[1] = new SqlParameter("@DoctorLists", DoctorLists);
        //    param[2] = new SqlParameter("@id", CategoryCode);
        //    param[3] = new SqlParameter("@hoscode", hoscode);
        //    param[4] = new SqlParameter("@user_code", usercode);
        //    param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[5].Direction = ParameterDirection.Output;
        //    param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
        //    param[6].Direction = ParameterDirection.ReturnValue;
        //    ApplyChanges("hos_investigation_price", param.ToArray(), Actions.Add);
        //    return param[6].Value.ToInt() > 0 ? true : false;
        //}

        public bool AddLabDoctorList(DataTable dt,  string hoscode, string usercode, string categorycode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "add_labdoctor");
            param[1] = new SqlParameter("@dtLabdoc", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@id", categorycode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_investigation_price", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt() > 0 ? true : false;
        }

        #region "Lab technician Mapp with Investigationgroup"
        public DataSet GetAllMapdata(string Hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getmapping_data");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_mapp_labtechnician", param);
            return rs;
        }
        public DataTable MappingTechnician(string CategoryCodes, string EmpCode, string hoscode, string usercode, out string status)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "map_labtechnician");
            param[1] = new SqlParameter("@CategoryCodes", CategoryCodes);
            param[2] = new SqlParameter("@EmpCode", EmpCode);
            param[3] = new SqlParameter("@hoscode", hoscode);
            param[4] = new SqlParameter("@user_code", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("hos_mapp_labtechnician", param);
            status = param[6].Value.ToInt() > 0 ? "1" : "0";
            return rs.Tables[0];
        }
        public bool EmployeeSignatureUpload(DataTable dt, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "emp_uploadimage");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_mapp_labtechnician", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }
        public bool UnMappingLabtechnican(string EmpCode, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "unmapping_technician");
            param[1] = new SqlParameter("@EmpCode", EmpCode);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_mapp_labtechnician", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }
        #endregion

        #region"Investigation Room Collection"
        public DataSet GetAllInvCollectionRoom(string Hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_investigation_collection", param);
            return rs;
        }

        public bool AddInvestigationCollectionRoom(DataTable dt, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("hos_investigation_collection", param.ToArray(), Actions.Add);
            return param[5].Value.ToInt() > 0 ? true : false;
        }

        #endregion
    }
}
