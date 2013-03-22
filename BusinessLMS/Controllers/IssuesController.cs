using BusinessLMS.ActionFilters;
using BusinessLMS.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{

	[BasicAuthentication]
	public class IssuesController : ApiController
	{
		public List<Project> GetProjects(bool withIssues = false)
		{
			IssuesHelper issues = new IssuesHelper();
			return issues.GetProjects(withIssues);
		}

		public List<Issue> GetProjectIssues(string Id)
		{

			return null;
		}

		public HttpResponseMessage PostIssue(Issue issue)
		{
			HttpResponseMessage response;
			//if (email.SendEmail(contact.name, contact.email, contact.token, EmailHelper.EmailType.resetEmail) == true)
			//{
			//	response = Request.CreateResponse(HttpStatusCode.Created, contact);
			//}
			//else
			//{
			//	response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while sending email");
			//}
			response = Request.CreateResponse(HttpStatusCode.Created, issue);
			response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = issue.IssueID }));
			return response;
		}

	}
}
