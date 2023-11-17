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
    public sealed class Payable_Master : BLayer
    {
        public Payable_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        #region "Lab Payable"
        public DataSet GetLabPayableData(string HosCode,string FromDate,string ToDate,int SupplierId,string IsPaid)
        {
            var param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", "get_labpayables");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", FromDate);
            param[3] = new SqlParameter("@to_date", ToDate);
            param[4] = new SqlParameter("@suppler_id", SupplierId);
            param[5] = new SqlParameter("@paid_unpaid", IsPaid);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_payables", param);
            return rs;
        }
        public DataSet LabPayabledata(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "get_all_lab");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_payables", param);
            return rs;
        }
        public DataSet GetSelectedAccountList(string HosCode,int headId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getaccount");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@head_id", headId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_payables", param);
            return rs;
        }
        public DataSet Save_LabPaybaleReport(string HosCode, string IDs,int PaymentTypeId,int ChannelId,string UserCode,
                                             string FromDate,string ToDate,int LabSupplierId, out int Status)
        {
            var param = new SqlParameter[11];
            param[0] = new SqlParameter("@action", "save_labpayablereport");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@pay_ids", IDs);
            param[3] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[4] = new SqlParameter("@ChannelId", ChannelId);
            param[5] = new SqlParameter("@UserCode", UserCode);
            param[6] = new SqlParameter("@from_date", FromDate);
            param[7] = new SqlParameter("@to_date", ToDate);
            param[8] = new SqlParameter("@suppler_id", LabSupplierId);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[10].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("hos_payables", param);
            Status = Convert.ToInt16(param[10].Value);
            return rs;
        }
        #endregion

        #region Receivable
        public DataSet GetLabReceivableData(string HosCode, string FromDate, string ToDate, int filterby)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_receivable");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", FromDate);
            param[3] = new SqlParameter("@to_date", ToDate);
            param[4] = new SqlParameter("@filterby", filterby);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_receivable", param);
            return rs;
        }
        public DataSet Save_receivableReport(string HosCode, int invoiceid, string FromDate, string ToDate, string UserCode, decimal totalamount,int filterby, int PaymentTypeId, int ChannelId,  out int Status)
        {
            var param = new SqlParameter[12];
            param[0] = new SqlParameter("@action", "save_receivablereport");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@invoice_id", invoiceid);
            param[3] = new SqlParameter("@UserCode", UserCode);
            param[4] = new SqlParameter("@from_date", FromDate);
            param[5] = new SqlParameter("@to_date", ToDate);
            param[6] = new SqlParameter("@total_invamount", totalamount);
            param[7] = new SqlParameter("@filterby", filterby);
            param[8] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[9] = new SqlParameter("@ChannelId", ChannelId);
            param[10] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[11].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("hos_receivable", param);
            Status = Convert.ToInt16(param[11].Value);
            return rs;
        }

        #endregion

        #region "IP/OP Refundable"
        public DataSet GetIpRefundInvestigation(string HosCode, string Patientcode = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getiprefundinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@Patientcode", Patientcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            return rs;
        }

        public DataSet GetOPDRefundInvestigation(string HosCode, string Patientcode = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getopdrefundinvestigation");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@Patientcode", Patientcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            return rs;
        }

        public DataSet Getouterpatientrefund(string HosCode, string Patientcode = null)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "outerpatientrefund");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@Patientcode", Patientcode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            return rs;
        }


        public DataSet Save_IP_RefundData(string HosCode, string UserCode, DataTable dt, int PaymentTypeId, int ChannelId, out int Status)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "save_ip_refund");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@dt_refund_data", dt);
            param[4] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[5] = new SqlParameter("@ChannelId", ChannelId);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            Status = Convert.ToInt16(param[7].Value);
            return rs;
        }

        public DataSet Save_OPD_RefundData(string HosCode, string UserCode, DataTable dt, int PaymentTypeId, int ChannelId, out int Status)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "save_opd_refund");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@dt_refund_data", dt);
            param[4] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[5] = new SqlParameter("@ChannelId", ChannelId);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            Status = Convert.ToInt16(param[7].Value);
            return rs;
        }

        public DataSet save_outerpatient_refund(string HosCode, string UserCode, DataTable dt, int PaymentTypeId, int ChannelId, out int Status)
        {
            var param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", "save_outerpatient_refund");
            param[1] = new SqlParameter("@hoscode", HosCode);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@dt_refund_data", dt);
            param[4] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
            param[5] = new SqlParameter("@ChannelId", ChannelId);
            param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[7].Direction = ParameterDirection.ReturnValue;
            var rs = SP_ResultSet("Hos_Refund_Investigation", param);
            Status = Convert.ToInt16(param[7].Value);
            return rs;
        }
        #endregion

    }
}
