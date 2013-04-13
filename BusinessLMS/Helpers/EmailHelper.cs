using MailChimp;
using MailChimp.Types;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace BusinessLMS.Helpers
{
	public class EmailHelper
	{
		public enum EmailType
		{
			subscription,
			resetEmail
		}

		private string serverToken { get { return ConfigurationManager.AppSettings["mandrillKey"]; } }
		private string mailingListToken { get { return ConfigurationManager.AppSettings["mailChimpKey"]; } }
		private string fromEmail { get { return ConfigurationManager.AppSettings["emailAddressFrom"]; } }
		private string fromName { get { return ConfigurationManager.AppSettings["emailFromName"]; } }
		private string urlBase { get { return ConfigurationManager.AppSettings["urlBase"]; } }
		private string mailChimpGeneralList { get { return ConfigurationManager.AppSettings["mailChimpGeneralList"]; } }
		private string mailChimpIBOList { get { return ConfigurationManager.AppSettings["mailChimpIBOList"]; } }

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

				List<Mandrill.Messages.Recipient> recipients = new List<Mandrill.Messages.Recipient>();
				Mandrill.Messages.Recipient recipient = new Mandrill.Messages.Recipient(to, name);
				recipients.Add(recipient);

				MandrillApi server = new MandrillApi(serverToken);
				Mandrill.Messages.Message message = new Mandrill.Messages.Message();

				message.FromEmail = fromEmail;
				message.FromName = fromName;
				message.Subject = Subject;
				message.Html = HtmlBody;
				message.Text = TextBody;
				message.To = recipients.ToArray();
				message.TrackOpens = true;

				MVList<Mandrill.Messages.SendResult> results = server.Send(message);

				if (results.Count > 0)
				{
					Mandrill.Messages.SendResult result = results[0];
					if (result.Status == Mandrill.Messages.Status.Sent)
					{
						return true;
					}
					else
					{
						return false;
					}
				}

			}
			catch
			{ }
			return false;
		}

		public bool AddToMailingList(string firstName, string lastName, string email, int list = 0)
		{
			MCApi server = new MCApi(mailingListToken, false);
			List.Merges merge_vars = new List.Merges();
			merge_vars.Add("fName", firstName);
			merge_vars.Add("lName", lastName);
			string mailChimpList;
			switch (list)
			{
				case 1: mailChimpList = mailChimpGeneralList; break;
				default: mailChimpList = mailChimpIBOList; break;
			}
			server.ListSubscribe(mailChimpIBOList, email, merge_vars);
			return false;
		}

		private string GetSubscriptionHtml(string name)
		{
			TagBuilder body = new TagBuilder("div");
			body.InnerHtml = string.Concat("Thanks ", name, " for joining IBO Virtual, together we are going to change your life");
			return body.ToString();
		}

		private string GetSubscriptionText(string name)
		{
			string body = string.Concat("Thanks ", name, " for joining IBO Virtual, together we are going to change your life");
			return body;
		}

		private string GetResetMailHtml(string name, string token)
		{
			string mailBody = string.Concat("<html><body>Hi ", name, " Reset Password <a href='",
				urlBase, "/Account/PasswordReset?token=", HttpUtility.HtmlEncode(token), "'>Here</a></body></html>");
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