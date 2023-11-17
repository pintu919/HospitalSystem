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
    public sealed class CancelAppintment_Entity
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

        [SqlKey(Name = "@is_refund", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int is_refund { get; set; }

        [SqlKey(Name = "@patient_age")]
        public int patient_age { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }
     
        [SqlKey(Name = "@cancelreason")]
        public string cancelreason { get; set; }

    


    }

    public sealed class CanelAppointment_Master : BLayer
    {
        public CanelAppointment_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }


        public DataSet GetAllCancelAppointment(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_CancelAppointment", param);
            return rs;

        }

        public DataSet GetAllbulkCancelAppointment(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getbulk");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_CancelAppointment", param);
            return rs;

        }

        public DataSet GetAllBulkAppointfilter(string HosCode, DateTime? fromdate, DateTime? todate, string hosdoccode, string appointment)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "getbulk");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@fromdate", fromdate);
            param[3] = new SqlParameter("@todate", todate);
            param[4] = new SqlParameter("@hos_doccode", hosdoccode);
            param[5] = new SqlParameter("@appointment_type", appointment);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_CancelAppointment", param);
            return rs;

        }

        public bool CancelBulkAppointment(DataTable dt, string hoscode, string usercode, string cancelreason)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "update");
            param[1] = new SqlParameter("@dt", dt);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@cancelreason", cancelreason);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_CancelAppointment", param.ToArray(), Actions.Add);
            return param[6].Value.ToInt() > 0 ? true : false;
        }

    }

    }
