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
    public sealed class DocAppintment_Entity
    {
        [SqlKey(Name = "@appointment_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 150)]
        public string appointment_code { get; set; }

        [SqlKey(Name = "@appointment_type")]
        public string appointment_type { get; set; }

        [SqlKey(Name = "@appointment_date")]
        public string appointment_date { get; set; }

        [SqlKey(Name = "@appointmentfrom_time")]
        public string appointmentfrom_time { get; set; }

        [SqlKey(Name = "@hos_depcode")]
        public string hos_depcode { get; set; }

        [SqlKey(Name = "@hos_doccode")]
        public string hos_doccode { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@is_appointment")]
        public bool is_appointment { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsVisited { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@appointment_id")]
        public int appointment_id { get; set; }

        [SqlKey(Name = "@patient_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        //[SqlKey(Name = "@patient_age", SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public int patient_age { get; set; }

        [SqlKey(Name = "@patient_profile", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_profile { get; set; }

        [SqlKey(Name = "@doctor_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Name = "@department_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string department_name { get; set; }

        [SqlKey(Name = "@appointment_time", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string appointment_time { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@is_non_register_patient")]
        public int is_non_register_patient { get; set; }

        [SqlKey(Name = "@pateint_firstname")]
        public string pateint_firstname { get; set; }

        [SqlKey(Name = "@patient_lastename")]
        public string patient_lastename { get; set; }

        [SqlKey(Name = "@patient_fathername")]
        public string patient_fathername { get; set; }

        [SqlKey(Name = "@patient_mothername")]
        public string patient_mothername { get; set; }
        
        [SqlKey(Name = "@blood_group")]
        public string blood_group { get; set; }

        [SqlKey(Name = "@patient_id_type")]
        public string patient_id_type { get; set; }

        [SqlKey(Name = "@nid_birthregno")]
        public string nid_birthregno { get; set; }

        [SqlKey(Name = "@patient_dob")]
        public string patient_dob { get; set; }

        [SqlKey(Name = "@patient_age")]
        public string patient_age { get; set; }

        [SqlKey(Name = "@patient_email")]
        public string patient_email { get; set; }

        [SqlKey(Name = "@pateint_mobile")]
        public string pateint_mobile { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@marital_status")]
        public string marital_status { get; set; }

        [SqlKey(Name = "@patient_address")]
        public string patient_address { get; set; }

        [SqlKey(Name = "@country_code")]
        public string country_code { get; set; }

        [SqlKey(Name = "@state_code")]
        public string state_code { get; set; }

        [SqlKey(Name = "@dist_code")]
        public string dist_code { get; set; }

        [SqlKey(Name = "@city_code")]
        public string city_code { get; set; }

        [SqlKey(Name = "@relation")]
        public string relation { get; set; }

        [SqlKey(Name = "@photo")]
        public string photo { get; set; }

        [SqlKey(Name = "@patient_spousename")]
        public string patient_spousename { get; set; }

        [SqlKey(Name = "@patient_professions")]
        public string patient_professions { get; set; }

        [SqlKey(Name = "@cancelreason")]
        public string cancelreason { get; set; }

        [SqlKey(Name = "@prefix", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string prefix { get; set; }

        [SqlKey(Name = "@isstatus", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isstatus { get; set; }

        [SqlKey(Name = "@isapprove", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isapprove { get; set; }

        [SqlKey(Name = "@appointment_datetime", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public DateTime appointment_datetime { get; set; }

        [SqlKey(Name = "@IsSelected")]
        public bool IsSelected { get; set; }

        [SqlKey(Name = "@patuniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid patuniqrow { get; set; }

        [SqlKey(Name = "@otstatus", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int otstatus { get; set; }

        [SqlKey(Name = "@otcomplete", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int otcomplete { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int service_id { get; set; }

        [SqlKey(Name = "@comission_agent_id")]
        public int comission_agent_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsFullPaid { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AllowTestReportEmailWithoutPayment { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Vital { get; set; }

        [SqlKey(Name = "@zip")]
        public string zip { get; set; }





    }

    public class TdyVital
    {
        [SqlKey(Display = true)]
        public string VitalCode { get; set; }
        [SqlKey(Display = true)]
        public string VitalName { get; set; }

        [SqlKey(Display = true)]
        public string VitalValue { get; set; }

        [SqlKey(Display = true)]
        public string VitalUnit { get; set; }
    }
    public class TodayVital
    {
        //public decimal BodyTemperature { get; set; }
        //public string BloodPressure { get; set; }
        //public decimal Weight { get; set; }
        //public decimal Sugar { get; set; }
        //public decimal OxygenLevel { get; set; }
        //public int Heartrate { get; set; }

        [SqlKey(Display = true)]
        public List<TdyVital> VitalList { get; set; }
        [SqlKey(Display = true)]
        public string Remarks { get; set; }
        [SqlKey(Display = true)]
        public string PatientCode { get; set; }
        [SqlKey(Display = true)]
        public string HOScode { get; set; }
        [SqlKey(Display = true)]
        public string Appointmentcode { get; set; }
        [SqlKey(Display = true)]
        public string Status { get; set; }
    }

    public sealed class OP_investigation_Entity
    {
        [SqlKey(Name = "@invoice_code", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 150)]
        public string invoice_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]

        private string Error { get; set; }

        [SqlKey(Name = "@patient_name", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        [SqlKey(Name = "@patient_profile", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_profile { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@patient_age")]
        public string patient_age { get; set; }

        [SqlKey(Name = "@pateint_mobile")]
        public string pateint_mobile { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@isstatus", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isstatus { get; set; }

        [SqlKey(Name = "@isapprove", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int isapprove { get; set; }

        [SqlKey(Name = "@cdate", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public DateTime cdate { get; set; }

        [SqlKey(Name = "@IsSelected", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsSelected { get; set; }

        [SqlKey(Name = "@patuniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid patuniqrow { get; set; }

        [SqlKey(Name = "@inv_date", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string inv_date { get; set; }

        [SqlKey(Name = "@referal_doctor", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string referal_doctor { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int IsFullPaid { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AllowTestReportEmailWithoutPayment { get; set; }



    }
    public sealed class avalday
    {
        public List<AvailabalDays> days { get; set; }
        public List<LeaveDate> date { get; set; }
    }
    public sealed class AvailabalDays
    {
        public int daynumber { get; set; }
    }
    public sealed class LeaveDate
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
    }

    public class ComissionAgent_Entity
    {
        [SqlKey(Display = true)]
        public string Comission_Agent_Id { get; set; }

        [SqlKey(Display = true)]
        public string Commision_Agent { get; set; }
    }
    public sealed class DocAppointment_Master : BLayer
    {
        public DocAppointment_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(DocAppintment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_DoctorAppointment", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        //public string AddNonRegPat(DocAppintment_Entity entity)
        //{
        //    var param = ToSqlParams(entity);
        //    param.Add(new SqlParameter("@action", "insert"));
        //    try
        //    {
        //        var rs = ApplyChanges("Hos_NonReg_pat_DoctorAppointment", param.ToArray(), Actions.Add);
        //        var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" || param.Find(f => f.ParameterName == "@statusmessage").Value.ToString().Split('-')[0] == "PAT" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
        //        return result;
        //    }
        //    catch (DException) { return "0"; }
        //}

        public DataTable AddNonRegPat(DocAppintment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = SP_ResultSet("Hos_NonReg_pat_DoctorAppointment_v1", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }



        public DataTable AddNonRegPatwalking(DocAppintment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = SP_ResultSet("Hos_NonReg_pat_WalkingDoctorAppointment", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }
        public bool Update(DocAppintment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_DoctorAppointment", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public bool Delete(int AppointmentId, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@appointment_id", AppointmentId);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorAppointment", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool Register(int AppointmentId, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "register");
            param[1] = new SqlParameter("@appointment_id", AppointmentId);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorAppointment", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet GetByCode(string Appointmentcode, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@appointment_code", Appointmentcode);
            param[2] = new SqlParameter("@hos_code", HosCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;
        }
        //public List<DocAppintment_Entity> GetAllAppoint(string HosCode)
        //{
        //    var param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@action", "get");
        //    param[1] = new SqlParameter("@hos_code", HosCode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    var rs = SP_Read<DocAppintment_Entity>("Hos_DoctorAppointment", param);
        //    return rs;

        //}

        public DataSet GetAllAppoint(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;
        }
        public DataSet GetAllAppointNew(string HosCode, DateTime? fromdate, DateTime? todate, string hosdoccode, string appointment)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@hos_doccode", hosdoccode);
            param[5] = new SqlParameter("@appointment_type", appointment);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;
        }
        public List<Doctor_Entity> GetByCodeDoctor(string HosCode, string HosDepCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getdoctor");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@hos_depcode", HosDepCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Doctor_Entity>("Hos_DoctorAppointment", param);
            return rs;

        }

        public List<Doctor_Entity> GetDocByCodeDoctor(string HosCode, string HosDepCode, string Day)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@hos_code", HosCode);
            param[1] = new SqlParameter("@hos_depcode", HosDepCode);
            param[2] = new SqlParameter("@day", Day);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Doctor_Entity>("Hos_doctor_available_datewise", param);
            return rs;

        }

        public DataTable GetAllPatient(string Prefix)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getpatient");
            param[1] = new SqlParameter("@patient_code", Prefix);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs.Tables[0];
        }

        public bool Checkpatient(string patient_code, string hos_code)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "checkpatient");
            param[1] = new SqlParameter("@patient_code", patient_code);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_DoctorAppointment", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }

        }

        public DataSet GetAlldropdownlist(string hosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getAlldrop");
            param[1] = new SqlParameter("@hos_code", hosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            //var rs = SP_Read<DocAppintment_Entity>("Hos_DoctorAppointment", param)
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;

        }

        public DataSet GetDoctorAvailabledays(string hosCode, string hosdoccode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@hos_code", hosCode);
            param[1] = new SqlParameter("@hos_doccode", hosdoccode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            //var rs = SP_Read<DocAppintment_Entity>("Hos_DoctorAppointment", param)
            var rs = SP_ResultSet("Hos_doctor_available_days", param);
            return rs;

        }

        public List<DoctorTimeSlot> GetDoctorTimeSlot(string HosCode, string hosdoccode, string Day, string AppointDate, string time, string AppointCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@hos_code", HosCode);
            param[1] = new SqlParameter("@hos_doccode", hosdoccode);
            param[2] = new SqlParameter("@day", Day);
            param[3] = new SqlParameter("@appointDate", AppointDate);
            param[4] = new SqlParameter("@time", time);
            param[5] = new SqlParameter("@appointCode", AppointCode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_Read<DoctorTimeSlot>("Hos_doctor_available_TimeSlot", param);
            return rs;

        }

        public DataSet GetRegPatientAppoinment(string hos_code, string fromdate, string todate, string patientcode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "visited");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@patient_code", patientcode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;
        }

        public DataSet GetPendingDoctorConfirmation(string hos_code)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "pendingconfirmation");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_DoctorAppointment", param);
            return rs;
        }

        public DataSet get_Print_html(string ApptCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_presc_html");
            param[1] = new SqlParameter("@appt_code", ApptCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("drsp_pricription", param);
            return rs;
        }




    }
}