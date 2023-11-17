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
using System.Text;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using SelectPdf;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSDoctorAppointmentController : _Base
    {
        // GET: HMSDoctorAppointment
        readonly DocAppointment_Master DocApp;
        readonly DocAppintment_Entity entity;
        readonly Common_Master comMaster;
        public HMSDoctorAppointmentController()
        {
            DocApp = new DocAppointment_Master(bsInfo);
            //comMaster = new Common_Master(bsInfo);
            entity = new DocAppintment_Entity();
            comMaster = new Common_Master(bsInfo);
        }

        [HttpPost]
        public JsonResult GatePatientCode(string Prefix)
        {
            //Note : you can bind same list from database  
            var dt = DocApp.GetAllPatient(Prefix);
            return Json(CustomHelper.DataTableToJSONWithJavaScriptSerializer(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Checkpatient(string patient_code)
        {
            //Note : you can bind same list from database  
            bool check = DocApp.Checkpatient(patient_code, Convert.ToString(Session["ClinicCode"]));
            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HMSDoctorAppointmentList()
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            var dataSet = DocApp.GetAllAppoint(Convert.ToString(Session["ClinicCode"]));
            Model.Doctorappointlist = Extend.ToList<DocAppintment_Entity>(dataSet.Tables[0]);
            Model.DoctorList = Extend.ToList<Doctor_Entity>(dataSet.Tables[1]);
            return View("HMSDoctorAppointmentList", Model);
        }
        public ActionResult AddHMSDoctorAppointment()
        {
            AppointmentModel model = new AppointmentModel();
            model.RegPatAppointment = new HMSDocAppointModel();
            var DataSet = DocApp.GetAlldropdownlist(Convert.ToString(Session["ClinicCode"]));
            model.PatientList = new List<Patient_Entity>();
            model.DoctorList = new List<Doctor_Entity>();
            model.DoctorTimeSlotList = new List<DoctorTimeSlot>();
            model.HosDepartmentList = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[0]);
            model.CountryLst = Extend.ToList<Country_Entity>(DataSet.Tables[1]);
            model.Comission_Agent_List = Extend.ToList<ComissionAgent_Entity>(DataSet.Tables[2]);
            model.StateLst = new List<State_Entity>();
            model.DistrictLst = new List<District_Entity>();
            model.CityLst = new List<City_Entity>();
            model.RegPatAppointment.Ctrl = 1;
            return View("AddHMSDoctorAppointment", model);
        }

        public PartialViewResult GetDatabyCode_data(string code, string Type)
        {
            AppointmentModel model = new AppointmentModel();
            model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            model.Doctorappointlist = new List<DocAppintment_Entity>();
            model.DoctorList = new List<Doctor_Entity>();
            model.DoctorTimeSlotList = new List<DoctorTimeSlot>();
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
            return PartialView("AddHMSDoctorAppointment", model);
        }

        public ActionResult AddWalkingDoctorAppointment()
        {
            WalkingAppointmodel Model = new WalkingAppointmodel();
            Model.RegPatwalkingAppointment = new HMSDocAppointWalkModel();
            var DataSet = DocApp.GetAlldropdownlist(Convert.ToString(Session["ClinicCode"]));
            Model.HosDepartmentList = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[0]);
            Model.DoctorList = new List<Doctor_Entity>();
            Model.CountryLst = Extend.ToList<Country_Entity>(DataSet.Tables[1]);
            Model.Comission_Agent_List = Extend.ToList<ComissionAgent_Entity>(DataSet.Tables[2]);
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            //Model.RegPatwalkingAppointment.Ctrl = 1;
            return View("AddWalkingDoctorAppointment", Model);
        }

        public PartialViewResult GetDatabyCode_data_waking(string code, string Type)
        {
            WalkingAppointmodel model = new WalkingAppointmodel();
            model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            model.DoctorList = new List<Doctor_Entity>();
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
            return PartialView("AddWalkingDoctorAppointment", model);
        }

        public ActionResult AddWalking(WalkingAppointmodel Appoint)
        {
            string status = "";
            Appoint.RegPatwalkingAppointment.HosCode = Convert.ToString(Session["ClinicCode"]);
            if (ModelState.IsValid)
            {
                if (Appoint.RegPatwalkingAppointment.patient_code.Length >= 15)
                {


                    var entity = SetEntityDatawalking(Appoint.RegPatwalkingAppointment);
                    var res = DocApp.Add(entity);
                    if (res != "same")
                    {
                        if (res.ToInt() > 0 ? true : false)
                        {
                            status = "Record Added Successfully!";
                            ModelState.Clear();
                        }
                        else
                            status = "Record Not Saved!";
                    }
                    else
                        status = "Record Allrady Exists!";
                }
                else
                    status = "Please Enter Valid Patient";
            }
            else
                status = "Record Not Saved!";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add_Non_reg_pat_Walking(WalkingAppointmodel Appoint)
        {
            List<RegPatient> RP = new List<RegPatient>();
            Appoint.NonregPatWalkAppoint.HosCode = Convert.ToString(Session["ClinicCode"]);
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
                    if (System.IO.File.Exists(folderpath + Appoint.NonregPatWalkAppoint.Photo))
                    {
                        System.IO.File.Delete(folderpath + Appoint.NonregPatWalkAppoint.Photo);
                    }
                    imgfile.SaveAs(fname);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 200,
                        Height = 200,
                        Format = "jpg"
                    };
                    ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    Appoint.NonregPatWalkAppoint.Photo = myfile.Replace(" ", string.Empty);
                }


                var entity = SetEntityData_non_reg_walking(Appoint.NonregPatWalkAppoint);
                DataTable res = DocApp.AddNonRegPatwalking(entity);
                RP = Extend.ToList<RegPatient>(res);
                if (Convert.ToString(res.Rows[0]["status"]) == "new")
                {
                    RP[0].status = "1";
                    //status[1] = "Record Added Successfully!";
                    ModelState.Clear();

                }
                else if (Convert.ToString(res.Rows[0]["status"]) == "old")
                {
                    //RP[0].status = "2";
                    // status[1] = "Record Not Saved patient Already Registered! Patient is " + res.Rows[0]["name"] + "";
                    RP = Extend.ToList<RegPatient>(res);

                }
                else
                {
                    RP[0].status = "3";
                    //status[1] = "Record Allrady Exists!";
                }

            }
            else
            {
                RP[0].status = "0";
                // status[1] = "Record Not Saved!";
            }
            return Json(RP, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddAppointment(AppointmentModel Appoint)
        {
            string status = "";
            Appoint.RegPatAppointment.HosCode = Convert.ToString(Session["ClinicCode"]);
            if (ModelState.IsValid)
            {
                if (Appoint.RegPatAppointment.patient_code.Length >= 15)
                {
                    var entity = SetEntityData(Appoint.RegPatAppointment);
                    var res = DocApp.Add(entity);
                    if (res != "same")
                    {
                        if (res.ToInt() > 0 ? true : false)
                        {
                            status = "Record Added Successfully!";
                            ModelState.Clear();
                        }
                        else
                            status = "Record Not Saved!";
                    }
                    else
                        status = "Record Allrady Exists!";
                }
                else
                    status = "Please Enter Valid Patient";
            }
            else
                status = "Record Not Saved!";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add_Non_reg_pat_Appointment(AppointmentModel NonAppoint)
        {
            //string[] status = new string[2];
            List<RegPatient> RP = new List<RegPatient>();
            NonAppoint.NonRegPatAppointment.HosCode = Convert.ToString(Session["ClinicCode"]);
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
                    if (System.IO.File.Exists(folderpath + NonAppoint.NonRegPatAppointment.Photo))
                    {
                        System.IO.File.Delete(folderpath + NonAppoint.NonRegPatAppointment.Photo);
                    }
                    imgfile.SaveAs(fname);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 200,
                        Height = 200,
                        Format = "jpg"
                    };
                    ImageBuilder.Current.Build(fname, fname, resizeSetting);
                    NonAppoint.NonRegPatAppointment.Photo = myfile.Replace(" ", string.Empty);
                }

                var entity = SetEntityDataNonReg(NonAppoint.NonRegPatAppointment);
                DataTable res = DocApp.AddNonRegPat(entity);
                RP = Extend.ToList<RegPatient>(res);
                if (Convert.ToString(res.Rows[0]["status"]) == "new")
                {
                    RP[0].status = "1";
                    //status[1] = "Record Added Successfully!";
                    ModelState.Clear();

                }
                else if (Convert.ToString(res.Rows[0]["status"]) == "old")
                {
                    //RP[0].status = "2";
                    // status[1] = "Record Not Saved patient Already Registered! Patient is " + res.Rows[0]["name"] + "";
                    RP = Extend.ToList<RegPatient>(res);

                }
                else
                {
                    RP[0].status = "3";
                    //status[1] = "Record Allrady Exists!";
                }

            }
            else
            {
                RP[0].status = "0";
                // status[1] = "Record Not Saved!";
            }

            return Json(RP, JsonRequestBehavior.AllowGet);
        }
        public DocAppintment_Entity SetEntityDataNonReg(HMSDocNonRegpatAppointModel Appoin)
        {
            //entity.appointment_code = Appoin.AppointmentCode;
            entity.hos_doccode = Appoin.HosDocCode;
            entity.hos_depcode = Appoin.HosDepCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.appointment_date = Appoin.AppointmentDate;
            entity.appointmentfrom_time = Appoin.AppointmentFromTime;
            entity.ctrl = Convert.ToInt32(1);
            entity.is_appointment = Appoin.IsAppointment;
            entity.appointment_type = Convert.ToString(Appoin.AppointmentType);
            entity.pateint_firstname = Appoin.PatientFirstName;
            entity.patient_lastename = Appoin.PatientLastName;
            entity.patient_fathername = Appoin.PatientFatherName;
            entity.patient_mothername = Appoin.PatientMotherName;
            entity.patient_id_type = Appoin.PatVerify;
            entity.nid_birthregno = Appoin.NID_Birthregno;
            entity.blood_group = Appoin.BloodGroup;
            entity.is_non_register_patient = Convert.ToInt32(1);
            entity.patient_dob = Convert.ToString(Appoin.PatientDOB);
            entity.patient_age = Convert.ToString(Appoin.PatientAge);
            entity.patient_email = Convert.ToString(Appoin.PatientEmail);
            entity.pateint_mobile = Convert.ToString(Appoin.PatientMobile);
            entity.gender = Appoin.Gender;
            entity.marital_status = Appoin.MaritalStatus;
            entity.patient_address = Appoin.PresentAddress;
            entity.country_code = Appoin.CountryCode;
            entity.state_code = Appoin.StateCode;
            entity.dist_code = Appoin.DistrictCode;
            entity.city_code = Appoin.CityCode;
            entity.photo = Appoin.Photo;
            entity.relation = Appoin.Relation;
            entity.patient_spousename = Appoin.SpouseName;
            entity.patient_professions = Appoin.PatientProfessions;
            entity.zip = Appoin.ZipCode;
            entity.comission_agent_id = Convert.ToInt32(Appoin.Comission_Agent_Id);
            return entity;
        }

        public DocAppintment_Entity SetEntityDatawalking(HMSDocAppointWalkModel Appoin)
        {
            entity.appointment_code = Appoin.AppointmentCode;
            entity.patient_code = Appoin.patient_code;
            entity.hos_doccode = Appoin.HosDocCode;
            entity.hos_depcode = Appoin.HosDepCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.appointment_date = Appoin.AppointmentDate;
            entity.appointmentfrom_time = Convert.ToString(DateTime.Now.ToString("HH:mm tt"));
            entity.ctrl = Convert.ToInt32(Appoin.Ctrl);
            entity.is_appointment = Appoin.IsAppointment;
            entity.appointment_type = "Walking";
            entity.comission_agent_id = Convert.ToInt32(Appoin.Comission_Agent_Id);
            return entity;
        }

        public DocAppintment_Entity SetEntityData_non_reg_walking(HMSDocNonregPatWalkAppointModel Appoin)
        {
            entity.appointment_code = Appoin.AppointmentCode;
            entity.hos_doccode = Appoin.HosDocCode;
            entity.hos_depcode = Appoin.HosDepCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.appointment_date = Appoin.AppointmentDate;
            entity.appointmentfrom_time = Convert.ToString(DateTime.Now.ToString("HH:mm tt"));
            entity.ctrl = Convert.ToInt32(Appoin.Ctrl);
            entity.is_appointment = Appoin.IsAppointment;
            entity.appointment_type = "Walking";
            entity.pateint_firstname = Appoin.PatientFirstName;
            entity.patient_lastename = Appoin.PatientLastName;
            entity.patient_fathername = Appoin.PatientFatherName;
            entity.patient_mothername = Appoin.PatientMotherName;
            entity.patient_id_type = Appoin.PatVerify;
            entity.nid_birthregno = Appoin.NID_Birthregno;
            entity.blood_group = Appoin.BloodGroup;
            entity.is_non_register_patient = Convert.ToInt32(1);
            entity.patient_dob = Convert.ToString(Appoin.PatientDOB);
            entity.patient_age = Convert.ToString(Appoin.PatientAge);
            entity.patient_email = Convert.ToString(Appoin.PatientEmail);
            entity.pateint_mobile = Convert.ToString(Appoin.PatientMobile);
            entity.gender = Appoin.Gender;
            entity.marital_status = Appoin.MaritalStatus;
            entity.patient_address = Appoin.PresentAddress;
            entity.country_code = Appoin.CountryCode;
            entity.state_code = Appoin.StateCode;
            entity.dist_code = Appoin.DistrictCode;
            entity.city_code = Appoin.CityCode;
            entity.photo = Appoin.Photo;
            entity.relation = Appoin.Relation;
            entity.patient_spousename = Appoin.SpouseName;
            entity.patient_professions = Appoin.PatientProfessions;
            entity.zip = Appoin.ZipCode;
            entity.comission_agent_id = Convert.ToInt32(Appoin.Comission_Agent_Id);
            return entity;
        }

        public DocAppintment_Entity SetEntityData(HMSDocAppointModel Appoin)
        {
            entity.appointment_code = Appoin.AppointmentCode;
            entity.patient_code = Appoin.patient_code;
            entity.hos_doccode = Appoin.HosDocCode;
            entity.hos_depcode = Appoin.HosDepCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.appointment_date = Appoin.AppointmentDate;
            entity.appointmentfrom_time = Appoin.AppointmentFromTime;
            entity.ctrl = Convert.ToInt32(Appoin.Ctrl);
            entity.is_appointment = Appoin.IsAppointment;
            entity.appointment_type = Convert.ToString(Appoin.AppointmentType);
            entity.cancelreason = Convert.ToString(Appoin.cancelreason);
            entity.comission_agent_id = Convert.ToInt32(Appoin.Comission_Agent_Id);
            return entity;
        }
        public ActionResult EditHMSDoctorAppointment()
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            var AppointmentCode = Convert.ToString(Session["UniqRow"]);
            if (AppointmentCode != null || AppointmentCode != "")
            {
                var HosCode = Convert.ToString(Session["ClinicCode"]);
                var DataSet = DocApp.GetByCode(AppointmentCode, HosCode);
                var CModel = Extend.ToList<DocAppintment_Entity>(DataSet.Tables[0]).SingleOrDefault();
                Model.DoctorList = Extend.ToList<Doctor_Entity>(DataSet.Tables[1]);
                Model.HosDepartmentList = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[2]);
                Model.Comission_Agent_List = Extend.ToList<ComissionAgent_Entity>(DataSet.Tables[3]);
                Model.PatientList = new List<Patient_Entity>();
                Model.DoctorTimeSlotList = new List<DoctorTimeSlot>();
                Model.RegPatAppointment.AppointmentCode = Convert.ToString(CModel.appointment_code);
                Model.RegPatAppointment.patient_code = CModel.patient_code;
                Model.RegPatAppointment.PatientName = CModel.patient_name;
                Model.RegPatAppointment.HosDepCode = CModel.hos_depcode;
                Model.RegPatAppointment.HosDocCode = CModel.hos_doccode;
                Model.RegPatAppointment.AppointmentDate = CModel.appointment_date;
                Model.RegPatAppointment.AppointmentFromTime = CModel.appointmentfrom_time;
                Model.RegPatAppointment.Ctrl = Convert.ToInt32(CModel.ctrl);
                Model.RegPatAppointment.IsAppointment = CModel.is_appointment;
                Model.RegPatAppointment.AppointmentType = Convert.ToString(CModel.appointment_type);
                Model.RegPatAppointment.Comission_Agent_Id = Convert.ToString(CModel.comission_agent_id);
            }
            return View("EditHMSDoctorAppointment", Model);
        }
        public ActionResult EditAppointment(AppointmentModel model)
        {
            string status = "";

            if ((model.RegPatAppointment.cancelreason == null || model.RegPatAppointment.cancelreason == "") && model.RegPatAppointment.Ctrl == 0)
            {
                status = "Please Enter Cancellation Reason";
            }
            else
            {
                var entity = SetEntityData(model.RegPatAppointment);
                var res = DocApp.Update(entity);
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

        public PartialViewResult GetDatabyCode(string code)
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            Model.DoctorList = DocApp.GetByCodeDoctor(Convert.ToString(Session["ClinicCode"]), code).ToList();
            Model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            Model.DoctorTimeSlotList = new List<DoctorTimeSlot>();
            Model.CountryLst = new List<Country_Entity>();
            Model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            return PartialView("AddHMSDoctorAppointment", Model);
        }

        public PartialViewResult GetDatabyCodeWalking(string code)
        {
            WalkingAppointmodel Model = new WalkingAppointmodel();
            Model.DoctorList = DocApp.GetByCodeDoctor(Convert.ToString(Session["ClinicCode"]), code).ToList();
            Model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            Model.CountryLst = new List<Country_Entity>();
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            Model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            return PartialView("AddWalkingDoctorAppointment", Model);
        }

        public PartialViewResult GetDocDatabyCode(string code, int day)
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            string Days = "";
            if (day == 0) { Days = "Sunday"; }
            else if (day == 1) { Days = "Monday"; }
            else if (day == 2) { Days = "Tuesday"; }
            else if (day == 3) { Days = "Wednesday"; }
            else if (day == 4) { Days = "Thursday"; }
            else if (day == 5) { Days = "Friday"; }
            else if (day == 6) { Days = "Saturday"; }
            Model.DoctorList = DocApp.GetDocByCodeDoctor(Convert.ToString(Session["ClinicCode"]), code, Days).ToList();
            Model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            Model.DoctorTimeSlotList = new List<DoctorTimeSlot>();
            Model.CountryLst = new List<Country_Entity>();
            Model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            return PartialView("AddHMSDoctorAppointment", Model);
        }

        public JsonResult GateDoctorDays(string code)
        {
            var date = new List<LeaveDate>();
            var days = new List<AvailabalDays>();
            var datset = DocApp.GetDoctorAvailabledays(Convert.ToString(Session["ClinicCode"]), code);
            days = (from DataRow dr in datset.Tables[0].Rows
                    select new AvailabalDays()
                    {
                        daynumber = Convert.ToInt32(dr["daynumber"]),
                    }).ToList();
            if (datset.Tables.Count > 1)
            {
                date = (from DataRow dr in datset.Tables[1].Rows
                        select new LeaveDate()
                        {
                            fromdate = Convert.ToString(dr["fromdate"]),
                            todate = Convert.ToString(dr["todate"]),
                        }).ToList();
            }
            avalday avldy = new avalday();
            avldy.days = days;
            avldy.date = date;
            return Json(avldy, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetDoctorTimeSlot(string Doccode, int day, string AppointDate, string time, string AppointCode)
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            string Days = "";
            if (day == 0) { Days = "Sunday"; }
            else if (day == 1) { Days = "Monday"; }
            else if (day == 2) { Days = "Tuesday"; }
            else if (day == 3) { Days = "Wednesday"; }
            else if (day == 4) { Days = "Thursday"; }
            else if (day == 5) { Days = "Friday"; }
            else if (day == 6) { Days = "Saturday"; }

            Model.DoctorTimeSlotList = DocApp.GetDoctorTimeSlot(Convert.ToString(Session["ClinicCode"]), Doccode, Days, AppointDate, time, AppointCode).ToList();
            Model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.CountryLst = new List<Country_Entity>();
            Model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            return PartialView("AddHMSDoctorAppointment", Model);
        }

        public PartialViewResult GetEditDoctorTimeSlot(string Doccode, int day, string AppointDate, string time, string AppointCode)
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            string Days = "";
            if (day == 0) { Days = "Sunday"; }
            else if (day == 1) { Days = "Monday"; }
            else if (day == 2) { Days = "Tuesday"; }
            else if (day == 3) { Days = "Wednesday"; }
            else if (day == 4) { Days = "Thursday"; }
            else if (day == 5) { Days = "Friday"; }
            else if (day == 6) { Days = "Saturday"; }
            Model.DoctorTimeSlotList = DocApp.GetDoctorTimeSlot(Convert.ToString(Session["ClinicCode"]), Doccode, Days, AppointDate, time, AppointCode).ToList();
            Model.HosDepartmentList = new List<HospitalDepartment_Entity>();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.CountryLst = new List<Country_Entity>();
            Model.Comission_Agent_List = new List<ComissionAgent_Entity>();
            Model.StateLst = new List<State_Entity>();
            Model.DistrictLst = new List<District_Entity>();
            Model.CityLst = new List<City_Entity>();
            return PartialView("EditHMSDoctorAppointment", Model);
        }

        public ActionResult DeleteAppointment(int AppointmentId)
        {
            bool result = false;

            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = DocApp.Delete(AppointmentId, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterAppointment(int AppointmentId)
        {
            bool result = false;


            var usercode = Convert.ToString(Session["UserCode"]);
            var res = DocApp.Register(AppointmentId, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetfilterAppointment(DateTime? fromdate, DateTime? todate, string hosdoccode, string appointment)
        {
            AppointmentModel Model = new AppointmentModel();
            Model.RegPatAppointment = new HMSDocAppointModel();
            var dataSet = DocApp.GetAllAppointNew(Convert.ToString(Session["ClinicCode"]), fromdate, todate, hosdoccode, appointment);
            Model.Doctorappointlist = Extend.ToList<DocAppintment_Entity>(dataSet.Tables[0]);
            Model.DoctorList = new List<Doctor_Entity>();
            return PartialView("HMSDoctorAppointmentList", Model);
        }

        public ActionResult PatientAppointmentList(AppointModel AM)
        {
            AppointModel AP = new AppointModel();
            DataSet rs = new DataSet();
            AP.Patient_AppList = new List<AppointList>();

            

            if (string.IsNullOrEmpty(AM.FromDate))
            {
                AM.FromDate = System.DateTime.Now.ToString("dd/MM/yyyy");
            }

            if (string.IsNullOrEmpty(AM.ToDate))
            {
                AM.ToDate = System.DateTime.Now.ToString("dd/MM/yyyy");
            }

            if (!string.IsNullOrEmpty(AM.PatientCode))
            {
                AM.FromDate = null;
                AM.ToDate = null;
            }

            rs = DocApp.GetRegPatientAppoinment(Convert.ToString(Session["ClinicCode"]), AM.FromDate, AM.ToDate, AM.PatientCode);
            DataRow[] dr = rs.Tables[0].Select();
            foreach (var item in dr)
            {
                var data = new AppointList
                {
                    UniqRow = Convert.ToString(item.ItemArray[0]),
                    HospitalCode = Convert.ToString(item.ItemArray[1]),
                    AppoinmentCode = Convert.ToString(item.ItemArray[2]),
                    PatientCode = Convert.ToString(item.ItemArray[3]),
                    PatientName = Convert.ToString(item.ItemArray[4]),
                    //PatientAddress = Convert.ToString(item.ItemArray[5]),
                    Mobile = Convert.ToString(item.ItemArray[5]),
                    Age = Convert.ToString(item.ItemArray[6]),
                    AppointmentTime = Convert.ToString(item.ItemArray[7]),
                    AppointDate = Convert.ToString(item.ItemArray[8]),
                    IsVisited = Convert.ToInt16(item.ItemArray[9]),
                    Gender = Convert.ToString(item.ItemArray[10]),
                    Profile = Convert.ToString(item.ItemArray[11]),
                    AppointDatetime = Convert.ToDateTime(item.ItemArray[12]),
                    patuniqrow = Convert.ToString(item.ItemArray[13]),
                    DoctorName = Convert.ToString(item.ItemArray[14]),
                    DepartmentName = Convert.ToString(item.ItemArray[15])
                };
                AP.Patient_AppList.Add(data);
              
            }

            AP.FromDate = AM.FromDate;
            AP.ToDate = AM.ToDate;
            AP.PatientName = AM.PatientName;

            return View("PatientAppointmentList", AP);
        }

        public ActionResult PendingDoctorConfirmation()
        {
            AppointModel APE = new AppointModel();
            DataSet rs = new DataSet();
            APE.Patient_AppList = new List<AppointList>();
            rs = DocApp.GetPendingDoctorConfirmation(Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = rs.Tables[0].Select();
            foreach (var item in dr)
            {
                var data = new AppointList
                {
                    UniqRow = Convert.ToString(item.ItemArray[0]),
                    HospitalCode = Convert.ToString(item.ItemArray[1]),
                    AppoinmentCode = Convert.ToString(item.ItemArray[2]),
                    PatientCode = Convert.ToString(item.ItemArray[3]),
                    PatientName = Convert.ToString(item.ItemArray[4]),
                    //PatientAddress = Convert.ToString(item.ItemArray[5]),
                    Mobile = Convert.ToString(item.ItemArray[5]),
                    Age = Convert.ToString(item.ItemArray[6]),
                    AppointmentTime = Convert.ToString(item.ItemArray[7]),
                    AppointDate = Convert.ToString(item.ItemArray[8]),
                    IsVisited = Convert.ToInt16(item.ItemArray[9]),
                    Gender = Convert.ToString(item.ItemArray[10]),
                    Profile = Convert.ToString(item.ItemArray[11]),
                    AppointDatetime = Convert.ToDateTime(item.ItemArray[12]),
                    status = Convert.ToString(item.ItemArray[13]),
                    paymenttype = Convert.ToString(item.ItemArray[14]),
                };
                APE.Patient_AppList.Add(data);
            }
            return View("PendingDoctorConfirmation", APE);
        }
        //public string Print_Presc(string ApptCode)
        //{

        //    string json = getprec_request("html", ApptCode);
        //    string response = CallApi(json);
        //    return response;

        //}

        public string getprec_request(string type, string AptCode)
        {

            string jsonreq = "{\"APIName\":\"GetPrescription\",\"Payload\":{\"AppointCode\":\"" + AptCode + "\",\"type\":\"" + type + "\"},\"TokenData\":{\"Token\":\"" + Convert.ToString(ConfigurationManager.AppSettings["ApiToken"]) + "\"}}";
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

        public JsonResult Print_Presc(string ApptCode)
        {
            string url = "/HMSDoctorAppointment/DownloadPdf?" + new QueryStringModule().Encrypt(ApptCode);
            string json = getprec_request("html", ApptCode);
            string response = CallApi(json);
            PdfClass PdfCls = JsonConvert.DeserializeObject<PdfClass>(response);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + ".html", PdfCls.BodyHtml.m_StringValue.ToString());
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "_Footer.html", PdfCls.FooterHtml.ToString());
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DownloadPdf(string enc)
        {
            string ApptCode = new QueryStringModule().Decrypt(enc);
            var encoding = new System.Text.UTF8Encoding();
            string BodyURL = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + ".html";
            string FooterURL = AppDomain.CurrentDomain.BaseDirectory + "ManagePdf\\" + ApptCode + "_Footer.html";
            var htm = System.IO.File.ReadAllText(Server.MapPath("/ManagePdf/") + ApptCode + ".html", encoding);
            var FooterHtml = System.IO.File.ReadAllText(Server.MapPath("/ManagePdf/") + ApptCode + "_Footer.html", encoding);
            //Generate Pdf
            //HtmlToPdfConverter htmltopdf = new HtmlToPdfConverter();
            //htmltopdf.Orientation = PageOrientation.Default;
            //htmltopdf.Size = PageSize.Default;
            //if (FooterHtml != "" && FooterHtml != null)
            //{
            //    htmltopdf.PageFooterHtml = FooterHtml;
            //    //htmltopdf.Margins = new PageMargins { Top = 55, Bottom = 48 };
            //}
            //var FileBytes = htmltopdf.GeneratePdf(htm);

            HtmlToPdf converter = new HtmlToPdf();
            int webPageWidth = 800;

            if (FooterHtml != "" && FooterHtml != null)
            {
                // footer settings
                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 80;
                PdfHtmlSection Footer = new PdfHtmlSection(FooterURL);
                Footer.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                Footer.WebPageWidth = webPageWidth;                
                converter.Footer.Add(Footer);
            }
            converter.Options.WebPageWidth = webPageWidth;
            PdfDocument doc = converter.ConvertHtmlString(htm);
            // save pdf document
            byte[] FileBytes = doc.Save();
            // close pdf document
            doc.Close();
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
            //Remove Html
            if (System.IO.File.Exists(BodyURL))
                System.IO.File.Delete(BodyURL);
            if (System.IO.File.Exists(FooterURL))
                System.IO.File.Delete(FooterURL);
            //End remove html
            return View();
        }


        public class PdfClass
        {
            public string status { get; set; }
            public BodyPart BodyHtml { get; set; }
            public string FooterHtml { get; set; }
        }

        public class BodyPart
        {
            public string m_StringValue { get; set; }
        }


        public class InveList
        {
            [SqlKey(Display = true)]
            public string investigationmaster_name { get; set; }
        }

        public class MedecienData
        {
            [SqlKey(Display = true)]
            public string Brand_Name { get; set; }
            [SqlKey(Display = true)]
            public string formula { get; set; }
            [SqlKey(Display = true)]
            public string drug_generic_name { get; set; }
            [SqlKey(Display = true)]
            public string strength { get; set; }
            [SqlKey(Display = true)]
            public int frequency { get; set; }
            [SqlKey(Display = true)]
            public int duration_days { get; set; }
            [SqlKey(Display = true)]
            public string dosage { get; set; }
            [SqlKey(Display = true)]
            public string remark { get; set; }
        }

    }
}