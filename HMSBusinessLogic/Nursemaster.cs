using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class Nurse_Entity
    {
        [SqlKey(Name = "@nurse_station_name")]
        public string nurse_station_name { get; set; }

        [SqlKey(Name = "@hoscode")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Hos_department_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@dtMenutable", SqlType = SqlDbType.Structured)]
        public DataTable Type_DepartmentMapping { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }


        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private int Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@station_id")]
        public int station_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string message { get; set; }
    }
    public sealed class Nurse_Master : BLayer
    {
        public Nurse_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        public DataSet GetAllNurse( string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getallnursestation");
            param[1] = new SqlParameter("@hoscode", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Nurse_master", param);
            return rs;
        }
        public bool SaveRoleAcess(DataTable dt)
        {
            Nurse_Entity entity = new Nurse_Entity();

            entity.Type_DepartmentMapping = dt;

            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Nurse_master", param.ToArray(), Actions.Add);
                return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
            }
            catch (DException) { return false; }
        }
        public bool SetInvestigationPrice(int service_id, int Investigationid,decimal Amount,string Appointmentcode,int PriceId, string hoscode, string UserCode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "updateinvestigationprice");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@amount", Amount);
            param[4] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[5] = new SqlParameter("@priceid", PriceId);
            param[6] = new SqlParameter("@hoscode", hoscode);
            param[7] = new SqlParameter("@user_code", UserCode);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Nurse_master", param.ToArray(), Actions.Update);
                return param[8].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public bool SetInpatientInvPrice(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId, string hoscode, string UserCode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "updateinpatientinvprice");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@amount", Amount);
            param[4] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[5] = new SqlParameter("@priceid", PriceId);
            param[6] = new SqlParameter("@hoscode", hoscode);
            param[7] = new SqlParameter("@user_code", UserCode);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Nurse_master", param.ToArray(), Actions.Update);
                return param[8].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public DataTable Savenursestation(Nurse_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insertnursestation"));
            try
            {
                var rs = SP_ResultSet("Hos_Nurse_master", param.ToArray());
                return rs.Tables[0];
            }
            catch (DException) { return new DataSet().Tables[0]; }
        }
        public List<HospitalDepartment_Entity>GetAll(string Hoscode = "", int StationId=0)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "GetSelectedDept");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@station_id", StationId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<HospitalDepartment_Entity>("Hos_Nurse_master", param);
            return rs;
        }
        public DataSet GetAllRegesterAppoinmentLists(DateTime? fromdate =null, DateTime? todate=null, string Hoscode = "", int StationId = 0, string hosdepcode ="", string doccode="")
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "get_register_appointment_lists");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@station_id", StationId);
            param[3] = new SqlParameter("@hosdepcode", hosdepcode);
            param[4] = new SqlParameter("@appointmentcode", doccode);
            param[5] = new SqlParameter("@fromdate", fromdate);
            param[6] = new SqlParameter("@todate", todate);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Nurse_master", param);
            return rs;
        }
        public bool DeleteRegisterAppointment(string RegAppointmentcode, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "regappointdel");
            param[1] = new SqlParameter("@appointmentcode", RegAppointmentcode);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Nurse_master", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }
        public DataSet GetAllRegistrAppoint(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getregappoint");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Nurse_master", param);
            return rs;
        }
        public bool DeleteNurseStation(int stationid, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@station_id", stationid);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Nurse_master", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet GetAllOPDPatientDoctor(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getinpatientdoctor");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_inpatient", param);
            return rs;
        }
        public bool AddOPDPatientOTTransfer(string doctor_code, string operation_date, string operation_time, string operation_type, string Uniqrow, string HosCode, string usercode)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "addopdPatientottransfer");
            param[1] = new SqlParameter("@opddoctor_code", doctor_code);
            param[2] = new SqlParameter("@operation_date", operation_date);
            param[3] = new SqlParameter("@operation_time", operation_time);
            param[4] = new SqlParameter("@operation_type", operation_type);
            param[5] = new SqlParameter("@uniqrow", Uniqrow);
            param[6] = new SqlParameter("@hoscode", HosCode);
            param[7] = new SqlParameter("@usercode", usercode);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_OPD_Patient_Transfer_OT", param.ToArray(), Actions.Add);
                return param[9].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        #region Cancalation Investigation
        public int CancelOPDInvestigation(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId, string hoscode, string UserCode, string CancelReason)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "cancelopdinvestigation");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@amount", Amount);
            param[4] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[5] = new SqlParameter("@priceid", PriceId);
            param[6] = new SqlParameter("@hoscode", hoscode);
            param[7] = new SqlParameter("@user_code", UserCode);
            param[8] = new SqlParameter("@cancellation_reason", CancelReason);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Cancel_Investigation", param.ToArray(), Actions.Update);
                return param[10].Value.ToInt();
            }
            catch (DException) { return 0; }
        }

        public int CancelInvestigation(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId, string hoscode, string UserCode, string CancelReason)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "cancelinvestigation");
            param[1] = new SqlParameter("@service_id", service_id);
            param[2] = new SqlParameter("@investigation_id", Investigationid);
            param[3] = new SqlParameter("@amount", Amount);
            param[4] = new SqlParameter("@appointmentcode", Appointmentcode);
            param[5] = new SqlParameter("@priceid", PriceId);
            param[6] = new SqlParameter("@hoscode", hoscode);
            param[7] = new SqlParameter("@user_code", UserCode);
            param[8] = new SqlParameter("@cancellation_reason", CancelReason);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Cancel_Investigation", param.ToArray(), Actions.Update);
                return param[10].Value.ToInt() ;
            }
            catch (DException) { return 0; }
        }
        #endregion
    }
}
