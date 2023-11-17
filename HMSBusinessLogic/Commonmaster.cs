using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class common_entity
    {
        [SqlKey(Name = "@dtMenutable", SqlType = SqlDbType.Structured)]
        public DataTable Type_Hospital_UserAcess { get; set; }
       
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private int Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }
    }

    public sealed class patientmadicine_entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string formulationname { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string strength { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Dosages { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Duration { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Remark { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Used { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int mapping_id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string status { get; set; }
    }
    public sealed class Common_Master : BLayer
    {
        public Common_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public List<Country_Entity> GetAllcountry(string Countrycode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getcountry");
            param[1] = new SqlParameter("@code", Countrycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Country_Entity>("crd_common_master", param);
            return rs;
        }
        public List<State_Entity> GetStateByCode(string Countrycode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getstate");
            param[1] = new SqlParameter("@code", Countrycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<State_Entity>("crd_common_master", param);
            return rs;
        }
        public List<District_Entity> GetDistrictByCode(string statecode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getdistrict");
            param[1] = new SqlParameter("@code", statecode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<District_Entity>("crd_common_master", param);
            return rs;
        }
        public List<City_Entity> GetCityByCode(string citycode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getcity");
            param[1] = new SqlParameter("@code", citycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<City_Entity>("crd_common_master", param);
            return rs;
        }
        public DataSet GetByData()
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@medicine_id", 0);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_drug_master", param);
            return rs;
        }
        public DataSet GetPharmaMedicineMappData()
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@mapping_id", 0);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_pharma_madicinemapp_master", param);
            return rs;
        }
        public List<Formulation_Entity> GetByFormulationCode(string Genericcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getformulation");
            param[1] = new SqlParameter("@code", Genericcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Formulation_Entity>("crd_common_master", param);
            return rs;
        }
        public List<Drug_Entity> GetDrugsStrength(string Genericcode,int formulationId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getmedicinestrength");
            param[1] = new SqlParameter("@code", Genericcode);
            param[2] = new SqlParameter("@id", formulationId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Drug_Entity>("crd_Drug_common_master", param);
            return rs;
        }
        public List<Diseasecategory_Entity> GetAllDisesCategory()
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "disescategory");
            param[1] = new SqlParameter("@code", "");
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Diseasecategory_Entity>("crd_common_master", param);
            return rs;
        }
        public List<Investigationgroup_Entity> GetAllGroupName()
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "investigationgroup");
            param[1] = new SqlParameter("@code", "");
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Investigationgroup_Entity>("crd_common_master", param);
            return rs;
        }
        public List<Investigation_Entity> GetAllInvestigationData( string categorycode ="")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "investigation");
            param[1] = new SqlParameter("@code", categorycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Investigation_Entity>("crd_common_master", param);
            return rs;
        }
        public DataSet GetData( string hoscode="")
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@doctor_code", "");
            param[2] = new SqlParameter("@type", null);
            param[3] = new SqlParameter("@HosCode", hoscode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_doctor_master", param);
            return rs;
        }

        

        public List<Usergroup_Entity> GetALlRoleName()
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getrolename");
            param[1] = new SqlParameter("@code", "");
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Usergroup_Entity>("crd_common_master", param);
            return rs;
        }
        //Hospital Useraccess function
        public DataSet GetHospitalMenuDataSet(int Id=0, string HosCode=null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "gethospitalrole");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@usergroupid", Id.ToString());
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_UserAccess", param);
            return rs;
        }
        public bool SaveRoleAcess(DataTable dt)
        {
            common_entity entity = new common_entity();
            entity.Type_Hospital_UserAcess = dt;
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("HOS_UserAccess", param.ToArray(), Actions.Add);
                return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
            }
            catch (DException) { return false; }
        }

        public void Insert_Login_log_history(string Browser, string Os, string Channel, string Logon_SystemName, string IpAddress, string UserValue, string UserType, string HostingAddress, string UserLocation, string Latitude, string Longitude, bool IsLogin,string error_msg)
        {
            var param = new SqlParameter[15];
            param[0] = new SqlParameter("@action", "login_log");
            param[1] = new SqlParameter("@Browser", Browser);
            param[2] = new SqlParameter("@Os", Os);
            param[3] = new SqlParameter("@Channel", Channel);
            param[4] = new SqlParameter("@Logon_SystemName", Logon_SystemName);
            param[5] = new SqlParameter("@IpAddress", IpAddress);
            param[6] = new SqlParameter("@UserValue", UserValue);
            param[7] = new SqlParameter("@UserType", UserType);
            param[8] = new SqlParameter("@HostingAddress", HostingAddress);
            param[9] = new SqlParameter("@UserLocation", UserLocation);
            param[10] = new SqlParameter("@Latitude", Latitude);
            param[11] = new SqlParameter("@Longitude", Longitude);
            param[12] = new SqlParameter("@Is_Login_Sucess", IsLogin);
            param[13] = new SqlParameter("@error_msg", error_msg);
            param[14] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[14].Direction = ParameterDirection.Output;
            ApplyChanges("Insert_log_history", param.ToArray(), Actions.Add);
        }

       

        public List<patientmadicine_entity> Save_Medecine(string Appt_code, string Dosages, int Duration, int mapping_id, string Remark, int Used, string MedicineIds, string doc_code)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "save_medicine");
            param[1] = new SqlParameter("@apt_code", Appt_code);
            param[2] = new SqlParameter("@Duration", Duration);
            param[3] = new SqlParameter("@Dosages", Dosages);
            param[4] = new SqlParameter("@mapping_id", mapping_id);
            param[5] = new SqlParameter("@Remark", Remark);
            param[6] = new SqlParameter("@Used", Used);
            param[7] = new SqlParameter("@doc_code", doc_code);
            param[8] = new SqlParameter("@MedicineIds", MedicineIds);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            var rs = SP_Read<patientmadicine_entity>("drsp_inpatientcheckup", param);
            return rs;
        }

        public int Insert_Email_history(string code, string type, string Subject, string emailtype, string Body, string ToEmail, int sendsucess)
        {
            try
            {
                var param = new SqlParameter[10];
                param[0] = new SqlParameter("@action", "insertemail_history");
                param[1] = new SqlParameter("@code", code);
                param[2] = new SqlParameter("@type", type);
                param[3] = new SqlParameter("@subject", Subject);
                param[4] = new SqlParameter("@emailtype", emailtype);
                param[5] = new SqlParameter("@body", Body);
                param[6] = new SqlParameter("@toemail", ToEmail);
                param[7] = new SqlParameter("@sendsucess", sendsucess);
                param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[8].Direction = ParameterDirection.Output;
                param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
                param[9].Direction = ParameterDirection.ReturnValue;
                var rs = ApplyChanges("crd_Insert_Email_history", param.ToArray(), Actions.Add);
                return Convert.ToInt16(param[9].Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Insert_Error_log_history(string message, string Exe_Source, string stacktrace, string Type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "error_log");
            param[1] = new SqlParameter("@Source", Exe_Source);
            param[2] = new SqlParameter("@Message", message);
            param[3] = new SqlParameter("@StackTrace", stacktrace);
            param[4] = new SqlParameter("@Project_Type", Type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            ApplyChanges("Insert_log_history", param.ToArray(), Actions.Add);
        }
    }
}
