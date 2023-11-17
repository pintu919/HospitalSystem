using HMS.BizLayer;
using HMS.Data;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class DischargeProfilemed_entity
    {
        [SqlKey(Name = "@mapping_id")]
        public int mappingid { get; set; }
        [SqlKey(Name ="@brand_code")]
        public string brand_code { get; set; }

        [SqlKey(Name = "@Brand_Name")]
        public string Brand_Name { get; set; }

        [SqlKey(Name = "@drug_generic_name")]
        public string drug_generic_name { get; set; }

        [SqlKey(Name = "@formulationname")]
        public string formulationname { get; set; }

        [SqlKey(Name = "@strength")]
        public string strength { get; set; }

        [SqlKey(Name = "@Dosages")]
        public string Dosages { get; set; }

        [SqlKey(Name = "@Used")]
        public int Used { get; set; }

        [SqlKey(Name = "@Duration")]
        public int Duration { get; set; }

        [SqlKey(Name = "@Remark")]
        public string Remark { get; set; }

        [SqlKey(Name = "@MedicineIds")]
        public string MedicineIds { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@section_id")]
        public int section_id { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string frequency { get; set; }

        [SqlKey(Name = "@RealMedicineName")]
        public string RealMedicineName { get; set; }
    }
    public sealed class ProfileSection_Entity
    {
        [SqlKey(Name = "@section_name")]
        public string section_name { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private int Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@section_id")]
        public int section_id { get; set; }

        [SqlKey(Name = "@general_advice")]
        public string general_advice { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }
    }





    public sealed class DischargeProfile_Master : BLayer
    {
        public DischargeProfile_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();


        public string Add(DischargeProfilemed_entity entity)
        {

            var param = ToSqlParams(entity);

            param.Add(new SqlParameter("@action", "save_medicine"));
            try
            {
                var rs = ApplyChanges("hos_discharge_patientProfile", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@statusmessage").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public DataSet GetDischargeMed(string HosCode, string usercode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_med");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@user_code", usercode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_discharge_patientProfile", param);
            return rs;
        }

        public DataSet GetDischargeMedSectionWise(string HosCode, string usercode, int SectionId)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "get_med_sectionwise");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@user_code", usercode);
            param[3] = new SqlParameter("@section_id", SectionId);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_discharge_patientProfile", param);
            return rs;
        }

        public DataSet GetDischargeProfilesection(string HosCode, string usercode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_section");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@user_code", usercode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_patientProfile_section", param);
            return rs;
        }

        public bool DeleteSection(int SectionId, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@section_id", SectionId);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("hos_patientProfile_section", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool DeleteMed(int SectionId, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "deletemed");
            param[1] = new SqlParameter("@section_id", SectionId);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("hos_discharge_patientProfile", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataTable SaveProfileSetion(ProfileSection_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertsection"));
            try
            {
                var rs = SP_ResultSet("hos_patientProfile_section", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataSet GetIPDoctor(string HosCode, string Appointmentcode, out string statusmessage)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "getdoctor");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@appointment_code", Appointmentcode);
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_Change_IP_Doctor", param);
            statusmessage = param[3].Value.ToString();
            return rs;
        }

        public bool ChangeDoctor(string DoctorCode, string Appointmentcode, string hoscode, string usercode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "changedoctor");
            param[1] = new SqlParameter("@doctor_code", DoctorCode);
            param[2] = new SqlParameter("@appointment_code", Appointmentcode);
            param[3] = new SqlParameter("@hos_code", hoscode);
            param[4] = new SqlParameter("@user_code", usercode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("hos_Change_IP_Doctor", param.ToArray(), Actions.Update);
                return param[6].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }



    }

}
