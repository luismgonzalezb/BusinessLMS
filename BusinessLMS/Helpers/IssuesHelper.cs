using DoneDone;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BusinessLMS.Helpers
{
	public class IssuesHelper
	{

		private string domain { get { return ConfigurationManager.AppSettings["donedoneDomain"]; } }
		private string token { get { return ConfigurationManager.AppSettings["donedoneKey"]; } }
		private string username { get { return ConfigurationManager.AppSettings["donedoneUser"]; } }
		private string password { get { return ConfigurationManager.AppSettings["donedonePass"]; } }
		private string projectId { get { return ConfigurationManager.AppSettings["donedoneProjectId"]; } }
		private string defaultUser { get { return ConfigurationManager.AppSettings["donedoneDefaultUser"]; } }

		private IssueTracker _issueTracker;
		public IssueTracker issueTracker { get { return _issueTracker; } }

		public IssuesHelper()
		{
			_issueTracker = new IssueTracker(domain, token, username, password);
		}

		public List<Project> GetProjects(bool withIsssues = false)
		{
			List<Project> projects = new List<Project>();
			try
			{
				string response = _issueTracker.GetProjects(withIsssues);
				projects = JsonConvert.DeserializeObject<List<Project>>(response);

			}
			catch
			{
			}
			return projects;
		}

		public List<Issue> GetProjectIssues(string ID)
		{
			List<Issue> issues = new List<Issue>();
			try
			{
				string response = _issueTracker.GetAllIssuesInProject(ID);
				issues = JsonConvert.DeserializeObject<List<Issue>>(ID);
			}
			catch
			{
			}
			return issues;
		}

		public List<People> GetProjectPeople(string ID)
		{
			List<People> result = new List<People>();
			try
			{
				string response = _issueTracker.GetAllPeopleInProject(ID);
				result = JsonConvert.DeserializeObject<List<People>>(response);
			}
			catch
			{
			}
			return result;
		}


		public List<PriorityLevel> GetPriorityLevels()
		{
			List<PriorityLevel> result = new List<PriorityLevel>();
			try
			{
				string response = _issueTracker.GetPriorityLevels();
				result = JsonConvert.DeserializeObject<List<PriorityLevel>>(response);
			}
			catch
			{
			}
			return result;
		}

		public bool CreateIssue(Ticket issue)
		{
			try
			{

				List<People> ProjectPeople = new List<People>();
				ProjectPeople = GetProjectPeople(projectId);
				People resolver = ProjectPeople.Where(p => p.ID == int.Parse(defaultUser)).FirstOrDefault();
				People tester = ProjectPeople.Where(p => p.ID == int.Parse(defaultUser)).FirstOrDefault();
				string response = _issueTracker.CreateIssue(projectId, issue.Title, issue.PriorityLevelID.ToString(), resolver.ID.ToString(), tester.ID.ToString());
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

}