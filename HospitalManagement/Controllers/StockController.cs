using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using HMS.BizLogic;
using System.Data;
using HMS.Data;
using System.Reflection.Emit;
using Org.BouncyCastle.Asn1.Ocsp;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class StockController : _Base
    {
        // GET: Stock
        readonly Invoice_Master INVMaster;
        readonly Stock_Master SM;
        DataSet ds = new DataSet();
        public StockController()
        {
            INVMaster = new Invoice_Master(bsInfo);
            SM = new Stock_Master(bsInfo);
        }

        #region "Stock Category"

        public ActionResult StockCategory()
        {
            StockCategoryModel model = new StockCategoryModel();
            ds = SM.GetstockCategoryRecord(Convert.ToString(Session["ClinicCode"]));
            model.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            model.ctrl = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add_Update_stock_category(StockCategoryModel Data)
        {
            if (Data.id == 0)
            {
                ds = SM.insert_update_stock_category(Data.id, Data.category_name, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = SM.insert_update_stock_category(Data.id, Data.category_name, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new stockStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[1]);
            }
            return View("StockCategory", Data);
        }

        public ActionResult DeleteStockCategory(int categoryid)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = SM.Deletestockcategory(categoryid, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Stock Item Master"
        public ActionResult StockItem()
        {
            StockItemModel model = new StockItemModel();
            ds = SM.GetstockCategoryRecord(Convert.ToString(Session["ClinicCode"]));
            model.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            model.ctrl = 1;
            return View(model);
        }
        public ActionResult AddStockItem(StockItemModel s)
        {
            string Status = "";
            if (ModelState.IsValid)
            {
                ds = SM.AddStockItem(s.id, s.Details, s.item_name, s.category_id, s.ctrl, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
                string st = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                string Action = Convert.ToString(ds.Tables[0].Rows[0]["Action"]);
                if (st == "1")
                {
                    if (Action == "update")
                        Status = "Record Updated Successfully!";
                    else
                        Status = "Record Added Successfully!";
                }
                else if (st == "2")
                {
                    Status = "Record Allrady Exists!";
                }
                else
                {
                    Status = "Record Not Saved!";
                }
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockItemList()
        {
            StockItemModel model = new StockItemModel();
            ds = SM.StockItemList(Convert.ToString(Session["ClinicCode"]));
            model.lst_item = Extend.ToList<StockItemList>(ds.Tables[0]);
            return View(model);
        }
        public ActionResult EditStockItem(int id)
        {
            StockItemModel model = new StockItemModel();
            ds = SM.GetStockItem(Convert.ToString(Session["ClinicCode"]), id);
            model.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            if (ds.Tables[1].Rows.Count > 0)
            {
                model.id = id;
                model.ctrl = Convert.ToInt16(ds.Tables[1].Rows[0]["ctrl"]);
                model.category_id = Convert.ToInt16(ds.Tables[1].Rows[0]["category_id"]);
                model.item_name = Convert.ToString(ds.Tables[1].Rows[0]["item_name"]);
                model.Details = Convert.ToString(ds.Tables[1].Rows[0]["details"]);
            }
            return View(model);
        }
        #endregion

        #region "Stock Purchase Master"
        public ActionResult StockPurchaseMaster()
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var res = SM.GetSupllier(hoscode);

            List<SelectListItem> PM = new List<SelectListItem>();
            foreach (DataRow dr in res.Tables[0].Rows)
            {
                var ListSupplier = new SelectListItem
                {
                    Text = (dr["name"]).ToString(),
                    Value = (dr["id"]).ToString(),
                };
                PM.Add(ListSupplier);
            }

            List<purchaseItem> sub = new List<purchaseItem> { new purchaseItem {
                Name = "", ItemID = "", BatchId = "", ExpiryDate = "", Qty = 0, Price = Convert.ToDecimal(0.00),
                Vat = Convert.ToDecimal(0.00), TotalPurchasePrice = Convert.ToDecimal(0.00) } };
            StockPurchase Model = new StockPurchase
            {
                PurchaseNo = generateID(),
                SupplierId = 0,
                ReceivedDate = "",
                InvoiceDate = "",
                InvoiceNo = "",
                Details = "",
                SubTotal = 0,
                Discount = "",
                GrandTotal = 0,
                paid_amount = "",
                due_amount = 0,
                PurchaseItemList = sub,
                SupplierList = PM
            };
            return View(Model);
        }
        public string generateID()
        {
            Random random = new Random();
            int value = random.Next(100000);
            return Convert.ToString("STK-PUR" + "-" + Convert.ToString(value));
        }
        [HttpPost]
        public JsonResult GetSearchStockItem(string prefix, string itemids, string supplierid)
        {
            if (itemids != "")
                itemids = itemids.Substring(0, itemids.Length - 1);
            List<ItemLists> ItemList = new List<ItemLists>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var res = SM.SearchStockItem(prefix, itemids, Convert.ToInt32(supplierid), hoscode);
            if (res.Tables[0].Rows.Count > 0)
                ItemList = Extend.ToList<ItemLists>(res.Tables[0]);
            return Json(ItemList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddPurchaseItem(StockPurchase p)
        {
            string Message = ""; string error = null;
            if (p.PurchaseItemList != null && p.PurchaseItemList.Count > 0)
            {
                string hoscode = Convert.ToString(Session["ClinicCode"]);
                string createdby = Convert.ToString(Session["UserCode"]);

                var lst = p.PurchaseItemList.ToList();
                string isQty = lst.Where(a => Convert.ToInt32(a.Qty) == 0).ToList().Count() > 0 ? "2" : "1";
                if (isQty == "2")
                {
                    Message = "2";
                }
                else if (lst.Where(a => Convert.ToDecimal(a.Price) == 0).ToList().Count() > 0)
                {
                    Message = "5";
                }
                else
                {
                    DataTable dt = Extend.ToDataTable(lst);
                    DataTable res_dt = SM.SavePurchaseItem(dt, p.PurchaseNo, p.InvoiceNo, p.InvoiceDate, p.ReceivedDate, p.SupplierId, p.Details,
                        p.SubTotal, Convert.ToDecimal(p.Discount), p.GrandTotal, hoscode, createdby, out error);

                    if (error != null && error != "")
                    {
                        Message = "0";
                    }
                    else
                    {
                        if (res_dt != null && res_dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(res_dt.Rows[0]["Status"]) == "1")
                                Message = "1";
                            else if (Convert.ToString(res_dt.Rows[0]["Status"]) == "2")
                                Message = "4";
                            else
                                Message = Convert.ToString(res_dt.Rows[0]["Message"]);
                        }
                        else
                            Message = "0";
                    }
                }
            }
            else
            {
                Message = "3";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Request To Store for Item"
        public ActionResult RequestToStoreForItem()
        {
            RequestToStoreModel Model = new RequestToStoreModel();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var res = SM.GetDepartment(hoscode);
            List<SelectListItem> PM = new List<SelectListItem>();
            foreach (DataRow dr in res.Tables[0].Rows)
            {
                var ListSupplier = new SelectListItem
                {
                    Text = (dr["Hos_department_name"]).ToString(),
                    Value = (dr["Hos_department_code"]).ToString(),
                };
                PM.Add(ListSupplier);
            }
            Model.DepartmentList = PM;
            return View(Model);
        }
        [HttpPost]
        public JsonResult SearchStockItem(string prefix)
        {
            List<ItemLists> ItemList = new List<ItemLists>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var res = SM.TypeSearchStockItem(prefix, hoscode);
            if (res.Tables[0].Rows.Count > 0)
                ItemList = Extend.ToList<ItemLists>(res.Tables[0]);
            return Json(ItemList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddRequestForStore(RequestToStoreModel r)
        {
            string[] Message = new string[2]; string error = null;
            DataTable res_dt = SM.SaveRequestToStore(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]),
                r.RequireQty, r.DepartmentCode, Convert.ToInt32(r.ItemID), r.ItemName, r.PurposeOfuse,
            r.DamageReason, out error);
            if (error != null && error != "")
            {
                Message[0] = "0";
                Message[1] = error;
            }
            else
            {
                if (res_dt != null && res_dt.Rows.Count > 0)
                {
                    Message[0] = Convert.ToString(res_dt.Rows[0]["Status"]);
                    Message[1] = Convert.ToString(res_dt.Rows[0]["Message"]);
                }
                else
                {
                    Message[0] = "0";
                    Message[1] = "System geting error, Request will be not generated.";
                }
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Stock Request Lists"
        public ActionResult StoreRequestList()
        {
            List<StoreRequestLists> storeRequestLists = new List<StoreRequestLists>();
            ds = SM.HospitalStockRequestLists(Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                storeRequestLists = ds.Tables[0].ToList<StoreRequestLists>();
            }
            return View(storeRequestLists);
        }
        public PartialViewResult SearchRequest(string QtyStatus, string AllocationStatus)
        {
            List<StoreRequestLists> storeRequestLists = new List<StoreRequestLists>();
            ds = SM.HospitalStockRequestLists(Convert.ToString(Session["ClinicCode"]), QtyStatus, AllocationStatus);
            if (ds.Tables[0].Rows.Count > 0)
            {
                storeRequestLists = ds.Tables[0].ToList<StoreRequestLists>();
            }
            return PartialView("StoreRequestList", storeRequestLists);
        }
        public ActionResult AvailableItemSupplier(int RequestId, int RequireQty, int AllocatedQty)
        {
            ds = SM.AvailableItemSupplierLists(Convert.ToString(Session["ClinicCode"]), RequestId);
            List<StoreRequestLists> Request = new List<StoreRequestLists>();
            Request.Add(new StoreRequestLists
            {
                allrady_use_damage_reason = "",
                AvailableQty = 0,
                category_name = "",
                details = "",
                Hos_department_name = "",
                item_name = "",
                purpose_of_use = "",
                QtyStatus = "",
                RequestId = RequestId,
                status = "",
                require_qty = Convert.ToInt16(RequireQty - AllocatedQty),
                AvailabeSupplierLst = ds.Tables[0].ToList<AvialableItemSupplierLists>()
            });
            return PartialView("_AvailableItemSupplier", Request);
        }
        public ActionResult ApproveRequest(int RequestId, string status, int SupplierId, int AllocatedQty)
        {
            string res = SM.ApproveRequestByStockDepartment(Convert.ToString(Session["UserCode"]), AllocatedQty, Convert.ToString(Session["ClinicCode"]), RequestId, status, SupplierId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Stock Supplier"
        public ActionResult StockSupplier()
        {
            StockSupplier model = new StockSupplier();
            ds = SM.GetStoreSupplierRecord(Convert.ToString(Session["ClinicCode"]));
            model.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            model.storesupplier_data = Extend.ToList<StoreSupplierlist>(ds.Tables[1]);
            model.ctrl = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add_Update_Store_Supplier(StockSupplier Data)
        {
            if (Data.Id == 0)
            {
                ds = SM.Insert_update_Store_Supplier(Data.Id, Data.name, Data.contact_person, Data.mobile_no, Data.email, Data.address, Data.category_id, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = SM.Insert_update_Store_Supplier(Data.Id, Data.name, Data.contact_person, Data.mobile_no, Data.email, Data.address, Data.category_id, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new paymentStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.storesupplier_data = Extend.ToList<StoreSupplierlist>(ds.Tables[1]);
                Data.ctrl = 1;
            }
            return View("StockSupplier", Data);
        }

        public ActionResult DeleteStoreSupplier(int SupplierId)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = SM.DeleteStoreSupplier(SupplierId, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region "Add Supplier Group Entry"

        [HttpPost]
        public JsonResult GetStockItem(string Prefix)
        {
            //Note : you can bind same list from database  
            var entity = SM.GetStockItemdata(Prefix, Convert.ToString(Session["ClinicCode"]));
            return Json(entity, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Add_Update_Store_Supplier_Group(SupplierGroupModel Data)
        {
            if (Data.Id == 0)
            {
                ds = SM.Insert_update_Store_Supplier_Group(Data.Id, Data.item_id, Data.group_name, Data.SupplierId, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            else
            {
                ds = SM.Insert_update_Store_Supplier_Group(Data.Id, Data.item_id, Data.group_name, Data.SupplierId, Convert.ToString(Session["ClinicCode"]), Data.ctrl, Convert.ToString(Session["UserCode"]));
            }
            if (ds.Tables.Count > 0)
            {
                Data.ResponseMsg = new paymentStatus();
                Data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Data.grouplist_data = Extend.ToList<SupplierGroupList>(ds.Tables[1]);
                Data.ctrl = 1;
            }
            return View("SupplierGroupEntry", Data);
        }
        public ActionResult SupplierGroupEntry()
        {
            string uinqrow = Convert.ToString(Request.Path.Split('/')[3]);
            SupplierGroupModel model = new SupplierGroupModel();
            ds = SM.GetStoreSuppliergroup(Convert.ToString(Session["ClinicCode"]), uinqrow);
            model.stockcategory_List = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            model.grouplist_data = Extend.ToList<SupplierGroupList>(ds.Tables[1]);
            model.ctrl = 1;
            model.SupplierId = Convert.ToInt32(ds.Tables[2].Rows[0]["Id"]);
            return View(model);
        }
        public ActionResult DeleteStoreSupplierGroup(int GroupId)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = SM.DeleteStoreSupplierGroup(GroupId, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Stock PurchaseList
        public ActionResult StockPurchaseList()
        {
            StockPurchaseListModel model = new StockPurchaseListModel();
            ds = SM.GetStoreSupplierRecord(Convert.ToString(Session["ClinicCode"]));
            model.storesupplier_data = Extend.ToList<StoreSupplierlist>(ds.Tables[1]);
            model.stkpurchaseList = new List<StockPurchaseList>();
            return View(model);
        }

        public PartialViewResult FilterStockPurchaseList(StockPurchaseListModel model)
        {
            var Ds = SM.GetPurchaseList(Convert.ToString(Session["ClinicCode"]), model.FromDate, model.ToDate, model.supplier_id);
            model.stkpurchaseList = Extend.ToList<StockPurchaseList>(Ds.Tables[0]);
            return PartialView("_PurchaseListTable", model);
        }

        public ActionResult StockPurchaseApproval()
        {
            StockPurchaseApproval model = new StockPurchaseApproval();
            var code = Convert.ToInt32(Request.Path.Split('/')[3]);
            var ds = SM.GetPurchaseGroupitemList(Convert.ToString(Session["ClinicCode"]), code);
            model.stkpurchaseList = Extend.ToList<StockPurchaseList>(ds.Tables[0]);
            model.stk_purchase_groupitem_list = Extend.ToList<StockPurchasegroupitem>(ds.Tables[1]);
            model.PurchaseId = code;
            return View("StockPurchaseApproval", model);
        }

        public ActionResult approve_stock_purchase(StockPurchaseApproval spa)
        {
            StockPurchaseApproval model = new StockPurchaseApproval();
            ds = SM.StockPurchaseApproval(spa.PurchaseId, Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables.Count > 0)
            {
                model.ResponseMsg = new stockStatus();
                model.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                model.stkpurchaseList = new List<StockPurchaseList>();
                model.stk_purchase_groupitem_list = new List<StockPurchasegroupitem>();
                model.PurchaseId = spa.PurchaseId;
            }
            return View("StockPurchaseApproval", model);
        }
        #endregion

        #region "Payment To Supplier"

        [HttpPost]
        public JsonResult GetPurchaseInvoice(string Prefix)
        {
            //Note : you can bind same list from database  
            var entity = SM.GetPurchaseInvoiceData(Prefix, Convert.ToString(Session["ClinicCode"]));
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PaymentToSupplier()
        {
            PaymentToSupplierModel model = new PaymentToSupplierModel();

            var ds = SM.GetPaymentType(Convert.ToString(Session["ClinicCode"]));
            model.PaymentType_List = Extend.ToList<PaymentTypeList>(ds.Tables[0]);
            model.paymentType_Channel_list = new List<PaymentTypeChannelList>();
            model.Date = DateTime.Now.ToString("yyyy-MM-dd");
            return View(model);
        }

        public PartialViewResult PaymentTypeChannel(int PaymentTypeId)
        {
            PaymentToSupplierModel model = new PaymentToSupplierModel();
            var DataSet = INVMaster.GetPaymentTypeChannel(PaymentTypeId, Convert.ToString(Session["ClinicCode"]));
            if (DataSet.Tables[0].Rows.Count > 0)
            {
                model.paymentType_Channel_list = Extend.ToList<PaymentTypeChannelList>(DataSet.Tables[0]);
            }
            return PartialView("PaymentToSupplier", model);
        }

        public ActionResult AddPaymentToSupplier(PaymentToSupplierModel data)
        {
            var ds = SM.add_payment_to_supplier(data.PurchaseId, data.Payment, data.ChannelId, data.PaymentTypeId, data.SupplierId, data.Remark, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            if (ds.Tables.Count > 0)
            {
                data.ResponseMsg = new stockStatus();
                data.ResponseMsg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            return View("PaymentToSupplier", data);
        }
        #endregion

        #region "Stock Report"
        public ActionResult StockReport()
        {
            StockReportModel Model = new StockReportModel();
            //Get Supplier ,Category Lists,reportLists
            ds = SM.GetStoreSupplierRecord(Convert.ToString(Session["ClinicCode"]));
            Model.CategoryList = Extend.ToList<stockcategoryList>(ds.Tables[0]);
            Model.SupplierList = Extend.ToList<StoreSupplierlist>(ds.Tables[1]);
            List<SelectListItem> PM = new List<SelectListItem>();
            PM.Add(new SelectListItem
            {
                Text = "Available Qty Report",
                Value = "available_qty_report",
            });
            PM.Add(new SelectListItem
            {
                Text = "Item Expire Report",
                Value = "item_expire_report",
            });
            PM.Add(new SelectListItem
            {
                Text = "Purchase Report",
                Value = "purchase_report",
            });
            PM.Add(new SelectListItem
            {
                Text = "Stock Allocation Report",
                Value = "stock_allocation_report",
            });
            Model.ReportLists = PM;
            //end
            Model.ReportData = new List<ReportLists>();
            return View(Model);
        }
        public PartialViewResult SearchReportData(int Sup_Id,int Cat_Id,string ReportName)
        {
            StockReportModel Model = new StockReportModel();
            Model.ReportName = ReportName;
            var dt = SM.GetReportData(Convert.ToString(Session["ClinicCode"]),Sup_Id,Cat_Id,ReportName);
            if(dt!=null)
            {
                Model.ReportData = Extend.ToList<ReportLists>(dt);
            }
            return PartialView("StockReport", Model);
        }
        #endregion

        #region "Stock Return"
        public ActionResult StockReturn()
        {
            ReturnStockPurchase Model = new ReturnStockPurchase();
            return View(Model);
        }
        #endregion
    }
}