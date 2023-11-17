using HMS;
using HMS.BizLogic;
using HospitalManagement;
using HospitalManagement.Helper;
using System.Linq;

namespace HospitalManagement.Controllers
{
    [Authenticate]
    public abstract class _Base : _AuthBase
    {
        public _Base()
        {
            var ctx = System.Web.HttpContext.Current;
            var Req = ctx.Request;
            var uAgent = Req.UserAgent;

            
            bsInfo.Browser = Req.Browser.Browser;
            bsInfo.Os = Req.Browser.Platform;
            bsInfo.Channel = Req.Browser.IsMobileDevice ? "Device-" + Req.Browser.MobileDeviceModel : "Web";

            bsInfo.SystemUser = ctx.User.Identity.Name;
            var logon = Req.LogonUserIdentity.Name.ToString();

            var ip = Req["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrWhiteSpace(ip))
            {
                ip = ip.Split(',').Last().Trim();
            }

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = Req.UserHostAddress;
            }

            bsInfo.IP = ip;

            if (ctx.Session["UserName"] != null)
            {
                var ent = ctx.Session["UserName"] as logininfo_Entity;
                bsInfo.UserCode = ent.Cliniq_Code;
                bsInfo.LogUser = ent.Cliniq_ID;
            }

            //bsInfo.LogTime = ctx.Request.Cookies["HMSCook"].Expires.AddHours(-1);

        }
    }
}