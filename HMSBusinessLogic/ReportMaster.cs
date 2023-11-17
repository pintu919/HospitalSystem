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
    public sealed class Summary_Entity
    {
       
        [SqlKey(Name = "@Total")]
        public decimal Total { get; set; }

        [SqlKey(Name = "@Balance")]
        public decimal Balance { get; set; }

        [SqlKey(Name = "@acc_name")]
        public string acc_name { get; set; }

        [SqlKey(Name = "@acc_id")]
        public int acc_id { get; set; }


    }

    public sealed class Summary_accountwise_entity
    {
        [SqlKey(Name = "@acc_id")]
        public int acc_id { get; set; }

        [SqlKey(Name = "@acc_name")]
        public string acc_name { get; set; }

        [SqlKey(Name = "@amount")]
        public decimal amount { get; set; }

        [SqlKey(Name = "@journal_ref")]
        public string journal_ref { get; set; }

        [SqlKey(Name = "@invoice_code")]
        public string invoice_code { get; set; }

    }


    public sealed class JournalTransaction_Entity
    {
       

        [SqlKey(Name = "@Row_no")]
        public int Row_no { get; set; }

        [SqlKey(Name = "@journal_date")]
        public string journal_date { get; set; }

        [SqlKey(Name = "@acc_id")]
        public int acc_id { get; set; }

        [SqlKey(Name = "@dr_amount")]
        public decimal dr_amount { get; set; }

        [SqlKey(Name = "@cr_amount")]
        public decimal cr_amount { get; set; }

        [SqlKey(Name = "@balance")]
        public decimal balance { get; set; }

        [SqlKey(Name = "@invoice_id")]
        public int invoice_id { get; set; }

        [SqlKey(Name = "@payment_type")]
        public string payment_type { get; set; }

        [SqlKey(Name = "@invoice_code")]
        public string invoice_code { get; set; }

        [SqlKey(Name = "@MonthYear")]
        public string MonthYear { get; set; }

        [SqlKey(Name = "@journal_ref")]
        public string journal_ref { get; set; }


    }



    public sealed class Report_Master : BLayer
    {
        public Report_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

        public DataSet GetAll(string CliniqUniqrow = null,string fromdate =null, string todate =null, int accountheadid =0 )
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_summary");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@from_date", fromdate);
            param[3] = new SqlParameter("@to_date", todate);
            param[4] = new SqlParameter("@acc_id", accountheadid);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Report_Data", param);
            return rs;
        }

        public DataSet GetAllledger(string CliniqUniqrow = null)
        {

            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_ledger");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Report_Data", param);
            return rs;
        }
        public DataSet GetAlljournal(string CliniqUniqrow = null, string fromdate = null, string todate = null, int acc_id=0)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_journal");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@from_date", fromdate);
            param[3] = new SqlParameter("@to_date", todate);
            param[4] = new SqlParameter("@acc_id", acc_id);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Report_Data", param);
            return rs;
        }
        public DataSet GetReceiptList(string HosCode, string Fromdate, string Todate, string Type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_receipt_list");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", Fromdate);
            param[3] = new SqlParameter("@to_date", Todate);
            param[4] = new SqlParameter("@Type", Type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Report_Data", param);
            return rs;
        }

        #region "IndividualReport Prepare By Dhaval"
        public DataSet GetreportData(string action, string HosCode, DateTime? Fromdate, DateTime? ToDate, string Type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", action);
            param[1] = new SqlParameter("@HosCode", HosCode);
            param[2] = new SqlParameter("@fromdate", Fromdate);
            param[3] = new SqlParameter("@todate", ToDate);
            param[4] = new SqlParameter("@Type", Type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_individual_report", param);
            return rs;
        }
        #endregion

        #region "InvoiceList For Settalment"
        public DataSet GetSettalmentInvoiceAll(string CliniqUniqrow = null, string action = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", action);
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_settlementinvoice_data", param);
            return rs;
        }
        public DataSet IPInvoiceSettalment(int ServiceId, string ServiceType, string InvoiceCode, decimal Settal_Discount, string hoscode, string UserCode, string action)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", action);
            param[1] = new SqlParameter("@invoice_code", InvoiceCode);
            param[2] = new SqlParameter("@service_type", ServiceType);
            param[3] = new SqlParameter("@serviceid", ServiceId);
            param[4] = new SqlParameter("@Settal_Discount", Settal_Discount);
            param[5] = new SqlParameter("@hos_code", hoscode);
            param[6] = new SqlParameter("@user_code", UserCode);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_settlementinvoice_data", param);
            return rs;
        }
        #endregion

        #region "Profit Reports Prepare By Dhaval"
        public DataSet GetProfitreportData(string action, string HosCode, DateTime? Fromdate, DateTime? ToDate, string Type)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", action);
            param[1] = new SqlParameter("@HosCode", HosCode);
            param[2] = new SqlParameter("@fromdate", Fromdate);
            param[3] = new SqlParameter("@todate", ToDate);
            param[4] = new SqlParameter("@Type", Type);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("HOS_profit_report", param);
            return rs;
        }
        #endregion

    }
}
