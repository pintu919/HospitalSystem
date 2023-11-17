using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class District_Entity
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string statename { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_code { get; set; }

        [SqlKey(Name = "@dcode", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string district_code { get; set; }

        [SqlKey(Name = "@scode")]
        public string state_code { get; set; }

        [SqlKey(Name = "@dname")]
        public string district { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string rowno { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int dist_no { get; set; }
    }

    public sealed class District_Master : BLayer
    {
        public District_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(District_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_district_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString():param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public bool Update(District_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_district_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Districtcode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@dcode", Districtcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_district_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString()== "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public List<District_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<District_Entity>("crd_district_master", param);
            return rs;
        }
        public DataSet GetByCode(string Districtcode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@dcode", Districtcode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            //var rs = SP_Read<District_Entity>("crd_district_master", param).SingleOrDefault();
            var rs = SP_ResultSet("crd_district_master", param);
            return rs;
        }
    }
}
