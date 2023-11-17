using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HMS.BizLayer;
using QRCoder;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using static iTextSharp.text.pdf.Barcode128;
using Newtonsoft.Json.Linq;
using HMS;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class TestAndInvestigationController : _Base
    {
        readonly TestAndInvestigation_Master TestMaster;
        readonly Lab_Master LabMaster;
        // GET: TestAndInvestigation
        public TestAndInvestigationController()
        {
            TestMaster = new TestAndInvestigation_Master(bsInfo);
            LabMaster = new Lab_Master(bsInfo);
        }
        #region "pending sample"
        public ActionResult PendingTestSample()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            DataSet ds = TestMaster.GetAllInvestigationAppointmentSample(Convert.ToString(Session["ClinicCode"]));
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            return View("PendingTestSample", model);
        }
        [HttpPost]
        public ActionResult AddSample(int Investigationid, string Appointmentcode)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            string barcode = GenerateBarCode();
            string barcodepath = barcode + ".png";
            var data = TestMaster.AddInvestigationsample(Investigationid, Appointmentcode, Convert.ToInt64(barcode), barcodepath);
            return PartialView("PendingTestSample", model);
        }
        public string GenerateBarCode()
        {
            string Password = DateTime.Now.ToString("yyyy''MM''dd''HH''mm''ss''ffff");

            QRCodeGenerator ObjQr = new QRCodeGenerator();

            QRCodeData qrCodeData = ObjQr.CreateQrCode(Password, QRCodeGenerator.ECCLevel.Q);

            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())

            {
                var folderpath = ConfigurationManager.AppSettings["addinvbarcode"];
                Response.ContentType = "image/png";
                bitMap.Save((folderpath + "" + Password + ".png"), ImageFormat.Png);

                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteImage = ms.ToArray();

                //ViewBag.Url = "data:image/png;base64," + Convert.ToBase64String(byteImage);

            }
            return Password;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    //The Image is drawn based on length of Barcode text.
            //    using (Bitmap bitMap = new Bitmap(Password.Length * 32, 100))
            //    {
            //        //The Graphics library object is generated for the Image.
            //        using (Graphics graphics = Graphics.FromImage(bitMap))
            //        {
            //            //The installed Barcode font.
            //            var fonts = new PrivateFontCollection();
            //            fonts.AddFontFile(Server.MapPath("~/wwwroot/assets/fonts/IDAutomationHC39M.ttf"));
            //            System.Drawing.Font oFont = new System.Drawing.Font((FontFamily)fonts.Families[0], 20f);
            //            //Font oFont = new Font("IDAutomationHC39M", 20);
            //            PointF point = new PointF(2f, 2f);
            //            //White Brush is used to fill the Image with white color.
            //            SolidBrush whiteBrush = new SolidBrush(Color.White);
            //            graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
            //            //Black Brush is used to draw the Barcode over the Image.
            //            SolidBrush blackBrush = new SolidBrush(Color.Black);
            //            graphics.DrawString("*" + Password + "*", oFont, blackBrush, point);
            //        }
            //        //The Bitmap is saved to Memory Stream.
            //        var folderpath = ConfigurationManager.AppSettings["addinvbarcode"];
            //        Response.ContentType = "image/png";
            //        bitMap.Save((folderpath + "" + Password + ".png"), ImageFormat.Png);
            //    }
            //}
            //return Password;
        }
        [HttpPost]
        public string Print_Barcode(string ApptCode, int invid)
        {
            DataSet ds = TestMaster.GetinvestigationBarcode(invid, ApptCode);

            string img = ConfigurationManager.AppSettings["getinvbarcode"] + Convert.ToString(ds.Tables[0].Rows[0]["barcode"]);
            string html = string.Empty;
            html += "<table><tr>" +
            "<td>" +
            "<td style='text-align:center !important;'><img width='200' height ='75' src=" + img + " alt=''></td>" +
            "</tr></table>";


            return html.ToString();
            //PrintHelpPage(HtmlStr.ToString());
        }
        #endregion
        #region "Lab Collection Operation"

        public ActionResult HosCollactionOperation()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();

            DataSet ds = TestMaster.GetAlloperationItem(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.OP_InvestigationCollection = new List<OP_investigation_Entity>();
            model.OP_InvestigationItems = new List<OP_investigationItem>();
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[1]);
            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }

            return View("HosCollactionOperation", model);
        }

        [HttpPost]
        public PartialViewResult Filterinvoperation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;

            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAlloperationItem(Convert.ToString(Session["ClinicCode"]), status, INV.fromdate, INV.todate);
            model.OP_InvestigationCollection = new List<OP_investigation_Entity>();
            model.OP_InvestigationItems = new List<OP_investigationItem>();
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[1]);
            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }

            return PartialView("HosCollactionOperation", model);
        }

        //oplaboperation
        public ActionResult OP_HosCollactionOperation()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            DataSet ds = TestMaster.GetAlloperationItem_OP(Convert.ToString(Session["ClinicCode"]), 0, null, null);

            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("HosCollactionOperation", model);
        }

        [HttpPost]
        public PartialViewResult Filteropdinvoperation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAlloperationItem_OP(Convert.ToString(Session["ClinicCode"]), status, INV.fromdateop, INV.todateop);
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }
            return PartialView("HosCollactionOperation", model);
        }

        [HttpPost]
        public ActionResult AddHosOperation(int service_id, int InvestigationmasterId, string AppoinmentCode, int supplierid)
        {
            int status = 0;
            try
            {
                TestAndInvestigationModel model = new TestAndInvestigationModel();
                string barcode = GenerateBarCode();
                string barcodepath = barcode + ".png";
                var hoscode = Convert.ToString(Session["ClinicCode"]);
                var usercode = Convert.ToString(Session["UserCode"]);
                int data = TestMaster.AddInvoperation(service_id, InvestigationmasterId, AppoinmentCode, supplierid, Convert.ToInt64(barcode), barcodepath, hoscode, usercode);
                if (data == 3)
                {
                    status = 3;
                }
                else if (data == 1)
                {
                    status = 1;
                }
                else
                    status = 0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                status = 0;
            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddHosOperation_OP(int service_id, int investigation_id, string invoice_code, int supplierid)
        {
            int status = 0;
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            string barcode = GenerateBarCode();
            string barcodepath = barcode + ".png";
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            bool data = TestMaster.AddInvoperation_OP(service_id, investigation_id, invoice_code, supplierid, Convert.ToInt64(barcode), barcodepath, hoscode, usercode);
            if (data)
            {
                status = 1;
            }
            else
                status = 0;

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region "Lab Collection Regent Item"
        public ActionResult HosCollectionRegentItem()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            //model.hospitalcollection = new HosCollection();
            DataSet ds = TestMaster.GetAllInvestigationCollection(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);

            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }

            //model.hospitalcollection.testlist = new List<TestItem_Entity>();
            //model.hospitalcollection.Reg_List = new List<SelectListItem>();
            return View("HosCollectionRegentItem", model);
        }
        [HttpPost]
        public PartialViewResult Filterinvcollection(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllInvestigationCollection(Convert.ToString(Session["ClinicCode"]), status, INV.fromdate, INV.todate);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);

            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }
            //model.hospitalcollection.testlist = new List<TestItem_Entity>();
            //model.hospitalcollection.Reg_List = new List<SelectListItem>();
            return PartialView("HosCollectionRegentItem", model);
        }
        public ActionResult HosCollectionRegentItem_OP()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            DataSet ds = TestMaster.GetAllInvestigationCollection_OP(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);

            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("HosCollectionRegentItem", model);
        }

        [HttpPost]
        public PartialViewResult Filteropdinvcollection(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllInvestigationCollection_OP(Convert.ToString(Session["ClinicCode"]), status, INV.fromdateop, INV.todateop);
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);

            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            //model.hospitalcollection.testlist = new List<TestItem_Entity>();
            //model.hospitalcollection.Reg_List = new List<SelectListItem>();
            return PartialView("HosCollectionRegentItem", model);
        }

        [HttpPost]
        public PartialViewResult AddRegentItem(int service_id, int Investigationid, string Appointmentcode)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            var usercode = Convert.ToString(Session["UserCode"]);
            DataSet data = TestMaster.AddInvestigationRegentItem(service_id, Investigationid, Appointmentcode, Convert.ToString(Session["ClinicCode"]), usercode);
            model.hospitalcollection = Extend.ToList<HosCollection>(data.Tables[0]);
            return PartialView("HosCollectionRegentItem", model);
        }

        [HttpPost]
        public PartialViewResult AddRegentItem_OP(int service_id, int investigation_id, string invoice_code)
        {
            //int status = 0;
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            var usercode = Convert.ToString(Session["UserCode"]);
            DataSet data = TestMaster.AddInvestigationRegentItem_OP(service_id, investigation_id, invoice_code, Convert.ToString(Session["ClinicCode"]), usercode);
            model.hospitalcollection = Extend.ToList<HosCollection>(data.Tables[0]);
            //if (data<0)
            //{
            //    status = 1;
            //}
            //else
            //status = 0;

            //return Json(status, JsonRequestBehavior.AllowGet);

            return PartialView("HosCollectionRegentItem", model);
        }
        [HttpPost]
        public ActionResult GenerateBarCode(List<BarcodeData> Data)
        {
            string[] image = new string[Data.Count()];
            int i = 0;
            foreach (var item in Data)
            {
                string prodCode = item.TestUniqId;
                if (prodCode.Length > 0)
                {
                    Barcode128 code128 = new Barcode128();
                    code128.CodeType = Barcode.CODE128;
                    code128.ChecksumText = true;
                    code128.GenerateChecksum = true;
                    code128.StartStopText = false;
                    code128.Code = prodCode;
                    //code128.AltText = prodCode;
                    //code128.Extended = true;
                    code128.BarHeight = 40;

                    // Create a blank image 
                    Bitmap bm = new Bitmap(155, 80);
                    // provide width and height based on the barcode image to be generated. harcoded for sample purpose

                    Graphics bmpgraphics = Graphics.FromImage(bm);
                    bmpgraphics.Clear(Color.White);
                    // Provide this, else the background will be black by default

                    // generate the code128 barcode
                    bmpgraphics.DrawImage(code128.CreateDrawingImage(Color.Black, Color.White), new System.Drawing.Point(0, 25));

                    //generate the text for investigation Name
                    bmpgraphics.DrawString(item.InvName, new System.Drawing.Font(FontFamily.GenericSerif, 8), SystemBrushes.WindowText, new System.Drawing.Point(15, 1));

                    //generate the text for Patient Name
                    bmpgraphics.DrawString(item.PatientName, new System.Drawing.Font(FontFamily.GenericSerif, 8), SystemBrushes.WindowText, new System.Drawing.Point(15, 11));

                    //generate the text below the barcode image. If you want the placement to be dynamic, calculate the point based on size of the image
                    bmpgraphics.DrawString(prodCode, new System.Drawing.Font(FontFamily.GenericSerif, 8), SystemBrushes.WindowText, new System.Drawing.Point(15, 64));

                    using (MemoryStream ms = new MemoryStream())
                    {
                        bm.Save(ms, ImageFormat.Png);
                        image[i] = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                    i++;
                }
            }
            return Json(image, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult AddRegentItem(int Investigationid, string Appointmentcode, string appointmentid)
        //{

        //    //HosCollectionOprModel model = new HosCollectionOprModel();

        //    TestAndInvestigationModel model = new TestAndInvestigationModel();

        //    model.hospitalcollection = new HosCollection();

        //    model.hospitalcollection.testlist = new List<TestItem_Entity>();
        //    model.hospitalcollection.Reg_List = new List<SelectListItem>();

        //    var res = LabMaster.GetRegentItemBytest(Investigationid);
        //    List<SelectListItem> EM = new List<SelectListItem>();
        //    foreach (DataRow dr in res.Tables[0].Rows)
        //    {
        //        var ListPro = new SelectListItem
        //        {
        //            Text = (dr["reagent_name"]).ToString(),
        //            Value = (dr["reagent_id"]).ToString(),
        //        };
        //        EM.Add(ListPro);
        //    }
        //    model.hospitalcollection.Reg_List = EM;
        //    model.hospitalcollection.testid = Convert.ToInt32(res.Tables[1].Rows[0]["test_id"]);
        //    model.hospitalcollection.testname = Convert.ToString(res.Tables[1].Rows[0]["test_name"]);
        //    model.hospitalcollection.ctrl = 1;
        //    model.hospitalcollection.appointment_code = Appointmentcode;
        //    model.hospitalcollection.appointment_id = appointmentid;
        //    return PartialView("_CollectionOperationModel", model);
        //}

        //[HttpPost]
        //public ActionResult AddHOSCollection(TestAndInvestigationModel collection)
        //{
        //    string result = string.Empty;
        //    int status = 0;
        //    if (collection.hospitalcollection.regentid != null)
        //    {
        //        if (collection.hospitalcollection.status != 0)
        //        {
        //            result = string.Join(",", collection.hospitalcollection.regentid);

        //            //string barcode = GenerateBarCode();
        //            //string barcodepath = barcode + ".png";
        //            var data = TestMaster.AddInvestigationRegentItem(collection.hospitalcollection.testid, collection.hospitalcollection.appointment_code, result, collection.hospitalcollection.status, Convert.ToString(Session["ClinicCode"]));
        //            status = data;
        //        }
        //        else
        //        {
        //            status = 3;
        //        }
        //    }
        //    else
        //    {
        //        status = 2;
        //    }

        //    return Json(status, JsonRequestBehavior.AllowGet);
        //    //return PartialView("PendingTestSample", model);
        //    //return PartialView("_CollectionOperationModel", model);
        //}

        #endregion
        #region "Test&investigation Add Para"
        public ActionResult PendingTestandInvestigation()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            DataSet ds = TestMaster.GetAllInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();
            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);
            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }
            return View("PendingTestandInvestigation", model);
        }
        public ActionResult FilterPendingTestandInvestigation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), status, INV.fromdate, INV.todate);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();
            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }

            return View("PendingTestandInvestigation", model);
        }
        public ActionResult PendingTestandInvestigation_OP()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            DataSet ds = TestMaster.GetAllInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), 0, null, null);

            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("PendingTestandInvestigation", model);
        }
        public ActionResult FilteropdPendingTestandInvestigation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), status, INV.fromdateop, INV.todateop);
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();
            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("PendingTestandInvestigation", model);
        }
        [HttpPost]
        public ActionResult ReagentLists(int service_id, int Investigationid, string Appointmentcode, string unique_invstigation_id, string InvType)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.parameter = new addvalue_para();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            model.parameter.service_id = service_id;
            model.parameter.Investigationid = Investigationid;
            model.parameter.Appointmentcode = Appointmentcode;
            model.parameter.unique_invstigation_id = unique_invstigation_id;
            model.parameter.InvType = InvType;

            DataSet ds = TestMaster.getReagentLists(Investigationid, Convert.ToString(Session["ClinicCode"]));
            model.investigationname = Convert.ToString(ds.Tables[0].Rows[0]["InvestigationName"]);
            model.appointment_code = Appointmentcode;
            model.service_id = service_id;

            model.reagent_list = ds.Tables[1].ToList<ReagentList>().ToList();

            if (InvType == "radiology")
            {
                return PartialView("_Investigation_radiologyparaModel", model);
            }
            else
            {
                return PartialView("_InvestigationparaModel", model);
            }
        }
        [HttpPost]
        public ActionResult AddInvestigationPara(int service_id, int Investigationid, string Appointmentcode, string unique_invstigation_id, string InvType, int reagent_id)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.parameter = new addvalue_para();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara(service_id, Investigationid, Appointmentcode, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, reagent_id);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.appointment_code = Appointmentcode;
            model.service_id = service_id;
            model.parameter.service_id = service_id;
            model.parameter.Investigationid = Investigationid;
            model.parameter.Appointmentcode = Appointmentcode;
            model.parameter.unique_invstigation_id = unique_invstigation_id;
            model.parameter.InvType = InvType;
            model.reagent_id = reagent_id;
            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[2]);
                model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[4]);
                model.reagent_list = Extend.ToList<ReagentList>(data.Tables[5]);
                if (data.Tables[3].Rows.Count > 0)
                {
                    model.Doctorcode = Convert.ToString(data.Tables[3].Rows[0]["doc_code"]);
                }
                else
                {
                    model.Doctorcode = "";
                }
                if (data.Tables[4].Rows.Count > 0)
                {
                    model.EmployeeCode = Convert.ToString(data.Tables[4].Rows[0]["EmployeeCode"]);
                }
                else
                {
                    model.EmployeeCode = "";
                }
                model.invtype = InvType;
                var SubLst = new HosInvestigationPara();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                        investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
                        investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
                    };
                }
                else
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = "",
                        investigationmaster_id = Investigationid,
                        investigationsubgrop_id = 0,
                        investigationmaster_name = "",
                        investigation_uniqcode = unique_invstigation_id
                    };
                }
                var list = new SubGroup();
                list.invParaList = new List<HosInvestigationPara>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_Investigation_radiologyparaModel", model);
            }
            //End
            else
            {
                model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[3]);
                model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[5]);
                model.reagent_list = Extend.ToList<ReagentList>(data.Tables[6]);
                if (data.Tables[4].Rows.Count > 0)
                    model.Doctorcode = Convert.ToString(data.Tables[4].Rows[0]["doc_code"]);
                else
                    model.Doctorcode = "";

                if (data.Tables[5].Rows.Count > 0)
                    model.EmployeeCode = Convert.ToString(data.Tables[5].Rows[0]["EmployeeCode"]);
                else
                    model.EmployeeCode = "";
                //var pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[2]);
                var pararesult = new List<HosInvestigationPara>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationPara
                    {
                        investigationmaster_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationsubgrop_id"]),
                        investigationsubgroup_name = Convert.ToString(data.Tables[2].Rows[i]["investigationsubgroup_name"]),
                        investigationpara_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationpara_id"]),
                        investigationpara_name = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_name"]),
                        investigationpara_reference = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_reference"]),
                        find_ip_reference = Convert.ToString(data.Tables[2].Rows[i]["find_ip_reference"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[2].Rows[i]["investigation_uniqcode"])
                    };
                    pararesult.Add(p_lst);
                }
                DataRow[] dr = data.Tables[1].Select();
                if (dr.Count() > 0)
                {
                    foreach (var item in dr)
                    {
                        var list = new SubGroup
                        {
                            InvestigationSubGroupId = Convert.ToInt32(item[2]),
                            SubgroupName = Convert.ToString(item[3]),
                            investigationmaster_id = Convert.ToInt32(item[4]),
                            investigation_uniqcode = pararesult.Count > 0 ? pararesult[0].investigation_uniqcode : "",
                            Unit = Convert.ToString(item[5]),
                            Reference = Convert.ToString(item[6]),
                            FindReference = Convert.ToString(item[7]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[2]))
                        };
                        list.para_type = list.Reference == null || list.Reference == "" ? "" : "ManageBySubgroup";
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroup
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
                    };
                    model.invSubgroupList.Add(list);

                }
                return PartialView("_InvestigationparaModel", model);
            }
        }
        [HttpPost]
        public ActionResult AddInvestigationPara_OP(int service_id, int investigation_id, string invoice_code, string unique_invstigation_id, string InvType, int reagent_id)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara_OP(service_id, investigation_id, invoice_code, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, reagent_id);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.invoice_code = invoice_code;
            model.service_id = service_id;
            model.reagent_id = reagent_id;
            model.parameter = new addvalue_para();
            model.parameter.service_id = service_id;
            model.parameter.Investigationid = investigation_id;
            model.parameter.invoice_code = invoice_code;
            model.parameter.unique_invstigation_id = unique_invstigation_id;
            model.parameter.InvType = InvType;
            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[2]);
                model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[4]);
                model.reagent_list = Extend.ToList<ReagentList>(data.Tables[5]);
                if (data.Tables[3].Rows.Count > 0)
                {
                    model.Doctorcode = Convert.ToString(data.Tables[3].Rows[0]["doc_code"]);
                }
                else
                {
                    model.Doctorcode = "";
                }
                if (data.Tables[4].Rows.Count > 0)
                {
                    model.EmployeeCode = Convert.ToString(data.Tables[4].Rows[0]["EmployeeCode"]);
                }
                else
                {
                    model.EmployeeCode = "";
                }
                model.invtype = InvType;
                var SubLst = new HosInvestigationPara();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationPara

                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                        investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
                        investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
                    };
                }
                else
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = "",
                        investigationmaster_id = investigation_id,
                        investigationsubgrop_id = 0,
                        investigationmaster_name = "",
                        investigation_uniqcode = unique_invstigation_id
                    };
                }
                var list = new SubGroup();
                list.invParaList = new List<HosInvestigationPara>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_Investigation_radiologyparaOPModel", model);
            }
            //End
            else
            {
                model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[3]);
                model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[5]);
                model.reagent_list = Extend.ToList<ReagentList>(data.Tables[6]);
                if (data.Tables[4].Rows.Count > 0)
                {
                    model.Doctorcode = Convert.ToString(data.Tables[4].Rows[0]["doc_code"]);
                }
                else
                {
                    model.Doctorcode = "";
                }
                if (data.Tables[5].Rows.Count > 0)
                {
                    model.EmployeeCode = Convert.ToString(data.Tables[5].Rows[0]["EmployeeCode"]);
                }
                else
                {
                    model.EmployeeCode = "";
                }
                //var pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[2]);
                var pararesult = new List<HosInvestigationPara>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationPara
                    {
                        investigationmaster_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationsubgrop_id"]),
                        investigationsubgroup_name = Convert.ToString(data.Tables[2].Rows[i]["investigationsubgroup_name"]),
                        investigationpara_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationpara_id"]),
                        investigationpara_name = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_name"]),
                        investigationpara_reference = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_reference"]),
                        find_ip_reference = Convert.ToString(data.Tables[2].Rows[i]["find_ip_reference"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[2].Rows[i]["investigation_uniqcode"])
                    };
                    pararesult.Add(p_lst);
                }
                DataRow[] dr = data.Tables[1].Select();
                if (dr.Count() > 0)
                {
                    foreach (var item in dr)
                    {
                        var list = new SubGroup
                        {
                            InvestigationSubGroupId = Convert.ToInt32(item[2]),
                            SubgroupName = Convert.ToString(item[3]),
                            investigationmaster_id = Convert.ToInt32(item[4]),
                            investigation_uniqcode = pararesult.Count > 0 ? pararesult[0].investigation_uniqcode : "",
                            Unit = Convert.ToString(item[5]),
                            Reference = Convert.ToString(item[6]),
                            FindReference = Convert.ToString(item[7]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[2]))
                        };
                        list.para_type = list.Reference == null || list.Reference == "" ? "" : "ManageBySubgroup";
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroup
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == investigation_id)
                    };
                    model.invSubgroupList.Add(list);
                }
            }
            return PartialView("_InvestigationparaOPModel", model);
        }
        [HttpPost]
        public ActionResult ReagentLists_op(int service_id, int investigation_id, string invoice_code, string unique_invstigation_id, string InvType)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.parameter = new addvalue_para();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            model.parameter.service_id = service_id;
            model.parameter.Investigationid = investigation_id;
            model.parameter.invoice_code = invoice_code;
            model.parameter.unique_invstigation_id = unique_invstigation_id;
            model.parameter.InvType = InvType;

            DataSet ds = TestMaster.getReagentLists(investigation_id, Convert.ToString(Session["ClinicCode"]));
            model.investigationname = Convert.ToString(ds.Tables[0].Rows[0]["InvestigationName"]);
            model.invoice_code = invoice_code;
            model.service_id = service_id;

            model.reagent_list = ds.Tables[1].ToList<ReagentList>().ToList();

            if (InvType == "radiology")
            {
                return PartialView("_Investigation_radiologyparaOPModel", model);
            }
            else
            {
                return PartialView("_InvestigationparaOPModel", model);
            }
        }

        [HttpPost]
        public ActionResult ViewInvestigationPara(int service_id, int Investigationid, string Appointmentcode, string unique_invstigation_id, string InvType, int reagent_id, string regent_name)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.parameter = new addvalue_para();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara(service_id, Investigationid, Appointmentcode, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, reagent_id);
            if (regent_name != null && regent_name != "")
                model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]) + "(" + regent_name + ")";
            else
                model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.appointment_code = Appointmentcode;
            model.service_id = service_id;
            model.parameter.service_id = service_id;
            model.parameter.Investigationid = Investigationid;
            model.parameter.Appointmentcode = Appointmentcode;
            model.parameter.unique_invstigation_id = unique_invstigation_id;
            model.parameter.InvType = InvType;
            model.reagent_id = reagent_id;
            //model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[3]);
            //model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[5]);
            //model.reagent_list = Extend.ToList<ReagentList>(data.Tables[6]);
            //if (data.Tables[4].Rows.Count > 0)
            //    model.Doctorcode = Convert.ToString(data.Tables[4].Rows[0]["doc_code"]);
            //else
            //    model.Doctorcode = "";

            //if (data.Tables[5].Rows.Count > 0)
            //    model.EmployeeCode = Convert.ToString(data.Tables[5].Rows[0]["EmployeeCode"]);
            //else
            //    model.EmployeeCode = "";
            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.invtype = InvType;
                var SubLst = new HosInvestigationPara();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                        investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
                        investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
                    };
                }
                else
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = "",
                        investigationmaster_id = Investigationid,
                        investigationsubgrop_id = 0,
                        investigationmaster_name = "",
                        investigation_uniqcode = unique_invstigation_id
                    };
                }
                var list = new SubGroup();
                list.invParaList = new List<HosInvestigationPara>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_ViewInvestigation_radiologyParaModel", model);
            }
            //End
            else
            {
                var pararesult = new List<HosInvestigationPara>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationPara
                    {
                        investigationmaster_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationsubgrop_id"]),
                        investigationsubgroup_name = Convert.ToString(data.Tables[2].Rows[i]["investigationsubgroup_name"]),
                        investigationpara_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationpara_id"]),
                        investigationpara_name = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_name"]),
                        investigationpara_reference = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_reference"]),
                        find_ip_reference = Convert.ToString(data.Tables[2].Rows[i]["find_ip_reference"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[2].Rows[i]["investigation_uniqcode"])
                    };
                    pararesult.Add(p_lst);
                }
                DataRow[] dr = data.Tables[1].Select();
                if (dr.Count() > 0)
                {
                    foreach (var item in dr)
                    {
                        var list = new SubGroup
                        {
                            InvestigationSubGroupId = Convert.ToInt32(item[2]),
                            SubgroupName = Convert.ToString(item[3]),
                            investigationmaster_id = Convert.ToInt32(item[4]),
                            investigation_uniqcode = pararesult.Count > 0 ? pararesult[0].investigation_uniqcode : "",
                            Unit = Convert.ToString(item[5]),
                            Reference = Convert.ToString(item[6]),
                            FindReference = Convert.ToString(item[7]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[2]))
                        };
                        list.para_type = list.Reference == null || list.Reference == "" ? "" : "ManageBySubgroup";
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroup
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
                    };
                    model.invSubgroupList.Add(list);

                }
                return PartialView("_ViewInvestigationParaModel", model);
            }
        }

        [HttpPost]
        public ActionResult ViewInvestigationPara_op(int service_id, int investigation_id, string invoice_code, string unique_invstigation_id, string InvType, int RagentId)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara_OP(service_id, investigation_id, invoice_code, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, RagentId);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.invoice_code = invoice_code;
            model.service_id = service_id;
            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.invtype = InvType;
                //var radio_pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[1]);
                //var list = new SubGroup
                //{
                //    SubgroupName = "",
                //    invParaList = radio_pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
                //};
                //model.invSubgroupList.Add(list);
                var SubLst = new HosInvestigationPara();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationPara
                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                        investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
                        investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
                    };
                }
                var list = new SubGroup();
                list.invParaList = new List<HosInvestigationPara>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_ViewInvestigation_radiologyParaModel", model);
            }
            //End
            else
            {
                var pararesult = new List<HosInvestigationPara>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationPara
                    {
                        investigationmaster_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationmaster_id"]),
                        investigationsubgrop_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationsubgrop_id"]),
                        investigationsubgroup_name = Convert.ToString(data.Tables[2].Rows[i]["investigationsubgroup_name"]),
                        investigationpara_id = Convert.ToInt32(data.Tables[2].Rows[i]["investigationpara_id"]),
                        investigationpara_name = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_name"]),
                        investigationpara_reference = Convert.ToString(data.Tables[2].Rows[i]["investigationpara_reference"]),
                        find_ip_reference = Convert.ToString(data.Tables[2].Rows[i]["find_ip_reference"]),
                        investigation_uniqcode = Convert.ToString(data.Tables[2].Rows[i]["investigation_uniqcode"])
                    };
                    pararesult.Add(p_lst);
                }
                DataRow[] dr = data.Tables[1].Select();
                if (dr.Count() > 0)
                {
                    foreach (var item in dr)
                    {
                        var list = new SubGroup
                        {
                            InvestigationSubGroupId = Convert.ToInt32(item[2]),
                            SubgroupName = Convert.ToString(item[3]),
                            investigationmaster_id = Convert.ToInt32(item[4]),
                            investigation_uniqcode = pararesult.Count > 0 ? pararesult[0].investigation_uniqcode : "",
                            Unit = Convert.ToString(item[5]),
                            Reference = Convert.ToString(item[6]),
                            FindReference = Convert.ToString(item[7]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[2]))
                        };
                        list.para_type = list.Reference == null || list.Reference == "" ? "" : "ManageBySubgroup";
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroup
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == investigation_id)
                    };
                    model.invSubgroupList.Add(list);

                }
            }
            return PartialView("_ViewInvestigationParaModel", model);
        }

        public ActionResult AddHOSInvestigation(TestAndInvestigationModel model)
        {
            string status = "";
            DataTable dt = CreateTable();
            //Added By Dhaval
            if (model.invtype == "radiology")
            {
                var para = model.invSubgroupList[0].invParaList.SingleOrDefault();
                DataRow dr = dt.NewRow();
                dr["service_id"] = Convert.ToInt32(model.service_id);
                dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                dr["investigationsubgroup_name"] = Convert.ToString(para.investigationsubgroup_name);
                dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                dr["appointment_code"] = Convert.ToString(model.appointment_code);
                dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                dt.Rows.Add(dr);
            }
            //End
            else
            {
                if (model.reagent_id > 0)
                {
                    if (model.invSubgroupList != null)
                    {
                        foreach (var item in model.invSubgroupList)
                        {
                            if (item.para_type != null)
                            {
                                DataRow dr1 = dt.NewRow();
                                dr1["service_id"] = Convert.ToInt32(model.service_id);
                                dr1["investigationmaster_id"] = Convert.ToInt32(item.investigationmaster_id);
                                dr1["investigationsubgrop_id"] = Convert.ToInt32(item.InvestigationSubGroupId);
                                //add by dhaval
                                dr1["investigationsubgroup_name"] = "";
                                //end dhaval
                                dr1["investigationpara_id"] = 0;
                                dr1["find_ip_reference"] = Convert.ToString(item.FindReference);
                                dr1["appointment_code"] = Convert.ToString(model.appointment_code);
                                dr1["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                dr1["investigation_uniqcode"] = Convert.ToString(item.investigation_uniqcode);
                                dr1["ManagePara"] = Convert.ToString(item.para_type);
                                dt.Rows.Add(dr1);
                            }
                            if (item.invParaList != null)
                            {
                                foreach (var para in item.invParaList)
                                {
                                    //if (para.find_ip_reference != null)
                                    //{
                                    DataRow dr = dt.NewRow();
                                    dr["service_id"] = Convert.ToInt32(model.service_id);
                                    dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                                    dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                                    //add by dhaval
                                    dr["investigationsubgroup_name"] = "";
                                    //end dhaval
                                    dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                                    dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                                    dr["appointment_code"] = Convert.ToString(model.appointment_code);
                                    dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                    dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                                    dr["ManagePara"] = "ManageByParameter";
                                    dt.Rows.Add(dr);
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                var usercode = Convert.ToString(Session["UserCode"]);
                bool add = TestMaster.AddHOSInvestigation(dt, model.Doctorcode, usercode, model.EmployeeCode, model.reagent_id);
                if (add)
                {
                    status = "1";
                }
                else
                    status = "0";
            }
            else
            {
                if (model.reagent_id == 0)
                    status = "3";
                else
                    status = "2";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("service_id", typeof(int));
            dt.Columns.Add("investigationmaster_id", typeof(int));
            dt.Columns.Add("investigationsubgrop_id", typeof(int));
            //Added by Dhaval
            dt.Columns.Add("investigationsubgroup_name", typeof(string));
            //end
            dt.Columns.Add("investigationpara_id", typeof(int));
            dt.Columns.Add("find_ip_reference", typeof(string));
            dt.Columns.Add("appointment_code", typeof(string));
            dt.Columns.Add("hoscode", typeof(string));
            dt.Columns.Add("investigation_uniqcode", typeof(string));
            dt.Columns.Add("ManagePara", typeof(string));
            return dt;
        }
        [HttpPost]
        public ActionResult AddHOSInvestigation_OP(TestAndInvestigationModel model)
        {
            string status = "";
            DataTable dt = CreateTableOP();
            //Added By Dhaval
            if (model.invtype == "radiology")
            {
                var para = model.invSubgroupList[0].invParaList.SingleOrDefault();
                DataRow dr = dt.NewRow();
                dr["service_id"] = Convert.ToInt32(model.service_id);
                dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                dr["investigationsubgroup_name"] = Convert.ToString(para.investigationsubgroup_name);
                dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                dr["invoice_code"] = Convert.ToString(model.invoice_code);
                dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                dt.Rows.Add(dr);
            }
            //End
            else
            {
                if (model.reagent_id > 0)
                {
                    if (model.invSubgroupList != null)
                    {
                        foreach (var item in model.invSubgroupList)
                        {
                            if (item.FindReference != null)
                            {
                                DataRow dr1 = dt.NewRow();
                                dr1["service_id"] = Convert.ToInt32(model.service_id);
                                dr1["investigationmaster_id"] = Convert.ToInt32(item.investigationmaster_id);
                                dr1["investigationsubgrop_id"] = Convert.ToInt32(item.InvestigationSubGroupId);
                                //add by dhaval
                                dr1["investigationsubgroup_name"] = "";
                                //end dhaval
                                dr1["investigationpara_id"] = 0;
                                dr1["find_ip_reference"] = Convert.ToString(item.FindReference);
                                dr1["invoice_code"] = Convert.ToString(model.invoice_code);
                                dr1["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                dr1["investigation_uniqcode"] = Convert.ToString(item.investigation_uniqcode);
                                dr1["ManagePara"] = Convert.ToString(item.para_type);
                                dt.Rows.Add(dr1);
                            }
                            if (item.invParaList != null)
                            {
                                foreach (var para in item.invParaList)
                                {
                                    if (para.find_ip_reference != null)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["service_id"] = Convert.ToInt32(model.service_id);
                                        dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                                        dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                                        //add by dhaval
                                        dr["investigationsubgroup_name"] = "";
                                        //end dhaval
                                        dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                                        dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                                        dr["invoice_code"] = Convert.ToString(model.invoice_code);
                                        dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                        dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                                        dr["ManagePara"] = "ManageByParameter";
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                var usercode = Convert.ToString(Session["UserCode"]);
                bool add = TestMaster.AddHOSInvestigation_OP(dt, model.Doctorcode, usercode, model.EmployeeCode, model.reagent_id);
                if (add)
                {
                    status = "1";
                }
                else
                    status = "0";
            }
            else
            {
                if (model.reagent_id == 0)
                    status = "3";
                else
                    status = "2";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTableOP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("service_id", typeof(int));
            dt.Columns.Add("investigationmaster_id", typeof(int));
            dt.Columns.Add("investigationsubgrop_id", typeof(int));
            //Added by Dhaval
            dt.Columns.Add("investigationsubgroup_name", typeof(string));
            //end
            dt.Columns.Add("investigationpara_id", typeof(int));
            dt.Columns.Add("find_ip_reference", typeof(string));
            dt.Columns.Add("invoice_code", typeof(string));
            dt.Columns.Add("hoscode", typeof(string));
            dt.Columns.Add("investigation_uniqcode", typeof(string));
            dt.Columns.Add("ManagePara", typeof(string));
            return dt;
        }
        #endregion
        #region "Out Side Lab Investigation"
        public ActionResult OutSideLabInvestigation()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();

            DataSet ds = TestMaster.GetAlloutsidelabInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }


            return View("OutSideLabInvestigation", model);
        }

        public ActionResult FilterOutSideLabInvestigation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAlloutsidelabInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), status, INV.fromdate, INV.todate);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            var lst = Extend.ToList<SupplierList>(ds.Tables[3]);

            for (int i = 0; i < model.investigationPriceList.Count; i++)
            {
                model.investigationPriceList[i].sup_lst = lst;
            }
            return View("OutSideLabInvestigation", model);
        }

        public ActionResult OutSideLabInvestigation_OP()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            DataSet ds = TestMaster.GetAlloutsidelabInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), 0, null, null);

            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("OutSideLabInvestigation", model);
        }

        public ActionResult FilterOutSideLabInvestigation_OP(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAlloutsidelabInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), status, INV.fromdateop, INV.todateop);
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            var lst = Extend.ToList<SupplierList>(ds.Tables[2]);

            for (int i = 0; i < model.OP_InvestigationItems.Count; i++)
            {
                model.OP_InvestigationItems[i].sup_lst = lst;
            }

            return View("OutSideLabInvestigation", model);
        }

        [HttpPost]
        public ActionResult AddOutSideLabInvestigationPara(int service_id, int Investigationid, string Appointmentcode, string unique_invstigation_id, string InvType, string Id)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara(service_id, Investigationid, Appointmentcode, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, 0);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.appointment_code = Appointmentcode;
            model.service_id = service_id;
            model.invtype = InvType;
            model.Id = Id;
            //model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[3]);
            //model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[5]);
            //if (data.Tables[4].Rows.Count > 0)
            //    model.Doctorcode = Convert.ToString(data.Tables[4].Rows[0]["doc_code"]);
            //else
            //    model.Doctorcode = "";

            //if (data.Tables[5].Rows.Count > 0)
            //    model.EmployeeCode = Convert.ToString(data.Tables[5].Rows[0]["EmployeeCode"]);
            //else
            //    model.EmployeeCode = "";
            //Added By Dhaval
            //if (InvType == "radiology")
            //{
            //    model.invtype = InvType;
            //    //var radio_pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[1]);
            //    //var list = new SubGroup
            //    //{
            //    //    SubgroupName = "",
            //    //    invParaList = radio_pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
            //    //};
            //    //model.invSubgroupList.Add(list);
            var SubLst = new HosInvestigationPara();
            //    if (data.Tables[1].Rows.Count > 0)
            //    {
            //        SubLst = new HosInvestigationPara
            //        {
            //            investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
            //            investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
            //            investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
            //            investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
            //            investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
            //        };
            //    }

            SubLst = new HosInvestigationPara
            {
                investigationsubgroup_name = "",
                investigationmaster_id = Investigationid,
                investigationsubgrop_id = 0,
                investigationmaster_name = "",
                investigation_uniqcode = unique_invstigation_id
            };

            var list = new SubGroup();
            list.invParaList = new List<HosInvestigationPara>();
            list.invParaList.Add(SubLst);
            model.invSubgroupList.Add(list);
            return PartialView("_UploadInvestigationReportModel", model);
        }
        //End


        public ActionResult AddHOSInvestigationReport(TestAndInvestigationModel model)
        {
            string status = "";

            DataTable dt = CreateTable();
            //Added By Dhaval
            if (Request.Files.Count > 0)
            {
                var CL = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase files = Request.Files;

                HttpPostedFileBase imgfile = files[0];
                string myfile = Path.GetExtension(imgfile.FileName);
                long size = imgfile.ContentLength;
                if (myfile == ".pdf")
                {

                    if ((size / 1024) <= 150)
                    {

                        var folderpath = ConfigurationManager.AppSettings["addhosinvreport"];
                        string myfile1 = CL + Path.GetExtension(imgfile.FileName);
                        string fname = folderpath + "" + myfile1.Replace(" ", string.Empty);
                        //string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                        //if (System.IO.File.Exists(folderpath + model.clklogo.cliniclogo))
                        //{
                        //    System.IO.File.Delete(folderpath + model.clklogo.cliniclogo);
                        //}
                        imgfile.SaveAs(fname);


                        foreach (var item in model.invSubgroupList)
                        {
                            foreach (var para in item.invParaList)
                            {

                                DataRow dr = dt.NewRow();
                                dr["service_id"] = Convert.ToInt32(model.service_id);
                                dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                                dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                                //add by dhaval
                                dr["investigationsubgroup_name"] = "";
                                //end dhaval
                                dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                                dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                                dr["appointment_code"] = Convert.ToString(model.appointment_code);
                                dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                                dt.Rows.Add(dr);

                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            var usercode = Convert.ToString(Session["UserCode"]);
                            bool add = TestMaster.AddHOSInvestigationOutSideReport(dt, usercode, myfile1);
                            if (add)
                            {
                                status = "1";
                            }
                            else
                                status = "0";
                        }
                        else
                            status = "2";
                    }
                    else
                        status = "4";

                }
                else
                    status = "3";
            }
            return Json(new { msg = status, Id = model.appointment_code + "$" + model.Id }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddOutSideLabInvestigationPara_OP(int service_id, int investigation_id, string invoice_code, string unique_invstigation_id, string InvType, string Id)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara_OP(service_id, investigation_id, invoice_code, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType, 0);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.invoice_code = invoice_code;
            model.service_id = service_id;
            model.Id = Id;
            model.invtype = InvType;
            //model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(data.Tables[3]);
            //model.Technicianlist = Extend.ToList<Technician_Entity>(data.Tables[5]);
            //if (data.Tables[4].Rows.Count > 0)
            //{
            //    model.Doctorcode = Convert.ToString(data.Tables[4].Rows[0]["doc_code"]);
            //}
            //else
            //{
            //    model.Doctorcode = "";
            //}

            //if (data.Tables[5].Rows.Count > 0)
            //{
            //    model.EmployeeCode = Convert.ToString(data.Tables[5].Rows[0]["EmployeeCode"]);
            //}
            //else
            //{
            //    model.EmployeeCode = "";
            //}
            //Added By Dhaval
            //if (InvType == "radiology")
            //{
            //    model.invtype = InvType;
            //    //var radio_pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[1]);
            //    //var list = new SubGroup
            //    //{
            //    //    SubgroupName = "",
            //    //    invParaList = radio_pararesult.FindAll(a => a.investigationmaster_id == Investigationid)
            //    //};
            //    //model.invSubgroupList.Add(list);
            var SubLst = new HosInvestigationPara();
            //    if (data.Tables[1].Rows.Count > 0)
            //    {
            //        SubLst = new HosInvestigationPara

            //        {
            //            investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
            //            investigationmaster_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationmaster_id"]),
            //            investigationsubgrop_id = Convert.ToInt32(data.Tables[1].Rows[0]["investigationsubgrop_id"]),
            //            investigationmaster_name = Convert.ToString(data.Tables[1].Rows[0]["investigationmaster_name"]),
            //            investigation_uniqcode = Convert.ToString(data.Tables[1].Rows[0]["investigation_uniqcode"])
            //        };
            //    }
            //    else
            //    {
            SubLst = new HosInvestigationPara
            {
                investigationsubgroup_name = "",
                investigationmaster_id = investigation_id,
                investigationsubgrop_id = 0,
                investigationmaster_name = "",
                investigation_uniqcode = unique_invstigation_id
            };
            //}
            var list = new SubGroup();
            list.invParaList = new List<HosInvestigationPara>();
            list.invParaList.Add(SubLst);
            model.invSubgroupList.Add(list);
            return PartialView("_UploadInvestigationReportOPModel", model);

            //End
        }

        public ActionResult AddHOSInvestigationReportOP(TestAndInvestigationModel model)
        {
            string status = "";

            DataTable dt = CreateTableOP();
            //Added By Dhaval
            if (Request.Files.Count > 0)
            {
                var CL = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase files = Request.Files;

                HttpPostedFileBase imgfile = files[0];
                string myfile = Path.GetExtension(imgfile.FileName);
                //FileInfo fi = new FileInfo(imgfile.FileName);
                long size = imgfile.ContentLength;
                if (myfile == ".pdf")
                {

                    if ((size / 1024) <= 150)
                    {

                        var folderpath = ConfigurationManager.AppSettings["addhosinvreport"];
                        string myfile1 = CL + Path.GetExtension(imgfile.FileName);
                        string fname = folderpath + "" + myfile1.Replace(" ", string.Empty);
                        //string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                        //if (System.IO.File.Exists(folderpath + model.clklogo.cliniclogo))
                        //{
                        //    System.IO.File.Delete(folderpath + model.clklogo.cliniclogo);
                        //}
                        imgfile.SaveAs(fname);


                        foreach (var item in model.invSubgroupList)
                        {
                            foreach (var para in item.invParaList)
                            {



                                DataRow dr = dt.NewRow();
                                dr["service_id"] = Convert.ToInt32(model.service_id);
                                dr["investigationmaster_id"] = Convert.ToInt32(para.investigationmaster_id);
                                dr["investigationsubgrop_id"] = Convert.ToInt32(para.investigationsubgrop_id);
                                //add by dhaval
                                dr["investigationsubgroup_name"] = "";
                                //end dhaval
                                dr["investigationpara_id"] = Convert.ToInt32(para.investigationpara_id);
                                dr["find_ip_reference"] = Convert.ToString(para.find_ip_reference);
                                dr["invoice_code"] = Convert.ToString(model.invoice_code);
                                dr["hoscode"] = Convert.ToString(Session["ClinicCode"]);
                                dr["investigation_uniqcode"] = Convert.ToString(para.investigation_uniqcode);
                                dt.Rows.Add(dr);

                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            var usercode = Convert.ToString(Session["UserCode"]);
                            bool add = TestMaster.AddHOSInvestigationOutSideReport_OP(dt, usercode, myfile1);
                            if (add)
                            {
                                status = "1";
                            }
                            else
                                status = "0";
                        }
                        else
                            status = "2";
                    }
                    else
                        status = "4";
                }
                else
                    status = "3";
            }
            return Json(new { msg = status, Id = model.invoice_code + "$" + model.Id }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region "Bottle validity Type"

        public ActionResult HosBottleValidityType()
        {

            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();

            DataSet ds = TestMaster.GetAllInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), 0, null, null);
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();


            return View("HosBottleValidityType", model);

        }




        #endregion
        #region "Log For Bottle Used"

        public ActionResult HosLogForBottleUsed()
        {

            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();

            DataSet ds = TestMaster.GetAllInvestigationforBottleUsed(Convert.ToString(Session["ClinicCode"]));
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            model.DoctorContractlist = new List<DoctorContractlist_Entity>();
            model.Technicianlist = new List<Technician_Entity>();

            return View("HosLogForBottleUsed", model);

        }
        #endregion
        #region "Approve Investigation"
        public ActionResult ApproveTestAndInvestigation()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            DataSet ds = TestMaster.GetAllApproveInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), 1, null, null);
            model.OP_InvestigationCollection = new List<OP_investigation_Entity>();
            model.OP_InvestigationItems = new List<OP_investigationItem>();
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            return View("ApproveTestAndInvestigation", model);
        }
        public ActionResult FilterApproveTestandInvestigation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllApproveInvestigationAppointment(Convert.ToString(Session["ClinicCode"]), status, INV.fromdate, INV.todate);
            model.OP_InvestigationCollection = new List<OP_investigation_Entity>();
            model.OP_InvestigationItems = new List<OP_investigationItem>();
            model.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            return View("ApproveTestAndInvestigation", model);
        }
        public ActionResult ApproveTestAndInvestigation_OP()
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            DataSet ds = TestMaster.GetAllApproveInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), 1, null, null);
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            return View("ApproveTestAndInvestigation", model);
        }
        public ActionResult FilteropdApproveTestandInvestigation(TestAndInvestigationModel INV)
        {
            TestAndInvestigationModel model = new TestAndInvestigationModel();
            model.invSubgroupList = new List<SubGroup>();
            int status = 0;
            if (INV.status == "pending")
                status = 0;
            else
                status = 1;
            DataSet ds = TestMaster.GetAllApproveInvestigationAppointment_OP(Convert.ToString(Session["ClinicCode"]), status, INV.fromdateop, INV.todateop);
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            model.investigationPriceList = new List<investigationparaamount>();
            model.OP_InvestigationCollection = Extend.ToList<OP_investigation_Entity>(ds.Tables[0]);
            model.OP_InvestigationItems = Extend.ToList<OP_investigationItem>(ds.Tables[1]);
            return View("ApproveTestAndInvestigation", model);
        }
        public ActionResult SendInvestingation(TestAndInvestigationModel model)
        {
            string status = "0"; string Bodyhtml = ""; string HeaderHtml = ""; string FooterHtml = "";
            byte[] FileBytes = null;
            DataSet ds = new DataSet();
            var lst = model.RegisterAppointmentlst.Where(a => a.IsSelected == true).ToList();
            String html = string.Empty;
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    bool isprocess = false;
                    if (item.AllowTestReportEmailWithoutPayment == 0)
                    {
                        if (item.IsFullPaid == 1)
                            isprocess = true;
                        else
                            isprocess = false;
                    }
                    else
                    {
                        isprocess = true;
                    }
                    if (isprocess)
                    {
                        DataTable dt = CreateTableForSendEmail();
                        ds = TestMaster.GetInvestigationID(Convert.ToString(item.appointment_code));
                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                var inv = TestMaster.GetInvestigation(Convert.ToString(item.appointment_code), Convert.ToInt32(ds.Tables[0].Rows[i]["investigationmaster_id"]), Convert.ToInt32(ds.Tables[0].Rows[i]["service_id"]), "get_inv_html", "WithHeader", Convert.ToString(ds.Tables[0].Rows[i]["investigation_uniqcode"]), Convert.ToInt16(ds.Tables[0].Rows[i]["reagent_id"]), Convert.ToString(ds.Tables[0].Rows[i]["grouptype"]));
                                Bodyhtml = Convert.ToString(inv.BodyHtml);
                                HeaderHtml = Convert.ToString(inv.HeaderHtml);
                                FooterHtml = Convert.ToString(inv.FooterHtml);
                                //Generate Pdf
                                HtmlToPdfConverter htmltopdf = new HtmlToPdfConverter();
                                htmltopdf.Orientation = PageOrientation.Default;
                                //htmltopdf.Size = PageSize.Default;
                                htmltopdf.PageHeaderHtml = HeaderHtml;
                                if (FooterHtml != "" && FooterHtml != null)
                                {
                                    htmltopdf.PageFooterHtml = FooterHtml;
                                    htmltopdf.Margins = new PageMargins { Top = 40 };
                                }
                                else
                                {
                                    htmltopdf.Margins = new PageMargins { Top = 50, Bottom = 25 };
                                }
                                FileBytes = htmltopdf.GeneratePdf(Bodyhtml);
                                DataRow dr = dt.NewRow();
                                dr["Filebyte"] = FileBytes;
                                dt.Rows.Add(dr);
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    byte[] bytes = FileBytes.ToArray();
                                    memoryStream.Close();
                                }
                                html += Bodyhtml;
                            }
                            Email_Entity_withattachment em = new Email_Entity_withattachment();
                            em.Subject = "Investigation Report";
                            em.Body = "Please Check below attchment file";
                            em.ToEmail = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                            em.dt = dt;
                            em.appointment_code = Convert.ToString(item.appointment_code);
                            em.bodyhtml = html;
                            string msg = new Email_Master(bsInfo).SendEmailwithAttchment(em);
                            html = string.Empty;
                        }
                    }
                }
                status = "1";
            }
            else
                status = "3";

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendInvestingation_OP(TestAndInvestigationModel model)
        {
            string status = "0"; string Bodyhtml = ""; string HeaderHtml = ""; string FooterHtml = "";
            byte[] FileBytes = null;
            DataTable dt = CreateTableForSendEmail();
            DataSet ds = new DataSet();
            var lst = model.OP_InvestigationCollection.Where(a => a.IsSelected == true).ToList();
            String html = string.Empty;
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    bool isprocess = false;
                    if (item.AllowTestReportEmailWithoutPayment == 0)
                    {
                        if (item.IsFullPaid == 1)
                            isprocess = true;
                        else
                            isprocess = false;
                    }
                    else
                    {
                        isprocess = true;
                    }
                    if (isprocess)
                    {
                        ds = TestMaster.GetInvestigationID(Convert.ToString(item.invoice_code));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                var inv = TestMaster.GetInvestigation(Convert.ToString(item.invoice_code), Convert.ToInt32(ds.Tables[0].Rows[i]["investigationmaster_id"]), Convert.ToInt32(ds.Tables[0].Rows[i]["service_id"]), "get_inv_html", "WithHeader", Convert.ToString(ds.Tables[0].Rows[i]["investigation_uniqcode"]), Convert.ToInt16(ds.Tables[0].Rows[i]["reagent_id"]), Convert.ToString(ds.Tables[0].Rows[i]["grouptype"]));
                                Bodyhtml = Convert.ToString(inv.BodyHtml);
                                HeaderHtml = Convert.ToString(inv.HeaderHtml);
                                FooterHtml = Convert.ToString(inv.FooterHtml);
                                //Generate Pdf
                                HtmlToPdfConverter htmltopdf = new HtmlToPdfConverter();
                                htmltopdf.Orientation = PageOrientation.Default;
                                //htmltopdf.Size = PageSize.Default;
                                htmltopdf.PageHeaderHtml = HeaderHtml;
                                if (FooterHtml != "" && FooterHtml != null)
                                {
                                    htmltopdf.PageFooterHtml = FooterHtml;
                                    htmltopdf.Margins = new PageMargins { Top = 40 };
                                }
                                else
                                {
                                    htmltopdf.Margins = new PageMargins { Top = 50, Bottom = 25 };
                                }
                                FileBytes = htmltopdf.GeneratePdf(Bodyhtml);
                                DataRow dr = dt.NewRow();
                                dr["Filebyte"] = FileBytes;
                                dt.Rows.Add(dr);
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    byte[] bytes = FileBytes.ToArray();
                                    memoryStream.Close();
                                }
                                html += Bodyhtml;
                            }
                            Email_Entity_withattachment em = new Email_Entity_withattachment();
                            em.Subject = "Investigation Report";
                            em.Body = "Please Check below attchment file";
                            em.ToEmail = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                            em.dt = dt;
                            em.appointment_code = Convert.ToString(item.invoice_code);
                            em.bodyhtml = html;
                            string msg = new Email_Master(bsInfo).SendEmailwithAttchment(em);
                            status = msg;
                        }
                        else
                            status = "0";
                    }
                }
            }
            else
                status = "3";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTableForSendEmail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Filebyte", typeof(byte[]));
            return dt;
        }
        public class InvReport
        {
            public StringBuilder BodyHtml { get; set; }
            public string HeaderHtml { get; set; }
            public string FooterHtml { get; set; }
        }
        public class InvsMultipleFile
        {
            public byte bytedata { get; set; }
        }
        #endregion
        public string getinv_request(string type, string AptCode, int InvestigationId, int ServiceId, string PrintType, int ReagentId, string uniq_id, string InvType)
        {
            string jsonreq = "{\"APIName\":\"getinvestigationreport\",\"Payload\":{\"AppointCode\":\"" + AptCode + "\",\"InvestigationId\":\"" + InvestigationId + "\",\"ServiceId\":\"" + ServiceId + "\",\"type\":\"" + type + "\",\"PrintType\":\"" + PrintType + "\",\"ReagentId\":\"" + ReagentId + "\",\"uniq_id\":\"" + uniq_id + "\",\"InvType\":\"" + InvType + "\"},\"TokenData\":{\"Token\":\"" + Convert.ToString(ConfigurationManager.AppSettings["ApiToken"]) + "\"}}";
            return jsonreq;
        }
        public JsonResult Print_Investigation(string ApptCode, int Investigationid, int service_id, string PrintType, int ReagentId, string uniq_id, string InvType)
        {
            string url = "/TestAndInvestigation/DownloadDocument?" + new QueryStringModule().Encrypt(ApptCode + "-" + Investigationid);
            string json = getinv_request("html", ApptCode, Investigationid, service_id, PrintType, ReagentId, uniq_id, InvType);
            string response = CallApi(json);
            //return response;
            //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "-" + Investigationid + ".html", JsonConvert.DeserializeObject<PdfClass>(response).m_StringValue.ToString());
            PdfClass PdfCls = JsonConvert.DeserializeObject<PdfClass>(response);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "-" + Investigationid + "Body.html", PdfCls.BodyHtml.m_StringValue.ToString());
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "-" + Investigationid + "_Header.html", PdfCls.HeaderHtml.ToString());
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "-" + Investigationid + "_Footer.html", PdfCls.FooterHtml.ToString());
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public string CallApi(string json)
        {
            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Convert.ToString(ConfigurationManager.AppSettings["ApiUrl"]));
                request.Accept = "application/json"; //"application/xml";
                request.Method = "POST";
                var data = Encoding.ASCII.GetBytes(json);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        ResponseString = (new StreamReader(stream)).ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Some error occured: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Some error occured: " + ex.Status.ToString();
                }
            }
            return ResponseString;
        }
        public ActionResult DownloadDocument(string enc)
        {
            string Bodyhtml = "";
            string HeaderHtml = "";
            string FooterHtml = "";
            string ApptCode = "";
            var encoding = new System.Text.UTF8Encoding();
            if (enc != null)
            {
                ApptCode = new QueryStringModule().Decrypt(enc);
                //string ReportURL = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + ".html";
                Bodyhtml = System.IO.File.ReadAllText(Server.MapPath("/ManagePdf/") + ApptCode + "Body.html", encoding);
                HeaderHtml = System.IO.File.ReadAllText(Server.MapPath("/ManagePdf/") + ApptCode + "_Header.html", encoding);
                FooterHtml = System.IO.File.ReadAllText(Server.MapPath("/ManagePdf/") + ApptCode + "_Footer.html", encoding);
            }
            else
            {
                if (Session["Bodyhtml"] != null && Session["FooterHtml"] != null)
                {
                    Bodyhtml = Convert.ToString(Session["Bodyhtml"]);
                    FooterHtml = Convert.ToString(Session["FooterHtml"]);
                }
            }
            //Generate Pdf
            HtmlToPdfConverter htmltopdf = new HtmlToPdfConverter();
            htmltopdf.Orientation = PageOrientation.Default;
            //htmltopdf.Size = PageSize.Default;
            htmltopdf.PageHeaderHtml = HeaderHtml;
            if (FooterHtml != "" && FooterHtml != null)
            {
                htmltopdf.PageFooterHtml = FooterHtml;
                htmltopdf.Margins = new PageMargins { Top = 30 };
            }
            else
            {
                htmltopdf.Margins = new PageMargins { Top = 50, Bottom = 25 };
            }
            var FileBytes = htmltopdf.GeneratePdf(Bodyhtml);
            //Generate Pdf
            //Download Pdf
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = (ApptCode == "" || ApptCode == null) ? "invoice" : ApptCode,
                Inline = true,
            };
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", cd.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(FileBytes);
            Response.End();
            //End Download pdf
            //Remove Folder
            if (enc != null)
            {
                string bodyroot = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "Body.html";
                string headerroot = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "_Header.html";
                string footerroot = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "_Footer.html";
                if (Directory.Exists(bodyroot))
                {
                    System.IO.File.Delete(bodyroot);
                }
                if (Directory.Exists(headerroot))
                {
                    System.IO.File.Delete(headerroot);
                }
                if (Directory.Exists(footerroot))
                {
                    System.IO.File.Delete(footerroot);
                }
            }
            return View();
        }
        public class PdfClass
        {
            public string status { get; set; }
            public BodyPart BodyHtml { get; set; }
            public string HeaderHtml { get; set; }
            public string FooterHtml { get; set; }
        }
        public class BodyPart
        {
            public string m_StringValue { get; set; }
        }
    }
}