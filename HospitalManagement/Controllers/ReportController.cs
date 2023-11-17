using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using ImageResizer;
using System.IO;
using System.Data;
using System.Globalization;
using HospitalManagement.Helper;
using System.Web.UI;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class ReportController : _Base
    {
        // GET: Report
        readonly Report_Master Rep_Master;
        public ReportController()
        {
            Rep_Master = new Report_Master(bsInfo);
        }
        public ActionResult SummaryReport()
        {
            ReportModel model = new ReportModel();
            var DataSet = Rep_Master.GetAll(Convert.ToString(Session["ClinicCode"]));
            model.SummaryData = new List<Summary_Entity>();
            model.SummaryAccountwiseData = new List<Summary_accountwise_entity>();
            model.HeadTypeList = Extend.ToList<AccountHeadType_Entity>(DataSet.Tables[2]);
            return View("SummaryReport", model);
        }
        public ActionResult getSummaryReport(ReportModel RM)
        {
            ReportModel model = new ReportModel();
            var DataSet = Rep_Master.GetAll(Convert.ToString(Session["ClinicCode"]), RM.FromDate, RM.ToDate, RM.AccountHeadId);
            model.SummaryData = Extend.ToList<Summary_Entity>(DataSet.Tables[0]);
            model.SummaryAccountwiseData = Extend.ToList<Summary_accountwise_entity>(DataSet.Tables[1]);
            model.HeadTypeList = Extend.ToList<AccountHeadType_Entity>(DataSet.Tables[2]);
            return PartialView("SummaryReport", model);
        }
        public ActionResult LedgerTransactionReport(JournalTransaction JT)
        {
            JournalTransaction model = new JournalTransaction();
            var DataSet = Rep_Master.GetAllledger(Convert.ToString(Session["ClinicCode"]));
            model.journal_transaction_data = new List<JournalTransaction_Entity>();
            model.openingbalance = new List<JournalTransaction_Entity>();
            model.Monthwise = new List<JournalTransaction_Entity>();
            model.Invoicewise = new List<JournalTransaction_Entity>();
            model.LedgerList = Extend.ToList<AccountLedger_Entity>(DataSet.Tables[0]);
            return View("LedgerTransactionReport", model);
        }
        public ActionResult SearchLedgerTransactionReport(JournalTransaction JT)
        {
            JournalTransaction model = new JournalTransaction();
            var DataSet = Rep_Master.GetAlljournal(Convert.ToString(Session["ClinicCode"]), JT.FromDate, JT.ToDate, JT.Acc_id);
            model.LedgerList = new List<AccountLedger_Entity>();
            if (JT.op_balance == true)
            {
                model.openingbalance = Extend.ToList<JournalTransaction_Entity>(DataSet.Tables[0]);
            }
            else
            {
                model.openingbalance = new List<JournalTransaction_Entity>();
            }
            model.Monthwise = Extend.ToList<JournalTransaction_Entity>(DataSet.Tables[1]);
            model.Invoicewise = Extend.ToList<JournalTransaction_Entity>(DataSet.Tables[2]);
            model.journal_transaction_data = Extend.ToList<JournalTransaction_Entity>(DataSet.Tables[3]);
            return View("TransactionReport", model);
        }
        public ActionResult Receiptlist()
        {
            ReceiptModel model = new ReceiptModel();
            model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.Type = "All";
            model.RecList = GetAllReportList(Convert.ToString(Session["ClinicCode"]), null, null, model.Type);
            return View("Receiptlist", model);
        }
        public List<ReceiptList> GetAllReportList(string HosCode, string Fromdate = null, string Todate = null, string Type = null)
        {
            var Ds = Rep_Master.GetReceiptList(HosCode, Fromdate, Todate, Type);
            List<ReceiptList> Data = Extend.ToList<ReceiptList>(Ds.Tables[0]);
            return Data;
        }
        public PartialViewResult FilterReceiptList(ReceiptModel model)
        {
            model.RecList = GetAllReportList(Convert.ToString(Session["ClinicCode"]), model.FromDate, model.ToDate, model.Type);
            return PartialView("_ReceiptListTable", model);
        }
        #region "IndividualReport prepared by Dhaval"
        public ActionResult IndividualReport()
        {
            IndividualReport Model = new IndividualReport();
            return View(Model);
        }
        public PartialViewResult GetReportData(IndividualReport M)
        {
            IndividualReport model = new IndividualReport();
            var ds = Rep_Master.GetreportData(M.reportType, Convert.ToString(Session["ClinicCode"]), Convert.ToDateTime(M.FromDate), Convert.ToDateTime(M.ToDate), M.Type);
            model.groupList = Extend.ToList<GroupWiseList>(ds.Tables[0]);
            model.gridList = new List<ReportList>();
            model.reportType = M.reportType;
            int Id = 0; string Code = "";
            if (M.reportType == "Expenses")
            {
                string[] array = new string[2];
                array[0] = "Services"; array[1] = "Investigation";
                for (int d = 0; d < array.Length; d++)
                {
                    DataView dv1 = new DataView(ds.Tables[1]);
                    dv1.RowFilter = "DataType ='"+ Convert.ToString(array[d])+"'";
                    DataTable dt = dv1.ToTable(); Id = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Id != Convert.ToInt32(dt.Rows[i]["ID"]))
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                            decimal amount = 0;
                            //Fillter Rows
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "ID= '" + Id + "' And DataType= '" + Convert.ToString(array[d]) + "'"; //"ID =" + Id;
                            DataTable DT_Filter = dv.ToTable();
                            //End Fillter
                            for (int j = 0; j < DT_Filter.Rows.Count; j++)
                            {
                                amount += Convert.ToDecimal(DT_Filter.Rows[j]["Amount"]);
                            }
                            var lst = new ReportList
                            {
                                Amount = amount,
                                group_id = Convert.ToInt32(dt.Rows[i]["group_id"]),
                                Name = Convert.ToString(dt.Rows[i]["Name"]),
                                Qty = DT_Filter.Rows.Count,
                                DataType = Convert.ToString(dt.Rows[i]["DataType"]),
                                SubReportLists = Extend.ToList<SubReport>(DT_Filter)
                            };
                            model.gridList.Add(lst);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (M.reportType == "Doctorfees" || M.reportType == "BedRent")
                    {
                        if (Code != (M.reportType == "Doctorfees" ? Convert.ToString(ds.Tables[1].Rows[i]["ID"]) : Convert.ToString(ds.Tables[1].Rows[i]["Name"])))
                        {
                            Code = M.reportType == "Doctorfees" ? Convert.ToString(ds.Tables[1].Rows[i]["ID"]) : Convert.ToString(ds.Tables[1].Rows[i]["Name"]);
                            decimal amount = 0;
                            //Fillter Rows
                            DataView dv = new DataView(ds.Tables[1]);
                            dv.RowFilter = M.reportType == "Doctorfees" ? string.Format("ID LIKE '%{0}%'", Code) : string.Format("Name LIKE '%{0}%'", Code);
                            DataTable DT_Filter = dv.ToTable();
                            //End Fillter
                            for (int j = 0; j < DT_Filter.Rows.Count; j++)
                            {
                                amount += Convert.ToDecimal(DT_Filter.Rows[j]["Amount"]);
                            }
                            var lst = new ReportList
                            {
                                Amount = amount,
                                group_id = Convert.ToInt32(ds.Tables[1].Rows[i]["group_id"]),
                                Name = Convert.ToString(ds.Tables[1].Rows[i]["Name"]),
                                Qty = DT_Filter.Rows.Count,
                                SubReportLists = Extend.ToList<SubReport>(DT_Filter)
                            };
                            model.gridList.Add(lst);
                        }
                    }
                    else
                    {
                        if (Id != Convert.ToInt32(ds.Tables[1].Rows[i]["ID"]))
                        {
                            Id = Convert.ToInt32(ds.Tables[1].Rows[i]["ID"]);
                            decimal amount = 0;
                            //Fillter Rows
                            DataView dv = new DataView(ds.Tables[1]);
                            dv.RowFilter = "ID =" + Id;
                            DataTable DT_Filter = dv.ToTable();
                            //End Fillter
                            for (int j = 0; j < DT_Filter.Rows.Count; j++)
                            {
                                amount += Convert.ToDecimal(DT_Filter.Rows[j]["Amount"]);
                            }
                            var lst = new ReportList
                            {
                                Amount = amount,
                                group_id = Convert.ToInt32(ds.Tables[1].Rows[i]["group_id"]),
                                Name = Convert.ToString(ds.Tables[1].Rows[i]["Name"]),
                                Qty = DT_Filter.Rows.Count,
                                SubReportLists = Extend.ToList<SubReport>(DT_Filter)
                            };
                            model.gridList.Add(lst);
                        }
                    }
                }
            }
            //model.gridList = Extend.ToList<ReportList>(ds.Tables[1]);
            return PartialView("IndividualReport", model);
        }
        #endregion

        #region "Invoice For Settalment"
        public ActionResult SettlementInvoiceList()
        {
            Set_Invoice Model = new Set_Invoice();
            var DataSet = Rep_Master.GetSettalmentInvoiceAll(Convert.ToString(Session["ClinicCode"]), "get_all_opdinvoice");
            Model.InvoiceList = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            Model.Type = "OPDInvoice";
            return View("SettlementInvoiceList", Model);
        }
        public PartialViewResult SettlementIpInvoiceList()
        {
            Set_Invoice Model = new Set_Invoice();
            var DataSet = Rep_Master.GetSettalmentInvoiceAll(Convert.ToString(Session["ClinicCode"]), "get_all_ipinvoice");
            Model.InvoiceList = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
            Model.Type = "IPInvoice";
            return PartialView("SettlementInvoiceList", Model);
        }
        public ActionResult SettlementInvoiceView(string enc)
        {
            InvoiceModel model = new InvoiceModel();
            model.InvoicePara = new Invoicepara();
            if (enc != "")
            {
                var DataSet = new Invoice_Master(bsInfo).GetAllInvoiceDetails(enc);
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.InvoicePara.opd_doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.InvoicePara.opd_investigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.InvoicePara.opd_advanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.InvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.PaidAmount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["paid_amount"]);
                model.ExtraDiscount = Convert.ToDecimal(DataSet.Tables[7].Rows[0]["ExtraDiscount"]);
                model.FooterHtml = Convert.ToString(DataSet.Tables[7].Rows[0]["Footer"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[8]);
            }
            return View(model);
        }
        public ActionResult SettlementIpInvoiceView(string enc)
        {
            InvoiceModel model = new InvoiceModel();
            model.IPInvoicePara = new IPInvoicepara();
            if (enc != "")
            {
                var DataSet = new Invoice_Master(bsInfo).GetIPInvoiceDetails(enc);
                model.InvoiceData = Extend.ToList<Invoice_Entity>(DataSet.Tables[0]);
                model.HospitalData = Extend.ToList<Cliniq_Entity>(DataSet.Tables[1]);
                model.PatientData = Extend.ToList<Patient_Entity>(DataSet.Tables[2]);
                model.IPInvoicePara.doctorvisit = Extend.ToList<IPDoctorVisit>(DataSet.Tables[3]);
                model.IPInvoicePara.ipinvestigation = Extend.ToList<IPInvestigation>(DataSet.Tables[4]);
                model.IPInvoicePara.ipadvanceservices = Extend.ToList<IPAdvanceServices>(DataSet.Tables[5]);
                model.IPInvoicePara.ipotservices = Extend.ToList<IPOtServices>(DataSet.Tables[6]);
                model.IPInvoicePara.IPMedList = Extend.ToList<IPMedicineList>(DataSet.Tables[7]);
                model.IPInvoicePara.IPAdmitRoomList = Extend.ToList<IPAdmitRoom>(DataSet.Tables[8]);
                model.PaidAmount = Convert.ToString(DataSet.Tables[9].Rows[0]["paid_amount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["paid_amount"]) : 0;
                model.ExtraDiscount = Convert.ToString(DataSet.Tables[9].Rows[0]["extra_discount"]) != "" ? Convert.ToDecimal(DataSet.Tables[9].Rows[0]["extra_discount"]) : 0;
                model.FooterHtml = Convert.ToString(DataSet.Tables[9].Rows[0]["Footer"]);
                model.JournalData = Extend.ToList<Journal_Entity>(DataSet.Tables[10]);
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult IPInvoiceSettalAccount(int ServiceId, string ServiceType, string InvoiceCode, decimal Settal_Discount, string Type)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            var dataset = Rep_Master.IPInvoiceSettalment(ServiceId, ServiceType, InvoiceCode, Settal_Discount, hoscode, usercode, Type == "IP" ? "settlement_ip_updatediscount" : "settlement_opd_updatediscount");
            var data = CustomHelper.DataTableToJSONWithJavaScriptSerializer(dataset.Tables[0]);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Profit report"
        public ActionResult ProfitReport()
        {
            IndividualReport Modal = new IndividualReport();
            return View(Modal);
        }
        //public JsonResult GetProfitData(IndividualReport M)
        //{
        //    var ds = Rep_Master.GetProfitreportData(M.reportType, Convert.ToString(Session["ClinicCode"]), Convert.ToDateTime(M.FromDate), Convert.ToDateTime(M.ToDate));
        //    List<ProfitReportList> data = Extend.ToList<ProfitReportList>(ds.Tables[0]);
        //    List<ProfitGraph> Graphdata = Extend.ToList<ProfitGraph>(ds.Tables[1]);
        //    List<string> name = new List<string>();
        //    List<decimal> Opval = new List<decimal>(); List<decimal> Ipval = new List<decimal>();
        //    List<decimal> DirectBillval = new List<decimal>();
        //    List<string> typ = new List<string>(); string grpname = "";
        //    foreach (var item in Graphdata)
        //    {
        //        if (grpname != item.Name)
        //        {
        //            grpname = item.Name;
        //            name.Add(item.Name);
        //            var lst = Graphdata.FindAll(h => h.Name == item.Name).ToList();

        //            if (lst.FindAll(h => h.Type == "DirectBilling").ToList().Count > 0)
        //                DirectBillval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "DirectBilling")[0].Value));
        //            else
        //                DirectBillval.Add(Convert.ToDecimal(0));

        //            if (lst.FindAll(h => h.Type == "IP").ToList().Count > 0)
        //                Ipval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "IP")[0].Value));
        //            else
        //                Ipval.Add(Convert.ToDecimal(0));

        //            if (lst.FindAll(h => h.Type == "OP").ToList().Count > 0)
        //                Opval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "OP")[0].Value));
        //            else
        //                Opval.Add(Convert.ToDecimal(0));
        //        }
        //    }
        //    var JSonGridResult = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Report/_ProfitreportGrid.cshtml", data, true);
        //    return Json(new { GraphOPvalue = Opval, GraphIpvalue = Ipval, GraphBillvalue = DirectBillval, Graphname = name, datahtml = JSonGridResult }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetProfitData(IndividualReport M)
        {
            var JSonGridResult = "";
            Profitdata data = new Profitdata();
            var ds = Rep_Master.GetProfitreportData(M.reportType, Convert.ToString(Session["ClinicCode"]), Convert.ToDateTime(M.FromDate), Convert.ToDateTime(M.ToDate), M.Type);
            data.servicegrouplst = Extend.ToList<ServiceGroup>(ds.Tables[0]);
            data.profitlst = Extend.ToList<ProfitReportList>(ds.Tables[1]);
            List<ProfitGraph> Graphdata = Extend.ToList<ProfitGraph>(ds.Tables[2]);
            List<string> name = new List<string>();
            List<decimal> Opval = new List<decimal>(); List<decimal> Ipval = new List<decimal>();
            List<decimal> DirectBillval = new List<decimal>();
            List<string> typ = new List<string>(); string grpname = "";
            foreach (var item in Graphdata)
            {
                if (grpname != item.Name)
                {
                    grpname = item.Name;
                    name.Add(item.Name);
                    var lst = Graphdata.FindAll(h => h.Name == item.Name).ToList();

                    if (lst.FindAll(h => h.Type == "DirectBilling").ToList().Count > 0)
                        DirectBillval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "DirectBilling")[0].Value));
                    else
                        DirectBillval.Add(Convert.ToDecimal(0));

                    if (lst.FindAll(h => h.Type == "IP").ToList().Count > 0)
                        Ipval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "IP")[0].Value));
                    else
                        Ipval.Add(Convert.ToDecimal(0));

                    if (lst.FindAll(h => h.Type == "OP").ToList().Count > 0)
                        Opval.Add(Convert.ToDecimal(lst.FindAll(h => h.Type == "OP")[0].Value));
                    else
                        Opval.Add(Convert.ToDecimal(0));
                }
            }
            JSonGridResult = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Report/_ProfitreportGrid.cshtml", data, true);
            return Json(new { GraphOPvalue = Opval, GraphIpvalue = Ipval, GraphBillvalue = DirectBillval, Graphname = name, datahtml = JSonGridResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult TransactionReport()
        {
            JournalTransaction model = new JournalTransaction();
            var DataSet = Rep_Master.GetAllledger(Convert.ToString(Session["ClinicCode"]));
            model.journal_transaction_data = new List<JournalTransaction_Entity>();
            model.openingbalance = new List<JournalTransaction_Entity>();
            model.Monthwise = new List<JournalTransaction_Entity>();
            model.Invoicewise = new List<JournalTransaction_Entity>();
            model.LedgerList = Extend.ToList<AccountLedger_Entity>(DataSet.Tables[0]);
            return View("TransactionReport", model);
        }

    }
}