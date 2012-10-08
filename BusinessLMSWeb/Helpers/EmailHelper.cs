using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BusinessLMSWeb.Helpers
{
    public class EmailHelper
    {

        private SmtpClient smtpClient;
        private MailMessage emailMsg;
        public EmailHelper()
        {
            smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("luismgonzalezb@gmail.com", "I290982d");
            smtpClient.EnableSsl = true;
        }

        public bool SendResetMail(string toEmail, string token)
        {
            try
            {
                emailMsg = new MailMessage();
                emailMsg.From = new MailAddress("luismgonzalezb@gmail.com", "Business LMS Support");
                emailMsg.To.Add(new MailAddress(toEmail));
                emailMsg.Subject = "Business LMS Password Reset";
                emailMsg.Body = ResertMailBody(token);
                emailMsg.IsBodyHtml = true;
                emailMsg.Priority = MailPriority.Normal;
                smtpClient.Send(emailMsg);
                return true;
            } catch { }
            return false;
        }

        public string ResertMailBody(string token)
        {
            string mailBody = String.Concat("<html><body>Reset Password <a href='http://", 
                HttpContext.Current.Request.Url.Host, "/Account/PasswordReset?token=", 
                HttpUtility.HtmlEncode(token), "'>Here</a></body></html>");
            return mailBody;
        }

    }
}