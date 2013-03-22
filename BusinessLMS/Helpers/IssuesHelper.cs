using DoneDone;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BusinessLMS.Helpers
{
	public class IssuesHelper
	{

		private string domain { get { return ConfigurationManager.AppSettings["donedoneDomain"]; } }
		private string token { get { return ConfigurationManager.AppSettings["donedoneKey"]; } }
		private string username { get { return ConfigurationManager.AppSettings["donedoneUser"]; } }
		private string password { get { return ConfigurationManager.AppSettings["donedonePass"]; } }

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
				issues = JsonConvert.DeserializeObject<List<Issue>>(response);
			}
			catch
			{
			}
			return issues;
		}

		public bool CreateIssue(Issue issue)
		{
			try
			{
				//string response = _issueTracker.CreateIssue(issue.ProjectID,issue.Title,issue.PriorityLevelID,issue.ResolverID,issue.TesterID,issue.des)
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	public class Project
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Issue> Issues { get; set; }
	}

	public class Issue
	{
		public int IssueID { get; set; }
		public string PriorityLevel { get; set; }
		public int PriorityLevelID { get; set; }
		public string State { get; set; }
		public string StateID { get; set; }
		public Nullable<DateTime> LastUpdatedDate { get; set; }
		public Nullable<DateTime> LastViewedDate { get; set; }
		public Nullable<DateTime> CreateDate { get; set; }
		public string Title { get; set; }
		public int OrderNumber { get; set; }
		public int ProjectID { get; set; }
		public int TesterID { get; set; }
		public string TesterName { get; set; }
		public int ResolverID { get; set; }
		public string ResolverName { get; set; }
		public int CreatorID { get; set; }
		public string CreatorName { get; set; }
		public Nullable<DateTime> DueDate { get; set; }
	}

}