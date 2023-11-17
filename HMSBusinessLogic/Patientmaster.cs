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
   public sealed class Patient_Entity
    {
        [SqlKey(Name = "@patient_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 200)]
        public string patient_code { get; set; }

        [SqlKey(Name = "@patient_firstname")]
        public string patient_firstname { get; set; }

        [SqlKey(Name = "@patient_lastname")]
        public string patient_lastname { get; set; }

        [SqlKey(Name = "@age")]
        public string age { get; set; }

        [SqlKey(Name = "@phone")]
        public string phone { get; set; }

        [SqlKey(Name = "@patient_fathername")]
        public string patient_fathername { get; set; }

        [SqlKey(Name = "@nid_birthregno")]
        public string nid_birthregno { get; set; }

        [SqlKey(Name = "@email")]
        public string email { get; set; }

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

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        public DataTable dtlogtable { get; set; }

        [SqlKey(Name = "@photo")]
        public string photo { get; set; }

        [SqlKey(Name = "@patient_id_type")]
        public string patient_id_type { get; set; }

        [SqlKey(Name = "@enc_password")]
        public string enc_password { get; set; }

        [SqlKey(Name = "@patient_id")]
        public Int64 patient_id { get; set; }

        [SqlKey(Name = "@password")]
        public string password { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string create_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string dateofbirth { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isverify { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ishistory { get; set; }

        [SqlKey(Name = "@is_id_verify")]

        public int is_id_verify { get; set; }

        [SqlKey(Name = "@relation")]
        public string relation { get; set; }

        [SqlKey(Name = "@spouse_name")]
        public string spouse_name { get; set; }

        [SqlKey(Name = "@profession")]
        public string profession { get; set; }
        [SqlKey(Name = "@patient_mothername")]
        public string patient_mothername { get; set; }
        [SqlKey(Name = "@blood_group")]
        public string blood_group { get; set; }
    }

    public sealed class patient_Master : BLayer
    {
        public patient_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        //public string Add(Patient_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "insert"));
        //    try
        //    {
        //        var rs = ApplyChanges("crd_patient_master", param.ToArray(), Actions.Add);
        //        var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
        //        return result;
        //    }
        //    catch (Exception e) { return "0"; }
        //}

        public DataTable Add(Patient_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "hos_insert"));
            try
            {
                var rs = SP_ResultSet("crd_patient_master", param.ToArray());
                //var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }
        public bool Update(Patient_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_patient_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool Delete(string PATcode, string UserCode, string hoscode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@patient_code", PATcode);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@hos_code", hoscode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_patient_master", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public DataSet GetAll(string CliniqUniqrow = null, DateTime? fromdate=null, DateTime? todate=null)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_patient_master", param);
            return rs;
        }

        public DataSet GetAll_vitalpara(string patientcode, string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getothervital");
            param[1] = new SqlParameter("@patient_code", patientcode);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_patient_master", param.ToArray());
            return rs;
        }

        //public DataSet GetAllVisitedPatient(string CliniqUniqrow = null, string fromdate =null, string todate =null, string hosdoccode=null)
        //{
        //    var param = new SqlParameter[6];
        //    param[0] = new SqlParameter("@action", "visitedpatientrecord");
        //    param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
        //    param[2] = new SqlParameter("@fromdate", fromdate);
        //    param[3] = new SqlParameter("@todate", todate);
        //    param[4] = new SqlParameter("@hos_doccode", hosdoccode);
        //    param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[5].Direction = ParameterDirection.Output;
        //    var rs = SP_ResultSet("HOS_patient_filter", param);
        //    return rs;
        //}

        public DataSet GetfiltervisitedPatient(string HosCode, string fromdate, string  todate, string hosdoccode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "visitedpatientrecord");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@hos_doccode", hosdoccode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_patient_filter", param);
            return rs;

        }

        public DataSet GetByCode(string PATcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@patient_code", PATcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_patient_master", param);
            return rs;
        }

        public Email_Entity GetPatientEmailDetails(int PatientId, string password)
        {

            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getmaildetail");
            param[1] = new SqlParameter("@patient_id", PatientId);
            param[2] = new SqlParameter("@password", password);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Email_Entity>("crd_patient_master", param.ToArray()).SingleOrDefault();
            return rs;
        }

        //Added By Dhaval For Other Patient Search
        public DataSet SearchOtherFilter(string hos_code, string PATcode, string email, string mobile, string bIrthyear, string P_Name, string P_father)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "other_patient_search");
            param[1] = new SqlParameter("@patient_code", PATcode);
            param[2] = new SqlParameter("@phone", mobile);
            param[3] = new SqlParameter("@email", email);
            param[4] = new SqlParameter("@patient_firstname", P_Name);
            param[5] = new SqlParameter("@patient_fathername", P_father);
            param[6] = new SqlParameter("@zip", bIrthyear);
            param[7] = new SqlParameter("@hos_code", hos_code);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_patient_master", param);
            return rs;
        }
        //End Dhaval

        public DataSet GetvisitedPatientprofile(string HosCode, string uniqrow)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "visitedpatientprofile");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@uniqrow", uniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_patient_filter", param);
            return rs;

        }

        public DataSet GetIPHistory(string HosCode, string Patientcode,string Type)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "patientiphistory");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@uniqrow", Patientcode);
            param[3] = new SqlParameter("@Type", Type);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_patient_filter", param);
            return rs;

        }

        public DataSet GetfiltervisitedPatienthistory(string HosCode, string fromdate, string todate,string type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "visitedpatienthistory");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@type", type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_visitedpatient_history", param);
            return rs;

        }


    }
}
