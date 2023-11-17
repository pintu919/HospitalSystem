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
using System.Globalization;
using System.Configuration;
using ImageResizer;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSDoctorController : _Base
    {
        // GET: HMSDoctor
        readonly Doctor_Master doc;
        readonly Hospital_Doctor_Entity hd;
        readonly DoctorContract_Entity entity;
        readonly DoctorSchedule_Entity sentity;
        readonly DoctorLeaves_Entity lentity;
        readonly Common_Master SpMaster;
        public HMSDoctorController()
        {
            doc = new Doctor_Master(bsInfo);
            SpMaster = new Common_Master(bsInfo);
            entity = new DoctorContract_Entity();
            sentity = new DoctorSchedule_Entity();
            lentity = new DoctorLeaves_Entity();
            hd = new Hospital_Doctor_Entity();
        }
        public ActionResult HMSDoctorList()
        {
            return View();
        }
        public ActionResult HMSDoctorContractList()
        {
            HMSDoctorData model = new HMSDoctorData();
            var entity = doc.GetAllDoctorContract(Convert.ToString(Session["ClinicCode"]), "");
            List<HMSDocotContract> EM = new List<HMSDocotContract>();
            //DataTable DT = entity.Tables[0];
            foreach (DataRow dr in entity.Tables[0].Rows)
            {
                var ListPro = new HMSDocotContract
                {
                    ContactId = Convert.ToInt32(dr["contract_id"]),
                    DoctorName = Convert.ToString(dr["doctor_name"]),
                    DepartmentName = Convert.ToString(dr["department_name"]),
                    VisitFees = Convert.ToDecimal(dr["Visit_fees"]),
                    FollowupFees = Convert.ToDecimal(dr["followup_fees"]),
                    FollowPolicy = Convert.ToInt32(dr["follow_policy"]),
                    OnlineApointAllow = Convert.ToBoolean(dr["Online_apoint_allow"]),
                    AppointmentSlot = Convert.ToInt32(dr["Appointmnt_slot"]),
                    Uniqrow = Convert.ToString(dr["uniqrow"]),
                    Ctrl = Convert.ToInt32(dr["ctrl"])
                };
                EM.Add(ListPro);
            }
            model.DocContractList = EM;

            return View("HMSDoctorContractList", model);
        }
        [HttpPost]
        public ActionResult AddDoctorContract(HMSDocotContract DOcCont)
        {
            string Status = "";
            //DOcCont.DoctorContractlist = doc.GetAllDoctorContractlist(Convert.ToString(Session["ClinicCode"])).OrderByDescending(o => o.employee_id).ToList();
            if (ModelState.IsValid)
            {
                var entity = SetEntityData(DOcCont);
                var res = doc.AddDoctorContractData(entity);
                if (res != "same" && res != "prefix")
                {
                    if (res.ToInt() > 0 ? true : false)
                    {
                        Status = "Record Added Successfully!";
                    }
                    else
                        Status = "Record Not Saved!";
                }
                else if (res == "prefix")
                {
                    Status = "Prefix already exists in this hospital";
                }
                else
                    Status = "Doctor Record Allrady Exists!";
                ModelState.Clear();
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddHMSDoctorContract()
        {
            HMSDocotContract model = new HMSDocotContract();
            DataSet ds = doc.GetAllDoctorContractlist(Convert.ToString(Session["ClinicCode"]));
            model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(ds.Tables[0]);
            model.doctor_Comission_Category = Extend.ToList<DoctorComissionCategory>(ds.Tables[1]);
            return View("AddHMSDoctorContract", model);
        }
        public DoctorContract_Entity SetEntityData(HMSDocotContract Doc)
        {
            entity.Visit_fees = Convert.ToDecimal(Doc.VisitFees);
            entity.followup_fees = Convert.ToDecimal(Doc.FollowupFees);
            entity.follow_policy = Doc.FollowPolicy;
            entity.Online_apoint_allow = Convert.ToInt32(Doc.OnlineApointAllow);
            entity.Appointmnt_slot = Doc.AppointmentSlot;
            entity.prefix = Convert.ToString(Doc.Prefix);
            entity.employee_code = Doc.EmployeeCode;
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.Ctrl = Convert.ToInt32(Doc.Ctrl);
            entity.doctor_comission_id = Convert.ToInt32(Doc.doctor_comission_id);
            return entity;
        }
        public ActionResult EditDoctorContract(HMSDocotContract HMSContract)
        {
            string status = "";
            DataSet ds = doc.GetAllDoctorContractlist(Convert.ToString(Session["ClinicCode"]));
            HMSContract.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(ds.Tables[0]);
            if (ModelState.IsValid)
            {
                var entity = SetEntityData(HMSContract);
                var res = doc.UpdateDoctorContract(entity);
                if (res == true)
                    status = "Record Update Sucessfully!!";
                else
                    status = "Record Does Not Update Sucessfully!!";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditHMSDoctorContract()
        {
            HMSDocotContract model = new HMSDocotContract();
            var contractuniqcode = Convert.ToString(Session["UniqRow"]);
            //var contractuniqcode = Request.Path.Split('/')[3];
            var DataSet = doc.GetByCodeDoctorContract(contractuniqcode, Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<DoctorContract_Entity>(DataSet.Tables[0]).SingleOrDefault();
            model.DoctorContractlist = Extend.ToList<DoctorContractlist_Entity>(DataSet.Tables[1]);
            model.doctor_Comission_Category = Extend.ToList<DoctorComissionCategory>(DataSet.Tables[2]);
            //model.DoctorContractlist = doc.GetAllDoctorContractlist(Convert.ToString(Session["ClinicCode"])).OrderByDescending(o => o.employee_id).ToList();
            model.EmployeeCode = CModel.employee_code;
            model.VisitFees = Math.Round(CModel.Visit_fees, 2);
            model.FollowupFees = Math.Round(CModel.followup_fees, 2);
            model.FollowPolicy = CModel.follow_policy;
            model.OnlineApointAllow = CModel.Online_apoint_allow.ToBool();
            model.AppointmentSlot = CModel.Appointmnt_slot;
            model.Prefix = CModel.prefix;
            model.ContactId = CModel.contract_id;
            model.Ctrl = Convert.ToInt32(CModel.Ctrl);
            model.doctor_comission_id = Convert.ToString(CModel.doctor_comission_id);
            return View("EditHMSDoctorContract", model);
        }
        public PartialViewResult GetData(string Submit)
        {
            HMSDoctorData Model = new HMSDoctorData();
            Model.ActionType = Submit.Split('/')[0];
            if (Submit.Split('/')[0] == "schedule")
            {
                Model.DocSchedleList = DoctorScheduledata(Submit.Split('/')[1]);
                Model.ContractId = Convert.ToInt32(Submit.Split('/')[1]);
                Model.DocContractList = null;
            }
            //else if (Submit.Split('/')[0] == "leaves")
            //{
            //    Model.DocLeavesList = DoctorLeavesdata(Submit.Split('/')[1]);
            //    Model.ContractId = Convert.ToInt32(Submit.Split('/')[1]);
            //    Model.DocContractList = null;
            //    Model.DocSchedleList = null;
            //}
            return PartialView("HMSDoctorContractList", Model);
        }
        public List<DoctorSchedule> DoctorScheduledata(string contractId)
        {
            List<DoctorSchedule> tm = new List<DoctorSchedule>();
            var rs = doc.GetAllDoctorSchedule(Convert.ToInt32(contractId));
            foreach (var item in rs)
            {
                tm.Add(new DoctorSchedule
                {
                    ContractId = item.ContractId,
                    Day = item.Day,
                    TimeFrom = item.TimeFrom,
                    TimeTo = item.TimeTo,
                    scheduleId = item.scheduleId
                });
            }
            return tm;
        }

        //public List<DoctorLeaves> DoctorLeavesdata(string contractId)
        //{

        //    List<DoctorLeaves> tm = new List<DoctorLeaves>();
        //    var rs = doc.GetAllDoctorLeaves(Convert.ToInt32(contractId));
        //    tm.Add(new DoctorLeaves
        //    {
        //        ContractId = rs != null ? rs.contractid : 0,
        //        FromDate = rs != null ? rs.fromdate : "",
        //        Todate = rs != null ? rs.fromdate : "",
        //        LeaveDays = rs != null ? rs.leavedays : 0,
        //        LeaveType = rs != null ? rs.leavetype : "",
        //    });

        //    return tm;
        //}
        [HttpPost]
        public PartialViewResult Insertschedule(DoctorSchedule schedule)
        {
            HMSDoctorData Modal = new HMSDoctorData();
            Modal.DocSchedleList = new List<DoctorSchedule>();
            List<DoctorSchedule> tm = new List<DoctorSchedule>();
            sentity.ContractId = schedule.ContractId;
            sentity.TimeFrom = schedule.TimeFrom;
            sentity.TimeTo = schedule.TimeTo;
            sentity.Day = schedule.Day;
            sentity.hos_code = Convert.ToString(Session["ClinicCode"]);
            sentity.user_code = Convert.ToString(Session["UserCode"]);
            sentity.ctrl = 1;

            if (ModelState.IsValid)
            {

                var res = doc.AddDoctorSchedule(sentity);
                if (res != "same")
                {
                    if (res.ToInt() > 0 ? true : false)
                    {
                        var dos = doc.GetAllDoctorSchedule(Convert.ToInt32(schedule.ContractId));
                        foreach (var item in dos)
                        {
                            tm.Add(new DoctorSchedule
                            {
                                ContractId = item.ContractId,
                                Day = item.Day,
                                TimeFrom = item.TimeFrom,
                                TimeTo = item.TimeTo,
                                scheduleId = item.scheduleId
                            });
                        }
                        Modal.DocSchedleList.AddRange(tm);
                    }
                }
            }
            return PartialView("HMSDoctorContractList", Modal);
        }

        [HttpPost]
        public ActionResult InsertLeaves(DoctorLeaves leaves)
        {
            string Status = "";
            HMSDoctorData Modal = new HMSDoctorData();
            Modal.DocSchedleList = new List<DoctorSchedule>();
            List<DoctorLeaves> tm = new List<DoctorLeaves>();
            lentity.contractid = Convert.ToInt32(leaves.ContractId);
            lentity.fromdate = leaves.FromDate;
            lentity.todate = leaves.Todate;
            lentity.leavedays = Convert.ToInt32(leaves.LeaveDays);
            lentity.leavetype = leaves.LeaveType;
            lentity.ctrl = 1;

            if (ModelState.IsValid)
            {
                var res = doc.AddDoctorLeaves(lentity);
                if (res != "same")
                {
                    if (res.ToInt() > 0 ? true : false)
                    {
                        Status = "Record Added Sucessfully!";
                    }
                    else
                    {
                        Status = "Record Update Sucessfully!";
                    }
                }
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContractedDrDelete(int ContractId)
        {
            bool result = false;
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = doc.DeleteDoctorContract(ContractId, hos_code, user_code);
            if (Convert.ToBoolean(res) == true)
            {

                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSchedule(int Schedule)
        {
            bool result = false;

            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = doc.DeleteSchedule(Schedule, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Updateschedule(DoctorSchedule schedule)
        {
            string status = "";
            sentity.ContractId = Convert.ToInt32(schedule.ContractId);
            sentity.TimeFrom = schedule.TimeFrom;
            sentity.TimeTo = schedule.TimeTo;
            sentity.Day = schedule.Day;
            sentity.scheduleId = schedule.scheduleId;
            sentity.hos_code = Convert.ToString(Session["ClinicCode"]);
            sentity.user_code = Convert.ToString(Session["UserCode"]);
            sentity.ctrl = 1;
            if (ModelState.IsValid)
            {

                var res = doc.UpdateDoctorschedule(sentity);
                if (res == true)
                {
                    status = "1";
                }
                else
                {
                    status = "Day Already Added Please Select another Day";
                }

            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDoctorBMDCCode(string Prefix)
        {
            var Codelist = "";
            return Json(Codelist, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DcotorProfile()
        {
            HMSDocotContract Model = new HMSDocotContract();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.DoctorEduList = new List<DoctorEducation_Entity>();
            Model.DoctorExepList = new List<DoctorExperience_Entity>();
            var contractuniqcode = Request.Path.Split('/')[3];
            DataSet ds = doc.GetAllDoctorProfiledata(Convert.ToString(Session["ClinicCode"]), contractuniqcode);
            Model.DoctorList = Extend.ToList<Doctor_Entity>(ds.Tables[0]);
            Model.DoctorEduList = Extend.ToList<DoctorEducation_Entity>(ds.Tables[1]);
            Model.DoctorExepList = Extend.ToList<DoctorExperience_Entity>(ds.Tables[2]);
            return View("DcotorProfile", Model);
        }

        public ActionResult DoctorAccessAdmitPatient()
        {
            doctorAcessAdmitpateint model = new doctorAcessAdmitpateint();
            model.doctordetaillst = new List<doctordetail>();
            var dataset = doc.GetRefDoctor(Convert.ToString(Session["ClinicCode"]));
            model.Doctorlst = Extend.ToList<RegDoctor_list>(dataset.Tables[0]);
            model.doctordetaillst = Extend.ToList<doctordetail>(dataset.Tables[1]);
            model.Status = 4;
            return View("DoctorAccessAdmitPatient", model);
        }

        public PartialViewResult Adddoctor(doctorAcessAdmitpateint DAA)
        {
            doctorAcessAdmitpateint model = new doctorAcessAdmitpateint();
            model.Doctorlst = new List<RegDoctor_list>();
            var dataset = doc.AddDcotor(DAA.doctor_code, Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            model.doctordetaillst = Extend.ToList<doctordetail>(dataset.Tables[0]);
            model.Status = Convert.ToInt32(dataset.Tables[1].Rows[0]["status"]);
            return PartialView("DoctorAccessAdmitPatient", model);
        }

        public ActionResult DoctorDelete(int ID)
        {
            bool result = false;
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = doc.DeleteDoctor(ID, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region"Hospital Add Doctor"
        public ActionResult AddHMSDoctor()
        {
            DoctorModel D = new DoctorModel();
            D.StateLst = new List<State_Entity>();
            D.DistrictLst = new List<District_Entity>();
            D.CityLst = new List<City_Entity>();
            var Data = SpMaster.GetData(Convert.ToString(Session["ClinicCode"]));
            D.CountryLst = Extend.ToList<Country_Entity>(Data.Tables[0]).OrderByDescending(o => o.country_id).ToList();
            D.SpecializeLst = Extend.ToList<Specialize_Entity>(Data.Tables[1]).OrderByDescending(o => o.specialize_id).ToList();
            D.DeptLst = Extend.ToList<Department_Entity>(Data.Tables[2]).OrderByDescending(o => o.department_id).ToList();
            return View("AddHMSDoctor", D);
        }
        [HttpPost]
        public ActionResult AddHosiptalDoctor(DoctorModel Dr)
        {
            string[] Message = new string[2];
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase imgfile = files[0];
                string FileName = DateTime.Now.ToString("yyMMddHHmmssff") + Path.GetExtension(imgfile.FileName);
                string FilePath = ConfigurationManager.AppSettings["AddDocImgUrl"] + "" + FileName.Replace(" ", string.Empty);
                if (System.IO.File.Exists(ConfigurationManager.AppSettings["AddDocImgUrl"] + Dr.DoctorImage))
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["AddDocImgUrl"] + Dr.DoctorImage);
                }
                imgfile.SaveAs(FilePath);
                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Width = 350,
                    Height = 350,
                    Mode = FitMode.Crop,
                    Format = "jpg"
                };
                ImageBuilder.Current.Build(FilePath, FilePath, resizeSetting);
                Dr.DoctorImage = FileName;
            }
            var entity = SetEntityDoctorData(Dr);
            if (string.IsNullOrEmpty(Dr.DoctorCode))
            {
                var res = doc.HospitalDoctorRegister(entity);
                if (res.Tables.Count > 1)
                {
                    if (Convert.ToInt16(res.Tables[1].Rows[0]["statusmassage"]) > 0)
                    {
                        var email = Extend.ToList<Email_Entity>(res.Tables[1]).SingleOrDefault();
                        string msg = new Email_Master(bsInfo).SendEmail(email);
                        if (msg == "1")
                        {
                            SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["Column1"]), "doctor", email.Subject, "hospital doctor register", email.Body, email.ToEmail, 1);
                            Message[0] = "1";
                            Message[1] = "Message: Regestration Successfully done Please Check your email and click verifaction link!";
                        }
                        else
                        {
                            Message[0] = "0";
                            Message[1] = "Message: Regestration Successfully but  email not send!";
                            SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["Column1"]), "doctor", email.Subject, "hospital doctor regester", email.Body, email.ToEmail, 0);
                        }
                    }
                    else { Message[0] = "0"; Message[1] = "Message: Error occured No Data Saved!"; }
                }
                else
                {
                        Message[0] = "2";
                        Message[1] = "Message: Regestration Allrady Exists!";
                }
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public Hospital_Doctor_Entity SetEntityDoctorData(DoctorModel Dr)
        {
            hd.doctor_name = Dr.DoctorName.Trim();
            hd.doctor_code = Dr.DoctorCode;
            hd.city_code = Dr.CityCode;
            hd.country_code = Dr.CountryCode;
            hd.Ctrl = Dr.Ctrl.ToInt();
            hd.qualification = Dr.DegreeotherSpecification != null ? Dr.DegreeotherSpecification.Trim() : Dr.DegreeotherSpecification;
            hd.occupation = Dr.DescriptionProfessionalStatement != null ? Dr.DescriptionProfessionalStatement.Trim() : Dr.DescriptionProfessionalStatement;
            hd.district_code = Dr.districtcode;
            hd.doctor_designation = Dr.DoctorDesignation;
            hd.doctor_image = Dr.DoctorImage;
            hd.doctor_mobile = Dr.DoctorMobile;
            hd.mobile_code = "";
            hd.doctror_bmdc_reg_no = Dr.DoctrorbmdcRegno;
            hd.gender = Dr.Gender;
            hd.state_code = Dr.statecode;
            //entity.verification_code = Dr.VerificationCode;
            hd.work_email = Dr.WorkEmail;
            hd.work_phone = Dr.WorkPhone;
            hd.specialize_code = Dr.SpecialityCode;
            hd.department_code = Dr.DoctorDepartmentCode;
            hd.hospital_code = Convert.ToString(Session["ClinicCode"]);
            return hd;
        }

        //public ActionResult DoctorVerification()
        //{

        //    if (Request.Path.Split('/').Length > 3)
        //    {
        //        string Uniqrow = Request.Path.Split('/')[3];
        //        if (Uniqrow != "")
        //        {
        //            var res = doc.Doctor_Verify(Uniqrow, "Doctor");
        //            if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmassage"]) == 1)
        //            {
        //                var email = Extend.ToList<Email_Entity>(res.Tables[0]).SingleOrDefault();
        //                string msg = new Email_Master(bsInfo).SendEmail(email);
        //                if (msg == "1")
        //                    SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["code"]), "doctor", email.Subject, "hospital doctor verify", email.Body, email.ToEmail, 1);
        //                else
        //                   SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["code"]), "doctor", email.Subject, "hospital doctor verify", email.Body, email.ToEmail, 0);
        //                TempData["verifiedStatus"] = "Your Account verify Success !";
        //            }
        //            else if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmassage"]) == 2)
        //            {
        //                TempData["verifiedStatus"] = "Your Account allrady verified !";
        //            }
        //            else if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmassage"]) == 0)
        //            {
        //                TempData["verifiedStatus"] = "Your Account not verified !";
        //            }
        //        }
        //        else
        //            TempData["verifiedStatus"] = "Sorry you are trying in wrong direction !";
        //    }
        //    else
        //        TempData["verifiedStatus"] = "Sorry you are trying in wrong direction !";

        //    return View("DoctorVerification");
        //}

        #endregion

    }
}