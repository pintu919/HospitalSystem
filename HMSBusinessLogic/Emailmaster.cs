using HMS.BizLayer;
using HMS.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



namespace HMS.BizLogic
{
    public class Email_Entity
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ToEmail { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string FromEmail { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Body { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Subject { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string CCEmail { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_UserName { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_PassWord { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_Name { get; set; }
    }

    public class Email_Entity_withattachment
    {
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string ToEmail { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string FromEmail { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Body { get; set; }
        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string Subject { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string appointment_code { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public string bodyhtml { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public byte[] memorystram { get; set; }

        [SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        public DataTable dt { get; set; }


        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_UserName { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_PassWord { get; set; }
        //[SqlKey(Display = true, SqlParamInclusion = SqlParamInclusion.Exclude)]
        //public string Cliniq_Name { get; set; }
    }
    public sealed class Email_Master : BLayer
    {
        public Email_Master(BsInfo info, DConnection cn = null) : base(info, cn) { }
        public string SendEmail(Email_Entity entity)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(entity.ToEmail);
                mail.From = new MailAddress("noreply@amaderdoctor.com","AmaderDoctor");
                if (entity.CCEmail != null && entity.CCEmail != "")
                {
                    mail.CC.Add(new MailAddress(entity.CCEmail));
                }
                mail.Bcc.Add(new MailAddress("wwanowar@gmail.com"));
                mail.Subject = entity.Subject;
                string Body = entity.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.rediffmailpro.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("noreply@amaderdoctor.com", "Jmjc90dA"); // senders User name and password   
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string SendEmailwithAttchment(Email_Entity_withattachment entity)
        {
            string Body = "";
            try
            {

                //   byte[] data = (byte[])entity.dt.Rows[0]["Filebyte"];// ds.Tables[0].Rows[0][2];
               
                int k = entity.dt.Rows.Count;

                MailMessage mail = new MailMessage();
                mail.To.Add(entity.ToEmail);
                mail.From = new MailAddress("noreply@amaderdoctor.com", "AmaderDoctor");
                mail.Bcc.Add(new MailAddress("wwanowar@gmail.com"));
                var ms = new MemoryStream();
                if (entity.dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= entity.dt.Rows.Count-1; i++)
                    {
                        byte[] data = (byte[])entity.dt.Rows[i]["Filebyte"];
                      
                        mail.Attachments.Add(new Attachment(new MemoryStream(data), "InvestigationReport" + i + ".pdf", "application/pdf"));
                        
                        
                    }
                }
               
                mail.Subject = entity.Subject;
                 Body = entity.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.rediffmailpro.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("noreply@amaderdoctor.com", "Jmjc90dA"); // senders User name and password   
                smtp.EnableSsl = true;
                smtp.Send(mail);
                AddHOSEmailHistory(entity.appointment_code, entity.bodyhtml, entity.ToEmail, 1);
                return "1";
                
            }
            catch (Exception ex)
            {
                AddHOSEmailHistory(entity.appointment_code, entity.bodyhtml, entity.ToEmail, 0);

                return ex.Message;
            }

           

        }

        public int AddHOSEmailHistory(string appointmentcode, string body, string toemail, int sendsucess )
        {
            var param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", "insertemail_history");
            param[1] = new SqlParameter("@code", appointmentcode);
            param[2] = new SqlParameter("@type", "InvestigationReport");
            param[3] = new SqlParameter("@subject", "InvestigationReport");
            param[4] = new SqlParameter("@emailtype", "patient_investigation");
            param[5] = new SqlParameter("@body", body);
            param[6] = new SqlParameter("@toemail", toemail);
            param[7] = new SqlParameter("@sendsucess", sendsucess);
            param[8] = new SqlParameter("@err", SqlDbType.VarChar, 1000);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[9].Direction = ParameterDirection.ReturnValue;
            ApplyChanges("crd_Insert_Email_history", param.ToArray(), Actions.Add);
            return param[9].Value.ToInt() > 0 ? 1 : 0;
        }

    }
}
