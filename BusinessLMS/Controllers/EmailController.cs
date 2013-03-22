using BusinessLMS.ActionFilters;
using BusinessLMS.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{
	[BasicAuthentication]
	public class EmailController : ApiController
	{
		public HttpResponseMessage PostResetEmail(ResertEmailContact contact)
		{
			EmailHelper email = new EmailHelper();
			HttpResponseMessage response;
			if (email.SendEmail(contact.name, contact.email, contact.token, EmailHelper.EmailType.resetEmail) == true)
			{
				response = Request.CreateResponse(HttpStatusCode.Created, contact);
			}
			else
			{
				response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while sending email");
			}
			response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contact.email }));
			return response;
		}
	}
}
