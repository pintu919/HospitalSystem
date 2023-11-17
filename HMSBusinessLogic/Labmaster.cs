using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.BizLayer;
using System.Data;
using System.Data.SqlClient;
using HMS.Data;

namespace HMS.BizLogic
{
    public sealed class Hos_Purchase_Entity
    {

        [SqlKey(Name = "@reagent_id")]
        public int reagent_id { get; set; }

        [SqlKey(Name = "@qty")]
        public int qty { get; set; }

        [SqlKey(Name = "@reagent_name")]
        public string reagent_name { get; set; }

        [SqlKey(Name = "@product_expiry")]
        public string product_expiry { get; set; }

        [SqlKey(Name = "@qty_per_unit")]
        public int qty_per_unit { get; set; }

        [SqlKey(Name = "@amount")]
        public decimal amount { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Return { get; set; }

        [SqlKey(Name = "@statusmessage", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string status { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

    }

    public sealed class hos_purchaseItem_Inv
    {

        [SqlKey(Name = "@pi_code")]
        public string pi_code { get; set; }

        [SqlKey(Name = "@total_amount")]
        public decimal total_amount { get; set; }

        [SqlKey(Name = "@supplier")]
        public string supplier { get; set; }

        [SqlKey(Name = "@invoice_ref_no")]
        public string invoice_ref_no { get; set; }

        [SqlKey(Name = "@purchase_date")]
        public string purchase_date { get; set; }

        [SqlKey(Name = "@cdate")]
        public string cdate { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }



    }

    public sealed class RegentItem_Entity
    {
        [SqlKey(Name = "@reagent_id")]
        public int reagent_id { get; set; }

        [SqlKey(Name = "@reagent_name")]
        public string reagent_name { get; set; }
    }

    public sealed class TestItem_Entity
    {

        [SqlKey(Name = "@test_id")]
        public int test_id { get; set; }

        [SqlKey(Name = "@test_name")]
        public string test_name { get; set; }
    }

    public sealed class Lab_Master : BLayer
    {
        public Lab_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public string Add(Hos_Purchase_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "insert"));
            try
            {
                var rs = ApplyChanges("Hos_Purchase_data", param.ToArray(), Actions.Add);
                var result = param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() == "same" ? param.Find(f => f.ParameterName == "@statusmessage").Value.ToString() : param.Find(f => f.ParameterName == "@returnvalue").Value.ToString();
                return result;
            }
            catch (DException) { return "0"; }
        }

        public DataSet GetAllRegent()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "getregent");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

        public DataSet GetAllTestInv()
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@action", "gettestinv");
            param[1] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[1].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

        public DataSet GetRegentItemBytest(int testid)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getregentitemtestwise");
            param[1] = new SqlParameter("@reagent_id", testid);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

        public DataSet GetAllPurchaseItemInv( string hoscode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "getpurchaseiteminv");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

        public string SaveHosPurchase(DataTable dt, string Supplier, string InvoiceRefNo, string PurchaeDate, string picode, string hos_code, string usercode )
        {
            dt.Columns.Remove("Reg_List");
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "save_hospurchase");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@dt_purchase_item", dt);
            param[3] = new SqlParameter("@supplier", Supplier);
            param[4] = new SqlParameter("@invoicerefno", InvoiceRefNo);
            param[5] = new SqlParameter("@purchasedate", PurchaeDate);
            param[6] = new SqlParameter("@piinvcode", picode);
            param[7] = new SqlParameter("@user_code", usercode);
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            ApplyChanges("Hos_Purchase_data", param.ToArray(), Actions.Add);
            return Convert.ToInt16(param[8].Value) > 0 ? "1" : "0";
        }

        public bool Delete(string  invcode, string hoscode, string usercode)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "delete");
            param[1] = new SqlParameter("@invoicerefno", invcode);
            param[2] = new SqlParameter("@hos_code", hoscode);
            param[3] = new SqlParameter("@user_code", usercode);
            param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("Hos_Purchase_data", param.ToArray(), Actions.Delete);
                return param[5].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet getPurchasedataEdit(string hos_code, string InvCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_purchaseinvedit");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@invoicerefno", InvCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

        public DataSet getPurchasedatacode(string hos_code, string InvCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_purchaseinvView");
            param[1] = new SqlParameter("@hos_code", hos_code);
            param[2] = new SqlParameter("@invoicerefno", InvCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Purchase_data", param);
            return rs;
        }

    }

}
