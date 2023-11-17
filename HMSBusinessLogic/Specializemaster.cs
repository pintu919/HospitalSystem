using HMS.BizLayer;
using HMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class Specialize_Entity
    {
        [SqlKey(Name = "@Spcode", Direction = SqlDirect.InOut, SqlType = SqlDbType.VarChar, Size = 10)]
        public string specialize_code { get; set; }
        [SqlKey(Name = "@Spname")]
        public string specialization { get; set; }
        [SqlKey(Name = "@ctrl")]
        public int Ctrl { get; set; }
        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }
        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int specialize_id { get; set; }
    }
    public sealed class Specialize_Master : BLayer
    {
        public Specialize_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(Specialize_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_specialize_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public bool Update(Specialize_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_specialize_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public string Delete(string Specializecode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@Spcode", Specializecode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("crd_specialize_master", param.ToArray(), Actions.Delete);
                return  param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public List<Specialize_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<Specialize_Entity>("crd_specialize_master", param);
            return rs;
        }
        public Specialize_Entity GetByCode(string Specializecode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@Spcode", Specializecode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Specialize_Entity>("crd_specialize_master", param).SingleOrDefault();
            return rs;
        }
    }
}
