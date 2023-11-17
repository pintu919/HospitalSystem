using HMS.BizLogic;
using HMS.Data;
using HospitalManagement.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class AccountHeadTreeviewController : _Base
    {
        // GET: AccountHeadTreeview
        readonly AccountHead_Master acc_master;
        public AccountHeadTreeviewController()
        {
            acc_master = new AccountHead_Master(bsInfo);
        }
        public ActionResult AccountHeadTreeview()
        {
            DataTable dt = acc_master.GetAllAccountHead(Convert.ToString(Session["ClinicCode"]));
            List<HeadCategory> categories = dt.ToList<HeadCategory>();
            return View(categories);
        }
    }
}