using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BizLogic
{
    public sealed class stock_item_entity
    {
        [SqlKey(Name = "@id")]
        public int id { get; set; }

        [SqlKey(Name = "@item_name")]
        public string item_name { get; set; }

    }
    public sealed class purchase_invoice_entity
    {
        [SqlKey(Name = "@purchase_code")]
        public string purchase_code { get; set; }

        [SqlKey(Name = "@supplier_name")]
        public string supplier_name { get; set; }

        [SqlKey(Name = "@supplier_id")]
        public int supplier_id { get; set; }

        [SqlKey(Name = "@id")]
        public int id { get; set; }
        [SqlKey(Name = "@grand_total")]
        public decimal grand_total { get; set; }

        [SqlKey(Name = "@paid_amount")]
        public decimal paid_amount { get; set; }

    }
    public sealed class Stock_Master : BLayer
    {
        public Stock_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        DataSet ds = new DataSet();

        public DataSet insert_update_stock_category(int Id, string category_name, string hos_code, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[7];
                param[0] = new SqlParameter("@action", "insert_update_stock_category");
                param[1] = new SqlParameter("@Id", Id);
                param[2] = new SqlParameter("@category_name", category_name);
                param[3] = new SqlParameter("@ctrl", ctrl);
                param[4] = new SqlParameter("@hos_code", hos_code);
                param[5] = new SqlParameter("@user_code", user_code);
                param[6] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[6].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_category", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetstockCategoryRecord(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_category", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public bool Deletestockcategory(int id, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete_stock_category");
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("sp_stk_category", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet AddStockItem(int ItemId,string Details,string ItemName, int CategoryId,int ctrl, string hos_code, string user_code)
        {
            try
            {
                var param = new SqlParameter[9];
                param[0] = new SqlParameter("@action", "insert_update_stock_item");
                param[1] = new SqlParameter("@id", ItemId);
                param[2] = new SqlParameter("@category_id", CategoryId);
                param[3] = new SqlParameter("@item_name", ItemName);
                param[4] = new SqlParameter("@ctrl", ctrl);
                param[5] = new SqlParameter("@hos_code", hos_code);
                param[6] = new SqlParameter("@user_code", user_code);
                param[7] = new SqlParameter("@Details", Details);
                param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[8].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet StockItemList(string hoscode)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet GetStockItem(string hos_code,int id)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "get");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@id", id);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_item_master", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        #region "Stock Purchase Master"
        public DataSet GetSupllier(string hoscode)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "get_supplier");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_purchase", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet SearchStockItem(string Prefix, string ItemIds, int SupplierId, string hoscode)
        {
            try
            {
                var param = new SqlParameter[6];
                param[0] = new SqlParameter("@action", "search_item");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@Prefix", Prefix.Trim());
                param[3] = new SqlParameter("@ItemIds", ItemIds);
                param[4] = new SqlParameter("@SupplierId", SupplierId);
                param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[5].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_purchase", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataTable SavePurchaseItem(DataTable dt, string PurchaseNo, string InvoiceNo, string InvoiceDate, string ReceivedDate, int SupplierId, string Details,
                        decimal SubTotal, decimal Discount, decimal GrandTotal, string hoscode, string createdby, out string error)
        {
            var param = new SqlParameter[15];
            param[0] = new SqlParameter("@action", "insert_purchase_data");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@dt_purchase", dt);
            param[3] = new SqlParameter("@purchase_code", PurchaseNo);
            param[4] = new SqlParameter("@s_invoice_code", InvoiceNo);
            param[5] = new SqlParameter("@received_date", ReceivedDate);
            param[6] = new SqlParameter("@invoice_date", InvoiceDate);
            param[7] = new SqlParameter("@details", Details);
            param[8] = new SqlParameter("@sub_total", SubTotal);
            param[9] = new SqlParameter("@grand_total", GrandTotal);
            param[10] = new SqlParameter("@usercode", createdby);
            param[11] = new SqlParameter("@discount_amount", Discount);
            param[12] = new SqlParameter("@SupplierId", SupplierId);
            param[13] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[13].Direction = ParameterDirection.ReturnValue;
            param[14] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[14].Direction = ParameterDirection.Output;
            DataSet ds = SP_ResultSet("sp_stk_purchase", param);
            error = Convert.ToString(param[14].Value);
            return ds.Tables[0];
        }
        #endregion
        #region Request to Store For Item
        public DataSet GetDepartment(string hoscode)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "get_department");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_request_to_store", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet TypeSearchStockItem(string Prefix,string hoscode)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "search_item");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@Prefix", Prefix.Trim());
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_request_to_store", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataTable SaveRequestToStore(string hoscode,string userCode,int RequireQty,string DepartmentCode,int ItemID,string ItemName,string PurposeOfuse,string DamageReason, out string error)
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "save_request_to_store");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@DepartmentCode", DepartmentCode);
            param[3] = new SqlParameter("@ItemID", ItemID);
            param[4] = new SqlParameter("@ItemName", ItemName);
            param[5] = new SqlParameter("@PurposeOfuse", PurposeOfuse);
            param[6] = new SqlParameter("@DamageReason", DamageReason);
            param[7] = new SqlParameter("@usercode", userCode);
            param[8] = new SqlParameter("@RequireQty", RequireQty);
            param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[9].Direction = ParameterDirection.Output;
            DataSet ds = SP_ResultSet("sp_stk_request_to_store", param);
            error = Convert.ToString(param[9].Value);
            return ds.Tables[0];
        }
        #endregion

        #region "Get Request Lists"
        public DataSet HospitalStockRequestLists(string hoscode, string QtyStatus=null, string AllocationStatus=null)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", "get_store_requestlists");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@QtyStatus", QtyStatus);
                param[3] = new SqlParameter("@AllocationStatus", AllocationStatus);
                param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_request_to_store", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public DataSet AvailableItemSupplierLists(string hoscode, int RequestId)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "avialable_item_supplier");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@RequestId", RequestId);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_request_to_store", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public string ApproveRequestByStockDepartment(string UserCode,int AllocatedQty,string hoscode,int RequestId,string ApproveStatus,int SupplierId)
        {
            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@action", "request_approval");
                param[1] = new SqlParameter("@hos_code", hoscode);
                param[2] = new SqlParameter("@RequestId", RequestId);
                param[3] = new SqlParameter("@SupplierId", SupplierId);
                param[4] = new SqlParameter("@ApproveStatus", ApproveStatus);
                param[5] = new SqlParameter("@AllocatedQty", AllocatedQty);
                param[6] = new SqlParameter("@usercode", UserCode);
                param[7] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[7].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_request_to_store", param);
                if (Convert.ToString(param[7].Value) == "")
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "1")
                        {
                            return "success";
                        }
                        else
                        {
                            return "System geting error. process not done";
                        }
                    }
                    else
                    {
                        return "System geting error. process not done";
                    }
                }
                else
                {
                    return Convert.ToString(param[7].Value);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region "Stock Supplier"
        public DataSet Insert_update_Store_Supplier(int Id, string name, string contact_person, string mobile_no, string email, string address, int category_id, string hos_code, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[12];
                param[0] = new SqlParameter("@action", "insert_update_store_supplier");
                param[1] = new SqlParameter("@Id", Id);
                param[2] = new SqlParameter("@name", name);
                param[3] = new SqlParameter("@contact_person", contact_person);
                param[4] = new SqlParameter("@mobile_no", mobile_no);
                param[5] = new SqlParameter("@email", email);
                param[6] = new SqlParameter("@address", address);
                param[7] = new SqlParameter("@category_id", category_id);
                param[8] = new SqlParameter("@ctrl", ctrl);
                param[9] = new SqlParameter("@hos_code", hos_code);
                param[10] = new SqlParameter("@user_code", user_code);
                param[11] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[11].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_supplier_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public bool DeleteStoreSupplier(int Id, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete_store_supplier");
            param[1] = new SqlParameter("@Id", Id);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("sp_stk_supplier_master_data", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        public DataSet GetStoreSupplierRecord(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_supplier_master_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        #endregion

        #region "Store Supplier Group"

        public List<stock_item_entity> GetStockItemdata(string Prefix, string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getstockitem");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@group_name", Prefix);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<stock_item_entity>("sp_stk_supplier_group_data", param);
            return rs;
        }

        public DataSet Insert_update_Store_Supplier_Group(int Id, int item_id, string group_name, int supplier_id, string hos_code, int ctrl, string user_code)
        {
            try
            {
                var param = new SqlParameter[9];
                param[0] = new SqlParameter("@action", "insert_update_store_supplier_group");
                param[1] = new SqlParameter("@Id", Id);
                param[2] = new SqlParameter("@item_id", item_id);
                param[3] = new SqlParameter("@group_name", group_name);
                param[4] = new SqlParameter("@supplier_id", supplier_id);
                param[5] = new SqlParameter("@ctrl", ctrl);
                param[6] = new SqlParameter("@hos_code", hos_code);
                param[7] = new SqlParameter("@user_code", user_code);
                param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[8].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_supplier_group_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet GetStoreSuppliergroup(string hos_code, string unqrow)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@action", "record");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@unqrow", unqrow);
                param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[3].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_supplier_group_data", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public bool DeleteStoreSupplierGroup(int Id, string UserCode)
        {
            var param = new SqlParameter[5];
            param[0] = new SqlParameter("@action", "delete_store_supplier_group");
            param[1] = new SqlParameter("@Id", Id);
            param[2] = new SqlParameter("@user_code", UserCode);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[4].Direction = ParameterDirection.ReturnValue;
            try
            {
                var rs = ApplyChanges("sp_stk_supplier_group_data", param.ToArray(), Actions.Delete);
                return param[4].Value.ToInt() > 0 ? true : false;
            }
            catch (DException) { return false; }
        }

        #endregion

        #region "Stock Purchase List"
        public DataSet GetPurchaseList(string HosCode, string Fromdate, string Todate, int SupplierId)
        {
            var param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", "get_purchase_list");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@from_date", Fromdate);
            param[3] = new SqlParameter("@to_date", Todate);
            param[4] = new SqlParameter("@SupplierId", SupplierId);
            param[5] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[5].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("sp_stk_purchase", param);
            return rs;
        }

        public DataSet GetPurchaseGroupitemList(string HosCode, int PurchaseId)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "get_purchase_item_list");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@purchaseid", PurchaseId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("sp_stk_purchase", param);
            return rs;
        }

        public DataSet StockPurchaseApproval(int PurchaseId, string HosCode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "update_stk_purchase");
            param[1] = new SqlParameter("@hos_code", HosCode);
            param[2] = new SqlParameter("@purchaseid", PurchaseId);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_ResultSet("sp_stk_purchase", param);
            return rs;
        }


        #endregion

        #region "Payment To Supplier"
        public List<purchase_invoice_entity> GetPurchaseInvoiceData(string Prefix, string hoscode)
        {
            var param = new SqlParameter[4];
            param[0] = new SqlParameter("@action", "getpurchaseinvoice");
            param[1] = new SqlParameter("@hos_code", hoscode);
            param[2] = new SqlParameter("@prefix", Prefix);
            param[3] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[3].Direction = ParameterDirection.Output;
            var rs = SP_Read<purchase_invoice_entity>("sp_stk_pay_to_supplier", param);
            return rs;

        }

        public DataSet GetPaymentType(string hos_code)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@action", "getpaymentType");
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[2].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_pay_to_supplier", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public DataSet add_payment_to_supplier(int purchaseid, decimal cost, int ChannelId, int PaymentTypeId, int supplier_id, string remark, string hos_code, string user_code)
        {
            try
            {
                var param = new SqlParameter[10];
                param[0] = new SqlParameter("@action", "paytosupplier");
                param[1] = new SqlParameter("@purchaseid", purchaseid);
                param[2] = new SqlParameter("@cost", cost);
                param[3] = new SqlParameter("@ChannelId", ChannelId);
                param[4] = new SqlParameter("@PaymentTypeId", PaymentTypeId);
                param[5] = new SqlParameter("@supplierId", supplier_id);
                param[6] = new SqlParameter("@remark", remark);
                param[7] = new SqlParameter("@hos_code", hos_code);
                param[8] = new SqlParameter("@user_code", user_code);
                param[9] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[9].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_pay_to_supplier", param);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        #endregion
        #region "Stock Report"
        public DataTable GetReportData(string hos_code, int SuppId,int Catid,string reportType)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@action", reportType);
                param[1] = new SqlParameter("@hos_code", hos_code);
                param[2] = new SqlParameter("@SupplierId", SuppId);
                param[3] = new SqlParameter("@categoryId", Catid);
                param[4] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;
                ds = SP_ResultSet("sp_stk_report", param);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                return ds.Tables[0];
            }
        }
        #endregion
    }
}
