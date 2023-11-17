using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Helper;
using HospitalManagement.Models;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSPatientController : _Base
    {
        // GET: HMSPatient
        readonly Common_Master comMaster;
        readonly Patient_Entity entity;
        readonly ForntendVital_Entity fe;
        readonly ForntendotherVital_Entity FV;
        readonly ForntendchronicVital_Entity FC;
        readonly patient_Master patMaster;
        readonly Vital_Master VitMaster;
        readonly Family_History_Entity he;
        readonly TestAndInvestigation_Master TestMaster;
        public HMSPatientController()
        {
            patMaster = new patient_Master(bsInfo);
            comMaster = new Common_Master(bsInfo);
            VitMaster = new Vital_Master(bsInfo);
            TestMaster = new TestAndInvestigation_Master(bsInfo);
            entity = new Patient_Entity();
            fe = new ForntendVital_Entity();
            FV = new ForntendotherVital_Entity();
            FC = new ForntendchronicVital_Entity();
            he = new Family_History_Entity();
        }
        public ActionResult HMSPatientList(DateTime? fromdate, DateTime? todate)
        {

            HMSPatientModel model = new HMSPatientModel();
            model.PatientStatusData = new PatientStatus();
            if (fromdate == null || todate == null)
            {
                fromdate = DateTime.Now;
                todate = DateTime.Now;
            }
            var dataSet = patMaster.GetAll(Convert.ToString(Session["ClinicCode"]), fromdate, todate);
            //model.vitalother.Vitalotherlst = new List<Vitalpara_Entity>();
            model.PatientStatusData.OrgansList = new List<patient_status_Organs>();
            model.visitpatientlist = Extend.ToList<Patient_Entity>(dataSet.Tables[0]);
            model.PatientStatusData.SurgeryList = Extend.ToList<patient_status_Surgery>(dataSet.Tables[1]);
            model.PatientStatusData.DiseaseList = Extend.ToList<patient_status_Disease>(dataSet.Tables[2]);
            model.PatientStatusData.Chronic_Formulation_List = Extend.ToList<FormulationList>(dataSet.Tables[3]);

            model.PatientStatusData.SurgeryData = new Surgery();
            model.PatientStatusData.DiseaseData = new Disease();
            model.PatientStatusData.Other_Med = new OtherMed();
            model.PatientStatusData.family_history = new FamilyHistory();
            model.PatientStatusData.ChronicMedicineData = new ChronicMedicine();
            model.PatientStatusData.Freq_List = SetUsed();
            model.PatientStatusData.SurgeryData.vitalsurgerylst = new List<ForntendVital_Entity>();
            model.PatientStatusData.DiseaseData.vitaldiseaselst = new List<ForntendVital_Entity>();
            model.PatientStatusData.Other_Med.vitalotherlst = new List<ForntendotherVital_Entity>();
            model.PatientStatusData.ChronicMedicineData.vitalchroniclst = new List<ForntendchronicVital_Entity>();
            model.PatientStatusData.family_history.familyhistorylst = new List<Family_History_Entity>();
            //Added By Dhaval Patel For Patient Search
            model.PatientSearch = new OtherPatientSearch();
            //End Dhaval
            return View("HMSPatientList", model);
        }
        //Added By Dhaval Patel For Patient Search
        [HttpPost]
        public ActionResult HMSPatientList_SearchOtherFilter(HMSPatientModel Data)
        {
            Data.PatientStatusData = new PatientStatus();
            Data.PatientStatusData.SurgeryList = new List<patient_status_Surgery>();
            Data.PatientStatusData.OrgansList = new List<patient_status_Organs>();
            Data.PatientStatusData.DiseaseList = new List<patient_status_Disease>();
            Data.PatientStatusData.SurgeryData = new Surgery();
            Data.PatientStatusData.DiseaseData = new Disease();
            Data.PatientStatusData.Other_Med = new OtherMed();
            Data.PatientStatusData.family_history = new FamilyHistory();
            Data.PatientStatusData.ChronicMedicineData = new ChronicMedicine();
            Data.PatientStatusData.SurgeryData.vitalsurgerylst = new List<ForntendVital_Entity>();
            Data.PatientStatusData.DiseaseData.vitaldiseaselst = new List<ForntendVital_Entity>();
            Data.PatientStatusData.Other_Med.vitalotherlst = new List<ForntendotherVital_Entity>();
            Data.PatientStatusData.ChronicMedicineData.vitalchroniclst = new List<ForntendchronicVital_Entity>();
            Data.PatientStatusData.family_history.familyhistorylst = new List<Family_History_Entity>();
            DataSet ds = patMaster.SearchOtherFilter(Convert.ToString(Session["ClinicCode"]), Data.PatientSearch.PatientCode, Data.PatientSearch.Email, Data.PatientSearch.MobileNo, Convert.ToString(Data.PatientSearch.PatientBirthYear), Data.PatientSearch.PatientName, Data.PatientSearch.PatientFatherName);
            Data.visitpatientlist = Extend.ToList<Patient_Entity>(ds.Tables[0]);
            return View("HMSPatientList", Data);
        }
        [HttpPost]
        public ActionResult HMSVisitedPatientList_SearchOtherFilter(HMSPatientModel Data)
        {
            Data.visitpatientlist = new List<Patient_Entity>();
            Data.DoctorList = new List<Doctor_Entity>();
            DataSet ds = patMaster.SearchOtherFilter(null, Data.PatientSearch.PatientCode, Data.PatientSearch.Email, Data.PatientSearch.MobileNo, Convert.ToString(Data.PatientSearch.PatientBirthYear), Data.PatientSearch.PatientName, Data.PatientSearch.PatientFatherName);
            Data.visitpatientlist = Extend.ToList<Patient_Entity>(ds.Tables[0]);
            return View("HMSVisitedPatientList", Data);
        }
        //End Dhaval
        public PartialViewResult GetVitalPara(string PatientCode)
        {
            HMSPatientModel model = new HMSPatientModel();
            model.PatientStatusData = new PatientStatus();
            model.PatientStatusData.SurgeryData = new Surgery();
            model.PatientStatusData.DiseaseData = new Disease();
            model.PatientStatusData.ChronicMedicineData = new ChronicMedicine();
            model.PatientStatusData.Other_Med = new OtherMed();
            model.PatientStatusData.family_history = new FamilyHistory();
            DataSet ds = VitMaster.GetAll_PatientStatus(PatientCode, Convert.ToString(Session["ClinicCode"]));
            model.PatientStatusData.SurgeryData.vitalsurgerylst = Extend.ToList<ForntendVital_Entity>(ds.Tables[0]).ToList();
            model.PatientStatusData.DiseaseData.vitaldiseaselst = Extend.ToList<ForntendVital_Entity>(ds.Tables[1]).ToList();
            if (ds.Tables[2].Rows.Count > 0)
            {
                model.PatientStatusData.Other_Med.AlergyMedicine = Convert.ToString(ds.Tables[2].Rows[0]["alergy_medicine"]);
                model.PatientStatusData.Other_Med.warning = Convert.ToString(ds.Tables[2].Rows[0]["warning"]);
                model.PatientStatusData.Other_Med.Other = Convert.ToString(ds.Tables[2].Rows[0]["other"]);
                model.PatientStatusData.Other_Med.IsPregnent = Convert.ToBoolean(ds.Tables[2].Rows[0]["is_pregnent"]);
                model.PatientStatusData.Other_Med.DeleviryDate = Convert.ToString(ds.Tables[2].Rows[0]["delivery_date"]);
                model.PatientStatusData.Other_Med.Msg = "";
            }
            else
            {
                model.PatientStatusData.Other_Med.AlergyMedicine = "";
                model.PatientStatusData.Other_Med.warning = "";
                model.PatientStatusData.Other_Med.Other = "";
                model.PatientStatusData.Other_Med.IsPregnent = false;
                model.PatientStatusData.Other_Med.DeleviryDate = "";
                model.PatientStatusData.Other_Med.Msg = "";
            }
            model.PatientStatusData.ChronicMedicineData.vitalchroniclst = Extend.ToList<ForntendchronicVital_Entity>(ds.Tables[3]).ToList();
            model.PatientStatusData.family_history.familyhistorylst = Extend.ToList<Family_History_Entity>(ds.Tables[4]).ToList();
            model.visitpatientlist = new List<Patient_Entity>();
            return PartialView("HMSPatientList", model);
        }
        //public List<SelectListItem> SetUsed()
        //{
        //    List<SelectListItem> listused = new List<SelectListItem>();
        //    listused.Add(new SelectListItem
        //    {
        //        Text = "1 times",
        //        Value = "1",
        //        Selected = true
        //    });
        //    listused.Add(new SelectListItem
        //    {
        //        Text = "2 times",
        //        Value = "2",
        //        Selected = true
        //    });
        //    listused.Add(new SelectListItem
        //    {
        //        Text = "3 times",
        //        Value = "3",
        //        Selected = true
        //    });
        //    listused.Add(new SelectListItem
        //    {
        //        Text = "4 times",
        //        Value = "4",
        //        Selected = true
        //    });
        //    listused.Add(new SelectListItem
        //    {
        //        Text = "5 times",
        //        Value = "5",
        //        Selected = true
        //    });
        //    return listused;
        //}

        public List<SelectListItem> SetUsed()
        {
            List<SelectListItem> listused = new List<SelectListItem>();
            listused.Add(new SelectListItem
            {
                Text = "1+0+0",
                Value = "1",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "1+0+1",
                Value = "7",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "1+1+1",
                Value = "3",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "0+1+0",
                Value = "4",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "0+0+1",
                Value = "5",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "1+1+0",
                Value = "2",
                Selected = true
            });
            listused.Add(new SelectListItem
            {
                Text = "0+1+1",
                Value = "6",
                Selected = true
            });

            listused.Add(new SelectListItem
            {
                Text = "1+1+1+1",
                Value = "8",
                Selected = true
            });
            return listused;
        }

        [HttpPost]
        public PartialViewResult AddPatientStatus(HMSPatientModel para, string Submit)
        {

            HMSPatientModel Modal = new HMSPatientModel();
            Modal.PatientStatusData = new PatientStatus();
            Modal.PatientStatusData.SurgeryData = new Surgery();
            Modal.PatientStatusData.DiseaseData = new Disease();
            Modal.PatientStatusData.Other_Med = new OtherMed();
            Modal.PatientStatusData.family_history = new FamilyHistory();
            Modal.PatientStatusData.ChronicMedicineData = new ChronicMedicine();
            if (Submit == "surgery")
            {
                fe.status_type_id = Convert.ToInt32(1);
                fe.status_name_id = Convert.ToInt32(para.PatientStatusData.SurgeryData.StatusSurgeryNameId);
                fe.organ_name = Convert.ToString(para.PatientStatusData.SurgeryData.sur_organ_name);
                //fe.status_organ_id = Convert.ToInt32(para.PatientStatusData.SurgeryData.SurgeryOrgansId);
                fe.status_date = Convert.ToString(para.PatientStatusData.SurgeryData.Surgeryfromdate);
                fe.remarks = Convert.ToString(para.PatientStatusData.SurgeryData.Surgeryremarks);
                fe.patient_code = Convert.ToString(para.PatientCode);
                fe.hos_code = Convert.ToString(Session["ClinicCode"]);
                fe.Ctrl = 1;
            }
            else if (Submit == "disease")
            {
                fe.status_type_id = Convert.ToInt32(2);
                fe.status_name_id = Convert.ToInt32(para.PatientStatusData.DiseaseData.StatusDiseaseNameId);
                fe.organ_name = Convert.ToString(para.PatientStatusData.DiseaseData.des_organ_name);
                //fe.status_organ_id = Convert.ToInt32(para.PatientStatusData.DiseaseData.DiseaseOrgansId);
                fe.status_date = Convert.ToString(para.PatientStatusData.DiseaseData.Diseasefromdate);
                fe.remarks = Convert.ToString(para.PatientStatusData.DiseaseData.Diseaseremarks);
                fe.patient_code = Convert.ToString(para.PatientCode);
                fe.hos_code = Convert.ToString(Session["ClinicCode"]);
                fe.Ctrl = 1;
            }
            else if (Submit == "chronic")
            {
                FC.brand_code= Convert.ToString(para.PatientStatusData.ChronicMedicineData.brand_code);
                FC.mapping_id = Convert.ToInt32(para.PatientStatusData.ChronicMedicineData.Mapping_id);
                FC.frequency = Convert.ToString(para.PatientStatusData.ChronicMedicineData.Frequence);
                FC.medicine_startdate = Convert.ToString(para.PatientStatusData.ChronicMedicineData.Med_StartDate);
                FC.remark = Convert.ToString(para.PatientStatusData.ChronicMedicineData.Remarks);
                FC.patient_code = Convert.ToString(para.PatientCode);
                FC.hos_code = Convert.ToString(Session["ClinicCode"]);
                FC.ctrl = 1;

            }
            else if (Submit == "other")
            {
                FV.alergy_medicine = Convert.ToString(para.PatientStatusData.Other_Med.AlergyMedicine);
                FV.warning = Convert.ToString(para.PatientStatusData.Other_Med.warning);
                FV.other = Convert.ToString(para.PatientStatusData.Other_Med.Other);
                FV.is_pregnent = Convert.ToInt32(para.PatientStatusData.Other_Med.IsPregnent);
                FV.delivery_date = Convert.ToString(para.PatientStatusData.Other_Med.DeleviryDate);
                FV.patient_code = Convert.ToString(para.PatientCode);
                FV.hos_code = Convert.ToString(Session["ClinicCode"]);
                FV.ctrl = 1;
            }
            else if (Submit == "familyhistory")
            {
                he.father_medical_history = Convert.ToString(para.PatientStatusData.family_history.father_Medical_history);
                he.mother_medical_history = Convert.ToString(para.PatientStatusData.family_history.mother_medical_history);
                he.history_date = Convert.ToString(para.PatientStatusData.family_history.history_date);
                he.patient_code = Convert.ToString(para.PatientCode);
                he.hos_code = Convert.ToString(Session["ClinicCode"]);
                he.ctrl = 1;
                

            }
            if (Submit == "surgery")
            {
                var res = VitMaster.AddFrontendVital(fe);
                Modal.PatientStatusData.SurgeryData.vitalsurgerylst = Extend.ToList<ForntendVital_Entity>(res).ToList();
            }
            else if (Submit == "disease")
            {
                var res = VitMaster.AddFrontendVital(fe);
                Modal.PatientStatusData.DiseaseData.vitaldiseaselst = Extend.ToList<ForntendVital_Entity>(res).ToList();
            }
            else if (Submit == "chronic")
            {
                var res = VitMaster.AddFrontendChronicVital(FC);
                Modal.PatientStatusData.ChronicMedicineData.vitalchroniclst = Extend.ToList<ForntendchronicVital_Entity>(res).ToList();
            }
            else if (Submit == "other")
            {
                var res = VitMaster.AddFrontendVitalOther(FV);
                Modal.PatientStatusData.Other_Med.vitalotherlst = Extend.ToList<ForntendotherVital_Entity>(res).ToList();
                if (res.Rows.Count > 0)
                {
                    Modal.PatientStatusData.Other_Med.AlergyMedicine = Convert.ToString(res.Rows[0]["alergy_medicine"]);
                    Modal.PatientStatusData.Other_Med.warning = Convert.ToString(res.Rows[0]["warning"]);
                    Modal.PatientStatusData.Other_Med.Other = Convert.ToString(res.Rows[0]["other"]);
                    Modal.PatientStatusData.Other_Med.IsPregnent = Convert.ToBoolean(res.Rows[0]["is_pregnent"]);
                    Modal.PatientStatusData.Other_Med.DeleviryDate = Convert.ToString(res.Rows[0]["delivery_date"]);
                    Modal.PatientStatusData.Other_Med.Msg = Convert.ToString(res.Rows[0]["message"]);
                }
                else
                {
                    Modal.PatientStatusData.Other_Med.AlergyMedicine = "";
                    Modal.PatientStatusData.Other_Med.warning = "";
                    Modal.PatientStatusData.Other_Med.Other = "";
                    Modal.PatientStatusData.Other_Med.IsPregnent = false;
                    Modal.PatientStatusData.Other_Med.DeleviryDate = "";
                    Modal.PatientStatusData.Other_Med.Msg = "";
                }


            }
            else if (Submit == "familyhistory")
            {
                var res = VitMaster.AddFrontendFamilyHistory(he);
                Modal.PatientStatusData.family_history.familyhistorylst = Extend.ToList<Family_History_Entity>(res).ToList();

            }

            Modal.PatientStatusData.Type = Submit;
            Modal.visitpatientlist = new List<Patient_Entity>();
            //}
            return PartialView("HMSPatientList", Modal);
        }
        public ActionResult DeleteVitalSurgery(int VitalId)
        {
            bool result = false;
            var res = VitMaster.DeleteVitalSurgery(VitalId);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteVitalChronic(int chronicid)
        {
            bool result = false;
            var res = VitMaster.DeleteVitalChronic(chronicid);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult HMSVisitedPatientList()
        {
            HMSPatientModel model = new HMSPatientModel();

           string fromdate   = System.DateTime.Now.ToString("dd/MM/yyyy");
            string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
            var dataSet = patMaster.GetfiltervisitedPatient( Convert.ToString(Session["ClinicCode"]), fromdate, todate,"");
            //Added By Dhaval Patel For Patient Search
            model.PatientSearch = new OtherPatientSearch();
            //End Dhaval
            model.visitpatientlist = Extend.ToList<Patient_Entity>(dataSet.Tables[0]);
            model.DoctorList = Extend.ToList<Doctor_Entity>(dataSet.Tables[1]);
            return View("HMSVisitedPatientList", model);
        }
        public ActionResult AddPatient(HMSPatientModel pat)
        {
            List<RegPatient> RP = new List<RegPatient>();
            HMSPatientModel model = new HMSPatientModel();
            //model.CountryLst = comMaster.GetAllcountry("").OrderByDescending(o => o.country_id).ToList();
            model.CountryLst = new List<Country_Entity>();
            model.StateLst = new List<State_Entity>();
            model.DistrictLst = new List<District_Entity>();
            model.CityLst = new List<City_Entity>();
            if (ModelState.IsValid)
            {
                if (pat.PatientFirstName != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var pa = DateTime.Now.ToString("yyMMddHHmmssff");
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase imgfile = files[0];
                        var folderpath = ConfigurationManager.AppSettings["addPatimgUrl"];
                        string myfile = pa + Path.GetExtension(imgfile.FileName);
                        //string fname = Path.Combine(Server.MapPath("~/Image/PatientLogo/"), imgfile.FileName.Replace(" ", string.Empty));
                        string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                        if (System.IO.File.Exists(folderpath + pat.Photo))
                        {
                            System.IO.File.Delete(folderpath + pat.Photo);
                        }
                        imgfile.SaveAs(fname);
                        ResizeSettings resizeSetting = new ResizeSettings
                        {
                            Width = 200,
                            Height = 200,
                            Format = "jpg"
                        };
                        ImageBuilder.Current.Build(fname, fname, resizeSetting);
                        pat.Photo = myfile.Replace(" ", string.Empty);//"/Image/PatientLogo/" + imgfile.FileName.Replace(" ", string.Empty);

                    }
                    pat.hospitalcode = Convert.ToString(Session["ClinicCode"]);
                    var entity = SetEntityData(pat);
                    string password = GeneratePassword();
                    entity.enc_password = patMaster.EncString(password);
                    DataTable ds = patMaster.Add(entity);

                    RP = Extend.ToList<RegPatient>(ds);
                    if (Convert.ToString(ds.Rows[0]["status"]) == "new")
                    {
                        RP[0].status = "1";
                        ModelState.Clear();

                    }
                    else if (Convert.ToString(ds.Rows[0]["status"]) == "old")
                    {

                        RP = Extend.ToList<RegPatient>(ds);

                    }
                    else
                    {
                        RP[0].status = "3";

                    }

                }
                else
                {
                    RP[0].status = "0";

                }



                ModelState.Clear();
            }
            return Json(RP, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddHMSPatient()
        {
            HMSPatientModel model = new HMSPatientModel();
            model.CountryLst = comMaster.GetAllcountry("").OrderByDescending(o => o.country_id).ToList();
            model.StateLst = new List<State_Entity>();
            model.DistrictLst = new List<District_Entity>();
            model.CityLst = new List<City_Entity>();
            return View("AddHMSPatient", model);
        }
        public ActionResult EditPatient(HMSPatientModel model)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var pa = DateTime.Now.ToString("yyMMddHHmmssff");
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase imgfile = files[0];
                    var folderpath = ConfigurationManager.AppSettings["addPatimgUrl"];
                    string myfile = pa + Path.GetExtension(imgfile.FileName);
                    string fname = folderpath + "" + myfile.Replace(" ", string.Empty);
                    if (System.IO.File.Exists(folderpath + model.Photo))
                    {
                        System.IO.File.Delete(folderpath + model.Photo);
                    }
                    imgfile.SaveAs(fname);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 200,
                        Height = 200,
                        Format = "jpg"
                    };
                    ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    model.Photo = myfile.Replace(" ", string.Empty);
                }
                var entity = SetEntityData(model);
                var res = patMaster.Update(entity);
                if (res == true)
                {
                    status = "Record Update Sucessfully!!";
                }
                else
                {
                    status = "Record Does Not Update Sucessfully!!";
                }

            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditHMSPatient()
        {
            HMSPatientModel model = new HMSPatientModel();
            var code = Convert.ToString(Session["UniqRow"]);
            //var code = Convert.ToString(Request.Path.Split('/')[3]);
            if (code != null || code != "")
            {
                var DataSet = patMaster.GetByCode(code);
                var CModel = Extend.ToList<Patient_Entity>(DataSet.Tables[0]).SingleOrDefault();
                model.CountryLst = comMaster.GetAllcountry("").OrderByDescending(o => o.country_id).ToList();
                model.BloodGroup = CModel.blood_group;
                model.PatientMotherName = CModel.patient_mothername;
                model.CityCode = CModel.city_code;
                model.CountryCode = CModel.country_code;
                model.DistrictCode = CModel.dist_code;
                model.StateCode = CModel.state_code;
                model.PatientFirstName = CModel.patient_firstname;
                model.PatientLasttName = CModel.patient_lastname;
                model.PatientFatherName = CModel.patient_fathername;
                model.PatientCode = CModel.patient_code;
                model.Age = Convert.ToString(CModel.age);
                model.Phone = CModel.phone;
                model.Email = CModel.email;
                model.ZipCode = CModel.zip;
                model.PresentAddress = CModel.present_address;
                model.Gender = CModel.gender;
                model.DateOfBirth = CModel.dob;
                model.NID_Birthregno = CModel.nid_birthregno;
                model.MaritalStatus = CModel.marital_status == "" ? "" : CModel.marital_status.Trim();
                model.Ctrl = Convert.ToInt32(CModel.Ctrl);
                model.CountryLst = Extend.ToList<Country_Entity>(DataSet.Tables[1]);
                model.StateLst = Extend.ToList<State_Entity>(DataSet.Tables[2]);
                model.DistrictLst = Extend.ToList<District_Entity>(DataSet.Tables[3]);
                model.CityLst = Extend.ToList<City_Entity>(DataSet.Tables[4]);
                model.Photo = CModel.photo;
                model.hospitalcode = CModel.hos_code;
                model.PatVerify = CModel.patient_id_type;
                model.isverify = CModel.isverify;
                model.SpouseName = CModel.spouse_name;
                model.PatientProfessions = CModel.profession;

            }
            else
            {
                //if (ModelState.IsValid)
                //{
                //    var entity = SetEntityData(pat);
                //    var res = patMaster.Update(entity);
                //    return RedirectToAction("HMSPatientList");
                //}
            }
            return View("EditHMSPatient", model);
        }
        public ActionResult HosPatientDelete(string PATCode)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var res = patMaster.Delete(Convert.ToString(PATCode), user_code, hoscode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public Patient_Entity SetEntityData(HMSPatientModel Pati)
        {
            entity.patient_firstname = Pati.PatientFirstName.Trim();
            entity.patient_lastname = Pati.PatientLasttName.Trim();
            entity.patient_fathername = Pati.PatientFatherName.Trim();
            entity.patient_code = Pati.PatientCode;
            entity.age = Convert.ToString(Pati.Age);
            entity.phone = Pati.Phone;
            entity.email = Pati.Email;
            entity.country_code = Pati.CountryCode;
            entity.city_code = Pati.CityCode;
            entity.zip = Pati.ZipCode;
            entity.present_address = Pati.PresentAddress;
            entity.dist_code = Pati.DistrictCode;
            entity.state_code = Pati.StateCode;
            entity.gender = Pati.Gender;
            entity.dob = Pati.DateOfBirth;
            entity.nid_birthregno = Pati.NID_Birthregno;
            entity.marital_status = Pati.MaritalStatus;
            entity.photo = Pati.Photo;
            entity.Ctrl = Convert.ToInt32(Pati.Ctrl);
            entity.hos_code = Pati.hospitalcode;
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.patient_id_type = Pati.PatVerify;
            entity.relation = Pati.Relation;
            entity.spouse_name = Pati.SpouseName;
            entity.profession = Pati.PatientProfessions;
            entity.patient_mothername=Pati.PatientMotherName;
            entity.blood_group = Pati.BloodGroup;
            //entity.verification_code = Dr.VerificationCode;
            return entity;
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
            return PartialView("EditHMSPatient", model);
        }
        public PartialViewResult GetPatientOrgans(int StatusNameId, string Type)
        {
            HMSPatientModel model = new HMSPatientModel();
            model.PatientStatusData = new PatientStatus();
            model.PatientStatusData.SurgeryData = new Surgery();
            model.PatientStatusData.DiseaseData = new Disease();
            model.PatientStatusData.Other_Med = new OtherMed();
            model.PatientStatusData.family_history = new FamilyHistory();
            model.PatientStatusData.ChronicMedicineData = new ChronicMedicine();
            model.PatientStatusData.SurgeryData.vitalsurgerylst = new List<ForntendVital_Entity>();
            model.PatientStatusData.DiseaseData.vitaldiseaselst = new List<ForntendVital_Entity>();
            model.PatientStatusData.ChronicMedicineData.vitalchroniclst = new List<ForntendchronicVital_Entity>();
            model.PatientStatusData.OrgansList = new List<patient_status_Organs>();
            if (Type == "surgery")
            {
                model.PatientStatusData.OrgansList = VitMaster.GetPatientOrgans(StatusNameId).OrderByDescending(o => o.status_organ_name).ToList();
            }
            else if (Type == "disease")
            {
                model.PatientStatusData.OrgansList = VitMaster.GetPatientOrgans(StatusNameId).OrderByDescending(o => o.status_organ_name).ToList();
            }

            return PartialView("HMSPatientList", model);
        }

        [HttpPost]
        public JsonResult GetSearchMedicine(string Prefix, string formulation_code, string MedIds,string BrandCodess)
        {
            // WriteJason();

            //var entity = aptMaster.GetAllMedicine(Prefix, AptCode, foumulaId);
            if (MedIds != "")
                MedIds = MedIds.Substring(0, MedIds.Length - 1);
            if (BrandCodess != "")
                BrandCodess = BrandCodess.Substring(0, BrandCodess.Length - 1);
            List<Medicine_Entity> entity = new List<Medicine_Entity>();
            string data = ConfigurationManager.AppSettings["JsonFile"];
            using (StreamReader sr = new StreamReader(data + "/MedecineAllData.json"))
            {
                entity = JsonConvert.DeserializeObject<List<Medicine_Entity>>(sr.ReadToEnd());
            }
            var MedList = from p in entity.FindAll(a => a.formulation_code == formulation_code).
                        FindAll(b => b.drug_generic_name.ToLower().Contains(Prefix.ToLower()) || b.Brand_Name.ToLower().Contains(Prefix.ToLower())).ToList()
                          where !BrandCodess.Split(',').Contains(p.brand_code)
                          select p;
            return Json(MedList, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetfilterVisitedPatient(string fromdate, string todate, string hosdoccode)
        {
            HMSPatientModel model = new HMSPatientModel();
            var dataSet = patMaster.GetfiltervisitedPatient(Convert.ToString(Session["ClinicCode"]), fromdate, todate, hosdoccode);
            model.visitpatientlist = Extend.ToList<Patient_Entity>(dataSet.Tables[0]);
            model.DoctorList = new List<Doctor_Entity>();
            return PartialView("HMSVisitedPatientList", model);
        }

        public ActionResult VisitedPatientProfile()
        {
            HMSPatientModel model = new HMSPatientModel();
            var uniqcode = Request.Path.Split('/')[3];
            var dataSet = patMaster.GetvisitedPatientprofile(Convert.ToString(Session["ClinicCode"]), uniqcode);
            model.ip_history = new List<iphistory>();
            model.inv_history = new List<invhistory>();
            model.visitpatientlist = Extend.ToList<Patient_Entity>(dataSet.Tables[0]);
            model.opd_history = Extend.ToList<opdhistory>(dataSet.Tables[1]);
            model.uniqrow = Convert.ToString(dataSet.Tables[0].Rows[0]["uniqrow"]);
            return View("VisitedPatientProfile", model);
        }

        public PartialViewResult GetIPHistory(string PatientCode, string Type, string uniqrow)
        {
            HMSPatientModel model = new HMSPatientModel();

            var dataSet = patMaster.GetIPHistory(Convert.ToString(Session["ClinicCode"]), PatientCode, Type);
            model.visitpatientlist = new List<Patient_Entity>();
            model.opd_history = new List<opdhistory>();
            model.ip_history = new List<iphistory>();
            model.inv_history = new List<invhistory>();
            model.uniqrow = uniqrow;
            if (Type.ToString().Trim() == "ip_history")
            {
                model.ip_history = Extend.ToList<iphistory>(dataSet.Tables[0]);
            }
            if (Type.ToString().Trim() == "inv_history")
            {
                model.inv_history = Extend.ToList<invhistory>(dataSet.Tables[0]);
            }

            return PartialView("VisitedPatientProfile", model);
        }

        [HttpPost]
        public ActionResult ViewInvestigationPara(int service_id, int investigation_id, string Appointmentcode, string unique_invstigation_id, string InvType)
        {
            HMSPatientModel model = new HMSPatientModel();
            model.invSubgroupList = new List<SubGroupPatient>();
            //model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            //model.investigationPriceList = new List<investigationparaamount>();
            var data = TestMaster.GetAllInvetigationPara(service_id, investigation_id, Appointmentcode, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType,0);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.appointment_code = Appointmentcode;

            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.invtype = InvType;
                var SubLst = new HosInvestigationParapatient();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationParapatient
                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                    };
                }
                var list = new SubGroupPatient();
                list.invParaList = new List<HosInvestigationParapatient>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_ViewInvestigationRediologyModel", model);
            }
            //End
            else
            {
                //var pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[2]);
                var pararesult = new List<HosInvestigationParapatient>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationParapatient
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
                        var list = new SubGroupPatient
                        {
                            SubgroupName = Convert.ToString(item[1]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[0]))
                        };
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroupPatient
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == investigation_id)
                    };
                    model.invSubgroupList.Add(list);
                }
            }
            return PartialView("_ViewInvestigationModel", model);
        }

        [HttpPost]
        public ActionResult ViewInvestigationPara_op(int service_id, int investigation_id, string invoice_code, string unique_invstigation_id, string InvType)
        {
            HMSPatientModel model = new HMSPatientModel();
            model.invSubgroupList = new List<SubGroupPatient>();
            var data = TestMaster.GetAllInvetigationPara_OP(service_id, investigation_id, invoice_code, unique_invstigation_id, Convert.ToString(Session["ClinicCode"]), InvType,0);
            model.investigationname = Convert.ToString(data.Tables[0].Rows[0]["InvestigationName"]);
            model.invoice_code = invoice_code;
            model.service_id = service_id;
            //Added By Dhaval
            if (InvType == "radiology")
            {
                model.invtype = InvType;
                var SubLst = new HosInvestigationParapatient();
                if (data.Tables[1].Rows.Count > 0)
                {
                    SubLst = new HosInvestigationParapatient
                    {
                        investigationsubgroup_name = Convert.ToString(data.Tables[1].Rows[0]["investigationsubgroup_name"]),
                    };
                }
                var list = new SubGroupPatient();
                list.invParaList = new List<HosInvestigationParapatient>();
                list.invParaList.Add(SubLst);
                model.invSubgroupList.Add(list);
                return PartialView("_ViewInvestigationRediologyModel", model);
            }
            //End
            else
            {
                //var pararesult = Extend.ToList<HosInvestigationPara>(data.Tables[2]);
                var pararesult = new List<HosInvestigationParapatient>();
                for (int i = 0; i < data.Tables[2].Rows.Count; i++)
                {
                    var p_lst = new HosInvestigationParapatient
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
                        var list = new SubGroupPatient
                        {
                            SubgroupName = Convert.ToString(item[1]),
                            invParaList = pararesult.FindAll(a => a.investigationsubgrop_id == Convert.ToInt32(item[0]))
                        };
                        model.invSubgroupList.Add(list);
                    }
                }
                else
                {
                    var list = new SubGroupPatient
                    {
                        SubgroupName = "",
                        invParaList = pararesult.FindAll(a => a.investigationmaster_id == investigation_id)
                    };
                    model.invSubgroupList.Add(list);
                }
            }
            return PartialView("_ViewInvestigationModel", model);
        }

        public string GeneratePassword()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(6); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }
        public ActionResult VisitedPatientHistory()
        {
            VisitedPatientHistory model = new VisitedPatientHistory();
                model.fromdate = System.DateTime.Now.ToString("dd/MM/yyyy");
                model.todate = System.DateTime.Now.ToString("dd/MM/yyyy");
          
            model.todate = System.DateTime.Now.ToString("dd/MM/yyyy");
            var dataSet = patMaster.GetfiltervisitedPatienthistory(Convert.ToString(Session["ClinicCode"]), model.fromdate, model.todate, "direct");
            model.totaldirectbilling = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totaldirectbilling"]);
            model.totalopdpatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalopdpatient"]);
            model.totalippatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalippatient"]);
            model.directilling_lst = Extend.ToList<Patient_Entity>(dataSet.Tables[1]);
            model.ip_patient_lst = new List<Patient_Entity>();
            model.opd_patient_lst = new List<Patient_Entity>();
            return View("VisitedPatientHistory",model);
        }

        public ActionResult FindVisitedHistory(VisitedPatientHistory VP)
        {
            VisitedPatientHistory model = new VisitedPatientHistory();
            if (string.IsNullOrEmpty(VP.fromdate))
            {
                model.fromdate = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                model.fromdate = VP.fromdate;
            }

            if (string.IsNullOrEmpty(VP.todate))
            {
                model.todate = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                model.todate = VP.todate;
            }
            
            var dataSet = patMaster.GetfiltervisitedPatienthistory(Convert.ToString(Session["ClinicCode"]), model.fromdate, model.todate, "direct");
            model.totaldirectbilling = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totaldirectbilling"]);
            model.totalopdpatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalopdpatient"]);
            model.totalippatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalippatient"]);
            model.directilling_lst = Extend.ToList<Patient_Entity>(dataSet.Tables[1]);
            model.ip_patient_lst = new List<Patient_Entity>();
            model.opd_patient_lst = new List<Patient_Entity>();
            return PartialView("VisitedPatientHistory", model);
        }

        public ActionResult Filter_visitedpatienthistory(string fromdate, string todate,string type)
        {
            VisitedPatientHistory model = new VisitedPatientHistory();
            model.fromdate = System.DateTime.Now.ToString("dd/MM/yyyy");
            model.todate = System.DateTime.Now.ToString("dd/MM/yyyy");

            var dataSet = patMaster.GetfiltervisitedPatienthistory(Convert.ToString(Session["ClinicCode"]), fromdate, todate, type);
            model.totaldirectbilling = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totaldirectbilling"]);
            model.totalopdpatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalopdpatient"]);
            model.totalippatient = Convert.ToInt32(dataSet.Tables[0].Rows[0]["totalippatient"]);
            if (type == "ip")
            {
                model.directilling_lst = new List<Patient_Entity>();
                model.ip_patient_lst = Extend.ToList<Patient_Entity>(dataSet.Tables[1]);
                model.opd_patient_lst = new List<Patient_Entity>();
            }
            if (type == "opd")
            {
                model.directilling_lst = new List<Patient_Entity>();
                model.ip_patient_lst = new List<Patient_Entity>();
                model.opd_patient_lst = Extend.ToList<Patient_Entity>(dataSet.Tables[1]);
            }

            return  PartialView("VisitedPatientHistory", model);
        
        }
    }
}