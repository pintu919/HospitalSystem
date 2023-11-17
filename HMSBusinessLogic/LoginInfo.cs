using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.BizLayer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace HMS.BizLogic
{
    public sealed class logininfo_Entity
    {

        [SqlKey(Name = "@username")]
        public string clinic_username { get; set; }

        [SqlKey(Name = "@uname_enc")]
        public string clinic_password { get; set; }

        [SqlKey(Name = "@cliniq_email")]
        public string Cliniq_email { get; set; }

        [SqlKey(Name = "@Cliniq_Code")]
        public string Cliniq_Code { get; set; }

        [SqlKey(Name = "@Cliniq_oldpass")]
        public string Cliniq_oldpass { get; set; }

        [SqlKey(Name = "@Cliniq_Newpass")]
        public string Cliniq_Newpass { get; set; }

        [SqlKey(Name = "@hosting_url")]
        public string hosting_url { get; set; }

        [SqlKey(Name = "@ctrl")]
        public string ctrl { get; set; }
        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }
        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }


        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Cliniq_ID { get; set; }

        [SqlKey(Name = "@clinic_logo", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string clinic_logo { get; set; }

        [SqlKey(Name = "@clinic_feviconicon", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string clinic_feviconicon { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Cliniq_Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Cliniq_Addr { get; set; }



    }

    public sealed class login_info : BLayer
    {
        public login_info(BsInfo info) : base(info) { }
        public bool Add(logininfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));

            var rs = ApplyChanges("crd_user_info", param.ToArray(), Actions.Add);
            var result = (param.Find(f => f.ParameterName == "@returnValue").Value.ToInt() > 0) ? true : false;
            if (result)
            {
                //entity.country_code = param.Find(f => f.ParameterName.ToLower() == "@uniqcode").Value.ToString();
                entity.Cliniq_ID = param.Find(f => f.ParameterName.ToLower() == "@Cliniq_ID").Value.ToInt();
            }
            return result;
        }
        public bool Update(logininfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));

            ApplyChanges("hos_login_info", param.ToArray(), Actions.Update);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public bool ChangePass(logininfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "changepass"));

            ApplyChanges("hos_login_info", param.ToArray(), Actions.Update);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public Email_Entity getchangepassword(string cliniccode, string password)
        {

            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getchangepassword");
            param[1] = new SqlParameter("@Cliniq_Code", cliniccode);
            param[2] = new SqlParameter("@Cliniq_Newpass", password);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Email_Entity>("hos_login_info", param.ToArray()).SingleOrDefault();
            return rs;
        }
        public bool Delete(logininfo_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "delete"));
            var rs = ApplyChanges("crd_user_info", param.ToArray(), Actions.Delete);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
        public int Auth(logininfo_Entity logininfo)
        {
            var param = ToSqlParams(logininfo);
            param.Add(new SqlParameter("@action", "auth"));

            var rs = SP_Exc("hos_login_info", param.ToArray());
            return param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt();

        }


        //public logininfo_Entity getDetailsByUser(logininfo_Entity userinfo)
        //{
        //    var param = ToSqlParams(userinfo);
        //    param.Add(new SqlParameter("@action", "record by uname"));
        //    var rs = SP_Read<logininfo_Entity>("hos_login_info", param.ToArray()).SingleOrDefault();
        //    return rs;

        //}

        public DataSet getDetailsByUser(logininfo_Entity userinfo)
        {
            var param = ToSqlParams(userinfo);
            param.Add(new SqlParameter("@action", "record by uname"));
            var rs = SP_ResultSet("hos_login_info", param.ToArray());
            return rs;
        }

        public Email_Entity getforgetpass(string emailaddress, string password)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getforgetpass");
            param[1] = new SqlParameter("@cliniq_email", emailaddress);
            param[2] = new SqlParameter("@uname_enc", password);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<Email_Entity>("hos_login_info", param.ToArray()).SingleOrDefault();
            return rs;

        }

        public bool AuthForget(logininfo_Entity logininfo)
        {
            var param = ToSqlParams(logininfo);
            param.Add(new SqlParameter("@action", "authForget"));

            var rs = SP_Exc("hos_login_info", param.ToArray());
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;

        }

        public bool AuthChangePass(logininfo_Entity logininfo)
        {
            var param = ToSqlParams(logininfo);
            param.Add(new SqlParameter("@action", "authchangePass"));

            var rs = SP_Exc("hos_login_info", param.ToArray());
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;

        }


    }
}
