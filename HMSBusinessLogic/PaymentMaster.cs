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
    public sealed class Payment_Master : BLayer
    {
        public Payment_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();

        public DataSet Insert_update_PaymentType( int Id,string PaymentType, string hos_code, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[7];
                param[0] = new SqlParameter("@action", "insert_update_payment_type");
                param[1] = new SqlParameter("@Id", Id);
                param[2] = new SqlParameter("@PaymentType", PaymentType);
                param[3] = new SqlParameter("@ctrl", ctrl);
                param[4] = new SqlParameter("@hos_code", hos_code);
                param[5] = new SqlParameter("@user_code", user_code);
                param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[6].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_paymenttype_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetPaymentTypeRecord(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_paymenttype_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public bool DeletePaymentType(int Id, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete_paymenttype");
            param[1] = new SqlParameter("@Id", Id);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("hos_paymenttype_master_data", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }


        public DataSet GetChannelTypeRecord(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_Channeltype_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetSelectedLedgerAccountList(string HosCode, int headId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getledgeraccount");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@head_id", headId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_Channeltype_master_data", param);
            return rs;
        }

        public DataSet AddChannelData(int PaymentTypeId, string ChannelName, int AccountHeadId, int AccountId, int ctrl, string hos_code, string user_code)
        {
            try
            {
                var param = new SqlParameter[9];
                param[0] = new SqlParameter("@action", "insert_update_channel_type");
                param[1] = new SqlParameter("@channelname", ChannelName);
                param[2] = new SqlParameter("@paymenttype_id", PaymentTypeId);
                param[3] = new SqlParameter("@head_id", AccountHeadId);
                param[4] = new SqlParameter("@account_id", AccountId);
                param[5] = new SqlParameter("@ctrl", ctrl);
                param[6] = new SqlParameter("@hos_code", hos_code);
                param[7] = new SqlParameter("@user_code", user_code);
                param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[8].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("hos_Channeltype_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetPaymentTypeChannelList(string HosCode)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@action", "allchannelracord");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[2].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("hos_Channeltype_master_data", param);
            return rs;
        }

    }

  



}
