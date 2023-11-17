using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using System.Data;
using System.IO;
using System;
using System.Collections.Generic;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class DashboardController : _Base
    {
        readonly Hos_Dashbord_Master dashmaster;
        public DashboardController()
        {
            dashmaster = new Hos_Dashbord_Master(bsInfo);
        }
        public ActionResult Dashboard()
        {
            var time = DateTime.Parse("2020-09-10 23:32:00");
            var clientZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(time, clientZone);
            var utcDate = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);
            var localTime = utcDate.ToLocalTime();
            DateTime utcDateTime = utcTime.ToLocalTime();
            HMSDashboardData dashborddata = new HMSDashboardData();
            DataSet ds = dashmaster.GetDashboarddata(Convert.ToString(Session["ClinicCode"]));
            //dashborddata.totaldoc = Convert.ToInt32(ds.Tables[0].Rows[0]["totaldoc"]);
            dashborddata.totalpat = Convert.ToInt32(ds.Tables[0].Rows[0]["totalpat"]);
            dashborddata.todayregister = Convert.ToInt32(ds.Tables[0].Rows[0]["todayregister"]);
            //dashborddata.totalvisited= Convert.ToInt32(ds.Tables[0].Rows[0]["totalvisited"]);
            //dashborddata.totalpending = Convert.ToInt32(ds.Tables[0].Rows[0]["totalpending"]);
            dashborddata.today_appoint = Convert.ToInt32(ds.Tables[0].Rows[0]["today_appoint"]);
            dashborddata.todayappoint_offline = Convert.ToInt32(ds.Tables[0].Rows[0]["todayappoint_offline"]);
            dashborddata.today_appoint_online = Convert.ToInt32(ds.Tables[0].Rows[0]["today_appoint_online"]);
            dashborddata.totalinpatient = Convert.ToInt32(ds.Tables[0].Rows[0]["totalinpatient"]);
            dashborddata.todayinpatient = Convert.ToInt32(ds.Tables[0].Rows[0]["todayinpatient"]);
            dashborddata.totalOT = Convert.ToInt32(ds.Tables[0].Rows[0]["totalOT"]);
            dashborddata.totalIPOT = Convert.ToInt32(ds.Tables[0].Rows[0]["totalIPOT"]);
            dashborddata.totalOPDOT = Convert.ToInt32(ds.Tables[0].Rows[0]["totalOPDOT"]);
            dashborddata.totalrealsepatient = Convert.ToInt32(ds.Tables[0].Rows[0]["totalrealsepatient"]);
            dashborddata.todayrealsepatient = Convert.ToInt32(ds.Tables[0].Rows[0]["todayrealsepatient"]);
            dashborddata.todayearning = Convert.ToDecimal(ds.Tables[0].Rows[0]["todayearning"]);
            dashborddata.TopPatientList = Extend.ToList<TopPatient>(ds.Tables[1]);
            dashborddata.TopDoctorList = Extend.ToList<TopDoctor>(ds.Tables[2]);
            dashborddata.TopAppointmentList = Extend.ToList<TopAppointment>(ds.Tables[3]);
            dashborddata.patientrationlist = Extend.ToList<patientration>(ds.Tables[4]);
            dashborddata.billinglist = Extend.ToList<billing>(ds.Tables[5]);
            dashborddata.employeelst = Extend.ToList<HOSEmployee>(ds.Tables[6]);
            return View("Dashboard", dashborddata);
        }
        [HttpPost]
        public ActionResult getPatientratio(string ratio)
        {
            DataSet ds = dashmaster.GetPatientratio(Convert.ToString(Session["ClinicCode"]),ratio);
            List<patientration> pat = ds.Tables[0].ToList<patientration>();
            return Json(pat, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getbilling(string ratio)
        {
            DataSet ds = dashmaster.Getbillingdata(Convert.ToString(Session["ClinicCode"]), ratio);
            List<billing> pat = ds.Tables[0].ToList<billing>();
            return Json(pat, JsonRequestBehavior.AllowGet);
        }
        public void SetParaValue(string Para)
        {
            Session["UniqRow"] = Para;
        }
        public ActionResult SessionTimeout()
        {
            Session.Timeout = Session.Timeout;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSessionAssignMenu(string PageName,string ActionName)
        {
            List<HpMenu_Entity> menulst = (List<HpMenu_Entity>)Session["AssignHospitalMenuList"];
            return Json(menulst.Find(a=>a.hp_actionname== ActionName && a.hp_pagename==PageName));
        }
    }
}