using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using PostmarkDotNet;

namespace BusinessLMS.Helpers
{
    public class EmailHelper
    {
        public enum EmailType
        {
            subscription,
            resetEmail
        }

        private string serverToken { 
            get {
                return ConfigurationManager.AppSettings["postmarkKey"];
            } 
        }

        private string fromEmail { 
            get {
                return ConfigurationManager.AppSettings["postmarkFrom"];
            }
        }

        private string urlBase {
            get {
                return ConfigurationManager.AppSettings["urlBase"];
            }
        }

        public bool SendEmail(string name, string to, string data, EmailType emailType)
        {
            try
            {
                string Subject = "";
                string HtmlBody = "";
                string TextBody = "";
                switch (emailType)
                {
                    case EmailType.subscription:
                        Subject = "Welcome to IBO Virtual";
                        HtmlBody = GetSubscriptionHtml(name);
                        TextBody = GetSubscriptionText(name);
                        break;
                    case EmailType.resetEmail:
                        Subject = "IBO Virtual Reset Password";
                        HtmlBody = GetResetMailHtml(name, data);
                        TextBody = GetResetMailText(name, data);
                        break;
                    default:
                        break;
                }
                PostmarkMessage message = new PostmarkMessage
                {
                    From = fromEmail,
                    To = to,
                    Subject = Subject,
                    HtmlBody = HtmlBody,
                    TextBody = TextBody,
                    ReplyTo = fromEmail,
                    Headers = new NameValueCollection { { "CUSTOM-HEADER", "value" } }
                };
                PostmarkClient client = new PostmarkClient(serverToken);
                PostmarkResponse response = client.SendMessage(message);
                if (response.Status != PostmarkStatus.Success)
                {
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        private string GetSubscriptionHtml(string name)
        {
            TagBuilder body = new TagBuilder("div");
            body.InnerHtml = string.Concat("Thanks ",name," for joining IBO Virtual, together we are going to change your life");
            return body.ToString();
        }

        private string GetSubscriptionText(string name)
        {
            string body = string.Concat("Thanks ",name, " for joining IBO Virtual, together we are going to change your life");
            return body;
        }

        private string GetResetMailHtml(string name, string token)
        {
            string mailBody = string.Concat("<html><body>Hi ",name," Reset Password <a href='",
                urlBase, "/Account/PasswordReset?token=",HttpUtility.HtmlEncode(token), "'>Here</a></body></html>");
            return mailBody;
        }

        private string GetResetMailText(string name, string token)
        {
            string mailBody = string.Concat("Hi ", name, " to Reset Password copy and paste the address ", urlBase, "/Account/PasswordReset?token=", HttpUtility.HtmlEncode(token));
            return mailBody;
        }
    }

    public class ResertEmailContact
    {
        public string name { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }

}