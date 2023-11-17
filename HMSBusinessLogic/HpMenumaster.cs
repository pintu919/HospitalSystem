using HMS.BizLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class HpMenu_Entity
    {
        [SqlKey(Name = "@hp_menuid", Direction = SqlDirect.InOut)]
        public int hp_menuid { get; set; }

        [SqlKey(Name = "@hp_menuname")]
        public string hp_menuname { get; set; }

        [SqlKey(Name = "@hp_keyword")]
        public string hp_keyword { get; set; }

        [SqlKey(Name = "@hp_pagename")]
        public string hp_pagename { get; set; }

        [SqlKey(Name = "@hp_actionname")]
        public string hp_actionname { get; set; }

        [SqlKey(Name = "@hp_seqno")]
        public int hp_seqno { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@row", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@dtlog", SqlType = SqlDbType.Structured)]
        public DataTable dtlogtable { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool IsSelected { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string hp_main_menu_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int seq_no { get; set; }
    }
    public sealed class HpMenu_master : BLayer
    {
        public HpMenu_master (BsInfo info) : base(info) { }

        public string Add(HpMenu_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("crd_hp_menu_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public bool Update(HpMenu_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            ApplyChanges("crd_hp_menu_master", param.ToArray(), Actions.Update);
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public string Delete(DataTable dt,int Id)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@hp_menuid", Id);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@statusmessage", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            param[5] = new SqlParameter("@dtlog", dt);
            try
            {
                var rs = ApplyChanges("crd_hp_menu_master", param.ToArray(), Actions.Delete);
                return param[3].Value.ToString() == "notallow" ? param[3].Value.ToString() : param[4].Value.ToString();
            }
            catch (DException) { return "0"; }
        }

        public HpMenu_Entity GetByCode(int Id)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hp_menuid", Id);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<HpMenu_Entity>("crd_hp_menu_master", param).SingleOrDefault();
            return rs;
        }
        public List<HpMenu_Entity> GetAll()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_Read<HpMenu_Entity>("crd_hp_menu_master", param);
            return rs;
        }
    }
    //class HpMenumaster
    //{
    //}
}
