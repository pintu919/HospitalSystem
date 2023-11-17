using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BizLogic;
using HMS.Data;
using HMS;
using HospitalManagement.Models;
using ImageResizer;
using System.IO;
using System.Data;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSNurseController : _Base
    {
        // GET: HMSNurse
        readonly InPatient_Master InPat;
        readonly Nurse_Master NusMaster;
        readonly Nurse_Entity ne;
        readonly Vital_Master VitMaster;
        readonly patientvital_Entity PE;
        
        public HMSNurseController()
        {
            InPat = new InPatient_Master(bsInfo);
            NusMaster = new Nurse_Master(bsInfo);
            ne = new Nurse_Entity();
            VitMaster = new Vital_Master(bsInfo);
            PE = new patientvital_Entity();
        }
        public ActionResult HMSNurseMapping()
        {
            NurseModel model = new NurseModel();
            var DataSet = NusMaster.GetAllNurse(Convert.ToString(Session["ClinicCode"]));
            model.dpartmentlst = Extend.ToList<HospitalDepartment_Entity>(DataSet.Tables[0]);
            model.Nurselstation = Extend.ToList<Nurse_Entity>(DataSet.Tables[1]);
            model.RegisterAppointmentlst = new List<DocAppintment_Entity>();
            return View("HMSNurseMapping", model);
        }

        public ActionResult HMSRegPatientAppointmentList()
        {
            MainNurseModel model = new MainNurseModel();
            model.Nurse = new NurseModel();
            //model.vital = new vitalpara();
            DataSet ds = NusMaster.GetAllRegistrAppoint(Convert.ToString(Session["ClinicCode"]));
            model.Nurse.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(ds.Tables[0]);
            model.Nurse.Nurselstation = Extend.ToList<Nurse_Entity>(ds.Tables[1]);
            model.Nurse.dpartmentlst = new List<HospitalDepartment_Entity>();
            model.Nurse.DoctorList = new List<Doctor_Entity>();
            //model.vital.Vitallst = new List<Vitalpara_Entity>();
            model.investigationPriceList = Extend.ToList<investigationparaamount>(ds.Tables[2]);
            return View("HMSRegPatientAppointmentList", model);
        }
        [HttpPost]
        public ActionResult AddInvestigationAmount( int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId)
        {
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = NusMaster.SetInvestigationPrice(service_id,Investigationid, Amount, Appointmentcode, PriceId, hoscode, usercode);
            return Json(result == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CancelOPDInvestigation(int service_id, int Investigationid, decimal Amount, string Appointmentcode, int PriceId)
        {
            MainNurseModel model = new MainNurseModel();
            model.Nurse = new NurseModel();
            //model.vital = new vitalpara();
            model.parameter = new investigationparaamount();
            model.parameter.service_id = Convert.ToInt32(service_id);
            model.parameter.investigationmaster_id = Convert.ToInt32(Investigationid);
            model.parameter.price = Convert.ToDecimal(Amount);
            model.parameter.appoinment_code = Convert.ToString(Appointmentcode);
            model.parameter.invpriceid = Convert.ToInt32(PriceId);
            return PartialView("_OPDInvestigationCancel_Reason", model);
            //var result = NusMaster.CancelOPDInvestigation(service_id, Investigationid, Amount, Appointmentcode, PriceId, hoscode, usercode);
            //return Json(result == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SureOPDCancelInvestigation(MainNurseModel M)
        {
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);

            MainNurseModel model = new MainNurseModel();
            model.Nurse = new NurseModel();
            //model.vital = new vitalpara();
            model.parameter = new investigationparaamount();
            var result = NusMaster.CancelOPDInvestigation(M.parameter.service_id, M.parameter.investigationmaster_id, M.parameter.price, M.parameter.appoinment_code, M.parameter.invpriceid, hoscode, usercode, M.parameter.cancel_reason);
            return Json(result.ToString(), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetAllRegesterAppoinmentLists(DateTime? fromdate, DateTime? todate, int sationid, string hosdepcode, string doccode)
        {
            MainNurseModel model = new MainNurseModel();
            model.Nurse = new NurseModel();

            var dataset = NusMaster.GetAllRegesterAppoinmentLists(fromdate, todate, Convert.ToString(Session["ClinicCode"]), sationid, hosdepcode, doccode);
            model.Nurse.RegisterAppointmentlst = Extend.ToList<DocAppintment_Entity>(dataset.Tables[0]);
            model.Nurse.dpartmentlst = Extend.ToList<HospitalDepartment_Entity>(dataset.Tables[1]);
            model.investigationPriceList = Extend.ToList<investigationparaamount>(dataset.Tables[2]);
            model.Nurse.DoctorList = Extend.ToList<Doctor_Entity>(dataset.Tables[3]);
            model.Nurse.Nurselstation = new List<Nurse_Entity>();
            // model.Nurse.Vitallst = new List<patientvital_Entity>();
            return PartialView("HMSRegPatientAppointmentList", model);
        }

        public ActionResult DeleteRegAppointment(string RegAppointmentcode)
        {
            bool result = false;
            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = NusMaster.DeleteRegisterAppointment(Convert.ToString(RegAppointmentcode), hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RoleAssign(List<DepartmentMapping> Data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("station_id", typeof(int));
            dt.Columns.Add("Hos_department_code", typeof(string));
            dt.Columns.Add("Hos_code", typeof(string));
            if (Data != null)
            {
                foreach (var item in Data)
                {
                    DataRow dr = dt.NewRow();
                    dr["station_id"] = item.StationId;
                    dr["Hos_department_code"] = item.Hos_department_code;
                    dr["Hos_code"] = Convert.ToString(Session["ClinicCode"]);

                    dt.Rows.Add(dr);
                }
            }
            var result = NusMaster.SaveRoleAcess(dt);
            return Json(result == true ? "1" : "0", JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetDataAccess(int sationid)
        {
            NurseModel model = new NurseModel();
            model.dpartmentlst = NusMaster.GetAll(Convert.ToString(Session["ClinicCode"]), sationid);
            model.Nurselstation = new List<Nurse_Entity>();
            return PartialView("HMSNurseMapping", model);
        }

        public PartialViewResult AddNurseStation(NurseModel Station)
        {
            NurseModel model = new NurseModel();
            if (ModelState.IsValid)
            {
                var entity = SetEntityData(Station);
                var res = NusMaster.Savenursestation(entity);
                model.Nurselstation = Extend.ToList<Nurse_Entity>(res).ToList();
            }
            return PartialView("HMSNurseMapping", model);
        }
        public Nurse_Entity SetEntityData(NurseModel station)
        {
            ne.station_id = station.NurseSationId;
            ne.nurse_station_name = station.NurseSationName;
            ne.hos_code = Convert.ToString(Session["ClinicCode"]);
            ne.user_code = Convert.ToString(Session["UserCode"]);
            ne.ctrl = station.Ctrl;
            return ne;
        }
        [HttpPost]
        public JsonResult GetAllVital()
        {
            DataTable dt = VitMaster.GetAllVital(Convert.ToString(Session["ClinicCode"]));
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Savetodayvitals(MainNurseModel Data)
        {
            if (Data.VitalPara.VitalList != null && Data.VitalPara.VitalList.Count > 0)
            {
                DataTable dt = Data.VitalPara.VitalList.ToDataTable();
                Data.VitalPara.HOScode = Convert.ToString(Session["ClinicCode"]);
                bool status = VitMaster.InsertTodayVital(Data.VitalPara.Appointmentcode, Data.VitalPara.HOScode, Data.VitalPara.PatientCode,
                                                     Data.VitalPara.Remarks, dt);
                Data.VitalPara.Status = status == true ? "1" : "0";
            }
            else
            {
                Data.VitalPara.Status = "2";
            }

            return Json(Data.VitalPara, JsonRequestBehavior.AllowGet);
        }

        //public PartialViewResult GetVitalPara(string PatientCode, string Appointmentcode)
        //{
        //    MainNurseModel model = new MainNurseModel();
        //    model.Nurse = new NurseModel();
        //    model.vital = new vitalpara();
        //    model.MainVital = new MainVitalPara();
        //    DataSet ds = VitMaster.GetAll_vitalpara(PatientCode, Appointmentcode, Convert.ToString(Session["ClinicCode"]));
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        model.vital.BodyTemperature = Convert.ToDecimal(ds.Tables[0].Rows[0]["body_temperature"]);
        //        model.vital.BloodPressure = Convert.ToString(ds.Tables[0].Rows[0]["blood_pressure"]);
        //        model.vital.Weight = Convert.ToDecimal(ds.Tables[0].Rows[0]["weight"]);
        //        model.vital.Sugar = Convert.ToDecimal(ds.Tables[0].Rows[0]["sugar"]);
        //        model.vital.Oxygenlevel = Convert.ToDecimal(ds.Tables[0].Rows[0]["oxygen_level"]);
        //        model.vital.Heartrate = Convert.ToInt32(ds.Tables[0].Rows[0]["heart_rate"]);
        //        model.vital.Remarks = Convert.ToString(ds.Tables[0].Rows[0]["remarks"]);
        //        model.vital.PatientCode = Convert.ToString(PatientCode);
        //        model.vital.HOScode = Convert.ToString(Session["ClinicCode"]);
        //        model.vital.Appointmentcode = Convert.ToString(Appointmentcode);
        //        model.vital.ctrl = Convert.ToInt32(1);
        //        model.vital.status = "";
        //    }
        //    else
        //    {
        //        model.vital.BodyTemperature = Convert.ToDecimal(0);
        //        model.vital.BloodPressure = "";
        //        model.vital.Weight = Convert.ToDecimal(0);
        //        model.vital.Sugar = Convert.ToDecimal(0);
        //        model.vital.Oxygenlevel = Convert.ToDecimal(0);
        //        model.vital.Heartrate = Convert.ToInt32(0);
        //        model.vital.Remarks = Convert.ToString("");
        //        model.vital.PatientCode = Convert.ToString(PatientCode);
        //        model.vital.HOScode = Convert.ToString(Session["ClinicCode"]);
        //        model.vital.Appointmentcode = Convert.ToString(Appointmentcode);
        //        model.vital.ctrl = Convert.ToInt32(1);
        //        model.vital.status = "";
        //    }

        //return PartialView("HMSRegPatientAppointmentList", model);
        //}
        //[HttpPost]
        //public PartialViewResult InsertVitalPara(vitalpara para)
        //{

        //    MainNurseModel Modal = new MainNurseModel();
        //    Modal.Nurse = new NurseModel();
        //    Modal.vital = new vitalpara();
        //    PE.body_temperature = Convert.ToDecimal(para.BodyTemperature);
        //    PE.blood_pressure = Convert.ToString(para.BloodPressure);
        //    PE.weight = Convert.ToDecimal(para.Weight);
        //    PE.sugar = Convert.ToDecimal(para.Sugar);
        //    PE.oxygenlevel = Convert.ToDecimal(para.Oxygenlevel);
        //    PE.heartrate = Convert.ToInt32(para.Heartrate);
        //    PE.remarks = Convert.ToString(para.Remarks);
        //    PE.patient_code = Convert.ToString(para.PatientCode);
        //    PE.hos_code = Convert.ToString(Session["ClinicCode"]);
        //    PE.user_code = Convert.ToString(Session["UserCode"]);
        //    PE.appointment_code = Convert.ToString(para.Appointmentcode);
        //    PE.ctrl = 1;
        //    var res = VitMaster.AddPatientVital(PE);
        //    if (res.Rows.Count > 0)
        //    {
        //        Modal.vital.BodyTemperature = Convert.ToDecimal(res.Rows[0]["body_temperature"]);
        //        Modal.vital.BloodPressure = Convert.ToString(res.Rows[0]["blood_pressure"]);
        //        Modal.vital.Weight = Convert.ToDecimal(res.Rows[0]["weight"]);
        //        Modal.vital.Sugar = Convert.ToDecimal(res.Rows[0]["sugar"]);
        //        Modal.vital.Oxygenlevel = Convert.ToDecimal(res.Rows[0]["oxygen_level"]);
        //        Modal.vital.Heartrate = Convert.ToInt32(res.Rows[0]["heart_rate"]);
        //        Modal.vital.Remarks = Convert.ToString(res.Rows[0]["remarks"]);
        //        Modal.vital.PatientCode = Convert.ToString(res.Rows[0]["patient_code"]);
        //        Modal.vital.HOScode = Convert.ToString(res.Rows[0]["hos_code"]);
        //        Modal.vital.Appointmentcode = Convert.ToString(res.Rows[0]["appointment_code"]);
        //        Modal.vital.ctrl = Convert.ToInt32(1);
        //        Modal.vital.status = Convert.ToString(res.Rows[0]["status"]);
        //    }
        //    return PartialView("HMSRegPatientAppointmentList", Modal);
        //}
        //[HttpPost]
        //public ActionResult InsertMainVitalPara(MainVitalPara para)
        //{
        //    string status = "";
        //    MainNurseModel Modal = new MainNurseModel();
        //    Modal.Nurse = new NurseModel();
        //    Modal.vital = new vitalpara();
        //    Modal.MainVital = new MainVitalPara();
        //    fe.father_Medical_history = Convert.ToString(para.FatherMedicalHistory);
        //    fe.mother_medical_history = Convert.ToString(para.MotherMedicalHistory);
        //    //fe.allergies = Convert.ToString(para.Allergies);
        //    fe.patient_code = Convert.ToString(para.patientCode);
        //    fe.hoscode = Convert.ToString(Session["ClinicCode"]);
        //    fe.appointment_code = Convert.ToString(para.Appointmentcode);
        //    fe.Ctrl = 1;
        //    if (ModelState.IsValid)
        //    {
        //        var res = VitMaster.AddMainVitalPara(fe);
        //        if (res != "same")
        //        {
        //            if (res.ToInt() > 0 ? true : false)
        //            {
        //                status = "Record Added Successfully !";
        //            }
        //            else
        //                status = "Record Not Saved!";
        //        }
        //        else
        //            status = "Record Update Successfully !";
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult DeleteVitalPara(int VitalId)
        {
            bool result = false;
            var res = VitMaster.DeleteVitalpara(Convert.ToString(VitalId));
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteNurseStation(int StationId)
        {
            bool result = false;

            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = NusMaster.DeleteNurseStation(StationId, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult OPD_Patient_Transfer_OT(string appointment_code)
        {
            var Doc = NusMaster.GetAllOPDPatientDoctor(Convert.ToString(Session["ClinicCode"]));
            MainNurseModel model = new MainNurseModel();
            model.OPDOTTransfer = new AddOPDOTTransfer();
            model.OPDOTTransfer.operation_date = "";
            model.OPDOTTransfer.operation_time = "";
            model.OPDOTTransfer.operation_type = "";
            model.OPDOTTransfer.OPDDoctor_lst = Extend.ToList<Doctor_list>(Doc.Tables[0]);
            model.OPDOTTransfer.appointment_code = appointment_code;
            return PartialView("_OPDPatientOTPopUp", model);
        }
        public JsonResult Add_OPD_OTTransfer(MainNurseModel DataI)
        {
            var usercode = Convert.ToString(Session["UserCode"]);
            var result = NusMaster.AddOPDPatientOTTransfer(DataI.OPDOTTransfer.doctor_code, DataI.OPDOTTransfer.operation_date, DataI.OPDOTTransfer.operation_time, "OPDOT", DataI.OPDOTTransfer.appointment_code, Convert.ToString(Session["ClinicCode"]), usercode);
            return Json(result == true ? DataI.OPDOTTransfer.appointment_code : "0", JsonRequestBehavior.AllowGet);
        }

     

    



    }
}