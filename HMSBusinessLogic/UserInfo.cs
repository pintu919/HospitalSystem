using HMS.BizLayer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class Userinfo_Entity
    {
        [SqlKey(Name = "@logno", Direction = SqlDirect.InOut)]
        public int logno { get; set; }
        [SqlKey(Name = "@uname")]
        public string uname { get; set; }
        [SqlKey(Name = "@uname_enc")]
        public string uname_enc { get; set; }
        [SqlKey(Name = "@ctrl")]
        public string ctrl { get; set; }
        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }
        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }
    }
    public sealed class user_info : BLayer
    {
        public user_info(BsInfo info) : base(info) { }
        public bool Add(Userinfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            
            var rs = ApplyChanges("crd_user_info", param.ToArray(), Actions.Add);
            var result = (param.Find(f => f.ParameterName == "@returnValue").Value.ToInt() > 0) ? true : false;
            if (result)
            {
                //entity.country_code = param.Find(f => f.ParameterName.ToLower() == "@uniqcode").Value.ToString();
                entity.logno = param.Find(f => f.ParameterName.ToLower() == "@logno").Value.ToInt();
            }
            return result;
        }
        public bool Update(Userinfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));

            ApplyChanges("crd_user_info", param.ToArray(), Actions.Update);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public bool Delete(Userinfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "delete"));
            var rs = ApplyChanges("crd_user_info", param.ToArray(), Actions.Delete);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        //public bool Auth(Userinfo_Entity userinfo)
        //{
        //    var param = ToSqlParams(userinfo);
        //    param.Add(new SqlParameter("@action", "auth"));

        //    var rs = SP_Exc("crd_user_info", param.ToArray()); 
        //    return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;

        //}
        public Userinfo_Entity getDetailsByUser(Userinfo_Entity userinfo)
        {
            var param = ToSqlParams(userinfo);
            param.Add(new SqlParameter("@action", "record by uname"));
            var rs = SP_Read<Userinfo_Entity>("crd_user_info", param.ToArray()).SingleOrDefault();
            return rs;
            //var param = new SqlParameter[2];
            //param[0] = new SqlParameter("@action", "record by uname");
            //param[1] = new SqlParameter("@uname", uname);
            //var rs = SP_Read<Userinfo_Entity>("crd_user_info", param).SingleOrDefault();
            //return rs;
        }
    }
}
