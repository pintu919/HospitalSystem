using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace HospitalManagement.Helper
{

    public class AuthenticateAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var ctx = filterContext.HttpContext;
        //    var authCookie = ctx.Request.Cookies["HMSCook"];
        //    try
        //    {
        //        bool isRedirectReq = false;
        //        if (authCookie != null)
        //        {
        //            // Get the forms authentication ticket.
        //            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //            if (authTicket.Expired)
        //            {
        //                if (authTicket.IsPersistent)
        //                {
        //                    ctx.Request.Cookies.Remove("HMSCook");
        //                }
        //                isRedirectReq = true;
        //            }
        //        }
        //        if (isRedirectReq || authCookie == null)
        //        {
        //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = ""}));
        //        }
        //    }
        //    catch { }
        //    base.OnActionExecuting(filterContext);
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = filterContext.HttpContext;
            var authCookie = ctx.Request.Cookies["HMSCook"];
            try
            {
                //Add for Authorization
                List<HMS.BizLogic.HpMenu_Entity> menulst = (List<HMS.BizLogic.HpMenu_Entity>)ctx.Session["AssignHospitalMenuList"];
                var HP_Actionname = ctx.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
                bool IsAuthorize = true;
                if (menulst != null && Convert.ToString(ctx.Session["UserCode"]).Split('-')[0] != "HP")
                    IsAuthorize = Convert.ToInt16(Convert.ToString(menulst.Where(u => u.hp_actionname.ToLower() == HP_Actionname).ToList().Count)) > 0 ? true : false;
                //End
                bool isRedirectReq = false;
                if (authCookie != null )
                {
                    // Get the forms authentication ticket.
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket.Expired)
                    {
                        if (authTicket.IsPersistent)
                        {
                            ctx.Request.Cookies.Remove("HMSCook");
                        }
                        isRedirectReq = true;
                    }
                }
                if (isRedirectReq || authCookie == null || ctx.Session["UserName"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
                }
                if (!IsAuthorize)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "UnAurthorize", action = "UnAurthorize", area = "" }));
                }
            }
            catch { }
            base.OnActionExecuting(filterContext);
        }





    }
}