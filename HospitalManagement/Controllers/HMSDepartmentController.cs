using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSDepartmentController : _Base
    {
        // GET: HMSDepartment
        readonly HospitalDepartment HosDep;
        readonly Department_Master Dep;
        readonly HospitalDepartment_Entity entity;

        public HMSDepartmentController()
        {
            HosDep = new HospitalDepartment(bsInfo);
            Dep = new Department_Master(bsInfo);
            entity = new HospitalDepartment_Entity();
        }

        public ActionResult HMSDepartmentList()
        {
            var entity = HosDep.GetAll( Convert.ToString(Session["ClinicCode"])).OrderByDescending(o =>o.Hos_department_id);
            return View("HMSDepartmentList", entity);
        }

        public ActionResult AddHMSDepartment(HMSDeparmentModel hmsdep)
        {
            HMSDeparmentModel model = new HMSDeparmentModel();
            model.DepartmentLst = Dep.GetAll().OrderByDescending(o =>o.department_id).ToList();
            if (ModelState.IsValid)
            {
                if (hmsdep.HosDepartmentName != null)
                {
                    var entity = SetEntityData(hmsdep);
                    var res = HosDep.Add(entity);
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

                return View("AddHMSDepartment", model);
        }

        public HospitalDepartment_Entity SetEntityData(HMSDeparmentModel hmsdep)
        {
           
            entity.Hos_department_name = hmsdep.HosDepartmentName.Trim();
            entity.Hos_department_code = hmsdep.HosDepartmentCode;
            entity.department_code = hmsdep.DepartmentCode;
            entity.Hosiptal_code = Convert.ToString(Session["ClinicCode"]);
            entity.user_code = Convert.ToString(Session["UserCode"]);
            entity.Ctrl = hmsdep.Ctrl.ToInt();
            return entity;
        }

        public ActionResult EditHMSDepartment(HMSDeparmentModel hmsdep)
        {
            HMSDeparmentModel model = new HMSDeparmentModel();
            var Hoscode = hmsdep.HosDepartmentCode;

        
            if (Hoscode == null || Hoscode == "")
            {
                 Hoscode = Convert.ToString(Session["ClinicCode"]);
                var Depcode = Convert.ToString(Session["UniqRow"]);
                //var Depcode = Request.Path.Split('/')[4];
                var DataSet = HosDep.GetByCode(Hoscode, Depcode);
                var CModel = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[0]).SingleOrDefault();
                model.DepartmentLst = Dep.GetAll().OrderByDescending(o => o.department_id).ToList();
                model.HosDepartmentCode = CModel.Hos_department_code;
                model.HosDepartmentName = CModel.Hos_department_name;
                model.HosiptalCode = CModel.Hosiptal_code;
                model.DepartmentCode = CModel.department_code;
                model.Ctrl = CModel.Ctrl.ToBool();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var entity = SetEntityData(hmsdep);
                    var res = HosDep.Update(entity);
                    return RedirectToAction("HMSDepartmentList");
                }
            }

            return View("EditHMSDepartment", model);
        }

        public ActionResult HospitalDepartmentDelete(int HosDepartId)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = HosDep.HospitalDepDeleteID(Convert.ToString(HosDepartId), hoscode,usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}