using HMS.BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagement.Models;
using HMS.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.Drawing;
using System.IO;
using System.Text;
using HospitalManagement.Helper;

namespace HospitalManagement.Controllers
{
    [CustomHandleError]
    public class LoginController : _AuthBase
    {
        // GET: Login
        readonly Doctor_Master doc;
        readonly logininfo_Entity entity;
        readonly login_info cltMaster;
        readonly Common_Master SpMaster;
        
        public LoginController()
        {
            entity = new logininfo_Entity();
            cltMaster = new login_info(bsInfo);
            doc = new Doctor_Master(bsInfo);
            SpMaster = new Common_Master(bsInfo);
        }
        public ActionResult Index()
        {

            if (Session["ClinicCode"] != null && Session["UserCode"] != null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                LoginModel model = new LoginModel();
                if (Request.Cookies["UserCode"] != null && Request.Cookies["Username"] != null && Request.Cookies["Pwd"] != null)
                {
                    model.HosCode = Request.Cookies["UserCode"].Value;
                    model.Username = Request.Cookies["Username"].Value;
                    model.Pwd = Request.Cookies["Pwd"].Value;
                }
                return View("Index", model);
            }
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            string clientCaptcha = model.Capcha;
            string serverCaptcha = Convert.ToString(Session["CAPTCHA"]);
            if (!clientCaptcha.Equals(serverCaptcha))
            {
                TempData["message"] = "Sorry, please write valid capcha code.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (model.Username != null && model.Pwd != null)
                {
                    string Email_enc = cltMaster.EncString(model.Username);
                    entity.Cliniq_Code = model.HosCode;
                    entity.clinic_username = model.Username;
                    entity.hosting_url = Request.Url.Scheme + "://" + Request.Url.Authority;
                    if (Request.Url.Authority == "localhost:52564")
                    {
                        entity.hosting_url = "http://getmycheckup.com";
                        //entity.hosting_url = "http://devhp.amaderdoctor.com";
                    }
                    entity.clinic_password = cltMaster.EncString(model.Pwd); // model.Pwd;
                    int auth = 0;
                    auth = cltMaster.Auth(entity);
                    if (auth == 1)
                    {
                        var m = cltMaster.getDetailsByUser(entity);
                        logininfo_Entity LE = Extend.ToList<logininfo_Entity>(m.Tables[0]).SingleOrDefault();
                        if (m != null && LE.clinic_username != null)
                        {
                            GetLogDetails(entity.Cliniq_Code, Email_enc, entity.clinic_password, model.RememberMe, true,"Login Success");
                            RememberMe(model);
                            var menulst = Extend.ToList<HpMenu_Entity>(m.Tables[1]).OrderBy(o => o.hp_seqno).ToList();
                            Session["AssignHospitalMenuList"] = menulst;
                            Session["UserName"] = LE;
                            Session["ClinicCode"] = LE.Cliniq_Code;
                            Session["clinic_name"] = LE.Cliniq_Name;
                            Session["clinic_addr"] = LE.Cliniq_Addr;
                            Session["UserCode"] = LE.Code;
                            Session["user_image"] = LE.clinic_logo;
                            Session["user_feviconicon"] = LE.clinic_feviconicon;
                            Session["HtmlMenu"] = CustomHelper.RenderViewToString(ControllerContext, "~/Views/Shared/_Menu.cshtml", menulst, true);
                            SetTicket(model);
                            return RedirectToAction("Dashboard", "Dashboard");
                        }
                        else
                        {
                            GetLogDetails(entity.Cliniq_Code, Email_enc, entity.clinic_password, model.RememberMe, false, "UserName or Password Incorrect!!");
                            TempData["message"] = "UserName or Password Incorrect!!";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    else if (auth == 2)
                    {
                        TempData["message"] = "hosting Url Incorract !!";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        GetLogDetails(entity.Cliniq_Code, Email_enc, entity.clinic_password, model.RememberMe, false, "UserName or Password Incorrect!!");
                        TempData["message"] = "UserName or Password Incorrect!!";
                        return RedirectToAction("Index", "Login");
                    }                    
                }
                else
                {
                    GetLogDetails(model.HosCode, model.Username,model.Pwd, model.RememberMe, false, "Enter UserName and Password!!");
                    TempData["message"] = "Enter UserName and Password!!";
                    return RedirectToAction("Index", "Login");
                }
            }
        }
        public void RememberMe(LoginModel Modal)
        {
            //RememberMe Code
            if (Modal.RememberMe)
            {
                Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Pwd"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Pwd"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Cookies["UserCode"].Value = Modal.HosCode.Trim();
            Response.Cookies["Username"].Value = Modal.Username.Trim();
            Response.Cookies["Pwd"].Value = Modal.Pwd.Trim();
            //End RememberMe Code
        }
        public ActionResult Signout()
        {
            ExpireTicket();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult ClinicChangePassword()
        {
            return View();
        }
        public ActionResult ChangePass(ChangePasswordmodel model)
        {
            bool authchange = false;
            if (model.oldPwd != null)
            {
                entity.Cliniq_oldpass = cltMaster.EncString(model.oldPwd);
                entity.Cliniq_Code = Convert.ToString(Session["ClinicCode"]);
                authchange = cltMaster.AuthChangePass(entity);
                if (authchange == true)
                {
                    entity.Cliniq_Newpass = cltMaster.EncString(model.newPwd);
                    entity.Cliniq_Code = Convert.ToString(Session["ClinicCode"]);
                    var res = cltMaster.ChangePass(entity);

                    if (res == true)
                    {
                        var emaildetails = cltMaster.getchangepassword(Convert.ToString(Session["ClinicCode"]), model.newPwd);
                        string msg = new Email_Master(bsInfo).SendEmail(emaildetails);
                        if (msg == "1")
                        {
                            TempData["message"] = "Username And Password Send To Email Address";
                            ModelState.Clear();
                        }
                        else
                            TempData["message"] = "Error When Send Email Address";
                    }
                    else
                    {
                        TempData["message"] = "Some Technically Problem Please Contact Admin !!!";
                    }
                }
                else
                {
                    TempData["message"] = "Please Enter Valid Old Password !!!";
                    ModelState.Clear();
                }
            }
            return View("ClinicChangePassword", model);
        }
        public ActionResult Forget(Forgetpass model)
        {
            bool authForget = false;
            if (model.Email != null)
            {
                entity.Cliniq_email = model.Email.Trim(); ;
                authForget = cltMaster.AuthForget(entity);
                if (authForget == true)
                {
                    string Password = GeneratePassword();
                    entity.clinic_password = cltMaster.EncString(Password);
                    entity.Cliniq_email = model.Email.Trim();
                    entity.clinic_username = model.Email.Trim();
                    var res = cltMaster.Update(entity);
                    if (res == true)
                    {
                        var emaildetails = cltMaster.getforgetpass(model.Email, Password);
                        string msg = new Email_Master(bsInfo).SendEmail(emaildetails);
                        if (msg == "1")
                            TempData["message"] = "Username And Password Send To Email Address";
                        else
                            TempData["message"] = "Error When Send Email Address";

                        TempData["message"] = "Forget Password Sucessfully !!!";
                    }
                    else
                    {
                        TempData["message"] = "Some Technically Problem Please Contact Admin !!!";
                    }
                }
                else
                {
                    TempData["message"] = "Please Enter Valid Email Address !!!";
                }
            }
            return RedirectToAction("ForgetPassword", "Login");
        }
        public string GeneratePassword()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(6); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }
        #region "Capcha Code"
        public FileResult GetCaptchaImage()
        {
            Session["CAPTCHA"] = GetRandomText();
            string text = Session["CAPTCHA"].ToString();
            int height = 60;
            int width = 60;
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15, FontStyle.Bold | FontStyle.Italic);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 35, (int)textSize.Height + 25);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.LightBlue;
            Color textColor = Color.Black;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);
            drawing.DrawRectangle(new Pen(Color.LightGray), 1, 1, width - 2, height - 2);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 4; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
        #endregion
        #region "Login track"
        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        public void GetLogDetails(string DoctorCode, string Email, string Password, bool Remember, bool IsLogin, string error_msg)
        {
            var ctx = System.Web.HttpContext.Current;
            string brows = ctx.Request.Browser.Browser;
            string os = ctx.Request.Browser.Platform;
            string channel = ctx.Request.Browser.IsMobileDevice ? "Device-" + ctx.Request.Browser.MobileDeviceModel : "Web";
            string Sys_logon = ctx.Request.LogonUserIdentity.Name.ToString();
            var ip = GetIp();//ctx.Request["HTTP_X_FORWARDED_FOR"];
            //if (!string.IsNullOrWhiteSpace(ip))
            //{
            //    ip = ip.Split(',').Last().Trim();
            //}
            //if (string.IsNullOrWhiteSpace(ip))
            //{
            //    ip = ctx.Request.UserHostAddress;
            //}
            string UserValue = "logincode: " + DoctorCode + "," + "Email: " + Email + "," + "Pass: " + Password + "," + "RememberMe: " + Remember;
            string UserType = "Hospital";
            string HostingAddress = ctx.Request.Url.AbsoluteUri;
            string userlocation = null;
            string lat = null;
            string longitude = null;
            if (ctx.Request.Url.Host != "localhost")
            {
                try
                {
                    LocationModel location = new LocationModel();
                    string url = string.Format("https://ip-api.io/json/{0}", ip); //string.Format("https://api.ipgeolocationapi.com/geolocate/{0}", ip);
                    using (WebClient client = new WebClient())
                    {
                        string json = client.DownloadString(url);
                        location = new JavaScriptSerializer().Deserialize<LocationModel>(json);
                    }
                    userlocation = location.country_name + "(" + location.Country_Code + ")" + "," + location.region_name + "(" + location.region_code + ")" + "," + location.city + "-" + location.zip_code;
                    lat = location.latitude;
                    longitude = location.longitude;
                }
                catch { }
            }
            new Common_Master(bsInfo).Insert_Login_log_history(brows, os, channel, Sys_logon, ip, UserValue, UserType, HostingAddress, userlocation, lat, longitude, IsLogin, error_msg);
        }
        

        public class LocationModel
        {
            public string Country_Code { get; set; }
            public string country_name { get; set; }
            public string region_name { get; set; }
            public string city { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string zip_code { get; set; }
            public string region_code { get; set; }
        }

        #endregion
        public ActionResult DoctorVerification()
        {

            if (Request.Path.Split('/').Length > 3)
            {
                string Uniqrow = Request.Path.Split('/')[3];
                if (Uniqrow != "")
                {

                    string pass = GeneratePassword();
                    string de_password = pass;
                    string enc_password = new user_info(bsInfo).EncString(pass);

                    var res = doc.Doctor_Verify(Uniqrow, "Doctor", de_password, enc_password);
                    if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmessage"]) == 1)
                    {
                        var email = Extend.ToList<Email_Entity>(res.Tables[0]).SingleOrDefault();
                        string msg = new Email_Master(bsInfo).SendEmail(email);
                        if (msg == "1")
                            SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["code"]), "doctor", email.Subject, "hospital doctor verify", email.Body, email.ToEmail, 1);
                        else
                            SpMaster.Insert_Email_history(Convert.ToString(res.Tables[0].Rows[0]["code"]), "doctor", email.Subject, "hospital doctor verify", email.Body, email.ToEmail, 0);
                        TempData["verifiedStatus"] = "Your Account verify Success !";
                    }
                    else if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmessage"]) == 2)
                    {
                        TempData["verifiedStatus"] = "Your Account allrady verified !";
                    }
                    else if (res.Tables.Count > 0 && Convert.ToInt16(res.Tables[0].Rows[0]["statusmessage"]) == 0)
                    {
                        TempData["verifiedStatus"] = "Your Account not verified !";
                    }
                }
                else
                    TempData["verifiedStatus"] = "Sorry you are trying in wrong direction !";
            }
            else
                TempData["verifiedStatus"] = "Sorry you are trying in wrong direction !";

            return View("DoctorVerification");
        }

        public void LogError(string ErrorPageName, Exception ex, string Type)
        {
            SpMaster.Insert_Error_log_history(ex.Message, ErrorPageName, ex.StackTrace, Type);
        }
    }
}