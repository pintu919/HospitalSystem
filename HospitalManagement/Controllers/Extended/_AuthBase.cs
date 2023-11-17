using HMS;
using HospitalManagement.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using HMS.Data;
using System.Configuration;

namespace HospitalManagement.Controllers
{
    public abstract class _AuthBase : Controller
    {
        protected BsInfo bsInfo;
        public _AuthBase()
        {
            var ctx = System.Web.HttpContext.Current.Session;

            var db = ConfigurationManager.AppSettings["db"];

            DbConnection dbCon = DbConnection.Development;
            if (db == "staging") dbCon = DbConnection.Staging;
            if (db == "development") dbCon = DbConnection.Development;
            if (db == "production") dbCon = DbConnection.Production;
            bsInfo = new BsInfo(dbCon);
        }
        public void SetTicket(LoginModel model)
        {

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(model);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
            model.HosCode,
            DateTime.Now,
            DateTime.Now.AddMinutes(1440),
            true,
            userData,
            FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie("HMSCook", encTicket));
        }
        public void ExpireTicket()
        {
            // Delete the user details from cache.
            Session.Abandon();
            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();
            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie("HMSCook", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
        }
    }
}