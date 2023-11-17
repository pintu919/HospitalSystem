using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{

    public sealed class Cliniq_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string statename { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string district { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string cityname { get; set; }

        [SqlKey(Name = "@cliniq_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.NVarChar, Size = 500)]
        public string cliniq_code { get; set; }

        [SqlKey(Name = "@cliniq_password", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 1000)]
        public string cliniq_password { get; set; }       
        [SqlKey(Name = "@Cliniq_Name")]
        public string Cliniq_Name { get; set; }

        [SqlKey(Name = "@Cliniq_Addr")]
        public string Cliniq_Addr { get; set; }

        [SqlKey(Name = "@Cliniq_phone")]
        public string Cliniq_phone { get; set; }

        [SqlKey(Name = "@tele_phone")]
        public string tele_phone { get; set; }

        [SqlKey(Name = "@Cliniq_tollfree")]
        public string Cliniq_tollfree { get; set; }

        [SqlKey(Name = "@Cliniq_emergency_no")]
        public string Cliniq_emergency_no { get; set; }

        [SqlKey(Name = "@Cliniq_landmark")]
        public string Cliniq_landmark { get; set; }

        [SqlKey(Name = "@Cliniq_lat")]
        public string Cliniq_lat { get; set; }

        [SqlKey(Name = "@Cliniq_lang")]
        public string Cliniq_lang { get; set; }

        [SqlKey(Name = "@tempid")]
        public string tempid { get; set; }
        [SqlKey(Name = "@de_pwd")]
        public string de_pwd { get; set; }

        [SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        public DataTable dtlogtable { get; set; }

        [SqlKey(Name = "@host_url")]
        public string hosting_url { get; set; }

        //Day1
        [SqlKey(Name = "@day1")]
        public bool day1 { get; set; }
        [SqlKey(Name = "@day1_Cliniq_open_time")]
        public TimeSpan day1_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day1_Cliniq_close_time")]
        public TimeSpan day1_Cliniq_close_time { get; set; }

        //Day2
        [SqlKey(Name = "@day2")]
        public bool day2 { get; set; }
        [SqlKey(Name = "@day2_Cliniq_open_time")]
        public TimeSpan day2_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day2_Cliniq_close_time")]
        public TimeSpan day2_Cliniq_close_time { get; set; }

        //Day3
        [SqlKey(Name = "@day3")]
        public bool day3 { get; set; }

        [SqlKey(Name = "@day3_Cliniq_open_time")]
        public TimeSpan day3_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day3_Cliniq_close_time")]
        public TimeSpan day3_Cliniq_close_time { get; set; }

        //Day4
        [SqlKey(Name = "@day4")]
        public bool day4 { get; set; }
        [SqlKey(Name = "@day4_Cliniq_open_time")]
        public TimeSpan day4_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day4_Cliniq_close_time")]
        public TimeSpan day4_Cliniq_close_time { get; set; }

        //Day5
        [SqlKey(Name = "@day5")]
        public bool day5 { get; set; }
        [SqlKey(Name = "@day5_Cliniq_open_time")]
        public TimeSpan day5_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day5_Cliniq_close_time")]
        public TimeSpan day5_Cliniq_close_time { get; set; }

        //Day6
        [SqlKey(Name = "@day6")]
        public bool day6 { get; set; }
        [SqlKey(Name = "@day6_Cliniq_open_time")]
        public TimeSpan day6_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day6_Cliniq_close_time")]
        public TimeSpan day6_Cliniq_close_time { get; set; }

        //Day7
        [SqlKey(Name = "@day7")]
        public bool day7 { get; set; }
        [SqlKey(Name = "@day7_Cliniq_open_time")]
        public TimeSpan day7_Cliniq_open_time { get; set; }
        [SqlKey(Name = "@day7_Cliniq_close_time")]
        public TimeSpan day7_Cliniq_close_time { get; set; }

        [SqlKey(Name = "@Cliniq_off_day")]
        public string Cliniq_off_day { get; set; }

        [SqlKey(Name = "@Cliniq_email")]
        public string Cliniq_email { get; set; }

        [SqlKey(Name = "@Cliniq_RegNo")]
        public string Cliniq_RegNo { get; set; }

        [SqlKey(Name = "@GSTNO")]
        public string GSTNO { get; set; }

        [SqlKey(Name = "@country_code")]
        public string country_code { get; set; }

        [SqlKey(Name = "@state_code")]
        public string state_code { get; set; }

        [SqlKey(Name = "@district_code")]
        public string district_code { get; set; }

        [SqlKey(Name = "@city_code")]
        public string city_code { get; set; }

        [SqlKey(Name = "@zip")]
        public string zip { get; set; }

        //[SqlKey(Name = "@type_id")]
        //public string type_id { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@dailyopen")]
        public bool dailyopen { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.NVarChar, Size = Int16.MaxValue)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Cliniq_ID { get; set; }

        [SqlKey(Name = "@clinic_logo")]
        public string clinic_logo { get; set; }

        [SqlKey(Name = "@clinic_doclogo")]
        public string clinic_doclogo { get; set; }

        [SqlKey(Name = "@inv_content")]
        public string inv_content { get; set; }

        [SqlKey(Name = "@clinic_feviconicon")]
        public string clinic_feviconicon { get; set; }

        [SqlKey(Name = "@prescriptionHeader_Image")]
        public string prescriptionHeader_Image { get; set; }

        [SqlKey(Name = "@prescriptionFooter_Image")]
        public string prescriptionFooter_Image { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AllowDischargeWithoutPayment { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int DoctorVisitFeesAddInInvoice { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AllowTestReportEmailWithoutPayment { get; set; }
        [SqlKey(Name = "@AdmissionFormTerms_Condition")]
        public string AdmissionFormTerms_Condition { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ShowPatientIamgeinPrescription { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ShowPatientDistrictinPrescription { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ShowEyePrescription { get; set; }

    }
    public sealed class HRSetting_Entity
    {
        [SqlKey(Name = "@clinc_hr_basic")]
        public decimal clinc_hr_basic { get; set; }

        [SqlKey(Name = "@clinc_hr_hra")]
        public decimal clinc_hr_hra { get; set; }

        [SqlKey(Name = "@clinic_hr_medical")]
        public decimal clinic_hr_medical { get; set; }

        [SqlKey(Name = "@clinic_hr_conveyallowance")]
        public decimal clinic_hr_conveyallowance { get; set; }
        [SqlKey(Name = "@clinic_hr_otherallowance")]
        public decimal clinic_hr_otherallowance { get; set; }

        [SqlKey(Name = "@clinic_hr_basicpf")]
        public decimal clinic_hr_basicpf { get; set; }

        [SqlKey(Name = "@clinic_hr_maxbasicpf")]
        public decimal clinic_hr_maxbasicpf { get; set; }

        [SqlKey(Name = "@clinic_code")]
        public string clinic_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

    }


    public sealed class default_prescription
    {

        [SqlKey(Name = "@tempid")]
        public int tempid { get; set; }

        [SqlKey(Name = "@htmlcode")]
        public string htmlcode { get; set; }
    }

    public sealed class hospital_new_entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

        [SqlKey(Name = "@news_img")]
        public string news_img { get; set; }

        [SqlKey(Name = "@news_Date")]
        public string news_Date { get; set; }

        [SqlKey(Name = "@news_title")]
        public string news_title { get; set; }

        [SqlKey(Name = "@news_content")]
        public string news_content { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }


    }

    public sealed class Cliniq_Master : BLayer
    {
        public Cliniq_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();
        public string Add(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public bool Update(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool UpdateHospitalLogo(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "updatehospitallogo"));
            try
            {
                ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool UpdatePresciptionLogo(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "updatepresciprionlogo"));
            try
            {
                ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool UpdateInvContent(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "invcontent"));
            try
            {
                ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool UpdateControl(string Cliniqcode, int AllowDischarge, int Is_Dr_Fees_Add, int IsEmilReportAllow, int Showpatientiamgeinprescription, int Showpatientdistrictinprescription, int Showeyeinprescription)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "set_hos_control");
            param[1] = new SqlParameter("@cliniq_code", Cliniqcode);
            param[2] = new SqlParameter("@AllowDischargeWithoutPayment", AllowDischarge);
            param[3] = new SqlParameter("@DoctorVisitFeesAddOPDInvoice", Is_Dr_Fees_Add);
            param[4] = new SqlParameter("@AllowTestReportEmailWithoutFullPayment", IsEmilReportAllow);
            param[5] = new SqlParameter("@ShowPatientIamgeinPrescription", Showpatientiamgeinprescription);
            param[6] = new SqlParameter("@ShowPatientDistrictinPrescription", Showpatientdistrictinprescription);
            param[7] = new SqlParameter("@ShowEyePrescription", Showeyeinprescription);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Delete);
                return Convert.ToBoolean(param[9].Value);
            }
            catch (DException) { return false; }
        }
        public bool UpdatePrescription(Cliniq_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "updateprescription"));
            try
            {
                ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Cliniqcode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@cliniq_code", Cliniqcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_cliniq_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }
        public List<Cliniq_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<Cliniq_Entity>("crd_cliniq_master", param);
            return rs;
        }
        public DataSet GetByCode(string Cliniqcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@cliniq_code", Cliniqcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            ////var rs = SP_Read<Cliniq_Entity>("crd_cliniq_master", param).SingleOrDefault();
            var rs = SP_ResultSet("crd_cliniq_master", param);
            return rs;
        }
        //public bool AddClinicHRSetting(HRSetting_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "insert"));
        //    try
        //    {
        //        var rs = ApplyChanges("crd_clinic_HRsetting", param.ToArray(), Actions.Add);
        //        var result = (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        //        return result;
        //    }
        //   catch(DException e) { return false; }
        //}
        public string AddClinicHRSetting(HRSetting_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_clinic_HRsetting", param.ToArray(), Actions.Add);
                var result = (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
                return Convert.ToString(result);
            }
            catch (Exception e) { return e.Message; }
        }
        public DataSet GetByCodeHRSetting(string Cliniqcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@clinic_code", Cliniqcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_clinic_HRsetting", param);
            return rs;
        }
        #region "Hospital News"
        public DataSet AddHosNews(hospital_new_entity entity)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "insert");
            param[1] = new SqlParameter("@news_img", entity.news_img);
            param[2] = new SqlParameter("@news_Date", entity.news_Date);
            param[3] = new SqlParameter("@news_title", entity.news_title);
            param[4] = new SqlParameter("@news_content", entity.news_content);
            param[5] = new SqlParameter("@hos_code", entity.hos_code);
            param[6] = new SqlParameter("@user_code", entity.user_code);
            param[7] = new SqlParameter("@ctrl", entity.ctrl);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_new_data", param);
            return rs;
        }
        public DataSet DeleteNewsData(int id, string hos_code)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "delete");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@id", id);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_new_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetAllNews(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "getall");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_new_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        #endregion
    }
}
