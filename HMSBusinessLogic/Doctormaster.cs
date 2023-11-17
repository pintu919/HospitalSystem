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
    public sealed class Doctor_Entity
    {
        [SqlKey(Name = "@doctor_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string doctor_code { get; set; }

        [SqlKey(Name = "@doctor_name")]
        public string doctor_name { get; set; }

        [SqlKey(Name = "@doctor_designation")]
        public string doctor_designation { get; set; }

        [SqlKey(Name = "@spcode")]
        public string specialize_code { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@country_code")]
        public string country_code { get; set; }

        [SqlKey(Name = "@state_code")]
        public string state_code { get; set; }

        [SqlKey(Name = "@district_code")]
        public string district_code { get; set; }

        [SqlKey(Name = "@city_code")]
        public string city_code { get; set; }

        [SqlKey(Name = "@doctor_image")]
        public string doctor_image { get; set; }

        [SqlKey(Name = "@doctor_mobile")]
        public string doctor_mobile { get; set; }

        [SqlKey(Name = "@verification_code")]
        public string verification_code { get; set; }

        [SqlKey(Name = "@work_email")]
        public string work_email { get; set; }

        [SqlKey(Name = "@work_phone")]
        public string work_phone { get; set; }

        [SqlKey(Name = "@doctror_bmdc_reg_no")]
        public string doctror_bmdc_reg_no { get; set; }

        [SqlKey(Name = "@description_professional_statement")]
        public string description_professional_statement { get; set; }

        [SqlKey(Name = "@degree_other_specification")]
        public string degree_other_specification { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string row { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int doctor_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string qualification { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string occupation { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string address { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string specialization { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string cdate { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string department { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_DOB { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string institiute { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string state { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string city { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string distict { get; set; }
    }
    public sealed class DoctorContract_Entity
    {

        [SqlKey(Name = "@Visit_fees")]
        public decimal Visit_fees { get; set; }

        [SqlKey(Name = "@followup_fees")]
        public decimal followup_fees { get; set; }

        [SqlKey(Name = "@follow_policy")]
        public int follow_policy { get; set; }

        [SqlKey(Name = "@Online_apoint_allow")]
        public int Online_apoint_allow { get; set; }

        [SqlKey(Name = "@Appointmnt_slot")]
        public int Appointmnt_slot { get; set; }

        [SqlKey(Name = "@prefix")]
        public string prefix { get; set; }


        [SqlKey(Name = "@employee_id")]
        public int employee_id { get; set; }

        [SqlKey(Name = "@employee_code")]
        public string employee_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Name = "@contract_id", Direction = SqlDirect.InOut, SqlType = SqlDbType.Int, Size = 10)]
        public int contract_id { get; set; }

        [SqlKey(Name = "@doctor_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Name = "@department_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string department_name { get; set; }

        [SqlKey(Name = "@doctor_comission_id")]
        public int doctor_comission_id { get; set; }
    }

    public sealed class DoctorContractlist_Entity
    {
        [SqlKey(Name = "@employee_code")]
        public string employee_code { get; set; }

        [SqlKey(Name = "@employee_id")]
        public int employee_id { get; set; }

        [SqlKey(Name = "@doctor_code")]
        public string doctor_code { get; set; }

        [SqlKey(Name = "@doctor_name")]
        public string doctor_name { get; set; }
    }

    public sealed class Technician_Entity
    {
        [SqlKey(Name = "@EmployeeCode")]
        public string EmployeeCode { get; set; }

        [SqlKey(Name = "@EmployeeName")]
        public string EmployeeName { get; set; }
    }

    public sealed class DoctorEducation_Entity
    {
        [SqlKey(Name = "@degree")]
        public string degree { get; set; }
       
        [SqlKey(Name = "@institute")]
        public string institute { get; set; }

        [SqlKey(Name = "@passout_year")]
        public string passout_year { get; set; }
    }

    public sealed class DoctorExperience_Entity
    {
        [SqlKey(Name = "@hos_name")]
        public string hos_name { get; set; }

        [SqlKey(Name = "@from_date")]
        public string from_date { get; set; }

        [SqlKey(Name = "@to_date")]
        public string to_date { get; set; }

        [SqlKey(Name = "@designation")]
        public string designation { get; set; }
    }

    public sealed class DoctorSchedule_Entity {
       
        [SqlKey(Name = "@Day")]
        public string Day { get; set; }

        [SqlKey(Name = "@TimeFrom")]

        public TimeSpan TimeFrom { get; set; }

        [SqlKey(Name = "@TimeTo")]
        public TimeSpan TimeTo { get; set; }

        [SqlKey(Name = "@ContractId")]
        public int ContractId { get; set; }

        [SqlKey(Name = "@scheduleId")]
        public int scheduleId { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]

        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]

        private string Error { get; set; }

       
    }
    public sealed class DoctorLeaves_Entity
    {
        [SqlKey(Name = "@fromdate")]
        public string fromdate { get; set; }

        [SqlKey(Name = "@todate")]

        public string  todate { get; set; }

        [SqlKey(Name = "@leavedays")]
        public int leavedays { get; set; }

        [SqlKey(Name = "@leavetype")]
        public string leavetype { get; set; }

        [SqlKey(Name = "@contractid")]
        public int contractid { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]

        private string Error { get; set; }
    }
    public sealed class DoctorTimeSlot
    {
        [SqlKey(Name = "@TimeSlot")]
        public string TimeSlot { get; set; }
    }


    #region "Add Doctor Hospital Side"
    public sealed class Hospital_Doctor_Entity
    {
        [SqlKey(Name = "@doctor_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.NVarChar, Size = 200)]
        public string doctor_code { get; set; }

        [SqlKey(Name = "@doctor_name")]
        public string doctor_name { get; set; }

        [SqlKey(Name = "@doctor_designation")]
        public string doctor_designation { get; set; }

        [SqlKey(Name = "@spcode")]
        public string specialize_code { get; set; }

        [SqlKey(Name = "@depcode")]
        public string department_code { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@country_code")]
        public string country_code { get; set; }

        [SqlKey(Name = "@state_code")]
        public string state_code { get; set; }

        [SqlKey(Name = "@district_code")]
        public string district_code { get; set; }

        [SqlKey(Name = "@city_code")]
        public string city_code { get; set; }

        [SqlKey(Name = "@doctor_image")]
        public string doctor_image { get; set; }

        [SqlKey(Name = "@doctor_mobile")]
        public string doctor_mobile { get; set; }

        [SqlKey(Name = "@mobile_code")]
        public string mobile_code { get; set; }

        [SqlKey(Name = "@is_bmdc_verify")]
        public int is_bmdc_verify { get; set; }

        [SqlKey(Name = "@work_email")]
        public string work_email { get; set; }

        [SqlKey(Name = "@work_phone")]
        public string work_phone { get; set; }

        [SqlKey(Name = "@doctror_bmdc_reg_no")]
        public string doctror_bmdc_reg_no { get; set; }

        [SqlKey(Name = "@description_professional_statement")]
        public string occupation { get; set; }

        [SqlKey(Name = "@degree_other_specification")]
        public string qualification { get; set; }

        [SqlKey(Name = "@enc_password")]
        public string enc_password { get; set; }

        [SqlKey(Name = "@de_password")]
        public string de_password { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@hospital_code")]
        public string hospital_code { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string row { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int doctor_id { get; set; }

        [SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        public DataTable dtlogtable { get; set; }

    }
    #endregion



    public sealed class Doctor_Master : BLayer
    {
        public Doctor_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(Doctor_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_doctor_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public bool Update(Doctor_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_doctor_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Drcode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@doctor_code", Drcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_doctor_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }
        public List<Doctor_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<Doctor_Entity>("crd_doctor_master", param);
            return rs;
        }
        public DataSet GetByCode(string Drcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@doctor_code", Drcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("crd_doctor_master", param);
            return rs;
        }

        #region"Doctor Contract"
        public string AddDoctorContractData(DoctorContract_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_DoctorContract", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@statusmessage").Value.ToString()=="prefix"? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString(): param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException e) { return "0"; }
        }
        public bool UpdateDoctorContract(DoctorContract_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_DoctorContract", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        //public string DeleteDoctorContract(string EMPcode)
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
        //        var rs = ApplyChanges("Hos_DoctorContract", param.ToArray(), Actions.Delete);
        //        return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
        //    }
        //    catch (DException) { return "0"; }
        //}
        public bool DeleteDoctorContract(int contractId, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@contract_id",contractId);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorContract", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public DataSet GetAllDoctorContract(string Hoscode = "", string Emp_Code = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@employee_code", Emp_Code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;
        }
        public DataSet GetAllDoctorProfiledata(string Hoscode = "", string Emp_Code = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getdoctorprofile");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@employee_code", Emp_Code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;
        }
        //public List<DoctorContractlist_Entity>GetAllDoctorContractlist(string Hoscode = "")
        //{
        //    var param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@action", "getcontractdoctor");
        //    param[1] = new SqlParameter("@hos_code", Hoscode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    var rs = SP_Read<DoctorContractlist_Entity>("Hos_DoctorContract", param);
        //    return rs;
        //}

        public DataSet GetAllDoctorContractlist(string Hoscode = "")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getcontractdoctor");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;

        }
        public DataSet GetAllDoctorContractlst(string Hoscode = "")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getcontractdoctor");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;
        }
        public EmployeeTiming_Entity GetAllTiming(string Hoscode = "", string Emp_Code = "")
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", Hoscode);
            param[2] = new SqlParameter("@emp_code", Emp_Code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<EmployeeTiming_Entity>("Hos_EmployeeTiming", param).SingleOrDefault();
            return rs;
        }
        public DataSet GetRefDoctor(string hos_code)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getrefdoctor");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;
        }

        public DataSet AddDcotor(string doctor_code, string hos_code, string user_code)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "adddoctor");
            param[1] = new SqlParameter("@employee_code", doctor_code);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@user_code", user_code);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;

        }

        public bool DeleteDoctor(int EmployeeId, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "deletedoctor");
            param[1] = new SqlParameter("@employee_id", EmployeeId);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorContract", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        #endregion
        #region DoctorSchedule
        public string AddDoctorSchedule(DoctorSchedule_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_DoctorSchedule", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public string AddDoctorLeaves(DoctorLeaves_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_DoctorLeaves", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public List<DoctorSchedule_Entity> GetAllDoctorSchedule(int ContractId)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getschedule");
            param[1] = new SqlParameter("@contract_id", ContractId);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<DoctorSchedule_Entity>("Hos_DoctorContract", param);
            return rs;
        }
        public DoctorLeaves_Entity GetAllDoctorLeaves(int ContractId)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@contractid", ContractId);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<DoctorLeaves_Entity>("Hos_DoctorLeaves", param).SingleOrDefault(); ;
            return rs;
        }
        public DoctorSchedule_Entity GetAllDoctorScheduleID(int ScheduleId)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getreturn");
            param[1] = new SqlParameter("@ContractId", ScheduleId);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<DoctorSchedule_Entity>("Hos_DoctorSchedule", param).SingleOrDefault();
            return rs;
        }
        public bool UpdateDoctorschedule(DoctorSchedule_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_DoctorSchedule", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool DeleteSchedule(int Schedule, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@scheduleId", Schedule);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorSchedule", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public DataSet GetByCodeDoctorContract(string UniqCode, string Hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@employee_code", UniqCode);
            param[2] = new SqlParameter("@hos_code", Hoscode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorContract", param);
            return rs;
        }
        #endregion

        #region"Hospital Add Doctor"
        public DataSet HospitalDoctorRegister(Hospital_Doctor_Entity entity)
        {
            var rs = new DataSet();
            try
            {
                var param = ToSqlParams(entity);
                param.Add(new SqlParameter("@action", "insert"));
                rs = SP_ResultSet("hospital_doctor_registration", param.ToArray());
                return rs;
            }
            catch (Exception)
            {
                return rs;
               
            }
        }

        public DataSet Doctor_Verify(string UniqRow, string Type, string de_password, string enc_password)
        {
            var rs = new DataSet();
            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@action", "verifyhospitaldoctor");
                param[1] = new SqlParameter("@otp_id", 0);
                param[2] = new SqlParameter("@row", UniqRow);
                param[3] = new SqlParameter("@Type", Type);
                param[4] = new SqlParameter("@de_password", de_password);
                param[5] = new SqlParameter("@enc_password", enc_password);
                param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
                param[7].Direction = ParameterDirection.ReturnValue;
                rs = SP_ResultSet("crd_OTP_history", param);
                return rs;
            }
            catch (Exception ex)
            {
                return rs;
            }
        }
        #endregion

    }
}
