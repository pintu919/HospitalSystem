using HMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Models
{
    #region "Stock Category"

    public class StockCategoryModel
    {
        [Required(ErrorMessage = "*")]
        public string category_name { get; set; }
        public int id { get; set; }
        public int ctrl { get; set; }
        public List<stockcategoryList> stockcategory_List { get; set; }
        public stockStatus ResponseMsg { get; set; }
    }

    public class stockStatus
    {
        public string StatusId { get; set; }
    }

    public class stockcategoryList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string category_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }
    }
    #endregion
    #region "Stock Item"
    public class StockItemModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "*")]
        public int category_id { get; set; }

        [Required(ErrorMessage = "*")]
        public string item_name { get; set; }
        public string Details { get; set; }
        public List<stockcategoryList> stockcategory_List { get; set; }
        public List<StockItemList> lst_item = new List<StockItemList>();
        public int ctrl { get; set; }

    }
    public class StockItemList
    {
        [SqlKey(Display = true)]
        public int id { get; set; }
        [SqlKey(Display = true)]
        public string item_name { get; set; }
        [SqlKey(Display = true)]
        public string category_name { get; set; }
        [SqlKey(Display = true)]
        public string details { get; set; }
    }

    #endregion
    #region "Purchase Stock"
    public class StockPurchase
    {
        public int SupplierId { get; set; }
        public string ReceivedDate { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string PurchaseNo { get; set; }
        public string Details { get; set; }

        [SqlKey(Display = true)]
        public decimal SubTotal { get; set; }

        //[SqlKey(Display = true)]
        //public decimal TotalVat { get; set; }

        [SqlKey(Display = true)]
        public string Discount { get; set; }

        [SqlKey(Display = true)]
        public decimal GrandTotal { get; set; }

        [SqlKey(Display = true)]
        public string paid_amount { get; set; }

        [SqlKey(Display = true)]
        public decimal due_amount { get; set; }
        public List<purchaseItem> PurchaseItemList { get; set; }
        public List<SelectListItem> SupplierList { get; set; }
        public string purchase_code { get; set; }
    }
    public class purchaseItem
    {
        [SqlKey(Display = true)]
        public string Name { get; set; }
        [SqlKey(Display = true)]
        public string ItemID { get; set; }

        [SqlKey(Display = true)]
        public string BatchId { get; set; }

        [SqlKey(Display = true)]
        public string ExpiryDate { get; set; }

        //[SqlKey(Display = true)]
        //public string BoxQty { get; set; }

        [SqlKey(Display = true)]
        public int Qty { get; set; }

        [SqlKey(Display = true)]
        public decimal Price { get; set; }

        [SqlKey(Display = true)]
        public decimal TotalPurchasePrice { get; set; }

        [SqlKey(Display = true)]
        public decimal Vat { get; set; }

        //[SqlKey(Display = true)]
        //public string VatType { get; set; }

        //[SqlKey(Display = true)]
        //public decimal VatAmount { get; set; }
    }
    public class ItemLists
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ItemId { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ItemName { get; set; }
    }
    #endregion
    #region "Request To Store for Item"
    public class RequestToStoreModel
    {
        public List<SelectListItem> DepartmentList { get; set; }
        public string DepartmentCode { get; set; }
        public string ItemID { get; set; }
        public int RequireQty { get; set; }
        public string ItemName { get; set; }
        public string PurposeOfuse { get; set; }
        public string DamageReason { get; set; }
    }
    #endregion
    #region "Store Request Lists"
    public class StoreRequestLists
    {
        [SqlKey(Display = true)]
        public int RequestId { get; set; }
        [SqlKey(Display = true)]
        public string Hos_department_name { get; set; }
        [SqlKey(Display = true)]
        public string purpose_of_use { get; set; }
        [SqlKey(Display = true)]
        public string item_name { get; set; }
        [SqlKey(Display = true)]
        public string category_name { get; set; }
        [SqlKey(Display = true)]
        public string allrady_use_damage_reason { get; set; }
        [SqlKey(Display = true)]
        public string status { get; set; }
        [SqlKey(Display = true)]
        public string QtyStatus { get; set; }
        [SqlKey(Display = true)]
        public int require_qty { get; set; }
        [SqlKey(Display = true)]
        public int AvailableQty { get; set; }
        [SqlKey(Display = true)]
        public string details { get; set; }
        [SqlKey(Display = true)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true)]
        public int AllocatedQty { get; set; }
        public List<AvialableItemSupplierLists> AvailabeSupplierLst { get; set; }
    }
    public class AvialableItemSupplierLists
    {
        [SqlKey(Display = true)]
        public int SupplierId { get; set; }
        [SqlKey(Display = true)]
        public string SupplierName { get; set; }
        [SqlKey(Display = true)]
        public int AvialableQty { get; set; }
    }

    #endregion
    #region StockSupplier
    public class StockSupplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string name { get; set; }

        public string contact_person { get; set; }

        [Required(ErrorMessage = "*")]
        public string mobile_no { get; set; }

        [Required(ErrorMessage = "*")]
        public string email { get; set; }
        public string address { get; set; }

        public int category_id { get; set; }
        public string hos_code { get; set; }
        public int ctrl { get; set; }
        public List<stockcategoryList> stockcategory_List { get; set; }

        public paymentStatus ResponseMsg { get; set; }

        public List<StoreSupplierlist> storesupplier_data { get; set; }
    }

    public class StoreSupplierlist
    {

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string contact_person { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string mobile_no { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string email { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string address { get; set; }

        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string category_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public Guid uniqrow { get; set; }

    }

    #endregion
    #region "Supplier Group Entry"
    public class SupplierGroupModel
    {

        [Required(ErrorMessage = "*")]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public int item_id { get; set; }

        [Required(ErrorMessage = "*")]
        public string group_name { get; set; }
        public int ctrl { get; set; }

        [Required(ErrorMessage = "*")]
        public int SupplierId { get; set; }
        public List<stockcategoryList> stockcategory_List { get; set; }
        public List<SupplierGroupList> grouplist_data { get; set; }
        public paymentStatus ResponseMsg { get; set; }
    }

    public class SupplierGroupList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int Id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]

        public int item_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string group_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string category_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }


    }
    #endregion
    #region "StockPurchaseList"
    public class StockPurchaseListModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        [Required(ErrorMessage = "*")]
        public int supplier_id { get; set; }
        public List<StoreSupplierlist> storesupplier_data { get; set; }
        public List<StockPurchaseList> stkpurchaseList { get; set; }
    }

    public class StockPurchaseList
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string hos_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string purchase_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string supplier_invoice_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string received_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string invoice_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int supplier_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string supplier_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string mobile_no { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string email { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string vat_type { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal sub_total { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal vat_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal discount_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal grand_total { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal paid_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal due_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string created_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int ctrl { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Status { get; set; }

    }

    public class StockPurchasegroupitem
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int item_id { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string group_name { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string batch_no { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string expiry_date { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int qty { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal vat_amount { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal unit_price { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Total_vat { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public decimal Total_amount { get; set; }
    }

    public class StockPurchaseApproval
    {
        public int PurchaseId { get; set; }
        public List<StockPurchaseList> stkpurchaseList { get; set; }
        public List<StockPurchasegroupitem> stk_purchase_groupitem_list { get; set; }
        public stockStatus ResponseMsg { get; set; }
    }
    #endregion
    #region "Payment To Supplier"
    public class PaymentToSupplierModel
    {
        [Required(ErrorMessage = "*")]
        public string Date { get; set; }

        [Required(ErrorMessage = "*")]
        public string PurchaseCode { get; set; }

        [Required(ErrorMessage = "*")]
        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Supplier { get; set; }

        [Required(ErrorMessage = "*")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "*")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "*")]
        public decimal Payment { get; set; }
        public string Remark { get; set; }
        public stockStatus ResponseMsg { get; set; }
        public List<PaymentTypeList> PaymentType_List { get; set; }

        [Required(ErrorMessage = "*")]
        public int PaymentTypeId { get; set; }
        public List<PaymentTypeChannelList> paymentType_Channel_list { get; set; }

        [Required(ErrorMessage = "*")]
        public int ChannelId { get; set; }
    }
    #endregion
    #region "Stock Report"
    public class StockReportModel
    {
        public int SupplierId { get; set; }
        public int Categoryid { get; set; }
        public string ReportName { get; set; }
        public List<stockcategoryList> CategoryList { get; set; }
        public List<StoreSupplierlist> SupplierList { get; set; }
        public List<SelectListItem> ReportLists { get; set; }
        public List<ReportLists> ReportData { get; set; }
    }
    public class ReportLists
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string SupplierName { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string category_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string item_name { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ExpireDate { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string BatchNo { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AvailableQty { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int AllocatedQty { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string purchase_code { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public int PurchaseQty { get; set; }
    }
    #endregion

    #region "Return Purchase Stock"
    public class ReturnStockPurchase
    {
        public string SupplierName { get; set; }
        public string ReceivedDate { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Returnreason { get; set; }
        public string PurchaseNo { get; set; }
        public string Details { get; set; }

        [SqlKey(Display = true)]
        public decimal SubTotal { get; set; }

        //[SqlKey(Display = true)]
        //public decimal TotalVat { get; set; }

        [SqlKey(Display = true)]
        public string Discount { get; set; }

        [SqlKey(Display = true)]
        public decimal GrandTotal { get; set; }

        [SqlKey(Display = true)]
        public string paid_amount { get; set; }

        [SqlKey(Display = true)]
        public decimal due_amount { get; set; }
        public List<purchase_return_item> PurchaseItemList { get; set; }
        public string purchase_code { get; set; }
    }
    public class purchase_return_item
    {
        [SqlKey(Display = true)]
        public string Name { get; set; }
        [SqlKey(Display = true)]
        public string ItemID { get; set; }

        [SqlKey(Display = true)]
        public string BatchId { get; set; }

        [SqlKey(Display = true)]
        public string ExpiryDate { get; set; }

        [SqlKey(Display = true)]
        public int ReturnQty { get; set; }

        [SqlKey(Display = true)]
        public int Qty { get; set; }

        [SqlKey(Display = true)]
        public decimal Price { get; set; }

        [SqlKey(Display = true)]
        public decimal TotalPurchasePrice { get; set; }

        [SqlKey(Display = true)]
        public decimal Vat { get; set; }

        [SqlKey(Display = true)]
        public decimal ReturnPrice { get; set; }
    }
    #endregion
}