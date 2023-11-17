using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Helper;
using HospitalManagement.Models;
using Newtonsoft.Json;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class InPatientController : _Base
    {
        // GET: InPatient
        readonly InPatient_Master InPat;
        readonly Nurse_Master NusMaster;
        readonly Service_Master Ser;
        readonly med_entity med;
        public InPatientController()
        {
            NusMaster = new Nurse_Master(bsInfo);
            InPat = new InPatient_Master(bsInfo);
            Ser = new Service_Master(bsInfo);
            med = new med_entity();
        }
        public ActionResult InPatientList()
        {
            InPatient model = new InPatient();
            model.IPservices = new AddIPservices();
            model.Investigation = new AddInvestigation();
            DataSet ds = InPat.GetInPatientData(Convert.ToString(Session["ClinicCode"]));
            model.InPatientlist = Extend.ToList<InPatientData>(ds.Tables[0]);
            model.doc_list = new List<Doctor_list>();
            return View("InPatientList", model);
        }
        [HttpPost]
        public PartialViewResult FilterInpatient(InPatient INV)
        {
            InPatient model = new InPatient();
            model.IPservices = new AddIPservices();
            model.Investigation = new AddInvestigation();
            int ctrl = 2;
            if (INV.status == "Is Release") { ctrl = 1; } else if (INV.status == "Admited") { ctrl = 0; }
            var dataset = InPat.GetAllfilter(Convert.ToString(Session["ClinicCode"]), ctrl, INV.FromDate, INV.Todate);
            model.InPatientlist = Extend.ToList<InPatientData>(dataset.Tables[0]);
            model.doc_list = new List<Doctor_list>();
            return PartialView("InPatientList", model);
        }
        public PartialViewResult GetInvandMed(string uniqrow)
        {
            InPatient model = new InPatient();
            DataSet ds = InPat.GetInPatienInvandMed(Convert.ToString(Session["ClinicCode"]), uniqrow);
            model.InPatientlist = new List<InPatientData>();
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[0]);
            model.InPatientMedecineList = Extend.ToList<SubMedicine_List_entity>(ds.Tables[1]);
            model.doctorsuggestion = Extend.ToList<doctor_suggestion>(ds.Tables[2]);
            model.doc_list = Extend.ToList<Doctor_list>(ds.Tables[3]);
            model.SuggestDoctor_list = Extend.ToList<SuggestDoctor_list>(ds.Tables[4]);
            model.InpatientVitalList = Extend.ToList<vital_listdata>(ds.Tables[5]);
            model.vital_para = Extend.ToList<Vital_Parameter>(ds.Tables[6]);
            model.uniqrowid = uniqrow;
            return PartialView("_InPatientStatus", model);
        }
        [HttpPost]
        public ActionResult AddInvestigationAmount(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId)
        {
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = NusMaster.SetInpatientInvPrice(service_id, Investigationid, Amount, Appointmentcode, PriceId, hoscode, usercode);
            return Json(result == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CancelInvestigation(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId)
        {
            InPatient model = new InPatient();
            model.parameter = new investigationparaamount();
            model.parameter.service_id = Convert.ToInt32(service_id);
            model.parameter.investigationmaster_id = Convert.ToInt32(Investigationid);
            model.parameter.price = Convert.ToDecimal(Amount);
            model.parameter.appoinment_code = Convert.ToString(Appointmentcode);
            model.parameter.invpriceid = Convert.ToInt32(PriceId);
            return PartialView("_InvestigationCancel_Reason", model);

        }
        [HttpPost]
        public ActionResult SureCancelInvestigation(InPatient I)
        {
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = NusMaster.CancelInvestigation(I.parameter.service_id, I.parameter.investigationmaster_id, I.parameter.price, I.parameter.appoinment_code, I.parameter.invpriceid, hoscode, usercode, I.parameter.cancel_reason);
            return Json(result.ToString(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult AddInPatientVital(InPatient INPV)
        {
            InPatient model = new InPatient();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            string[] words = INPV.InPatientvitaldata.vital_para.Split('$');
            DataSet ds = InPat.AddInPatientvital(words[0], INPV.InPatientvitaldata.vital_value +" "+ words[1], INPV.InPatientvitaldata.vital_time, INPV.InPatientvitaldata.hiddencode, hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.uniqrowid = INPV.InPatientvitaldata.hiddencode;
            model.InpatientVitalList = Extend.ToList<vital_listdata>(ds.Tables[1]);
            return PartialView("_InPatientStatus", model);
        }
        public PartialViewResult AddInPatientdoctorsug(InPatient INP)
        {
            InPatient model = new InPatient();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = InPat.Adddoctorsuggestion(INP.InPatientDoctorData.doctorcode, INP.InPatientDoctorData.uniqrow, hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.uniqrowid = INP.InPatientDoctorData.uniqrow;
            model.doc_list = Extend.ToList<Doctor_list>(ds.Tables[1]);
            model.SuggestDoctor_list = Extend.ToList<SuggestDoctor_list>(ds.Tables[2]);
            return PartialView("_InPatientStatus", model);
        }
        [HttpPost]
        public ActionResult InserMedStatus(int med_id, int status, string timing)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var result = InPat.AddPateintMedStatus(med_id, status, timing);
            return Json(result == true ? 1 : 0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Print_Discharge_Summary(string ApptCode, string Type)
        {
            string url = "/InPatient/DownloadDocument?" + new QueryStringModule().Encrypt(ApptCode + "+" + "Discharge");
            string jsondt = getdischarg_request(ApptCode, Type);
            string response = CallApi(jsondt);
            Discharg_Pdf Dis = JsonConvert.DeserializeObject<Discharg_Pdf>(response);
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Body.html", Dis.BodyHtml.m_StringValue.ToString().Replace("#DISCHARGESUMMARY", "Discharge Summary"));
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Header.html", Dis.HeaderHtml.ToString());
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Footer.html", Dis.FooterHtml.ToString());
            return Json(url, JsonRequestBehavior.AllowGet);

        }
        public string getdischarg_request(string AptCode, string Type)
        {
            string jsonreq = "{\"APIName\":\"get_discharge_summary\",\"Payload\":{\"AppointCode\":\"" + AptCode + "\",\"Type\":\"" + Type + "\"},\"TokenData\":{\"Token\":\"" + Convert.ToString(ConfigurationManager.AppSettings["ApiToken"]) + "\"}}";
            return jsonreq;
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
            string Code = new QueryStringModule().Decrypt(enc);
            string ApptCode = Code.Split('+')[0];
            string Bodyhtml;
            string HeaderHtml = "";
            string FooterHtml = "";
            if (Code.Split('+')[1] == "admission")
            {
                Bodyhtml = Server.MapPath("/ManagePdf/") + ApptCode + "/Body.html";
            }
            else
            {
                Bodyhtml = Server.MapPath("/DischargePdf/") + ApptCode + "/Body.html";
                HeaderHtml = Server.MapPath("/DischargePdf/") + ApptCode + "/Header.html";
                FooterHtml = Server.MapPath("/DischargePdf/") + ApptCode + "/Footer.html";
            }
            //Generate Pdf
            //HtmlToPdfConverter htmltopdf = new HtmlToPdfConverter();
            //htmltopdf.Orientation = PageOrientation.Default;
            ////htmltopdf.Size = PageSize.Default;
            //htmltopdf.PageHeaderHtml = HeaderHtml;
            //if (FooterHtml != "" && FooterHtml != null)
            //{
            //    htmltopdf.PageFooterHtml = FooterHtml;
            //    htmltopdf.Margins = new PageMargins { Top = 60 };

            //}
            //else
            //{
            //    if (Code.Split('+')[1] != "admission")
            //        htmltopdf.Margins = new PageMargins { Top = 70, Bottom = 25 };
            //}
            //var FileBytes = htmltopdf.GeneratePdf(Bodyhtml);

            HtmlToPdf converter = new HtmlToPdf();
            int webPageWidth = 800;
            if (FooterHtml != null && FooterHtml != "")
            {
                converter.Options.DisplayHeader = true;
                converter.Header.DisplayOnFirstPage = true;
                converter.Header.DisplayOnOddPages = true;
                converter.Header.DisplayOnEvenPages = true;
                if (System.IO.File.ReadAllText(FooterHtml) != "" && System.IO.File.ReadAllText(FooterHtml) != null)
                    converter.Header.Height = 175;
                else
                    converter.Header.Height = 91;
                PdfHtmlSection headerHtml = new PdfHtmlSection(HeaderHtml);
                headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Header.Add(headerHtml);
            }

            //End Header 
            if (FooterHtml != null && FooterHtml != "")
            {
                if (System.IO.File.ReadAllText(FooterHtml) != "" && System.IO.File.ReadAllText(FooterHtml) != null)
                {
                    //Footer
                    converter.Options.DisplayFooter = true;
                    converter.Footer.DisplayOnFirstPage = true;
                    converter.Footer.DisplayOnOddPages = true;
                    converter.Footer.DisplayOnEvenPages = true;
                    converter.Footer.Height = 80;
                    PdfHtmlSection Footer = new PdfHtmlSection(FooterHtml);
                    Footer.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                    converter.Footer.Add(Footer);
                    //End Footer
                }
                else
                {
                    converter.Options.MarginTop = 105;
                    converter.Options.MarginBottom = 85;
                }
            }
            //else
            //{
            //    converter.Options.MarginTop = 70;
            //    converter.Options.MarginBottom = 25;
            //}
            converter.Options.WebPageWidth = webPageWidth;
            PdfDocument doc = converter.ConvertUrl(Bodyhtml);
            byte[] FileBytes = doc.Save();
            doc.Close();
            //Generate Pdf
            //Download Pdf
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "inline; filename=" + ApptCode + ".pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(FileBytes);
            Response.End();
            //End Download pdf
            //Remove Folder
            string root = "";
            if (Code.Split('+')[1] == "admission")
            {
                root = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode;
                if (Directory.Exists(root))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "\\Body.html");
                    Directory.Delete(root);
                }
            }
            else
            {
                root = AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode;
                if (Directory.Exists(root))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Body.html");
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Header.html");
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "DischargePdf\\" + ApptCode + "\\Footer.html");
                    Directory.Delete(root);
                }
            }
            return View();
        }
        public JsonResult Print_Patient_Admission(string ApptCode)
        {
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string url = "/InPatient/DownloadDocument?" + new QueryStringModule().Encrypt(ApptCode + "+" + "admission");
            var inv = InPat.Patient_Admission(ApptCode, hoscode);
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "\\Body.html", Convert.ToString(inv.Tables[0].Rows[0]["Body"]));
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult AddNewInvestigation(string UniqRow)
        {
            var inv = InPat.GetAllinvestigationList(Convert.ToString(Session["ClinicCode"]));
            InPatient model = new InPatient();
            model.Investigation = new AddInvestigation();
            model.Investigation.InvList = Extend.ToList<InvListData>(inv.Tables[0]);
            model.Investigation.uniqrowid = UniqRow;
            model.Investigation.checkexisting = 0;
            return PartialView("_InvestigationPopUp", model);
        }
        public PartialViewResult Add_IP_Investigation(InPatient Data)
        {
            string Uniqrow = Data.Investigation.uniqrowid;
            var usercode = Convert.ToString(Session["UserCode"]);
            int invid = Data.Investigation.investigationmaster_id;
            string instuction = Data.Investigation.Instruction;
            string uniqid = Data.Investigation.uniqrowid;
            var ds = InPat.AddNewInvestigation(Data.Investigation.investigationmaster_id, Data.Investigation.Instruction, Data.Investigation.uniqrowid, Convert.ToString(Session["ClinicCode"]), usercode, Data.Investigation.checkexisting);
            Data = new InPatient();
            Data.InPatientlist = new List<InPatientData>();
            Data.status = Convert.ToString(ds.Tables[1].Rows[0]["status"]);
            Data.uniqrowid = Uniqrow;
            if (Data.status == "1")
            {
                Data.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[0]);
                return PartialView("_InPatientStatus", Data);
            }
            else
            {
                Data.Investigation = new AddInvestigation();
                Data.Investigation.existinvestigationList = Extend.ToList<investigationparaamount>(ds.Tables[0]);
                Data.Investigation.investigationmaster_id = invid;
                Data.Investigation.Instruction = instuction;
                Data.Investigation.uniqrowid = uniqid;
                Data.Investigation.checkexisting = 1;
                return PartialView("_ExistingInvestigationPopUp", Data);
            }
        }
        [HttpPost]
        public PartialViewResult Patient_Transfer_OT(string uniqrow)
        {
            var Doc = InPat.GetAllInPatientDoctor(Convert.ToString(Session["ClinicCode"]));
            InPatient model = new InPatient();
            model.OTTransfer = new AddOTTransfer();
            model.OTTransfer.operation_date = "";
            model.OTTransfer.operation_time = "";
            model.OTTransfer.operation_type = "";
            model.OTTransfer.IPDoctor_lst = Extend.ToList<Doctor_list>(Doc.Tables[0]);
            model.OTTransfer.uniqrowid = uniqrow;
            return PartialView("_InPatientOTPopUp", model);
        }
        public JsonResult Add_IP_OTTransfer(InPatient DataI)
        {
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = InPat.AddPatientOTTransfer(DataI.OTTransfer.doctor_code, DataI.OTTransfer.operation_date, DataI.OTTransfer.operation_time,DataI.OTTransfer.operation_type, DataI.OTTransfer.uniqrowid, Convert.ToString(Session["ClinicCode"]), usercode);
            return Json(result == true ? DataI.OTTransfer.uniqrowid : "0", JsonRequestBehavior.AllowGet);
        }
        public ActionResult InPatientOTList()
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst = new List<SelectListItem>();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            var data = InPat.GetOttransfer(Convert.ToString(Session["ClinicCode"]));
            model.OT_transfer_List = Extend.ToList<Inpatient_OT_List>(data.Tables[0]);
            return View("InPatientOTList", model);
        }
        [HttpPost]
        public PartialViewResult AddIPServices(string UniqRow)
        {
            var data = InPat.GetAllIPServices(Convert.ToString(Session["ClinicCode"]), UniqRow);
            InPatient model = new InPatient();
            model.IPservices = new AddIPservices();
            model.IPservices.IPBillingServicelst = Extend.ToList<IPBillingService_Item>(data.Tables[0]);
            model.IPservices.IPBillingServicelstdata = Extend.ToList<IPBillingService_Item_list>(data.Tables[1]);
            model.IPservices.uniqrowid = UniqRow;
            return PartialView("_ServicesPopUp", model);
        }
        public PartialViewResult GetServiceCost(string ServiceId, string Type)
        {
            InPatient model = new InPatient();
            model.IPservices = new AddIPservices();
            model.IPservices.IPBillingServicelst = new List<IPBillingService_Item>();
            var ds = Ser.GetServicecost(ServiceId, Type, Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.IPservices.SellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["selling_price"]);
                model.IPservices.MinSellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["minimum_selling_price"]);
                model.IPservices.is_realtime = Convert.ToInt32(ds.Tables[0].Rows[0]["is_realtime"]);
                model.IPservices.ServiceType = Convert.ToString(ds.Tables[0].Rows[0]["type"]);
                model.IPservices.Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Cost"]);
            }
            return PartialView("_ServicesPopUp", model);
        }
        public PartialViewResult Add_IP_Services(InPatient DataS)
        {
            InPatient model = new InPatient();
            model.IPservices = new AddIPservices();
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = InPat.AddPatientIPServices(DataS.IPservices.Service_id, DataS.IPservices.ServiceName, DataS.IPservices.Cost, DataS.IPservices.SellingCost, DataS.IPservices.MinSellingCost, DataS.IPservices.Discount, "OtherService", DataS.IPservices.is_realtime, DataS.IPservices.Remark, DataS.IPservices.uniqrowid, Convert.ToString(Session["ClinicCode"]), usercode);
            model.IPservices.IPBillingServicelstdata = Extend.ToList<IPBillingService_Item_list>(result.Tables[1]);
            model.status = Convert.ToString(result.Tables[0].Rows[0]["status"]);
            return PartialView("_ServicesPopUp", model);
        }
        public PartialViewResult GetOTtrasferDetails(int TransferId)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            DataSet ds = InPat.GetOTtrasferDetails(Convert.ToString(Session["ClinicCode"]), TransferId);
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OT_transfer_List = new List<Inpatient_OT_List>();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPreOperative = new OT_PreOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            model.OTdetail = new OT_Detail();
            model.RemarkList = new List<MedicineRemark_List>();
            model.OTPostOperative.med_list = Extend.ToList<med_entity>(ds.Tables[0]);
            model.TransferId = TransferId;
            if (ds.Tables[1].Rows.Count > 0)
            {
                model.OTPreOperative.appointment_code = Convert.ToString(ds.Tables[1].Rows[0]["appointment_code"]);
                model.OTPreOperative.pre_operative_date = Convert.ToString(ds.Tables[1].Rows[0]["pre_operative_date"]);
                model.OTPreOperative.pre_operative_time = Convert.ToString(ds.Tables[1].Rows[0]["pre_operative_time"]);
                model.OTPreOperative.pre_operative_advice = Convert.ToString(ds.Tables[1].Rows[0]["pre_operative_advice"]);
                model.OTPreOperative.pre_operative_gen_advice = Convert.ToString(ds.Tables[1].Rows[0]["pre_operative_gen_advice"]);
            }
            model.OTdetail.surgeon_lst = GetDoctorList(ds.Tables[2]);
            model.OTdetail.asst_surgeon_lst = GetAssDoctorList(ds.Tables[3]);
            if (ds.Tables[4].Rows.Count > 0)
            {
                model.OTdetail.Name_of_Operation = Convert.ToString(ds.Tables[4].Rows[0]["name_of_operation"]);
                model.OTdetail.Indication = Convert.ToString(ds.Tables[4].Rows[0]["indication"]);

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[4].Rows[0]["surgeon"])))
                {
                    model.OTdetail.Surgeon = Convert.ToString(ds.Tables[4].Rows[0]["surgeon"]).Split(',');
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[4].Rows[0]["asstt_surgeon"])))
                {
                    model.OTdetail.Asst_Surgeon = Convert.ToString(ds.Tables[4].Rows[0]["asstt_surgeon"]).Split(',');
                }

                //model.OTdetail.Surgeon = Convert.ToString(ds.Tables[4].Rows[0]["surgeon"]);
                //model.OTdetail.Asst_Surgeon = Convert.ToString(ds.Tables[4].Rows[0]["asstt_surgeon"]);
                model.OTdetail.Findings = Convert.ToString(ds.Tables[4].Rows[0]["findings"]);
                model.OTdetail.Procedure = Convert.ToString(ds.Tables[4].Rows[0]["ot_procedure"]);
                model.OTdetail.Histology = Convert.ToString(ds.Tables[4].Rows[0]["histology"]);
                model.OTdetail.Anaesthetist = Convert.ToString(ds.Tables[4].Rows[0]["anaesthetist"]);
                model.OTdetail.Anaesthetic_Used = Convert.ToString(ds.Tables[4].Rows[0]["anaesthetic_used"]);
                model.OTdetail.Unista_Blood_Transfusion = Convert.ToString(ds.Tables[4].Rows[0]["unista_blood_transfusion"]);
                model.OTdetail.From_Time = Convert.ToString(ds.Tables[4].Rows[0]["ot_from_time"]);
                model.OTdetail.To_Time = Convert.ToString(ds.Tables[4].Rows[0]["ot_to_time"]);
                model.OTdetail.OT_Date = Convert.ToString(ds.Tables[4].Rows[0]["ot_date"]);
                model.OTdetail.OT_Nurse = Convert.ToString(ds.Tables[4].Rows[0]["ot_nurse"]);
                model.OTdetail.appointment_code = Convert.ToString(ds.Tables[4].Rows[0]["appointment_code"]);
                model.OTdetail.Asst_Surgeon_name = Convert.ToString(ds.Tables[4].Rows[0]["asstt_surgeon_name"]);
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                model.OTPostOperative.post_operative_date = Convert.ToString(ds.Tables[5].Rows[0]["post_operative_date"]);
                model.OTPostOperative.post_operative_time = Convert.ToString(ds.Tables[5].Rows[0]["post_operative_time"]);
                model.OTPostOperative.post_operative_advice = Convert.ToString(ds.Tables[5].Rows[0]["post_operative_advice"]);
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                model.OTservice.ot_service_list = Extend.ToList<OTService_Entity>(ds.Tables[6]);
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                model.OTservice.Ot_Service_entity = Extend.ToList<ServiceItem_Entity>(ds.Tables[7]);
            }
            model.RemarkList = Extend.ToList<MedicineRemark_List>(ds.Tables[8]);

            return PartialView("_InPatientOTStatus", model);
        }

        private static List<SelectListItem> GetDoctorList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(dt.Rows[i]["doctor_name"]),
                    Value = Convert.ToString(dt.Rows[i]["doctor_code"])
                });
            }
            return items;
        }

        private static List<SelectListItem> GetAssDoctorList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(dt.Rows[i]["doctor_name"]),
                    Value = Convert.ToString(dt.Rows[i]["doctor_code"])
                });
            }
            return items;
        }

        public PartialViewResult AddPreOperative(IP_OT_Transfer PRE)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst = new List<SelectListItem>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTservice.Ot_Service_entity = new List<ServiceItem_Entity>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = InPat.AddPreoperativedata(PRE.OTPreOperative.pre_operative_date, PRE.OTPreOperative.pre_operative_time, PRE.OTPreOperative.pre_operative_advice, PRE.OTPreOperative.pre_operative_gen_advice, PRE.OTPreOperative.TransferId, hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.TransferId = PRE.OTPreOperative.TransferId;
            return PartialView("_InPatientOTStatus", model);
        }
   
        public PartialViewResult GetOTServiceCost(string ServiceId, string Type, string TransferId)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            model.OTPreOperative = new OT_PreOperative();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst = new List<SelectListItem>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTservice.Ot_Service_entity = new List<ServiceItem_Entity>();
            DataSet ds = InPat.GetServicecost(ServiceId, Type, Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.OTservice.SellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["selling_price"]);
                model.OTservice.MinSellingCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["minimum_selling_price"]);
                model.OTservice.is_realtime = Convert.ToInt32(ds.Tables[0].Rows[0]["is_realtime"]);
                model.OTservice.ServiceType = Convert.ToString(ds.Tables[0].Rows[0]["type"]);
                model.OTservice.Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Cost"]);
                model.OTservice.ServiceName = Convert.ToString(ds.Tables[0].Rows[0]["service_name"]);
            }
            model.TransferId = Convert.ToInt32(TransferId);
            return PartialView("_InPatientOTStatus", model);
        }

        public PartialViewResult AddPostOperative(IP_OT_Transfer Post)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst =new List<SelectListItem>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTservice.Ot_Service_entity = new List<ServiceItem_Entity>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = InPat.Addpostoperativedata(Post.OTPostOperative.post_operative_date, Post.OTPostOperative.post_operative_time, Post.OTPostOperative.post_operative_advice,  Post.OTPostOperative.TransferId, hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.TransferId = Post.OTPostOperative.TransferId;
            return PartialView("_InPatientOTStatus", model);
        }

        public PartialViewResult Add_Post_med(IP_OT_Transfer Post_med)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTservice.Ot_Service_entity = new List<ServiceItem_Entity>();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst = new List<SelectListItem>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            model.OTPreOperative = new OT_PreOperative();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();           
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = InPat.AddPostmedicine(Post_med.OTPostOperative.otpost_med.brand_code, Post_med.OTPostOperative.otpost_med.genericname, Post_med.OTPostOperative.otpost_med.mappingid, Post_med.OTPostOperative.otpost_med.Dosages, Post_med.OTPostOperative.otpost_med.Duration, Post_med.OTPostOperative.otpost_med.Used, Post_med.OTPostOperative.otpost_med.Remark, Post_med.OTPostOperative.TransferId,Post_med.OTPostOperative.otpost_med.other, hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.TransferId = Post_med.OTPostOperative.TransferId;
            model.OTPostOperative.med_list = Extend.ToList<med_entity>(ds.Tables[1]);
            return PartialView("_InPatientOTStatus", model);
        }

        public PartialViewResult AddOTDetail(IP_OT_Transfer OT)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTservice.Ot_Service_entity = new List<ServiceItem_Entity>();
            model.OTPreOperative = new OT_PreOperative();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);

            string surgeon = "";
            if (OT.OTdetail.Surgeon != null)
            {
                foreach (var item in OT.OTdetail.Surgeon)
                {
                    surgeon += item + ",";
                }           
            }
            string Asssurgeon = "";
            if (OT.OTdetail.Asst_Surgeon != null)
            {
                foreach (var item in OT.OTdetail.Asst_Surgeon)
                {
                    Asssurgeon += item + ",";
                }
            }
            if (surgeon != "")
            {
                surgeon = surgeon.TrimEnd(',');
            }
            if (Asssurgeon != "")
            {
                Asssurgeon = Asssurgeon.TrimEnd(',');
            }

            DataSet ds = InPat.AddOtdetails(OT.OTdetail.Name_of_Operation, OT.OTdetail.Indication, surgeon, Asssurgeon, OT.OTdetail.Findings,OT.OTdetail.Procedure,OT.OTdetail.Histology,OT.OTdetail.Anaesthetist,OT.OTdetail.Anaesthetic_Used,OT.OTdetail.Unista_Blood_Transfusion, OT.OTdetail.From_Time,OT.OTdetail.To_Time,OT.OTdetail.OT_Date,OT.OTdetail.OT_Nurse, OT.OTdetail.TransferId, hoscode, usercode,OT.OTdetail.Asst_Surgeon_name);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.TransferId = OT.OTdetail.TransferId;
            model.OTdetail = new OT_Detail();
            if (ds.Tables[1].Rows.Count > 0)
            {
                model.OTdetail.Name_of_Operation = Convert.ToString(ds.Tables[1].Rows[0]["name_of_operation"]);
                model.OTdetail.Indication = Convert.ToString(ds.Tables[1].Rows[0]["indication"]);
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["surgeon"])))
                {
                    model.OTdetail.Surgeon = Convert.ToString(ds.Tables[1].Rows[0]["surgeon"]).Split(',');
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["asstt_surgeon"])))
                {
                    model.OTdetail.Asst_Surgeon = Convert.ToString(ds.Tables[1].Rows[0]["asstt_surgeon"]).Split(',');
                }

                //model.OTdetail.Asst_Surgeon = Convert.ToString(ds.Tables[1].Rows[0]["asstt_surgeon"]);
                model.OTdetail.Findings = Convert.ToString(ds.Tables[1].Rows[0]["findings"]);
                model.OTdetail.Procedure = Convert.ToString(ds.Tables[1].Rows[0]["ot_procedure"]);
                model.OTdetail.Histology = Convert.ToString(ds.Tables[1].Rows[0]["histology"]);
                model.OTdetail.Anaesthetist = Convert.ToString(ds.Tables[1].Rows[0]["anaesthetist"]);
                model.OTdetail.Anaesthetic_Used = Convert.ToString(ds.Tables[1].Rows[0]["anaesthetic_used"]);
                model.OTdetail.Unista_Blood_Transfusion = Convert.ToString(ds.Tables[1].Rows[0]["unista_blood_transfusion"]);
                model.OTdetail.From_Time = Convert.ToString(ds.Tables[1].Rows[0]["ot_from_time"]);
                model.OTdetail.To_Time = Convert.ToString(ds.Tables[1].Rows[0]["ot_to_time"]);
                model.OTdetail.OT_Date = Convert.ToString(ds.Tables[1].Rows[0]["ot_date"]);
                model.OTdetail.OT_Nurse = Convert.ToString(ds.Tables[1].Rows[0]["ot_nurse"]);
                model.OTdetail.appointment_code = Convert.ToString(ds.Tables[1].Rows[0]["appointment_code"]);
                model.OTdetail.Asst_Surgeon_name = Convert.ToString(ds.Tables[1].Rows[0]["asstt_surgeon_name"]);
            }
            model.OTdetail.surgeon_lst = GetDoctorList(ds.Tables[2]);
            model.OTdetail.asst_surgeon_lst = GetAssDoctorList(ds.Tables[3]);
            

            return PartialView("_InPatientOTStatus", model);
        }

        public PartialViewResult AddOTServices(IP_OT_Transfer service)
        {
            IP_OT_Transfer model = new IP_OT_Transfer();
            model.OTservice = new OT_Service();
            model.OTservice.ot_service_list = new List<OTService_Entity>();
            model.OTdetail = new OT_Detail();
            model.OTdetail.surgeon_lst = new List<SelectListItem>();
            model.OTdetail.asst_surgeon_lst = new List<SelectListItem>();
            model.OTPreOperative = new OT_PreOperative();
            model.OTPostOperative = new OT_PostOperative();
            model.OTPostOperative.med_list = new List<med_entity>();
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            string usercode = Convert.ToString(Session["UserCode"]);
            DataSet ds = InPat.AddOTService(service.OTservice.Service_id, service.OTservice.ServiceName,service.OTservice.Cost, service.OTservice.SellingCost,service.OTservice.MinSellingCost,service.OTservice.Discount,service.OTservice.Remark, "Ot", service.OTservice.is_realtime, service.OTservice.TransferId,  hoscode, usercode);
            model.statusmsg = Convert.ToString(ds.Tables[0].Rows[0]["message"]);
            model.TransferId = service.OTservice.TransferId;
            model.OTservice.Ot_Service_entity = Extend.ToList<ServiceItem_Entity>(ds.Tables[1]);
            return PartialView("_InPatientOTStatus", model);
        }

        public ActionResult DeleteMed(int Id)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = InPat.DeleteMed(Id, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmServiceDelete(int Id,string AppCode)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = InPat.DeleteService(Id, AppCode, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmOTDteails(int Serviceid)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = InPat.ReleaseOT(Serviceid, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OperationDetails()
        {
            var code = Request.Path.Split('/')[3];
            List<Operation> OT = new List<Operation>();
            var ds = InPat.GetOTDetails(code);
            var Post_Oplist = Extend.ToList<PostOperative>(ds.Tables[1]);
            var Po_MedList = Extend.ToList<POmedicine>(ds.Tables[2]);
            var Pre_Oplist = Extend.ToList<PreOperative>(ds.Tables[3]);
            var OtDetails = Extend.ToList<OtDetails>(ds.Tables[4]);
            var otservices = Extend.ToList<OtServices>(ds.Tables[5]);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int OtId = Convert.ToInt16(ds.Tables[0].Rows[i]["OtId"]);
                var op = new Operation
                {
                    OtId = OtId,
                    name_of_operation = Convert.ToString(ds.Tables[0].Rows[i]["name_of_operation"]),
                    OperationDate = Convert.ToString(ds.Tables[0].Rows[i]["OperationDate"]),
                    OperationTime = Convert.ToString(ds.Tables[0].Rows[i]["OperationTime"]),
                    doctor_name = Convert.ToString(ds.Tables[0].Rows[i]["doctor_name"]),
                    Postoperative = Post_Oplist.Find(d => d.OtId == OtId),
                    PoMedicinelist = Po_MedList.FindAll(d => d.OtId == OtId),
                    Preoperative = Pre_Oplist.Find(d => d.OtId == OtId),
                    Otdetail = OtDetails.Find(d => d.OtId == OtId),
                    Services = otservices.FindAll(h => h.OtId == OtId)
                };
                OT.Add(op);
            }

            return View(OT);
        }

        public ActionResult OPDOperationDetails()
        {
            var code = Request.Path.Split('/')[3];
            List<Operation> OT = new List<Operation>();
            var ds = InPat.GetOPDOTDetails(code);
            var Post_Oplist = new List<PostOperative>();
            var Po_MedList = new List<POmedicine>();
            var Pre_Oplist = new List<PreOperative>();
            var OtDetails = Extend.ToList<OtDetails>(ds.Tables[1]);
            var otservices = Extend.ToList<OtServices>(ds.Tables[2]);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int OtId = Convert.ToInt16(ds.Tables[0].Rows[i]["OtId"]);
                var op = new Operation
                {
                    OtId = OtId,
                    name_of_operation = Convert.ToString(ds.Tables[0].Rows[i]["name_of_operation"]),
                    OperationDate = Convert.ToString(ds.Tables[0].Rows[i]["OperationDate"]),
                    OperationTime = Convert.ToString(ds.Tables[0].Rows[i]["OperationTime"]),
                    doctor_name = Convert.ToString(ds.Tables[0].Rows[i]["doctor_name"]),
                    Otdetail = OtDetails.Find(d => d.OtId == OtId),
                    Services = otservices.FindAll(h => h.OtId == OtId)
                };
                OT.Add(op);
            }
            return View(OT);
        }
   
      
        public ActionResult OPDConfirmOTDteails(int Serviceid)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = InPat.ReleaseOPDOT(Serviceid, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public class Discharg_Pdf
        {
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