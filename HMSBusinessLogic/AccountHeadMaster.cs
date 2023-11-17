using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HMS.BizLogic
{
    public sealed class AccountHead_Entity
    {
        [SqlKey(Name = "@head_name")]
        public string head_name { get; set; }

        [SqlKey(Name = "@parent_id")]
        public int parent_id { get; set; }

        [SqlKey(Name = "@show")]
        public int show { get; set; }

        [SqlKey(Name = "@head_type")]
        public string head_type { get; set; }

        [SqlKey(Name = "@is_editable")]
        public int is_editable { get; set; }

        [SqlKey(Name = "@donotshowtoall")]
        public int donotshowtoall { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }



    }
    public class HeadCategory
    {
        [SqlKey(Name = "@head_id")]
        public string head_id { get; set; }

        [SqlKey(Name = "@head_name")]
        public string head_name { get; set; }

        [SqlKey(Name = "@icon")]
        public string icon { get; set; }


        [SqlKey(Name = "@parent_id")]
        public string parent_id { get; set; }
    }

    public class TreeNode
    {
        [SqlKey(Name = "@head_id")]
        public int head_id { get; set; }

        [SqlKey(Name = "@head_name")]
        public string head_name { get; set; }
        public TreeNode ParentCategory { get; set; }
        public List<TreeNode> Children { get; set; }
    }

    public sealed class AccountLedger_Entity
    {
        [SqlKey(Name = "@acc_head_id")]
        public int acc_head_id { get; set; }

        [SqlKey(Name = "@acc_name")]
        public string acc_name { get; set; }

        [SqlKey(Name = "@special_account")]
        public string special_account { get; set; }

        [SqlKey(Name = "@contact_person")]
        public string contact_person { get; set; }

        [SqlKey(Name = "@telephone")]
        public string telephone { get; set; }

        [SqlKey(Name = "@mobile")]
        public string mobile { get; set; }

        [SqlKey(Name = "@email")]
        public string email { get; set; }

        [SqlKey(Name = "@is_show")]
        public int is_show { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@user_code")]
        public string user_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@acc_id")]
        public int acc_id { get; set; }

    }

    public sealed class AccountHeadType_Entity
    {
        [SqlKey(Name = "@head_name")]
        public string head_name { get; set; }

        [SqlKey(Name = "@parent_id")]
        public int parent_id { get; set; }

        [SqlKey(Name = "@head_id")]
        public int head_id { get; set; }

        [SqlKey(Name = "@head_type")]
        public string head_type { get; set; }

        [SqlKey(Name = "@is_editable")]
        public int is_editable { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

    }


    public sealed class AccountHead_Master : BLayer
    {
        public AccountHead_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(AccountHead_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("account_head_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public List<AccountHeadType_Entity> GetHeadTypeList(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "record");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<AccountHeadType_Entity>("account_head_master", param);
            return rs;
        }
        public DataSet GetHeadType(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_head");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param); //SP_Read<AccountHeadType_Entity>("account_head_master", param);
            return rs;
        }
        public DataSet GetHeadTypeByCode(int HeadId, string hos_code)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getbycode");
            param[1] = new SqlParameter("@parent_id", HeadId);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param);
            return rs;
        }
        public bool Update(AccountHead_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("account_head_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

       

        public List<AccountHeadType_Entity> GetAllHeadTypeList( string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<AccountHeadType_Entity>("account_head_master", param);
            return rs;
        }
        public string AddLedger(AccountLedger_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("account_ledger_master", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }
        public List<AccountLedger_Entity> GetAllLedgerAccount( string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_Read<AccountLedger_Entity>("account_ledger_master", param);
            return rs;
        }

        public DataSet GetLedgerTypeByCode(int Accid, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getbycode");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@acc_head_id", Accid);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_ledger_master", param);
            return rs;
        }

        public bool UpdateLedger(AccountLedger_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("account_ledger_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }

        public DataTable GetAllAccountHead(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getallhead");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param);
            return rs.Tables[0];
        }

        public DataTable GetAllAccountsubHead(string HosCode, int parent_id)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getallsubhead");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@parent_id", parent_id);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param);
            return rs.Tables[0];
        }
        public DataSet GetledgerAccount(string HosCode, int HeadId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_ledgeraccount");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@parent_id", HeadId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param); //SP_Read<AccountHeadType_Entity>("account_head_master", param);
            return rs;
        }

        public DataSet GetAccountAmount(int HeadFrom, int Ledgerfrom, string HosCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "get_account_amount");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@parent_id", HeadFrom);
            param[3] = new SqlParameter("@ledger_id", Ledgerfrom);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("account_head_master", param); //SP_Read<AccountHeadType_Entity>("account_head_master", param);
            return rs;
        }

        public DataSet save_journal_entry(string HosCode, string UserCode, int HeadIdFrom, int HeadIdTo, int LeadgerIdFrom, int LedgerIdTo, decimal Amount, out int Status)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "save_Journal_entry");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@HeadIdFrom", HeadIdFrom);
            param[4] = new SqlParameter("@HeadIdTo", HeadIdTo);
            param[5] = new SqlParameter("@LeadgerIdFrom", LeadgerIdFrom);
            param[6] = new SqlParameter("@LedgerIdTo", LedgerIdTo);
            param[7] = new SqlParameter("@Amount", Amount);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Sp_Journal_Entry", param);
            Status = Convert.ToInt16(param[9].Value);
            return rs;
        }
    }

}
