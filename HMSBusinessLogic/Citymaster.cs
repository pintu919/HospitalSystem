using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class City_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string statename { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string district { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string state_code { get; set; }

        [SqlKey(Name = "@ctcode", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string city_code { get; set; }

        [SqlKey(Name = "@dcode")]
        public string district_code { get; set; }

        [SqlKey(Name = "@ctname")]
        public string cityname { get; set; }

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
        public int city_id { get; set; }
    }

    public sealed class City_Master : BLayer
    {
        public City_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(City_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_city_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString():param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                //if (result)
                //{
                //    entity.city_code = param.Find(f => f.ParameterName.ToLower() == "@ctcode").Value.ToString();
                //}
                return result;
            }
            catch (DException) { return "0"; }
        }

        public bool Update(City_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_city_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public bool Delete(string Citycode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@ctcode", Citycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[3].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_city_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public List<City_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<City_Entity>("crd_city_master", param);
            return rs;
        }

        public DataSet GetByCode(string Citycode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@ctcode", Citycode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            //var rs = SP_Read<City_Entity>("crd_city_master", param).SingleOrDefault();
            var rs = SP_ResultSet("crd_city_master", param);
            return rs;
        }
    }
}
