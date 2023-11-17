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
    public sealed class PatientService_Entity
    {
        [SqlKey(Name = "@investigationmaster_id", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int[] investigationmaster_id { get; set; }

        [SqlKey(Name = "@investigation_ids")]
        public string investigation_ids { get; set; }

        [SqlKey(Name = "@patient_firstname")]
        public string patient_firstname { get; set; }

        [SqlKey(Name = "@patient_lastname")]
        public string patient_lastname { get; set; }

        [SqlKey(Name = "@dob")]
        public string dob { get; set; }

        [SqlKey(Name = "@age")]
        public int age { get; set; }

        [SqlKey(Name = "@gender")]
        public string gender { get; set; }

        [SqlKey(Name = "@email")]
        public string email { get; set; }

        [SqlKey(Name = "@mobile")]
        public string mobile { get; set; }

        [SqlKey(Name = "@address")]
        public string address { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

    }
    public sealed class PatientService_Master : BLayer
    {
        public PatientService_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }


        public string Add(PatientService_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Patient_Service_Data", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (Exception e) { return "0"; }
        }

        public DataSet GetOtherData( string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getotherdata");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Patient_Service_Data", param);
            return rs;
        }

    }

}
