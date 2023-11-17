using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{

    [CustomHandleError]
    public class VitalParaController : _Base
    {
        readonly MapVitalMaster HV;
        readonly VitalMaster_Entity entity;
        public VitalParaController()
        {
            HV = new MapVitalMaster(bsInfo);
            entity = new VitalMaster_Entity();
        }
        public ActionResult VitalParaLists()
        {
            var entity = HV.GetAll(Convert.ToString(Session["ClinicCode"])).OrderByDescending(o => o.hos_vital_id);
            return View("VitalParaLists", entity);
        }
        public ActionResult AddVitalPara(HMSVitalParaModel hmsvi)
        {
            HMSVitalParaModel model = new HMSVitalParaModel();
            model.VitalLst = Extend.ToList<VitalLists>(HV.GetAllmainPara()).OrderByDescending(o => o.vital_id).ToList();
            if (ModelState.IsValid)
            {
                if (hmsvi.HosVitalParaName != null)
                {
                    var entity = SetEntityData(hmsvi);
                    var res = HV.Add(entity);
                    if (res != "same")
                    {
                        if (res.ToInt() > 0 ? true : false)
                        {
                            TempData["message"] = "Record Added Successfully!";
                        }
                        else
                            TempData["message"] = "Record Not Saved!";
                    }
                    else
                        TempData["message"] = "Record Allrady Exists!";
                }
                ModelState.Clear();
            }
            return View("AddVitalPara", model);
        }
        public VitalMaster_Entity SetEntityData(HMSVitalParaModel hmsvi)
        {
            entity.hos_vital_name = hmsvi.HosVitalParaName.Trim();
            entity.hos_vital_code = hmsvi.HosVitalParaCode;
            entity.hos_vital_unit = hmsvi.VitalUnit;
            entity.vital_code = hmsvi.VitalParaCode;
            entity.Hosiptal_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.Ctrl = hmsvi.Ctrl.ToInt();
            return entity;
        }
        public ActionResult EditVitalPara(HMSVitalParaModel hmsvi)
        {
            HMSVitalParaModel model = new HMSVitalParaModel();
            if (hmsvi.HosVitalParaCode == null || hmsvi.HosVitalParaCode == "")
            {
                var Hoscode = Convert.ToString(Session["ClinicCode"]);
                var HosUniqRow = Convert.ToString(Session["UniqRow"]);
                var DataSet = HV.GetByCode(Hoscode, HosUniqRow);
                var CModel = Extend.ToList<VitalMaster_Entity>(DataSet.Tables[0]).SingleOrDefault();
                model.VitalLst = Extend.ToList<VitalLists>(HV.GetAllmainPara()).OrderByDescending(o => o.vital_id).ToList();
                model.HosVitalParaCode = CModel.hos_vital_code;
                model.HosVitalParaName = CModel.hos_vital_name;
                model.VitalUnit = CModel.hos_vital_unit;
                model.HosiptalCode = CModel.Hosiptal_code;
                model.VitalParaCode = CModel.vital_code;
                model.Ctrl = CModel.Ctrl.ToBool();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var entity = SetEntityData(hmsvi);
                    var res = HV.Update(entity);
                    return RedirectToAction("VitalParaLists");
                }
            }
            return View("EditVitalPara", model);
        }
        public ActionResult HospitalVitalDelete(int HosVitalId)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = HV.HospitalVitalDelete(Convert.ToString(HosVitalId), hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}