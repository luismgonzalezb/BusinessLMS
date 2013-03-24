using BusinessLMS.ActionFilters;
using BusinessLMS.Helpers;
using DoneDone;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{

	[BasicAuthentication]
	public class IssuesController : ApiController
	{

		private string projectId { get { return ConfigurationManager.AppSettings["donedoneProjectId"]; } }

		public List<Project> GetProjects(bool withIssues = false)
		{
			IssuesHelper issues = new IssuesHelper();
			return issues.GetProjects(withIssues);
		}

		public List<PriorityLevel> GetPriorityLevels()
		{
			IssuesHelper issues = new IssuesHelper();
			return issues.GetPriorityLevels();
		}

		public List<Issue> GetProjectIssues()
		{
			IssuesHelper issues = new IssuesHelper();
			return issues.GetProjectIssues(projectId);
		}

		public List<People> GetProjectPeople()
		{
			IssuesHelper issues = new IssuesHelper();
			return issues.GetProjectPeople(projectId);
		}

		public HttpResponseMessage PostIssue(Ticket issue)
		{
			HttpResponseMessage response;
			IssuesHelper issues = new IssuesHelper();
			bool result = issues.CreateIssue(issue);
			if (result == true)
			{
				response = Request.CreateResponse(HttpStatusCode.Created, true);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", "0"));
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, false);
				response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while sending email");
			}

			return response;
		}

	}
}
