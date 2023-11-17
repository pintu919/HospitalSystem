using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class State_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string country_name { get; set; }

        [SqlKey(Name = "@ccode")]
        public string country_code { get; set; }

        [SqlKey(Name = "@sname")]
        public string statename { get; set; }

        [SqlKey(Name = "@scode", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string state_code { get; set; }
        
        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string rowno{ get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int stateid { get; set; }
    }

   public sealed class State_Master: BLayer
    {
        public State_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(State_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_state_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString():param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public bool Update(State_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_state_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Statecode)
        {            
            var param = new SqlParameter[5];
            param[0]= new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@scode", Statecode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_state_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public List<State_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<State_Entity>("crd_state_master", param);
            return rs;
        }
        public State_Entity GetByCode(string statecode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@scode", statecode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<State_Entity>("crd_state_master", param).SingleOrDefault();
            return rs;
        }
    }
}
