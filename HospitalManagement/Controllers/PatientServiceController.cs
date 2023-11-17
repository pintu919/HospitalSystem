using HMS;
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
    public class PatientServiceController : _Base
    {
        // GET: PatientService
        readonly PatientService_Master psmMaster;
        readonly PatientService_Entity entity;
        public PatientServiceController()
        {
            psmMaster = new PatientService_Master(bsInfo);
            entity = new PatientService_Entity();
        }
        public ActionResult PatientServiceDetails()
        {
            PatientServiceDetailModel model = new PatientServiceDetailModel();
            var res = psmMaster.GetOtherData(Convert.ToString(Session["ClinicCode"]));
            List<SelectListItem> EM = new List<SelectListItem>();
            foreach (DataRow dr in res.Tables[0].Rows)
            {
                var ListPro = new SelectListItem
                {
                    Text = (dr["investigationmaster_name"]).ToString(),
                    Value = (dr["investigationmaster_id"]).ToString(),
                };
                EM.Add(ListPro);
            }
            model.Inv_List = EM;
            return View("PatientServiceDetails",model);
        }
        public ActionResult AddPatientService(PatientServiceDetailModel pat)
        {
            string status = "";
            string result = string.Empty;
            List<string> results = new List<string>();
            if (pat.investigationmaster_id != null)
            {
                result = string.Join(",", pat.investigationmaster_id);
                PatientServiceDetailModel model = new PatientServiceDetailModel();
                model.Inv_List = new List<SelectListItem>();
                if (ModelState.IsValid)
                {
                    if (pat.PatientFirstName != null)
                    {
                        var entity = SetEntityData(pat);
                        entity.investigation_ids = result;
                        var res = psmMaster.Add(entity);
                        if (res != "same")
                        {
                            if (res.ToInt() > 0)
                            {
                                status = "Record Added Successfully!";
                            }
                            else
                                status = "Record Not Saved!";
                        }
                        else
                            status = "Record Allrady Exists!";
                    }
                }
               
                ModelState.Clear();
            }
            else
                status = "Please Seleact atleast one investigation!!";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public PatientService_Entity SetEntityData(PatientServiceDetailModel Pati)
        {
            //entity.investigationmaster_id = Pati.investigationmaster_id;
            entity.patient_firstname = Pati.PatientFirstName.Trim();
            entity.patient_lastname = Pati.PatientLasttName.Trim();
            entity.dob = Pati.DateOfBirth;
            entity.age = Pati.Age;
            entity.gender = Pati.Gender;
            entity.email = Pati.Email.Trim();
            entity.mobile = Pati.Phone;
            entity.address = Pati.PresentAddress;
            entity.ctrl = Convert.ToInt32(1);
            entity.hos_code = Convert.ToString(Session["ClinicCode"]);
            return entity;
        }
    }
}