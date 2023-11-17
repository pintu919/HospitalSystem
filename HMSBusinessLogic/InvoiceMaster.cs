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
    public sealed class Invoice_Entity
    {
        [SqlKey(Name = "@invoice_code")]
        public string invoice_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string doctor_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string patient_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string phone { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string cdate { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string print_date { get; set; }
              
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal discount_amount { get; set; }

        [SqlKey(Name = "@appoinment_code")]
        public string appoinment_code { get; set; }

        [SqlKey(Name = "@total_amount")]
        public decimal  total_amount { get; set; }

        [SqlKey(Name = "@total")]
        public decimal total { get; set; }

        [SqlKey(Name = "@paid_amount")]
        public decimal paid_amount { get; set; }

        [SqlKey(Name = "@refundamount")]
        public decimal refundamount { get; set; }

        [SqlKey(Name = "@balance")]
        public decimal balance { get; set; }


        [SqlKey(Name = "@panding_amount")]
        public decimal panding_amount { get; set; }

        [SqlKey(Name = "@payment_date")]
        public string payment_date { get; set; }

        [SqlKey(Name = "@due_date")]
        public string due_date { get; set; }

        [SqlKey(Name = "@uniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

        [SqlKey(Name = "@returnValue", Direction = SqlDirect.RETURN, Display = false)]
        private string Return { get; set; }

        [SqlKey(Name = "@err", Direction = SqlDirect.OUT, Display = false, SqlType = SqlDbType.VarChar, Size = 1000)]
        private string Error { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Name = "@invoice_id", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid invoice_id { get; set; }

        [SqlKey(Name = "@patuniqrow", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid patuniqrow { get; set; }

        [SqlKey(Name = "@UserName", SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string UserName { get; set; }


    }

    public class Journal_Entity
    {
        [SqlKey(Name = "@journal_ref")]
        public string journal_ref { get; set; }

        [SqlKey(Name = "@journal_date")]
        public string journal_date { get; set; }

        [SqlKey(Name = "@payment_type")]
        public string payment_type { get; set; }

        [SqlKey(Name = "@paymode")]
        public string paymode { get; set; }

        [SqlKey(Name = "@dr_amount")]
        public decimal dr_amount { get; set; }

    }

    public class Advance_Payment_Entity
    {
        [SqlKey(Name = "@Id")]
        public int Id { get; set; }

        [SqlKey(Name = "@patient_code")]
        public string patient_code { get; set; }

        [SqlKey(Name = "@advance_amount")]
        public decimal advance_amount { get; set; }

        [SqlKey(Name = "@payment_type")]
        public string payment_type { get; set; }

        [SqlKey(Name = "@payment_date")]
        public string payment_date { get; set; }

        [SqlKey(Name = "@invoice_code")]
        public string invoice_code { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }

    }

    public class Invoicepara
    {
        public List<IPDoctorVisit> opd_doctorvisit { get; set; }
        public List<IPInvestigation> opd_investigation { get; set; }
        public List<IPAdvanceServices> opd_advanceservices { get; set; }
        public List<IPOtServices> ipotservices { get; set; }

    }

    public class IPInvoicepara
    {
        public List<IPDoctorVisit> doctorvisit { get; set; }
        public List<IPInvestigation> ipinvestigation { get; set; }
        public List<IPAdvanceServices> ipadvanceservices { get; set; }
        public List<IPOtServices> ipotservices { get; set; }
        public List<IPMedicineList> IPMedList { get; set; }
        public List<IPAdmitRoom> IPAdmitRoomList { get; set; }

    }

    public class IPMedicineList
    {
      

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Brand_Name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string drug_generic_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string strength { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string formulationname { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Used { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Duration { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Dosages { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string medicine_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Decimal Price { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Decimal Discount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Decimal DiscountAmount { get; set; }


    }

    public class IPAdmitRoom
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_facility { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string room_no { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string bed_no { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ward_type { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AdmitDays { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Price { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Decimal Discount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Decimal DiscountAmount { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string admited_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Per_Day_rent { get; set; }

        [SqlKey(Name = "@serviceid")]
        public int serviceid { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public bool isrelease { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal settlement_discount { get; set; }

    }

    public class IPDoctorVisit
    {
        [SqlKey(Name = "@Date")]
        public string Date { get; set; }
        [SqlKey(Name = "@ServiceName")]
        public string ServiceName { get; set; }
        [SqlKey(Name = "@Price")]
        public decimal Price { get; set; }
        [SqlKey(Name = "@Discount")]
        public decimal Discount { get; set; }

        [SqlKey(Name = "@DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [SqlKey(Name = "@serviceid")]
        public int serviceid { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal settlement_discount { get; set; }

    }

    public class IPInvestigation
    {
        [SqlKey(Name = "@Date")]
        public string Date { get; set; }
        [SqlKey(Name = "@ServiceName")]
        public string ServiceName { get; set; }
        [SqlKey(Name = "@Price")]
        public decimal Price { get; set; }
        [SqlKey(Name = "@Discount")]
        public decimal Discount { get; set; }

        [SqlKey(Name = "@DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [SqlKey(Name = "@serviceid")]
        public int serviceid { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal settlement_discount { get; set; }

        [SqlKey(Display = true)]
        public string collection_room_no { get; set; }

        [SqlKey(Display = true)]
        public string investigation_group_code { get; set; }

        [SqlKey(Display = true)]
        public int ctrl { get; set; }

    }

    public class IPAdvanceServices
    {
        [SqlKey(Name = "@Date")]
        public string Date { get; set; }
        [SqlKey(Name = "@ServiceName")]
        public string ServiceName { get; set; }
        [SqlKey(Name = "@Price")]
        public decimal Price { get; set; }
        [SqlKey(Name = "@Discount")]
        public decimal Discount { get; set; }

        [SqlKey(Name = "@DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [SqlKey(Name = "@serviceid")]
        public int serviceid { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal settlement_discount { get; set; }

        [SqlKey(Name = "@ctrl")]
        public int ctrl { get; set; }
    }

    public class IPOtServices
    {
        [SqlKey(Name = "@Date")]
        public string Date { get; set; }
        [SqlKey(Name = "@ServiceName")]
        public string ServiceName { get; set; }
        [SqlKey(Name = "@Price")]
        public decimal Price { get; set; }
        [SqlKey(Name = "@Discount")]
        public decimal Discount { get; set; }

        [SqlKey(Name = "@DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [SqlKey(Name = "@serviceid")]
        public int serviceid { get; set; }

        [SqlKey(Name = "@type")]
        public string type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal settlement_discount { get; set; }
    }



    public class journalInv_Entity
    {
        [SqlKey(Name = "@journal_date")]
        public DateTime journal_date { get; set; }

        [SqlKey(Name = "@journal_ref")]
        public string journal_ref { get; set; }

        [SqlKey(Name = "@trans_date")]
        public DateTime trans_date { get; set; }

        [SqlKey(Name = "@trans_for_month")]
        public DateTime trans_for_month { get; set; }

        [SqlKey(Name = "@exp_type")]
        public int exp_type { get; set; }

        [SqlKey(Name = "@payment_type")]
        public string payment_type { get; set; }

        [SqlKey(Name = "@bank_name")]
        public string bank_name { get; set; }

        [SqlKey(Name = "@checkNo")]
        public string checkNo { get; set; }

        [SqlKey(Name = "@checkDate")]
        public string checkDate { get; set; }

        [SqlKey(Name = "@particulars")]
        public string particulars { get; set; }

        [SqlKey(Name = "@dr_amount")]
        public decimal dr_amount { get; set; }

        [SqlKey(Name = "@cr_amount")]
        public decimal cr_amount { get; set; }

        [SqlKey(Name = "@Dep2BankId")]
        public int Dep2BankId { get; set; }

        [SqlKey(Name = "@ischeckClear")]
        public int ischeckClear { get; set; }

        [SqlKey(Name = "@isPosted")]
        public int isPosted { get; set; }

        [SqlKey(Name = "@userid")]
        public int userid { get; set; }

        [SqlKey(Name = "@billid")]
        public int billid { get; set; }

        [SqlKey(Name = "@invoice_id")]
        public int invoice_id { get; set; }

        [SqlKey(Name = "@hos_code")]
        public string hos_code { get; set; }
    }

    public sealed class Invoice_Master : BLayer
    {
        public Invoice_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }

       
        public bool Update(Patient_Entity entity)
        {
            var param = ToSqlParams(entity);
            param.Add(new SqlParameter("@action", "update"));
            try
            {
                ApplyChanges("crd_patient_master", param.ToArray(), Actions.Update);
            }
            catch (DException) { return false; }
            return (param.Find(f => f.ParameterName.ToLower() == "@returnvalue").Value.ToInt() > 0) ? true : false;
        }
       
        public DataSet GetAll(string CliniqUniqrow = null, string InvoiceUniqrow =null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_allgeneralinvoice");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_getinvoice_data", param);
            return rs;
        }

        public DataSet GetAllIPInvoice(string CliniqUniqrow = null, string InvoiceUniqrow = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_allipinvoice");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_InPatient_invoice_data", param);
            return rs;
        }

        public DataSet GetAllfilter(string CliniqUniqrow, int status, string fromdate, string todate,string invoicecode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "get_allinvoice_filter");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@ctrl", status);
            param[3] = new SqlParameter("@from_date", fromdate);
            param[4] = new SqlParameter("@to_date", todate);
            param[5] = new SqlParameter("@invoice_code", invoicecode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_getinvoice_data", param);
            return rs;
        }

        public DataSet GetAllIPfilter(string CliniqUniqrow, int status, string fromdate, string todate, string invoicecode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "get_allipinvoice_filter");
            param[1] = new SqlParameter("@hos_code", CliniqUniqrow);
            param[2] = new SqlParameter("@ctrl", status);
            param[3] = new SqlParameter("@from_date", fromdate);
            param[4] = new SqlParameter("@to_date", todate);
            param[5] = new SqlParameter("@invoice_code", invoicecode);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_InPatient_invoice_data", param);
            return rs;
        }

        public DataSet GetAllInvoiceDetails(string InvoiceUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "invoicedetails");
            param[1] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_getinvoice_data", param);
            return rs;
        }
        public DataSet GetIPInvoiceDetails(string InvoiceUniqrow = null)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "invoicedetails");
            param[1] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_InPatient_invoice_data", param);
            return rs;
        }

        public DataSet GetPaidInvoicedata(string InvoiceUniqrow = null, string hos_code=null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "paiddata");
            param[1] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_getinvoice_data", param);
            return rs;
        }

        public DataSet GetPaymentTypeChannel(int paymentTypeId, string hos_code)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getpaymenttypechannel");
            param[1] = new SqlParameter("@PaymentTypeId", paymentTypeId);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_getinvoice_data", param);
            return rs;
        }

        public DataSet GetIPPaidInvoicedata(string InvoiceUniqrow = null, string hos_code = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "paiddata");
            param[1] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[2] = new SqlParameter("@hos_code", hos_code);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_InPatient_invoice_data", param);
            return rs;
        }

        public int UpdateStatus(string invoicecode, decimal currentamount, string paymenttype, string hoscode, string UserCode, int isadvanced, DateTime dt, int PaymentTypeId, int ChannelId, string TransactionId,out int isvisited)
        {           
            var param = new SqlParameter[12];
            param[0] = new SqlParameter("@journal_date", dt);
            param[1] = new SqlParameter("@invoice_code", invoicecode);
            param[2] = new SqlParameter("@payment_type", paymenttype);
            param[3] = new SqlParameter("@amount", currentamount);
            param[4] = new SqlParameter("@hos_code", hoscode);
            param[5] = new SqlParameter("@user_code", UserCode);
            param[6] = new SqlParameter("@isadvanced", isadvanced);
            param[7] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[8] = new SqlParameter("@ChannelId", ChannelId);
            param[9] = new SqlParameter("@transaction_id", TransactionId);
            param[10] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[11].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = SP_ResultSet("Acc_Insert_journal_new", param);
                if (rs != null && rs.Tables[0].Rows.Count > 0)
                {
                    isvisited = Convert.ToInt32(rs.Tables[0].Rows[0]["Isvisited"]);
                    return Convert.ToInt32(rs.Tables[0].Rows[0]["Status"]);
                }
                else
                {
                    isvisited = 0;
                    return 0;
                }
            }
            catch (DException) { isvisited = 0; return 0;}
        }
        public DataSet IPInvoiceUpdateDiscount(int ServiceId, string ServiceType ,string InvoiceCode, decimal Discount, string hoscode, string UserCode)
        {
            var param = new SqlParameter[9];
            param[0] = new SqlParameter("@action", "ipupdatediscount");
            param[1] = new SqlParameter("@invoice_code", InvoiceCode);
            param[2] = new SqlParameter("@service_type", ServiceType);
            param[3] = new SqlParameter("@serviceid", ServiceId);
            param[4] = new SqlParameter("@discount", Discount);
            param[5] = new SqlParameter("@hos_code", hoscode);
            param[6] = new SqlParameter("@user_code", UserCode);
            param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_invoice_update_disount", param);
             return rs;
        }
        public DataSet OPDInvoiceUpdateDiscount(string InvoiceCode, decimal Discount, string hoscode, string UserCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "opdupdatediscount");
            param[1] = new SqlParameter("@invoice_code", InvoiceCode);
            param[2] = new SqlParameter("@discount", Discount);
            param[3] = new SqlParameter("@hos_code", hoscode);
            param[4] = new SqlParameter("@user_code", UserCode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_invoice_update_disount", param);
            return rs;
        }
        public DataSet IPInvoiceGrossDiscount(string InvoiceCode, decimal Discount, string hoscode, string UserCode)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "ip_gross_discount");
            param[1] = new SqlParameter("@invoice_code", InvoiceCode);
            param[2] = new SqlParameter("@discount", Discount);
            param[3] = new SqlParameter("@hos_code", hoscode);
            param[4] = new SqlParameter("@user_code", UserCode);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[6].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_invoice_update_disount", param);
            return rs;
        }

        public DataSet GetINVDetails(string InvoiceUniqrow, out string InvoiceType)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "invoicedetails");
            param[1] = new SqlParameter("@invoice_code", InvoiceUniqrow);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@InvType", SqlDbType.NVarChar, 1000);
            param[3].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("outer_invoice_data", param);
            InvoiceType = Convert.ToInt32(param[3].Value) == 1 ? "opd_invoicedetails" : "ip_invoicedetails";
            return rs;
        }
        public bool AddInvoiceRemark(string InvoiceCode,string Remark,string hoscode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "add_remark");
            param[1] = new SqlParameter("@invoice_code", InvoiceCode);
            param[2] = new SqlParameter("@InvoiceRemark", Remark);
            param[3] = new SqlParameter("@hos_code", hoscode);
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("Hos_invoice_update_disount", param.ToArray(), Actions.Update);
            return param[4].Value.ToInt() > 0 ? true : false;
        }
    }
}
