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
    public sealed class Usergroup_Entity
    {
        [SqlKey(Name = "@usergroup_id", Direction = SqlDirect.InOut)]
        public Int64 usergroup_id { get; set; }

        [SqlKey(Name = "@groupname")]
        public string groupname { get; set; }  
        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }
        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string rowno { get; set; }
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }
        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }
        [SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        public DataTable dtlogtable { get; set; }
    }

    public sealed class Usergroup_Master : BLayer
    {
        public Usergroup_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(Usergroup_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_usergroup", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public bool Update(Usergroup_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_usergroup", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public string Delete(DataTable dt, int groupid)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@usergroup_id", groupid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@dtlog", dt);
            try
            {
                var rs = ApplyChanges("crd_usergroup", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public List<Usergroup_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<Usergroup_Entity>("crd_usergroup", param);
            return rs;
        }

        public Usergroup_Entity GetByCode(int groupid)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@usergroup_id", groupid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<Usergroup_Entity>("crd_usergroup", param).SingleOrDefault();
            return rs;
        }
    }
}
