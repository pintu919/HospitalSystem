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
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class HMSRoomsController : _Base
    {
        // GET: HMSRooms
        readonly RoomType_Master RoomMaster;
        readonly RoomType_Entity entity;
        readonly wardType_Entity WE;
        readonly Patient_Allotment_Entity et;
        public HMSRoomsController()
        {
            RoomMaster = new RoomType_Master(bsInfo);
            entity = new RoomType_Entity();
            WE = new wardType_Entity();
            et = new Patient_Allotment_Entity();
        }

        public ActionResult HMSRoomType()
        {
            RoomTypeModel model = new RoomTypeModel();
            var dataSet = RoomMaster.GetAll(Convert.ToString(Session["ClinicCode"]));
            model.RoomTypeList = Extend.ToList<RoomTypeList>(dataSet.Tables[0]);
            model.WardTypeList = Extend.ToList<wardType_Entity>(dataSet.Tables[1]);
            return View("HMSRoomType", model);
        }

        [HttpPost]
        public PartialViewResult AddRoomType(RoomTypeModel RTM)
        {
            RoomTypeModel model = new RoomTypeModel();

            DataTable dt = new DataTable();
            dt.Columns.Add("roomtype_id", typeof(int));
            dt.Columns.Add("ward_id", typeof(int));
            dt.Columns.Add("room_facility", typeof(string));
            dt.Columns.Add("no_of_room", typeof(string));
            dt.Columns.Add("no_of_bed", typeof(int));
            dt.Columns.Add("per_day_rent", typeof(decimal));
            dt.Columns.Add("facility", typeof(string));
            dt.Columns.Add("hos_code", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            string[] rooms = RTM.NoofRoom.Split(',');
         

            if (rooms != null)
            {
                foreach (var item in rooms)
                {
                    DataRow dr = dt.NewRow();
                    dr["roomtype_id"] = RTM.RoomTypeId;
                    dr["ward_id"] = RTM.WardId;
                    dr["room_facility"] = RTM.Roomfacility;
                    dr["no_of_room"] = Convert.ToString(item);
                    dr["no_of_bed"] = RTM.NoofBed;
                    dr["per_day_rent"] = Convert.ToDecimal(RTM.PerDayRent);
                    dr["facility"] = RTM.Facility;
                    dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
                    dr["ctrl"] = RTM.Ctrl;
                    dt.Rows.Add(dr);
                }
            }
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var user_code = Convert.ToString(Session["UserCode"]);
         
            var result = RoomMaster.Add(dt, hos_code, user_code,RTM.prefix,RTM.start_serial);

            //if (ModelState.IsValid)
            //{
            //    var entity = SetEntityData(RTM);
            //    var res = RoomMaster.Add(entity);
                model.WardTypeList = new List<wardType_Entity>();
                model.RoomTypeList = Extend.ToList<RoomTypeList>(result.Tables[0]).ToList();
            //    model.RoomTypeList = Extend.ToList<RoomType_Entity>(res.Tables[0]).ToList();

            //}
            return PartialView("HMSRoomType", model);
           
        }

        //public RoomType_Entity SetEntityData(RoomTypeModel RoomType)
        //{
        //    entity.roomtype_id = Convert.ToInt32(RoomType.RoomTypeId);
        //    entity.ward_id = RoomType.WardId;
        //    entity.room_facility = RoomType.Roomfacility;
        //    entity.no_of_room = Convert.ToString(RoomType.NoofRoom);
        //    entity.no_of_bed = Convert.ToInt32(RoomType.NoofBed);
        //    entity.per_day_rent = Convert.ToDecimal(RoomType.PerDayRent);
        //    entity.facility = RoomType.Facility;
        //    entity.hos_code = Convert.ToString(Session["ClinicCode"]);
        //    entity.ctrl = Convert.ToInt32(RoomType.Ctrl);
        //    return entity;
        //}

        public ActionResult DeleteRoomType(int WardId, string Roomfacility, string Noofroom)
        {
            bool result = false;

            RoomTypeModel model = new RoomTypeModel();

            DataTable dt = new DataTable();
            dt.Columns.Add("roomtype_id", typeof(int));
            dt.Columns.Add("ward_id", typeof(int));
            dt.Columns.Add("room_facility", typeof(string));
            dt.Columns.Add("no_of_room", typeof(string));
            dt.Columns.Add("no_of_bed", typeof(int));
            dt.Columns.Add("per_day_rent", typeof(decimal));
            dt.Columns.Add("facility", typeof(string));
            dt.Columns.Add("hos_code", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            string[] rooms = Noofroom.Split(',');
            if (rooms != null)
            {
                foreach (var item in rooms)
                {
                    DataRow dr = dt.NewRow();
                    dr["roomtype_id"] = 0;
                    dr["ward_id"] = WardId;
                    dr["room_facility"] = Roomfacility;
                    dr["no_of_room"] = Convert.ToString(item);
                    dr["no_of_bed"] = 0;
                    dr["per_day_rent"] = 0;
                    dr["facility"] = "";
                    dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
                    dr["ctrl"] = 0;
                    dt.Rows.Add(dr);
                }
            }
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = RoomMaster.DeleteRoomType(dt,hos_code, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRoomType(int WardId, string Roomfacility, string Noofroom, decimal RoomRent)
        {
            bool result = false;

            RoomTypeModel model = new RoomTypeModel();

            DataTable dt = new DataTable();
            dt.Columns.Add("roomtype_id", typeof(int));
            dt.Columns.Add("ward_id", typeof(int));
            dt.Columns.Add("room_facility", typeof(string));
            dt.Columns.Add("no_of_room", typeof(string));
            dt.Columns.Add("no_of_bed", typeof(int));
            dt.Columns.Add("per_day_rent", typeof(decimal));
            dt.Columns.Add("facility", typeof(string));
            dt.Columns.Add("hos_code", typeof(string));
            dt.Columns.Add("ctrl", typeof(int));
            string[] rooms = Noofroom.Split(',');
            if (rooms != null)
            {
                foreach (var item in rooms)
                {
                    DataRow dr = dt.NewRow();
                    dr["roomtype_id"] = 0;
                    dr["ward_id"] = WardId;
                    dr["room_facility"] = Roomfacility;
                    dr["no_of_room"] = Convert.ToString(item);
                    dr["no_of_bed"] = 0;
                    dr["per_day_rent"] = Convert.ToDecimal(RoomRent);
                    dr["facility"] = "";
                    dr["hos_code"] = Convert.ToString(Session["ClinicCode"]);
                    dr["ctrl"] = 0;
                    dt.Rows.Add(dr);
                }
            }
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var user_code = Convert.ToString(Session["UserCode"]);
            var res = RoomMaster.UpdateRoomType(dt, hos_code, user_code);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HMSPatientAllotment()
        {
            RoomTypeModel model = new RoomTypeModel();
            var dataSet = RoomMaster.GetAllotment(Convert.ToString(Session["ClinicCode"]));
            model.PatientAllotmentList = Extend.ToList<Patient_Allotment_Entity>(dataSet.Tables[0]);
            return View("HMSPatientAllotment", model);
        }

        public ActionResult RoomAllotment()
        {
            var admit_id = Convert.ToInt32(Session["UniqRow"]);
            AllotmentModel model = new AllotmentModel();
            var dataset = RoomMaster.GetRoomAllotment(Convert.ToString(Session["ClinicCode"]), admit_id);
            //model.Roomfacility = Convert.ToString(dataset.Tables[1].Rows[0]["room_facility"]);
            //model.RoomTypeList = Extend.ToList<RoomTypeList>(dataset.Tables[0]);
            model.RoomTypeList = new List<RoomTypeList>();
            model.ward_id = 0;
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            model.patientname = Convert.ToString(dataset.Tables[1].Rows[0]["patientname"]);
            model.doctorname = Convert.ToString(dataset.Tables[1].Rows[0]["doctorname"]);
            model.ctrl = 1;
            return View("RoomAllotment", model);
        }

        public PartialViewResult GetwardType(string roomfacility = "")
        {
            AllotmentModel model = new AllotmentModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();

            var dataset = RoomMaster.GetWards(Convert.ToString(Session["ClinicCode"]), roomfacility);
            model.RoomTypeList = Extend.ToList<RoomTypeList>(dataset.Tables[0]);



            return PartialView("RoomAllotment", model);
        }

        public PartialViewResult GetRooms(int roomtype_id, string Type, int roomno =0, string roomfacility ="")
        {
            AllotmentModel model = new AllotmentModel();
            model.RoomTypeList = new List<RoomTypeList>();
            model.RoomList = new List<Room_Entity>();
            model.BedList = new List<Bed_Entity>();
            
            if (Type == "rooms")
            {
                var dataset = RoomMaster.GetRooms(Convert.ToString(Session["ClinicCode"]), roomtype_id, roomfacility);
                model.RoomList = Extend.ToList<Room_Entity>(dataset.Tables[0]);

            }
            else if (Type == "beds")
            {
                var dataset = RoomMaster.GetBeds(Convert.ToString(Session["ClinicCode"]), roomtype_id, roomno);
                model.BedList = Extend.ToList<Bed_Entity>(dataset.Tables[0]);
            }
            return PartialView("RoomAllotment", model);
        }

        [HttpPost]
        public ActionResult UpdateRoomallotment(AllotmentModel AM)
        {
           
            AllotmentModel model = new AllotmentModel();
            string status = "";
            if (ModelState.IsValid)
            {
                var et = SetEntityallotment(AM);
                var res = RoomMaster.Updateallotment(et);
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
        public Patient_Allotment_Entity SetEntityallotment(AllotmentModel am)
        {
            et.roomtype_id = Convert.ToInt32(am.room_no);
            et.bed_no = Convert.ToInt32(am.bed_no);
            et.hos_code = Convert.ToString(Session["ClinicCode"]);
            et.user_code = Convert.ToString(Session["UserCode"]);
            et.ctrl = Convert.ToInt32(am.ctrl);
            et.admit_id = Convert.ToInt32(Session["UniqRow"]);
            return et;
        }

        public ActionResult HMSWardType()
        {
            WardTypeModel model = new WardTypeModel();
            var dataSet = RoomMaster.GetAllWard(Convert.ToString(Session["ClinicCode"]));
            model.WardTypeList = Extend.ToList<wardType_Entity>(dataSet.Tables[0]);
            model.ctrl = 1;
            return View("HMSWardType", model);
        }

        [HttpPost]
        public PartialViewResult AddWardType(WardTypeModel WTM)
        {
            WardTypeModel model = new WardTypeModel();
                var WE = SetEntityWard(WTM);
            var res = RoomMaster.AddWardType(WE);
                model.WardTypeList = Extend.ToList<wardType_Entity>(res).ToList();
            return PartialView("HMSWardType", model);
        }

        public wardType_Entity SetEntityWard(WardTypeModel WardType)
        {
            WE.ward_id = Convert.ToInt32(WardType.WardId);
            WE.ward_type = WardType.WardType;
            WE.hos_code = Convert.ToString(Session["ClinicCode"]);
            WE.user_code = Convert.ToString(Session["UserCode"]);
            WE.ctrl = Convert.ToInt32(WardType.ctrl);
            return WE;
        }

        public ActionResult DeleteWardType(int WardId)
        {
            bool result = false;

            var hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            var res = RoomMaster.DeleteWardType(WardId, hoscode, usercode);
            if (Convert.ToBoolean(res) == true)
                result = true;
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdmitPatientapproval()
        {

            RoomTypeModel model = new RoomTypeModel();
            var dataSet = RoomMaster.GetAllotmentApproval(Convert.ToString(Session["ClinicCode"]));
            model.PatientAllotmentList = Extend.ToList<Patient_Allotment_Entity>(dataSet.Tables[0]);
            return View("AdmitPatientapproval", model);
           

        }

        
        public ActionResult ApprovepPtientAdmission(string admitid, string gardianname, string mobile)
        {
            int result = 0;
            string hoscode = Convert.ToString(Session["ClinicCode"]);
            var usercode = Convert.ToString(Session["UserCode"]);
            result = RoomMaster.ApprovepPtientAdmission(admitid,gardianname,mobile, hoscode, usercode);
            return Json(result == 1 ? "1" : result == 2 ? "2" : "0", JsonRequestBehavior.AllowGet);

        }


    }
}