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
    public class AccountHeadController : _Base
    {
        readonly AccountHead_Master acc_master;
        readonly AccountHead_Entity en;
        readonly AccountLedger_Entity led;
        public AccountHeadController()
        {
            acc_master = new AccountHead_Master(bsInfo);
            en = new AccountHead_Entity();
            led = new AccountLedger_Entity();
        }
        // GET: AccountHead
        public ActionResult AccountHead()
        {
            AccountHeadModel Model = new AccountHeadModel();
            Model.HeadTypeLst = new List<AccountHeadTypeDto>();
            DataTable dt = acc_master.GetHeadType(Convert.ToString(Session["ClinicCode"])).Tables[0];
            Model.HeadTypeLst = BuildTrees(0,dt.ToList<AccountHeadTypeDto>());
            Model.ctrl = Convert.ToInt32(1);
            return View("AccountHead",Model);
        }
        //public List<AccountHeadTypeDto> BuildTrees(List<AccountHeadTypeDto> lst)
        //{
        //    var dtos = lst.Select(c => new AccountHeadTypeDto
        //    {
        //        Id = c.Id,
        //        Title = c.Title,
        //        ParentId = c.ParentId,
        //    }).ToList();
        //    return BuildTrees(0, lst);
        //}
        public List<AccountHeadTypeDto> BuildTrees(int? pid, List<AccountHeadTypeDto> lst)
        {
            var subs = lst.Where(c => c.parentId == pid).ToList();
            if (subs.Count() == 0)
            {
                return new List<AccountHeadTypeDto>();  // required an empty list instead of a null ! 
            }
            foreach (var i in subs)
            {
                i.subs = BuildTrees(i.id, lst);
            }
            return subs;
        }
        public ActionResult AddAccountHead(AccountHeadModel Acchead)
        {
            string Status = "";
            if (ModelState.IsValid)
            {
                if (Acchead.ParentId > 0)
                {
                    var entity = SetEntityData(Acchead);
                    var res = acc_master.Add(entity);
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
                }
                else
                {
                    Status = "Please Select HeadType!.";
                }
                ModelState.Clear();
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public AccountHead_Entity SetEntityDataEdit(AccountHeadModel Acc)
        {
            en.head_name = Convert.ToString(Acc.HeadName);
            en.parent_id = Convert.ToInt32(Acc.HeadId);
            en.show = 1;
            en.head_type = Convert.ToString(Acc.HeadType);
            en.is_editable = 1;
            en.donotshowtoall = 1;
            en.ctrl = Convert.ToInt32(Acc.ctrl);
            en.hos_code = Convert.ToString(Session["ClinicCode"]);
            en.user_code = Convert.ToString(Session["UserCode"]);
            return en;
        }
        public AccountHead_Entity SetEntityData(AccountHeadModel Acc)
        {
            en.head_name = Convert.ToString(Acc.HeadName);
            en.parent_id = Convert.ToInt32(Acc.ParentId);
            en.show = 1;
            en.head_type = Convert.ToString(Acc.HeadType);
            en.is_editable = 1;
            en.donotshowtoall = 1;
            en.ctrl = Convert.ToInt32(Acc.ctrl);
            en.hos_code = Convert.ToString(Session["ClinicCode"]);
            en.user_code = Convert.ToString(Session["UserCode"]);
            return en;
        }
        public ActionResult AccountHeadList()
        {
            AccountHeadModel Model = new AccountHeadModel();
            Model.HeadTypeList = new List<AccountHeadType_Entity>();
            Model.HeadTypeList = acc_master.GetAllHeadTypeList(Convert.ToString(Session["ClinicCode"])).OrderBy(o => o.head_id).ToList();
            return View("AccountHeadList", Model);
        }
        public ActionResult EditAccountHead()
        {
            AccountHeadModel Model = new AccountHeadModel();
            Model.HeadTypeList = new List<AccountHeadType_Entity>();
            var code = Convert.ToString(Session["UniqRow"]);
            var hos_code = Convert.ToString(Session["ClinicCode"]);
            var DataSet = acc_master.GetHeadTypeByCode(Convert.ToInt32(code), hos_code);
            Model.HeadTypeList = Extend.ToList<AccountHeadType_Entity>(DataSet.Tables[0]);
            var CModel = Extend.ToList<AccountHeadType_Entity>(DataSet.Tables[1]).SingleOrDefault();
            Model.ParentId = CModel.parent_id;
            Model.HeadId = CModel.head_id;
            Model.HeadName = CModel.head_name;
            Model.ctrl = CModel.ctrl;
            Model.HosCode= Convert.ToString(Convert.ToString(Session["ClinicCode"]));
            return View("EditAccountHead", Model);
        }
        public ActionResult EditAccHead(AccountHeadModel model)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                var entity = SetEntityDataEdit(model);
                var res = acc_master.Update(entity);
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
        public ActionResult LedgerAccount()
        {
            AccountLedgerModel Model = new AccountLedgerModel();
            Model.HeadTypeList = new List<AccountHeadType_Entity>();
            Model.HeadTypeList = acc_master.GetAllHeadTypeList(Convert.ToString(Session["ClinicCode"])).OrderBy(o => o.head_id).ToList();
            Model.ctrl = 1;
            return View("LedgerAccount", Model);
        }
        public ActionResult AddLedgerAccount(AccountLedgerModel AccLedger)
        {
            string Status = "";
            if (ModelState.IsValid)
            {
                var entity = SetEntityLedgerData(AccLedger);
                var res = acc_master.AddLedger(entity);
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
                ModelState.Clear();
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public AccountLedger_Entity SetEntityLedgerData(AccountLedgerModel AccLed)
        {
            led.acc_head_id = Convert.ToInt32(AccLed.AccountHeadId);
            led.acc_name = Convert.ToString(AccLed.AccountName);
            led.special_account = Convert.ToString(AccLed.SpecialAccount);
            led.contact_person = Convert.ToString(AccLed.ContactPerson);
            led.telephone = Convert.ToString(AccLed.Telephone);
            led.mobile = Convert.ToString(AccLed.Mobile);
            led.email = Convert.ToString(AccLed.Email);
            led.is_show = Convert.ToInt32(1);
            led.hos_code = Convert.ToString(Session["ClinicCode"]);
            led.user_code = Convert.ToString(Session["UserCode"]);
            led.ctrl = Convert.ToInt32(AccLed.ctrl);
            led.acc_id = Convert.ToInt32(AccLed.acc_id);
            return led;
        }
        public ActionResult LedgerAccountList()
        {
            AccountLedgerModel Model = new AccountLedgerModel();
            Model.LedgerAccountList = new List<AccountLedger_Entity>();
            Model.LedgerAccountList = acc_master.GetAllLedgerAccount(Convert.ToString(Session["ClinicCode"])).OrderBy(o => o.acc_name).ToList();
            return View("LedgerAccountList", Model);
        }
        public ActionResult EditLedgerAccount()
        {
            AccountLedgerModel Model = new AccountLedgerModel();
            Model.HeadTypeList = new List<AccountHeadType_Entity>();
            var code = Convert.ToString(Session["UniqRow"]);
            Model.HeadTypeList = acc_master.GetAllHeadTypeList(Convert.ToString(Session["ClinicCode"])).OrderBy(o => o.head_id).ToList();
            var DataSet = acc_master.GetLedgerTypeByCode(Convert.ToInt32(code), Convert.ToString(Session["ClinicCode"]));
            var CModel = Extend.ToList<AccountLedger_Entity>(DataSet.Tables[0]).SingleOrDefault();
            Model.AccountName = CModel.acc_name;
            Model.SpecialAccount = CModel.special_account;
            Model.AccountHeadId = CModel.acc_head_id;
            Model.acc_id = CModel.acc_id;
            Model.ContactPerson = CModel.contact_person;
            Model.Telephone = CModel.telephone;
            Model.Mobile = CModel.mobile;
            Model.Email = CModel.email;
            Model.ctrl = CModel.ctrl;
            return View("EditLedgerAccount", Model);
        }
        public ActionResult EditLedgerAcc(AccountLedgerModel model)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                var entity = SetEntityLedgerData(model);
                var res = acc_master.UpdateLedger(entity);
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

        public ActionResult GeneralAccountEntry()
        {
            GeneralAccountEntry Model = new GeneralAccountEntry();
            Model.Journal_entry = new List<Journal_data>();
            Model.HeadTypeLst = new List<AccountHeadTypeDto>();
            DataSet ds = acc_master.GetHeadType(Convert.ToString(Session["ClinicCode"]));
            Model.HeadTypeLst = BuildTrees(0, ds.Tables[0].ToList<AccountHeadTypeDto>());
            // DataSet journaldata = acc_master.GetJournalEntry(Convert.ToString(Session["ClinicCode"]));
            //  Model.Journal_entry = Extend.ToList<Journal_data>(journaldata.Tables[0]);
            //Model.ctrl = Convert.ToInt32(1);
            return View("GeneralAccountEntry", Model);
        }

        [HttpGet]
        public ActionResult GetLedgerAccount(int HeadId)
        {
            List<AccountLedger_Entity> lst = new List<AccountLedger_Entity>();
            DataSet ds = acc_master.GetledgerAccount(Convert.ToString(Session["ClinicCode"]), HeadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lst.Add(new AccountLedger_Entity() { acc_id = Convert.ToInt32(ds.Tables[0].Rows[i]["acc_id"]), acc_name = Convert.ToString(ds.Tables[0].Rows[i]["acc_name"]) });
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
            //return lst;
        }

        public ActionResult GetAccountAmount(int HeadIdfrom, int LedgerIdFrom)
        {

            GeneralAccountEntry model = new GeneralAccountEntry();
            model.HeadTypeLst = new List<AccountHeadTypeDto>();
            model.Journal_entry = new List<Journal_data>();


            DataSet ds = acc_master.GetAccountAmount(HeadIdfrom, LedgerIdFrom, Convert.ToString(Session["ClinicCode"]));
            model.Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"]);
            model.Journal_entry = Extend.ToList<Journal_data>(ds.Tables[1]);
            return PartialView("GeneralAccountEntry", model);

        }

        public ActionResult AddJournalEntry(GeneralAccountEntry Entry)
        {
            int val;
            GeneralAccountEntry model = new GeneralAccountEntry();
            model.HeadTypeLst = new List<AccountHeadTypeDto>();
            model.Journal_entry = new List<Journal_data>();
            if (Entry.ParentIdTo != 0 && Entry.LedgerIdTo != 0)
            {

                var ds = acc_master.save_journal_entry(Convert.ToString(Session["ClinicCode"]), Convert.ToString(Session["UserCode"]), Entry.ParentIdFrom, Entry.ParentIdTo, Entry.LedgerIdFrom, Entry.LedgerIdTo, Entry.Particulars, out val);
                if (val > 0)
                {
                    model.Status = 1;
                    model.Journal_entry = Extend.ToList<Journal_data>(ds.Tables[0]);
                }
                else
                {
                    model.Status = 3;
                }
            }
            else
            {
                model.Status = 2;
            }
            return PartialView("GeneralAccountEntry", model);
        }

    }
}