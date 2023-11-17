using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Data;
using HMS.BizLogic;
using HospitalManagement.Models;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.Mvc;
using HMS;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class CancelAppointmentController : _Base
    {
        // GET: CancelAppointmentssssss
        readonly CanelAppointment_Master CanApp;
        readonly CancelAppintment_Entity entity;
        public CancelAppointmentController()
        {
            CanApp = new CanelAppointment_Master(bsInfo);
            entity = new CancelAppintment_Entity();
        }
        public ActionResult CancelAppointment()
        {
            CancelAppointmentModel Model = new CancelAppointmentModel();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.DoctorCanappointlist = new List<HMSCancelAppointModel>();
            var dataSet = CanApp.GetAllCancelAppointment(Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = dataSet.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new HMSCancelAppointModel
                {
                    AppointmentId = Convert.ToInt32(item[0]),
                    AppointmentCode = Convert.ToString(item[1]),
                    Gender = Convert.ToString(item[2]),
                    patient_code = Convert.ToString(item[3]),
                    HosDocCode = Convert.ToString(item[4]),
                    PatientName = Convert.ToString(item[5]),
                    PatientAge = Convert.ToInt32(item[6]),
                    PatientProfile = Convert.ToString(item[7]),
                    DoctorName = Convert.ToString(item[8]),
                    HosDepName = Convert.ToString(item[9]),
                    AppointmentDate = Convert.ToString(item[10]),
                    AppointmentTime = Convert.ToString(item[11]),
                    Ctrl = Convert.ToBoolean(item[12]),
                    IsAppointment = Convert.ToBoolean(item[13]),
                    uniqrow = Convert.ToString(item[14]),
                    AppointmentType = Convert.ToString(item[15]),
                    Isrefund = Convert.ToInt32(item[16]),
                    cancelreason = Convert.ToString(item[17])
                };
                Model.DoctorCanappointlist.Add(list);
            }
            return View("CancelAppointment", Model);
        }
        public ActionResult BulkAppointmentCancel()
        {
            CancelAppointmentModel Model = new CancelAppointmentModel();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.DoctorCanappointlist = new List<HMSCancelAppointModel>();
            var dataSet = CanApp.GetAllbulkCancelAppointment(Convert.ToString(Session["ClinicCode"]));
            DataRow[] dr = dataSet.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new HMSCancelAppointModel
                {
                    AppointmentId = Convert.ToInt32(item[0]),
                    AppointmentCode = Convert.ToString(item[1]),
                    Gender = Convert.ToString(item[2]),
                    patient_code = Convert.ToString(item[3]),
                    HosDocCode = Convert.ToString(item[4]),
                    PatientName = Convert.ToString(item[5]),
                    PatientAge = Convert.ToInt32(item[6]),
                    PatientProfile = Convert.ToString(item[7]),
                    DoctorName = Convert.ToString(item[8]),
                    HosDepName =Convert.ToString(item[9]),
                    AppointmentDate =Convert.ToString(item[10]),
                    AppointmentTime = Convert.ToString(item[11]),
                    Ctrl =Convert.ToBoolean(item[12]),
                    IsAppointment = Convert.ToBoolean(item[13]),
                    uniqrow = Convert.ToString(item[14]),
                    AppointmentType = Convert.ToString(item[15]),
                    Isrefund = 0,
                    cancelreason = ""
                };
                Model.DoctorCanappointlist.Add(list);
            }
            Model.DoctorList = Extend.ToList<Doctor_Entity>(dataSet.Tables[1]);
            return View("BulkAppointmentCancel", Model);
        }
        public PartialViewResult GetfilterBulkAppointment(DateTime? fromdate, DateTime? todate, string hosdoccode, string appointment)
        {
            CancelAppointmentModel Model = new CancelAppointmentModel();
            Model.DoctorList = new List<Doctor_Entity>();
            Model.DoctorCanappointlist = new List<HMSCancelAppointModel>();
           var dataSet = CanApp.GetAllBulkAppointfilter(Convert.ToString(Session["ClinicCode"]), fromdate, todate, hosdoccode, appointment);
            DataRow[] dr = dataSet.Tables[0].Select();
            foreach (var item in dr)
            {
                var list = new HMSCancelAppointModel
                {
                    AppointmentId = Convert.ToInt32(item[0]),
                    AppointmentCode = Convert.ToString(item[1]),
                    Gender = Convert.ToString(item[2]),
                    patient_code = Convert.ToString(item[3]),
                    HosDocCode = Convert.ToString(item[4]),
                    PatientName = Convert.ToString(item[5]),
                    PatientAge = Convert.ToInt32(item[6]),
                    PatientProfile = Convert.ToString(item[7]),
                    DoctorName = Convert.ToString(item[8]),
                    HosDepName = Convert.ToString(item[9]),
                    AppointmentDate = Convert.ToString(item[10]),
                    AppointmentTime = Convert.ToString(item[11]),
                    Ctrl = Convert.ToBoolean(item[12]),
                    IsAppointment = Convert.ToBoolean(item[13]),
                    uniqrow = Convert.ToString(item[14]),
                    AppointmentType = Convert.ToString(item[15]),
                    Isrefund = 0,
                    cancelreason = ""
                };
                Model.DoctorCanappointlist.Add(list);
            }
            return PartialView("BulkAppointmentCancel", Model);
        }
        public ActionResult CancelBulkAppointment(CancelAppointmentModel model)
        {
            string status = "";
            DataTable dt = CreateTable();
            var lst = model.DoctorCanappointlist.Where(a => a.Ctrl == true).ToList();
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr["appointmentid"] = Convert.ToInt32(item.AppointmentId);
                    dr["cancelreason"] = Convert.ToString(model.cancelreason);
                   
                    dt.Rows.Add(dr);
                }
                var hos_code = Convert.ToString(Session["ClinicCode"]);
                var user_code = Convert.ToString(Session["UserCode"]);
                bool add = CanApp.CancelBulkAppointment(dt, hos_code, user_code,model.cancelreason);
                if (add)
                {
                    status = "Appointment Cancel Successfully!";
                }
                else
                    status = "Problem ! Appointment Cancelation";
            }
            else
                status = "Select atleast one record";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("appointmentid", typeof(int));
            dt.Columns.Add("cancelreason", typeof(string));
            return dt;
        }
    }
}