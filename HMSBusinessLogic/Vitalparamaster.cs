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

    public sealed class patientvital_Entity
    {
        [SqlKey(Name = "@body_temperature")]
        public decimal body_temperature { get; set; }

        [SqlKey(Name = "@blood_pressure")]
        public string blood_pressure { get; set; }

        [SqlKey(Name = "@weight")]
        public decimal weight { get; set; }

        [SqlKey(Name = "@sugar")]
        public decimal sugar { get; set; }

        [SqlKey(Name = "@oxygenlevel")]
        public decimal oxygenlevel { get; set; }

        [SqlKey(Name = "@heartrate")]
        public int heartrate { get; set; }

        [SqlKey(Name = "@remarks")]
        public string remarks { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@appointment_code")]
        public string appointment_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]

        public string status { get; set; }


    }

    public sealed class patient_status_Surgery
    {
        [SqlKey(Name = "@status_name_id")]
        public int status_name_id { get; set; }

        [SqlKey(Name = "@status_name")]
        public string  status_name { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

    }

    public sealed class patient_status_Disease
    {
        [SqlKey(Name = "@status_name_id")]
        public int status_name_id { get; set; }

        [SqlKey(Name = "@status_name")]
        public string status_name { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

    }

    public sealed class patient_status_Chronic
    {
        [SqlKey(Name = "@status_name_id")]
        public int status_name_id { get; set; }

        [SqlKey(Name = "@status_name")]
        public string status_name { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

    }

    public sealed class patient_status_Organs
    {
        [SqlKey(Name = "@status_organ_id")]
        public int status_organ_id { get; set; }

        [SqlKey(Name = "@status_organ_name")]
        public string status_organ_name { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

    }

    public class FormulationList
    {
        [SqlKey(Name = "@formula_id")]
        public int formula_id { get; set; }
        [SqlKey(Name = "@formulation_code")]
        public string formulation_code { get; set; }
        [SqlKey(Name = "@title")]
        public string title { get; set; }
    }

    public sealed class Medicine_Entity
    {
        [SqlKey(Name = "@medicine_id")]
        public string medicine_id { get; set; }
        [SqlKey(Name = "@brand_code")]
        public string brand_code { get; set; }

        [SqlKey(Name = "@drug_generic_name")]
        public string drug_generic_name { get; set; }
        [SqlKey(Name = "@Brand_Name")]
        public string Brand_Name { get; set; }

        [SqlKey(Name = "@formula_id")]
        public int formula_id { get; set; }
        [SqlKey(Name = "@formulation_code")]
        public string formulation_code { get; set; }
    }
   
    public sealed class ForntendVital_Entity
    {
        [SqlKey(Name = "@status_type_id")]
        public int status_type_id { get; set; }

        [SqlKey(Name = "@status_name_id")]
        public int status_name_id { get; set; }

        //[SqlKey(Name = "@status_organ_id")]
        //public int status_organ_id { get; set; }

        [SqlKey(Name = "@organ_name")]
        public string organ_name { get; set; }


        [SqlKey(Name = "@status_date")]
        public string status_date { get; set; }

        [SqlKey(Name = "@remarks")]
        public string remarks { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string status_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string status_organ_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
    }

    public sealed class ForntendotherVital_Entity
    {
        [SqlKey(Name = "@alergy_medicine")]
        public string alergy_medicine { get; set; }

        [SqlKey(Name = "@warning")]
        public string warning { get; set; }

        [SqlKey(Name = "@other")]
        public string  other { get; set; }

        [SqlKey(Name = "@is_pregnent")]
        public int  is_pregnent { get; set; }

        [SqlKey(Name = "@delivery_date")]
        public string delivery_date { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }
    }

    public sealed class ForntendchronicVital_Entity
    {
        [SqlKey(Name = "@mapping_id")]
        public int mapping_id { get; set; }
        [SqlKey(Name = "@brand_code")]
        public string brand_code { get; set;}
        [SqlKey(Name = "@frequency")]
        public string frequency { get; set; }
        [SqlKey(Name = "@medicine_startdate")]
        public string medicine_startdate { get; set; }
        [SqlKey(Name = "@remark")]
        public string remark { get; set; }
        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string formulation { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string strength { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
    }

    public sealed class Family_History_Entity
    {
        [SqlKey(Name = "@father_medical_history")]
        public string father_medical_history { get; set; }

        [SqlKey(Name = "@mother_medical_history")]
        public string mother_medical_history { get; set; }

        [SqlKey(Name = "@history_date")]
        public string history_date { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

    }

    public sealed class Vital_Master : BLayer
    {
        public Vital_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        //public string Add(Vitalpara_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "insert"));
        //    try
        //    {
        //        var rs = ApplyChanges("crd_vitalpara_master", param.ToArray(), Actions.Add);
        //        var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
        //        return result;
        //    }
        //    catch (DException) { return "0"; }
        //}

        public DataTable AddFrontendVital(ForntendVital_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertvitalpara"));
            try
            {
                var rs = SP_ResultSet("Hos_Patient_Status_History", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataTable AddFrontendChronicVital(ForntendchronicVital_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertchronicvitalpara"));
            try
            {
                var rs = SP_ResultSet("Hos_Patient_Status_chronic_History", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataTable AddFrontendFamilyHistory(Family_History_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertfamilyhistory"));
            try
            {
                var rs = SP_ResultSet("Hos_Patient_Status_Family_History", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataTable AddFrontendVitalOther(ForntendotherVital_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertothervitalpara"));
            try
            {
                var rs = SP_ResultSet("Hos_Patient_Status_other_History", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public DataTable AddPatientVital(patientvital_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertvitalpara"));
            try
            {
                var rs = SP_ResultSet("Add_patient_Vital", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }

        public string AddMainVitalPara(ForntendVital_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Add_VitalPara", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public bool DeleteVitalpara(string vitalid)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "vitalparadel");
            param[1] = new SqlParameter("@vitalpara_code", vitalid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Add_VitalPara", param.ToArray(), Actions.Delete);
                return param[3].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool DeleteVitalSurgery(int vitalid)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "delvitalsurgery");
            param[1] = new SqlParameter("@status_type_id", vitalid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Patient_Status_History", param.ToArray(), Actions.Delete);
                return param[3].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool DeleteVitalChronic(int chronicid)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "delvitalchronic");
            param[1] = new SqlParameter("@mapping_id", chronicid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Patient_Status_chronic_History", param.ToArray(), Actions.Delete);
                return param[3].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }



        //public string Update(Vitalpara_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "update"));
        //    try
        //    {
        //        ApplyChanges("crd_vitalpara_master", param.ToArray(), Actions.Update);
        //    }
        //    catch (DException) { return "0"; }
        //    return param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
        //}
        public string Delete(DataTable dt, string code)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@vitalpar_code", code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@dtlog", dt);
            try
            {
                var rs = ApplyChanges("crd_vitalpara_master", param.ToArray(), Actions.Delete);
                return  param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public DataTable GetAll_Update(string code)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@vitalpar_code", code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_vitalpara_master", param.ToArray());
            return rs.Tables[0];
        }

        public DataSet GetAll_PatientStatus(string patientcode,string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getall");
            param[1] = new SqlParameter("@patient_code", patientcode);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Status_History", param.ToArray());
            return rs;
        }

        public List<patient_status_Organs> GetPatientOrgans(int StatusNameId)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getorgans");
            param[1] = new SqlParameter("@status_name_id", StatusNameId);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<patient_status_Organs>("Hos_Patient_Status_History", param);
            return rs;
        }

        //public DataSet GetAll_vitalpara(string patientcode, string Appointmentcode, string hoscode)
        //{
        //    var param = new SqlParameter[5];
        //    param[0] = new SqlParameter("@action", "get");
        //    param[1] = new SqlParameter("@patient_code", patientcode);
        //    param[2] = new SqlParameter("@appointment_code", Appointmentcode);
        //    param[3] = new SqlParameter("@hos_code", hoscode);
        //    param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[4].Direction = ParameterDirection.Output;
        //    var rs = SP_ResultSet("Add_patient_Vital", param.ToArray());
        //    return rs;
        //}

        public DataTable GetAllVital(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_vital");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("drsp_patientvital", param);
            return rs.Tables[0];
        }

        public bool InsertTodayVital(string Appcode, string HOScode, string PatientCode, string Remarks, DataTable dt)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "addvital");
            param[1] = new SqlParameter("@Appcode", Appcode);
            param[2] = new SqlParameter("@hoscode", HOScode);
            param[3] = new SqlParameter("@PatientCode", PatientCode);
            param[4] = new SqlParameter("@Remarks", Remarks);
            param[5] = new SqlParameter("@dt_vital", dt);
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            ApplyChanges("drsp_patientvital", param.ToArray(), Actions.Add);
            var res = Convert.ToInt16(param[6].Value) > 0 ? true : false;
            return res;
        }

        //public Vitalpara_Entity GetByCode(string pharmacode)
        //{
        //    var param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@action", "get");
        //    param[1] = new SqlParameter("@phcode", pharmacode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    var rs = SP_Read<Vitalpara_Entity>("crd_vitalpara_master", param).SingleOrDefault();
        //    return rs;
        //}
    }
}
