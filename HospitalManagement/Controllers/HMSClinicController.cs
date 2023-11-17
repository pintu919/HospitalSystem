using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using System.Data;
using System.IO;
using ImageResizer;
using System.Configuration;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSClinicController : _Base
    {
        readonly Common_Master comMaster;
        readonly Cliniq_Entity entity;
        readonly HRSetting_Entity hr_entity;
        readonly Cliniq_Master clk;
        readonly hospital_new_entity hn;

        // GET: HMSClinic
        public HMSClinicController()
        {
            clk = new Cliniq_Master(bsInfo);
            comMaster = new Common_Master(bsInfo);
            entity = new Cliniq_Entity();
            hr_entity = new HRSetting_Entity();
            hn = new hospital_new_entity();
        }
        public ActionResult ViewClinicProfile()
        {
            CliniqModel model = new CliniqModel();
            var entity = clk.GetByCode(Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<Cliniq_Entity>(entity.Tables[0]).SingleOrDefault();
            model.CliniqCode = CModel.cliniq_code;
            model.Cliniq_Addr = CModel.Cliniq_Addr;
            model.Cliniq_email = CModel.Cliniq_email;
            model.CliniqName = CModel.Cliniq_Name;
            model.Cliniq_phone = CModel.Cliniq_phone;
            model.Cliniq_RegNo = CModel.Cliniq_RegNo;
            model.cliniclogo = CModel.clinic_logo;
            model.Day1 = CModel.day1;
            model.Day1_Cliniq_open_time = CModel.day1_Cliniq_open_time;
            model.Day1_Cliniq_close_time = CModel.day1_Cliniq_close_time;
            model.Day2 = CModel.day2;
            model.Day2_Cliniq_open_time = CModel.day2_Cliniq_open_time;
            model.Day2_Cliniq_close_time = CModel.day2_Cliniq_close_time;
            model.Day3 = CModel.day3;
            model.Day3_Cliniq_open_time = CModel.day3_Cliniq_open_time;
            model.Day3_Cliniq_close_time = CModel.day3_Cliniq_close_time;
            model.Day4 = CModel.day4;
            model.Day4_Cliniq_open_time = CModel.day4_Cliniq_open_time;
            model.Day4_Cliniq_close_time = CModel.day4_Cliniq_close_time;
            model.Day5 = CModel.day5;
            model.Day5_Cliniq_open_time = CModel.day5_Cliniq_open_time;
            model.Day5_Cliniq_close_time = CModel.day5_Cliniq_close_time;
            model.Day6 = CModel.day6;
            model.Day6_Cliniq_open_time = CModel.day6_Cliniq_open_time;
            model.Day6_Cliniq_close_time = CModel.day6_Cliniq_close_time;
            model.Day7 = CModel.day7;
            model.Day7_Cliniq_open_time = CModel.day7_Cliniq_open_time;
            model.Day7_Cliniq_close_time = CModel.day7_Cliniq_close_time;


            return View("ViewClinicProfile", model);
        }
        public ActionResult EditClinicProfile()
        {
            CliniqModel model = new CliniqModel();

            var entity = clk.GetByCode(Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<Cliniq_Entity>(entity.Tables[0]).SingleOrDefault();
            model.CountryLst = comMaster.GetAllcountry("").OrderByDescending(o => o.country_id).ToList();
            model.CliniqCode = CModel.cliniq_code;
            model.CliniqName = CModel.Cliniq_Name;
            model.CityCode = CModel.city_code;
            model.Cliniq_Addr = CModel.Cliniq_Addr;
            model.Cliniq_email = CModel.Cliniq_email;
            model.Cliniq_emergency_no = CModel.Cliniq_emergency_no;
            model.Cliniq_landmark = CModel.Cliniq_landmark;
            model.Cliniq_off_day = CModel.Cliniq_off_day;
            model.Cliniq_phone = CModel.Cliniq_phone;
            model.Cliniq_RegNo = CModel.Cliniq_RegNo;
            model.Cliniq_tollfree = CModel.Cliniq_tollfree;
            model.CountryCode = CModel.country_code;
            model.DistrictCode = CModel.district_code;
            model.StateCode = CModel.state_code;
            model.zip = CModel.zip;
            model.Daily = CModel.dailyopen;
            model.Ctrl = CModel.Ctrl.ToBool();
            model.Telephone = CModel.tele_phone;
            model.hosting_url = CModel.hosting_url;
            model.cliniclogo = CModel.clinic_logo;
            //Time
            model.Day1 = CModel.day1;
            model.Day1_Cliniq_open_time = CModel.day1_Cliniq_open_time;
            model.Day1_Cliniq_close_time = CModel.day1_Cliniq_close_time;
            model.Day2 = CModel.day2;
            model.Day2_Cliniq_open_time = CModel.day2_Cliniq_open_time;
            model.Day2_Cliniq_close_time = CModel.day2_Cliniq_close_time;
            model.Day3 = CModel.day3;
            model.Day3_Cliniq_open_time = CModel.day3_Cliniq_open_time;
            model.Day3_Cliniq_close_time = CModel.day3_Cliniq_close_time;
            model.Day4 = CModel.day4;
            model.Day4_Cliniq_open_time = CModel.day4_Cliniq_open_time;
            model.Day4_Cliniq_close_time = CModel.day4_Cliniq_close_time;
            model.Day5 = CModel.day5;
            model.Day5_Cliniq_open_time = CModel.day5_Cliniq_open_time;
            model.Day5_Cliniq_close_time = CModel.day5_Cliniq_close_time;
            model.Day6 = CModel.day6;
            model.Day6_Cliniq_open_time = CModel.day6_Cliniq_open_time;
            model.Day6_Cliniq_close_time = CModel.day6_Cliniq_close_time;
            model.Day7 = CModel.day7;
            model.Day7_Cliniq_open_time = CModel.day7_Cliniq_open_time;
            model.Day7_Cliniq_close_time = CModel.day7_Cliniq_close_time;
            model.CountryLst = Extend.ToList<Country_Entity>(entity.Tables[1]);
            model.StateLst = Extend.ToList<State_Entity>(entity.Tables[2]);
            model.DistrictLst = Extend.ToList<District_Entity>(entity.Tables[3]);
            model.CityLst = Extend.ToList<City_Entity>(entity.Tables[4]);



            return View("EditClinicProfile", model);

        }

        [HttpPost]
        public ActionResult UpdateClinic(CliniqModel model)
        {
            string status = "";
            if (Request.Files.Count > 0)
            {
                var CL = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase imgfile = files[0];

                if (imgfile.FileName == "" || imgfile.FileName == null)
                {
                    status = "Please Select File";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                var folderpath = ConfigurationManager.AppSettings["addhosimgUrl"];
                string myfile = CL + Path.GetExtension(imgfile.FileName);
                string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                //string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                if (System.IO.File.Exists(folderpath + model.cliniclogo))
                {
                    System.IO.File.Delete(folderpath + model.cliniclogo);
                }
                imgfile.SaveAs(fname);
                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Width = 250,
                    Height = 75,
                    Format = "png"
                };
                ImageBuilder.Current.Build(fname, fname, resizeSetting);
                model.cliniclogo = myfile.Replace(" ", string.Empty);  // "/Image/ClinicLogo/" + imgfile.FileName.Replace(" ", string.Empty);
            }

            var entity = SetClinicEntity(model);

            var res = clk.Update(entity);

            if (res == true)
            {
                status = "Record Update Sucessfully!!";
            }
            else
            {
                status = "Record Does Not Update Sucessfully!!";
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public Cliniq_Entity SetClinicEntity(CliniqModel Cliniq)
        {
            entity.cliniq_code = Cliniq.CliniqCode;
            entity.Cliniq_Name = Cliniq.CliniqName.Trim();
            entity.Cliniq_Addr = Cliniq.Cliniq_Addr;
            entity.city_code = Cliniq.CityCode;
            entity.Cliniq_email = Cliniq.Cliniq_email;
            entity.Cliniq_emergency_no = Cliniq.Cliniq_emergency_no;
            entity.Cliniq_landmark = Cliniq.Cliniq_landmark;
            entity.Cliniq_off_day = Cliniq.Cliniq_off_day;
            entity.Cliniq_phone = Cliniq.Cliniq_phone;
            entity.Cliniq_RegNo = Cliniq.Cliniq_RegNo;
            entity.Cliniq_tollfree = Cliniq.Cliniq_tollfree;
            entity.country_code = Cliniq.CountryCode;
            entity.district_code = Cliniq.DistrictCode;
            // entity.GSTNO = Cliniq.GSTNO;           
            entity.state_code = Cliniq.StateCode;
            entity.zip = Cliniq.zip;
            entity.dailyopen = Cliniq.Daily;
            entity.tele_phone = Cliniq.Telephone;
            entity.hosting_url = Cliniq.hosting_url;
            entity.clinic_logo = Cliniq.cliniclogo;
            //Time
            entity.day1 = Cliniq.Day1;
            entity.day1_Cliniq_open_time = Cliniq.Day1_Cliniq_open_time;
            entity.day1_Cliniq_close_time = Cliniq.Day1_Cliniq_close_time;
            entity.day2 = Cliniq.Day2;
            entity.day2_Cliniq_open_time = Cliniq.Day2_Cliniq_open_time;
            entity.day2_Cliniq_close_time = Cliniq.Day2_Cliniq_close_time;
            entity.day3 = Cliniq.Day3;
            entity.day3_Cliniq_open_time = Cliniq.Day3_Cliniq_open_time;
            entity.day3_Cliniq_close_time = Cliniq.Day3_Cliniq_close_time;
            entity.day4 = Cliniq.Day4;
            entity.day4_Cliniq_open_time = Cliniq.Day4_Cliniq_open_time;
            entity.day4_Cliniq_close_time = Cliniq.Day4_Cliniq_close_time;
            entity.day5 = Cliniq.Day5;
            entity.day5_Cliniq_open_time = Cliniq.Day5_Cliniq_open_time;
            entity.day5_Cliniq_close_time = Cliniq.Day5_Cliniq_close_time;
            entity.day6 = Cliniq.Day6;
            entity.day6_Cliniq_open_time = Cliniq.Day6_Cliniq_open_time;
            entity.day6_Cliniq_close_time = Cliniq.Day6_Cliniq_close_time;
            entity.day7 = Cliniq.Day7;
            entity.day7_Cliniq_open_time = Cliniq.Day7_Cliniq_open_time;
            entity.day7_Cliniq_close_time = Cliniq.Day7_Cliniq_close_time;

            return entity;
        }

        public ActionResult ClinicTheamSetting()
        {
            ClinicProfile model = new ClinicProfile();
            model.clklogo = new Cliniclogo();
            model.inv_content = new InvoiceContent();
            model.Hos_Control = new HospitalControl();
            if (Convert.ToString(Session["ClinicCode"]) == "" || Convert.ToString(Session["ClinicCode"]) == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var entity = clk.GetByCode(Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<Cliniq_Entity>(entity.Tables[0]).SingleOrDefault();
            model.clklogo.cliniclogo = CModel.clinic_logo;
            model.clklogo.ClinicDoclogo = CModel.clinic_doclogo;
            model.clklogo.clinicfevicon = CModel.clinic_feviconicon;
            model.inv_content.invcontent = CModel.inv_content;
            model.inv_content.AdmissionFormTerms_Condition = CModel.AdmissionFormTerms_Condition;
            model.Hos_Control.AllowDischargeWithoutPayment = Convert.ToBoolean(CModel.AllowDischargeWithoutPayment);
            model.Hos_Control.DoctorVisitFeesAddOPDInvoice = Convert.ToBoolean(CModel.DoctorVisitFeesAddInInvoice);
            model.Hos_Control.AllowTestReportEmailWithoutFullPayment = Convert.ToBoolean(CModel.AllowTestReportEmailWithoutPayment);
            model.Hos_Control.Showpatientiamgeinprescription = Convert.ToBoolean(CModel.ShowPatientIamgeinPrescription);
            model.Hos_Control.Showpatientdistrictinprescription = Convert.ToBoolean(CModel.ShowPatientDistrictinPrescription);
            model.Hos_Control.Showeyeinprescription = Convert.ToBoolean(CModel.ShowEyePrescription);
            return View("ClinicTheamSetting", model);
        }
        public ActionResult SetHosControl(ClinicProfile model)
        {
            string status = "";
            var CliniqCode = Convert.ToString(Session["ClinicCode"]);
            var res = clk.UpdateControl(CliniqCode, Convert.ToInt16(model.Hos_Control.AllowDischargeWithoutPayment),
                Convert.ToInt16(model.Hos_Control.DoctorVisitFeesAddOPDInvoice),
                Convert.ToInt16(model.Hos_Control.AllowTestReportEmailWithoutFullPayment),
                Convert.ToInt16(model.Hos_Control.Showpatientiamgeinprescription),
                Convert.ToInt16(model.Hos_Control.Showpatientdistrictinprescription),
                Convert.ToInt16(model.Hos_Control.Showeyeinprescription));
            if (res == true)
            {
                status = "Hospital Control Set Sucessfully!!";
            }
            else
            {
                status = "problem has been occurred while submitting your data.";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClinicChageProfilePic(ClinicProfile model)
        {

            string[] status = new string[2];

            if (Request.Files.Count > 0)
            {
                var CL = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase imgfile = files[0];

                if (imgfile.FileName == "" || imgfile.FileName == null)
                {
                    status[0] = "Please Select File";
                    status[1] = null;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                var folderpath = ConfigurationManager.AppSettings["addhosimgUrl"];
                string myfile = CL + Path.GetExtension(imgfile.FileName);
                string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                //string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                if (System.IO.File.Exists(folderpath + model.clklogo.cliniclogo))
                {
                    System.IO.File.Delete(folderpath + model.clklogo.cliniclogo);
                }
                imgfile.SaveAs(fname);
                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Width = 250,
                    Height = 75,
                    Format = "png"
                };
                ImageBuilder.Current.Build(fname, fname, resizeSetting);
                model.clklogo.cliniclogo = myfile.Replace(" ", string.Empty);  // "/Image/ClinicLogo/" + imgfile.FileName.Replace(" ", string.Empty);
                Session["user_image"] = model.clklogo.cliniclogo;

                var rendom = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase docfiles = Request.Files;
                HttpPostedFileBase docimgfile = docfiles[0];

                if (docimgfile.FileName == "" || docimgfile.FileName == null)
                {
                    status[0] = "Please Select File";
                    status[1] = null;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                var docfolderpath = ConfigurationManager.AppSettings["addhosimgUrl"];
                string docmyfile = rendom + Path.GetExtension(docimgfile.FileName);
                string docfname = docfolderpath + "" + docmyfile.Replace(" ", string.Empty);
                //string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                imgfile.SaveAs(docfname);
                ResizeSettings docresizeSetting = new ResizeSettings
                {
                    //Width = 400,
                    //Height = 80,
                    Format = "png"
                };
                ImageBuilder.Current.Build(docfname, docfname, docresizeSetting);
                model.clklogo.ClinicDoclogo = docmyfile.Replace(" ", string.Empty);  // "/Image/ClinicLogo/" + imgfile.FileName.Replace(" ", string.Empty);



                model.clklogo.clinicfevicon = "";
                model.clklogo.CliniqCode = Convert.ToString(Session["ClinicCode"]);

                var entity = SetEntityData(model);
                var res = clk.UpdateHospitalLogo(entity);
                if (res == true)
                {
                    status[0] = "Profile Change Sucessfully!!";
                    status[1] = ConfigurationManager.AppSettings["gethosimgUrl"] + model.clklogo.cliniclogo;
                }
                else
                {
                    status[0] = "problem has been occurred while submitting your data.";
                    status[1] = null;
                }

            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClinicChagePrescriptionPic(ClinicProfile model)
        {


            string[] status = new string[2];

            if (Request.Files.Count > 0)
            {
                //var CL = DateTime.Now.ToString("yyMMddHHmmssff");
                HttpFileCollectionBase files = Request.Files;
                //HttpPostedFileBase imgfile = files[0];

                //if (imgfile.FileName == "" || imgfile.FileName == null)
                //{
                //    status[0] = "Please Select File";
                //    status[1] = null;
                //    return Json(status, JsonRequestBehavior.AllowGet);
                //}
                var folderpath = ConfigurationManager.AppSettings["addhosimgUrl"];
                //string myfile = CL + Path.GetExtension(imgfile.FileName);
                //string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                ////string fname = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                //if (System.IO.File.Exists(folderpath + model.clklogo.cliniclogo))
                //{
                //    System.IO.File.Delete(folderpath + model.clklogo.cliniclogo);
                //}
                //imgfile.SaveAs(fname);
                //ResizeSettings resizeSetting = new ResizeSettings
                //{
                //    Width = 250,
                //    Height = 75,
                //    Format = "png"
                //};
                //ImageBuilder.Current.Build(fname, fname, resizeSetting);
                //model.clklogo.cliniclogo = myfile.Replace(" ", string.Empty);  // "/Image/ClinicLogo/" + imgfile.FileName.Replace(" ", string.Empty);
                //Session["user_image"] = model.clklogo.cliniclogo;



                HttpPostedFileBase imgfile1 = files[0];

                if (imgfile1.FileName == "" || imgfile1.FileName == null)
                {
                    status[0] = "Please Select File";
                    status[1] = null;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                var CL1 = "PREC_" + DateTime.Now.ToString("yyMMddHHmmssff");
                var folderpath1 = ConfigurationManager.AppSettings["addhosimgUrl"];
                string myfile1 = CL1 + Path.GetExtension(imgfile1.FileName);
                //string fname1 = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile1.FileName.Replace(" ", string.Empty));
                string fname1 = folderpath + "" + myfile1.Replace(" ", string.Empty);
                if (System.IO.File.Exists(folderpath1 + model.clklogo.clinicfevicon))
                {
                    System.IO.File.Delete(folderpath1 + model.clklogo.clinicfevicon);
                }
                imgfile1.SaveAs(fname1);
                ResizeSettings resizeSetting1 = new ResizeSettings
                {
                    Width = 250,
                    Height = 75,
                    Format = "png"
                };
                ImageBuilder.Current.Build(fname1, fname1, resizeSetting1);
                model.clklogo.clinicfevicon = myfile1.Replace(" ", string.Empty);         // "/Image/ClinicLogo/" + imgfile1.FileName.Replace(" ", string.Empty);
                                                                                          //model.clklogo.clinicfevicon = "";
                model.clklogo.CliniqCode = Convert.ToString(Session["ClinicCode"]);

                var entity = SetEntityData(model);
                var res = clk.UpdatePresciptionLogo(entity);
                if (res == true)
                {
                    status[0] = "Profile Change Sucessfully!!";
                    status[1] = ConfigurationManager.AppSettings["gethosimgUrl"] + model.clklogo.clinicfevicon;
                }
                else
                {
                    status[0] = "problem has been occurred while submitting your data.";
                    status[1] = null;
                }

            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddInvContent(ClinicProfile model)
        {
            string status = "";
            model.inv_content.CliniqCode = Convert.ToString(Session["ClinicCode"]);
            var entity = SetEntitycontent(model.inv_content);
            var res = clk.UpdateInvContent(entity);
            if (res == true)
            {
                status = "Invoice Content Added Sucessfully!!";
            }
            else
            {
                status = "problem has been occurred while submitting your data.";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPrescriptionSetting(Prescriptionsetting model)
        {
            string status = "";
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;

                HttpPostedFileBase imgfile = files[0];
                if (imgfile.FileName == "" || imgfile.FileName == null)
                {
                    status = "Please Select File";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                string fname = Path.Combine(Server.MapPath("~/Image/PrescriptionSetting/"), imgfile.FileName.Replace(" ", string.Empty));
                if (System.IO.File.Exists(fname))
                {
                    System.IO.File.Delete(fname);
                }
                imgfile.SaveAs(fname);
                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Width = 1024,
                    Height = 768,
                    Format = "jpg"
                };
                ImageBuilder.Current.Build(fname, fname, resizeSetting);
                model.ClinicPre.HeaderImage = "/Image/PrescriptionSetting/" + imgfile.FileName.Replace(" ", string.Empty);

                HttpPostedFileBase imgfile1 = files[1];

                if (imgfile1.FileName == "" || imgfile1.FileName == null)
                {
                    status = "Please Select File";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                string fname1 = Path.Combine(Server.MapPath("~/Image/PrescriptionSetting/"), imgfile1.FileName.Replace(" ", string.Empty));
                if (System.IO.File.Exists(fname1))
                {
                    System.IO.File.Delete(fname1);
                }
                imgfile1.SaveAs(fname1);
                ResizeSettings resizeSetting1 = new ResizeSettings
                {
                    Width = 1024,
                    Height = 768,
                    Format = "jpg"
                };
                ImageBuilder.Current.Build(fname1, fname1, resizeSetting1);
                model.ClinicPre.FooterImage = "/Image/PrescriptionSetting/" + imgfile1.FileName.Replace(" ", string.Empty);
                model.ClinicPre.CliniqCode = Convert.ToString(Session["ClinicCode"]);

                var entity = SetEntityPrescription(model);
                var res = clk.UpdatePrescription(entity);
                if (res == true)
                {
                    status = "Profile Change Sucessfully!!";
                }
                else
                {
                    status = "problem has been occurred while submitting your data.";
                }

            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public Cliniq_Entity SetEntityPrescription(Prescriptionsetting CP)
        {
            if (CP.ClinicPre.HeaderImage != null)
                entity.prescriptionHeader_Image = CP.ClinicPre.HeaderImage;
            if (CP.ClinicPre.FooterImage != null)
                entity.prescriptionFooter_Image = CP.ClinicPre.FooterImage;
            entity.cliniq_code = CP.ClinicPre.CliniqCode;


            //entity.verification_code = Dr.VerificationCode;

            return entity;
        }

        public ActionResult ClinicPrescriptionSetting()
        {
            Prescriptionsetting model = new Prescriptionsetting();
            model.ClinicPre = new ClinicPrescriptionsetting();
            model.DefaultPescription = new List<default_prescription>();

            if (Convert.ToString(Session["ClinicCode"]) == "" || Convert.ToString(Session["ClinicCode"]) == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var entity = clk.GetByCode(Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<Cliniq_Entity>(entity.Tables[0]).SingleOrDefault();
            model.ClinicPre.HeaderImage = CModel.prescriptionHeader_Image;
            model.ClinicPre.FooterImage = CModel.prescriptionFooter_Image;
            //model.DefaultPescription = Extend.ToList<default_prescription>(entity.Tables[6]).ToList();
            return View("ClinicPrescriptionSetting", model);
        }


        public Cliniq_Entity SetEntityData(ClinicProfile CP)
        {
            if (CP.clklogo.cliniclogo != null)
                entity.clinic_logo = CP.clklogo.cliniclogo;
            entity.clinic_doclogo = CP.clklogo.ClinicDoclogo;
            if (CP.clklogo.clinicfevicon != null)
                entity.clinic_feviconicon = CP.clklogo.clinicfevicon;
            entity.cliniq_code = CP.clklogo.CliniqCode;


            //entity.verification_code = Dr.VerificationCode;

            return entity;
        }
        public Cliniq_Entity SetEntitycontent(InvoiceContent CP)
        {
            if (CP.invcontent != null)
                entity.inv_content = CP.invcontent;
            if (CP.AdmissionFormTerms_Condition != null)
                entity.AdmissionFormTerms_Condition = CP.AdmissionFormTerms_Condition;
            entity.cliniq_code = CP.CliniqCode;
            return entity;
        }
        public ActionResult ClinicHRSetting()
        {

            HRSetting model = new HRSetting();

            var entity = clk.GetByCodeHRSetting(Convert.ToString(Session["ClinicCode"]));
            List<HRSetting_Entity> EM = new List<HRSetting_Entity>();
            DataTable DT = entity.Tables[0];

            foreach (DataRow dr in entity.Tables[0].Rows)
            {
                model.clinc_hr_basic = Convert.ToDecimal(dr["clinc_hr_basic"]);
                model.clinc_hr_hra = Convert.ToDecimal(dr["clinc_hr_hra"]);
                model.clinic_hr_medical = Convert.ToDecimal(dr["clinic_hr_medical"]);
                model.clinic_hr_conveyallowance = Convert.ToDecimal(dr["clinic_hr_conveyallowance"]);
                model.clinic_hr_otherallowance = Convert.ToDecimal(dr["clinic_hr_otherallowance"]);
                model.clinic_hr_basicpf = Convert.ToDecimal(dr["clinic_hr_basicpf"]);
                model.clinic_hr_maxbasicpf = Convert.ToDecimal(dr["clinic_hr_maxbasicpf"]);
                model.Ctrl = true;
            }

            return View("ClinicHRSetting", model);

        }

        public ActionResult AddClinicHRSetting(HRSetting model)
        {

            if (ModelState.IsValid)
            {
                decimal total = (model.clinc_hr_basic + model.clinc_hr_hra + model.clinic_hr_conveyallowance + model.clinic_hr_medical + model.clinic_hr_otherallowance);

                if (total >= 100)
                {

                    model.clinic_code = Convert.ToString(Session["ClinicCode"]);
                    model.Ctrl = true;
                    var entity = SetEntityDataHRA(model);
                    string res = clk.AddClinicHRSetting(entity);

                    if (res == "True")
                    {
                        TempData["message"] = "Record Added Successfully!";
                    }
                    else
                        TempData["message"] = "Record Update Successfully!";
                }
                else
                {
                    TempData["message"] = "Complete 100 % salary Setting";
                }

                ModelState.Clear();
            }

            return View("ClinicHRSetting", model);

        }

        public HRSetting_Entity SetEntityDataHRA(HRSetting HR)
        {
            hr_entity.clinc_hr_basic = HR.clinc_hr_basic;
            hr_entity.clinc_hr_hra = HR.clinc_hr_hra;
            hr_entity.clinic_hr_medical = HR.clinic_hr_medical;
            hr_entity.clinic_hr_conveyallowance = HR.clinic_hr_conveyallowance;
            hr_entity.clinic_hr_otherallowance = HR.clinic_hr_otherallowance;
            hr_entity.clinic_hr_basicpf = HR.clinic_hr_basicpf;
            hr_entity.clinic_hr_maxbasicpf = HR.clinic_hr_maxbasicpf;
            hr_entity.ctrl = Convert.ToInt32(HR.Ctrl);
            hr_entity.clinic_code = HR.clinic_code;
            hr_entity.user_code = Convert.ToString(Session["UserCode"]);
            //entity.verification_code = Dr.VerificationCode;
            return hr_entity;
        }



        public PartialViewResult GetDatabyCode(string code, string Type)
        {
            HMSPatientModel model = new HMSPatientModel();
            model.CountryLst = new List<Country_Entity>();

            if (Type == "state")
            {
                model.StateLst = comMaster.GetStateByCode(code).OrderByDescending(o => o.stateid).ToList();
                model.DistrictLst = new List<District_Entity>();
                model.CityLst = new List<City_Entity>();
            }
            else if (Type == "district")
            {
                model.StateLst = new List<State_Entity>();
                model.DistrictLst = comMaster.GetDistrictByCode(code).OrderByDescending(o => o.dist_no).ToList();
                model.CityLst = new List<City_Entity>();
            }
            else if (Type == "city")
            {
                model.StateLst = new List<State_Entity>();
                model.DistrictLst = new List<District_Entity>();
                model.CityLst = comMaster.GetCityByCode(code).OrderByDescending(o => o.district_code).ToList();
            }
            return PartialView("EditClinicProfile", model);
        }

        public ActionResult ClinicRolesAndPermission()
        {
            return View();
        }

        #region "Hospital News"
        public ActionResult HospitalNew()
        {
            HosNewsData model = new HosNewsData();
            DataSet ds = clk.GetAllNews(Convert.ToString(Session["ClinicCode"]));
            model.hos_news_data = Extend.ToList<hospital_new_entity>(ds.Tables[0]);
            return View("HospitalNew", model);
        }

        [HttpPost]


        public ActionResult AddHosNews(HosNewsData model)
        {
            if (Request.Files.Count > 0)
            {

                HttpFileCollectionBase files = Request.Files;
                var folderpath = ConfigurationManager.AppSettings["addhosimgUrl"];
                HttpPostedFileBase imgfile1 = files[0];


                var CL1 = "NEWS_" + DateTime.Now.ToString("yyMMddHHmmssff");
                var folderpath1 = ConfigurationManager.AppSettings["addhosimgUrl"];
                string myfile1 = CL1 + Path.GetExtension(imgfile1.FileName);
                //string fname1 = Path.Combine(Server.MapPath("~/Image/ClinicLogo/"), imgfile1.FileName.Replace(" ", string.Empty));
                string fname1 = folderpath + "" + myfile1.Replace(" ", string.Empty);
                if (System.IO.File.Exists(folderpath1 + model.news_img))
                {
                    System.IO.File.Delete(folderpath1 + model.news_img);
                }
                imgfile1.SaveAs(fname1);
                ResizeSettings resizeSetting1 = new ResizeSettings
                {
                    Width = 1200,
                    Height = 800,
                    Format = "png"
                };
                ImageBuilder.Current.Build(fname1, fname1, resizeSetting1);
                model.news_img = myfile1.Replace(" ", string.Empty);         // "/Image/ClinicLogo/" + imgfile1.FileName.Replace(" ", string.Empty);
                                                                             //model.clklogo.clinicfevicon = "";
                model.hos_code = Convert.ToString(Session["ClinicCode"]);
                model.user_code = Convert.ToString(Session["UserCode"]);

                model.msg = new Statusmsg();
                var entity = SetEntityNewsData(model);
                DataSet ds = clk.AddHosNews(entity);
                model.msg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                model.hos_news_data = Extend.ToList<hospital_new_entity>(ds.Tables[1]);


            }
            return View("HospitalNew", model);
        }

        public hospital_new_entity SetEntityNewsData(HosNewsData CP)
        {
            if (CP.news_img != null)
                hn.news_img = CP.news_img;
            hn.news_title = CP.news_title;
            hn.news_Date = CP.news_Date;
            hn.news_content = CP.news_content;
            hn.hos_code = CP.hos_code;
            hn.user_code = CP.user_code;
            hn.ctrl = 1;
            return hn;
        }

        [HttpPost]
        public ActionResult DeleteNews(int id)
        {
            HosNewsData Model = new HosNewsData();
            DataSet ds = clk.DeleteNewsData(id, Convert.ToString(Session["ClinicCode"]));
            if (ds.Tables.Count > 0)
            {
                Model.msg = new Statusmsg();
                Model.msg.StatusId = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                Model.hos_news_data = Extend.ToList<hospital_new_entity>(ds.Tables[1]);
            }
            return View("HospitalNew", Model);
        }

        #endregion


    }
}