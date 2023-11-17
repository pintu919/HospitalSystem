using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HospitalManagement.Models;
using Newtonsoft.Json;
using HMS;
using System.Data;
using HMS.Data;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class DischargeProfileController : _Base
    {
        // GET: DischargeProfile
        readonly DischargeProfile_Master DEMaster;
        readonly DischargeProfilemed_entity DE;
        readonly ProfileSection_Entity PE;
        public DischargeProfileController()
        {
            DEMaster = new DischargeProfile_Master(bsInfo);
            DE = new DischargeProfilemed_entity();
            PE = new ProfileSection_Entity();
        }
        DataSet ds = new DataSet();
        public ActionResult DischargeProfile()
        {
            discharge_profile model = new discharge_profile();
            model.Prog_Med = new ProgressMedicine();
            model.med_list = new List<DischargeProfilemed_entity>();
            model.ProfileSectionlist = new List<ProfileSection_Entity>();
            ds = DEMaster.GetDischargeProfilesection(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            model.ProfileSectionlist = Extend.ToList<ProfileSection_Entity>(ds.Tables[0]);
            model.RemarkList = Extend.ToList<MedicineRemark_List>(ds.Tables[1]);
            return View("DischargeProfile", model);
        }
        [HttpPost]
        public JsonResult GetSearchMedicine(string Prefix, string MedIds, string FilterBy,string BrandCodess)
        {
            if (MedIds != "")
                MedIds = MedIds.Substring(0, MedIds.Length - 1);
            if (BrandCodess != "")
                BrandCodess = BrandCodess.Substring(0, BrandCodess.Length - 1);
            List<Med_Entity> entity = new List<Med_Entity>();
            string data = ConfigurationManager.AppSettings["JsonFile"];
            entity = JsonConvert.DeserializeObject<List<Med_Entity>>(System.IO.File.ReadAllText(data + "/MedecineAllData.json"));
            IEnumerable<Med_Entity> MedList;
            List<Med_Entity> FinalMedList = new List<Med_Entity>();
            if (FilterBy.ToLower() == "brandwise")
            {
                MedList = from p in entity.
                              FindAll(b => b.Brand_Name.ToLower().StartsWith(Prefix.ToLower())).ToList()
                          where !BrandCodess.Split(',').Contains(p.brand_code)
                          select p;
                FinalMedList = MedList.ToList();
            }
            else
            {
                MedList = from p in entity.
                          FindAll(b => b.drug_generic_name.ToLower().StartsWith(Prefix.ToLower())).ToList()
                          where !BrandCodess.Split(',').Contains(p.brand_code)
                          select p;
                FinalMedList = MedList.ToList();
            }
            return Json(FinalMedList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Add_Discharge_med(discharge_profile BS)
        {
            string Status = "";
            var entity = SetEntityData(BS);
            var res = DEMaster.Add(entity);
            if (res != "same")
            {
                if (res.ToInt() > 0 ? true : false)
                {
                    Status = "Record Added Successfully!";
                }
                else
                    Status = "Record Not Saved!";
            }
            else
                Status = "Record Allrady Exists!";
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public DischargeProfilemed_entity SetEntityData(discharge_profile BSM)
        {
            DE.Dosages = BSM.Prog_Med.Dosages;
            DE.hos_code = Convert.ToString(Session["ClinicCode"]);
            DE.user_code = Convert.ToString(Session["UserCode"]);
            DE.Duration = BSM.Prog_Med.Duration;
            DE.mappingid = BSM.Prog_Med.mappingid;
            DE.brand_code = BSM.Prog_Med.brand_code;
            DE.Remark = BSM.Prog_Med.Remark;
            DE.Used = BSM.Prog_Med.Used;
            DE.MedicineIds = BSM.Prog_Med.MedicineIds;
            DE.ctrl = 0;
            DE.section_id = BSM.Prog_Med.SectionId;
            DE.RealMedicineName = BSM.Prog_Med.RealMedicineName;
            return DE;
        }
        public ActionResult GetDischargeMed()
        {
            discharge_profile model = new discharge_profile();
            model.Prog_Med = new ProgressMedicine();
            model.med_list = new List<DischargeProfilemed_entity>();
            model.ProfileSectionlist = new List<ProfileSection_Entity>();
            ds = DEMaster.GetDischargeMed(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]));
            model.med_list = Extend.ToList<DischargeProfilemed_entity>(ds.Tables[0]);
            model.ProfileSectionlist = Extend.ToList<ProfileSection_Entity>(ds.Tables[1]);
            return View("DischargeProfile", model);
        }
        public PartialViewResult AddProfileSection(discharge_profile Section)
        {
            discharge_profile model = new discharge_profile();
            model.ProfileSectionlist = new List<ProfileSection_Entity>();
            model.Prog_Med = new ProgressMedicine();
            var entity = SetEntityDataSetion(Section);
            var res = DEMaster.SaveProfileSetion(entity);
            model.ProfileSectionlist = Extend.ToList<ProfileSection_Entity>(res).ToList();
            return PartialView("DischargeProfile", model);
        }
        public ProfileSection_Entity SetEntityDataSetion(discharge_profile station)
        {
            PE.section_id = station.pfsection.SectionId;
            PE.section_name = station.pfsection.SectionName;
            PE.hos_code = Convert.ToString(Session["ClinicCode"]);
            PE.user_code = Convert.ToString(Session["UserCode"]);
            PE.ctrl = station.pfsection.Ctrl;
            PE.general_advice = station.pfsection.GeneralAdvice;
            return PE;
        }
        public ActionResult DeleteSection(int SectionId)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = DEMaster.DeleteSection(SectionId, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMed(int Id)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = DEMaster.DeleteMed(Id, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDischargeMedSectionWise(int SectionId)
        {
            discharge_profile model = new discharge_profile();
            model.Prog_Med = new ProgressMedicine();
            model.med_list = new List<DischargeProfilemed_entity>();
            model.ProfileSectionlist = new List<ProfileSection_Entity>();
            ds = DEMaster.GetDischargeMedSectionWise(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), SectionId);
            model.med_list = Extend.ToList<DischargeProfilemed_entity>(ds.Tables[0]);
            model.ProfileSectionlist = Extend.ToList<ProfileSection_Entity>(ds.Tables[1]);
            return View("DischargeProfile", model);
        }

        public ActionResult ChangeIPDoctor()
        {
            ChangeIPDoctorModel model = new ChangeIPDoctorModel();
            model.Doctor_Assign_List_Data = new List<doctor_Assgin_list>();
            model.Message = string.Empty;
            return View("ChangeIPDoctor", model);
        }
        public PartialViewResult FindDoctor(ChangeIPDoctorModel IP)
        {
            string statusmessage = "";
            ChangeIPDoctorModel model = new ChangeIPDoctorModel();
            model.Doctor_Assign_List_Data = new List<doctor_Assgin_list>();
            ds = DEMaster.GetIPDoctor(Convert.ToString(Session["ClinicCode"]), IP.AppointmentCode, out statusmessage);
            if (statusmessage == "success")
            {
                model.Message = statusmessage;
                model.DoctorName = Convert.ToString(ds.Tables[0].Rows[0]["doctor_name"]);
                model.Doctor_Assign_List_Data = Extend.ToList<doctor_Assgin_list>(ds.Tables[1]);
                model.AppointmentCode = IP.AppointmentCode;
            }
            else
            {
                model.Message = statusmessage;
                model.Doctor_Assign_List_Data = new List<doctor_Assgin_list>();
            }
            return PartialView("ChangeIPDoctor", model);
        }
        public ActionResult ConfirmchangeDoctor(string DoctorCode, string AppointmentCode)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = DEMaster.ChangeDoctor(DoctorCode, AppointmentCode, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}