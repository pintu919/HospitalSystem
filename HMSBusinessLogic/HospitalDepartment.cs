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
    public sealed class HospitalDepartment_Entity
    {
        [SqlKey(Name = "@hosdecode", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string Hos_department_code { get; set; }
        [SqlKey(Name = "@hosdename")]
        public string Hos_department_name { get; set; }

        [SqlKey(Name = "@hoscode")]
        public string Hosiptal_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@decode")]
        public string department_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid row { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Hos_department_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsSelected { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string employee_code { get; set; }

    }

    public sealed class HospitalDepartment : BLayer
    {
        public HospitalDepartment(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(HospitalDepartment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Department", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException ex) { return "0"; }
        }
        public bool Update(HospitalDepartment_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("Hos_Department", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Depcode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@decode", Depcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Department", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public List<HospitalDepartment_Entity> GetAll( string Hoscode = "")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<HospitalDepartment_Entity>("Hos_Department", param);
            return rs;
        }

        //public List<HospitalDepartment_Entity> GetAllActive(string Hoscode = "")
        //{
        //    var param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@action", "recordactive");
        //    param[1] = new SqlParameter("@hoscode", Hoscode);
        //    param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
        //    param[2].Direction = ParameterDirection.Output;
        //    var rs = SP_Read<HospitalDepartment_Entity>("Hos_Department", param);
        //    return rs;
        //}

        public DataSet GetAllActiveDept_Role(string Hoscode = "")
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "recordactive");
            param[1] = new SqlParameter("@hoscode", Hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Department", param);
            return rs;
        }

        public DataSet GetByCode(string HosCode,string Depcode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@decode", Depcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Department", param);
            return rs;
            
        }

        public bool HospitalDepDeleteID(string departmentcode, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "hosdepdel");
            param[1] = new SqlParameter("@decode", departmentcode);
            param[2] = new SqlParameter("@hoscode", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Department", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

      
    }
}
