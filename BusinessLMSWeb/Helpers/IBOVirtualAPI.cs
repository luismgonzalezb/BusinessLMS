using BusinessLMS.Models;
using BusinessLMSWeb.Models;
using BusinessLMSWeb.ModelsView;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace BusinessLMSWeb.Helpers
{
	public static class IBOVirtualAPI
	{
		public static string baseApiUrl
		{
			get { return ConfigurationManager.AppSettings["ApiUrl"]; }
		}

		#region Generic Methods

		public static T Get<T>(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, string.Concat(typeof(T).ToString(), "s"), string.Concat("Get", typeof(T).ToString()));
			return client.Get<T>(id);
		}

		public static bool Create<T>(T model)
		{
			BaseClient client = new BaseClient(baseApiUrl, string.Concat(typeof(T).ToString(), "s"), string.Concat("Post", typeof(T).ToString()));
			return client.Post<T>(model);
		}

		public static string Update<T>(string id, T model)
		{
			BaseClient client = new BaseClient(baseApiUrl, string.Concat(typeof(T).ToString(), "s"), string.Concat("Put", typeof(T).ToString()));
			return client.Put<T>(id.ToString(), model);
		}

		public static string Delete<T>(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, string.Concat(typeof(T).ToString(), "s"), string.Concat("Delete", typeof(T).ToString()));
			return client.Delete(id);
		}

		#endregion Generic Methods

		#region Lists

		public static List<Timeframe> GetTimeFrames(int level, int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTimeframesLevel");
			NameValueCollection parms = new NameValueCollection() {
				{ "level", level.ToString() },
				{ "languageId", languageId.ToString() }
			};
			return client.Get<List<Timeframe>>(parms);
		}

		public static List<Area> GetAreas(int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetAreas");
			return client.Get<List<Area>>(languageId.ToString());
		}

		public static List<Tool> GetTools(int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTools");
			return client.Get<List<Tool>>(languageId.ToString());
		}

		public static List<Language> GetLanguages()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetLanguages");
			return client.Get<List<Language>>();
		}

		public static List<ContactType> GetContactTypes(int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetContactTypes");
			return client.Get<List<ContactType>>(languageId.ToString());
		}

		public static List<Alert> GetAlerts(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlertsIBO");
			return client.Get<List<Alert>>(iboNum);
		}

		public static List<Alert> GetAllAlerts()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlerts");
			return client.Get<List<Alert>>();
		}

		public static List<ContactFollowup> GetFollowups(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "ContactFollowups", "GetIBOFollowup");
			return client.Get<List<ContactFollowup>>(iboNum);
		}

		public static List<ContactFollowup> GetFollowups(string iboNum, string fromDate, string toDate)
		{
			NameValueCollection parms = new NameValueCollection() {
				{ "id", iboNum },
				{ "fromDate", fromDate },
				{ "toDate", toDate }
			};
			BaseClient client = new BaseClient(baseApiUrl, "ContactFollowups", "GetIBOFollowup");
			return client.Get<List<ContactFollowup>>(parms);
		}

		public static List<MenuItem> GetMenuItems(int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
			return client.Get<List<MenuItem>>(languageId.ToString());
		}

		#endregion Lists

		#region IBO

		public static IBO GetIBOByUId(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "IBOs", "GetIBOByUId");
			return client.Get<IBO>(id);
		}

		public static bool CreateResetEmail(ResertEmailContact model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Email", "PostResetEmail");
			return client.Post<ResertEmailContact>(model);
		}

		#endregion IBO

		#region Contacts

		public static List<Contact> GetIBOContacts(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetIBOContacts");
			return client.Get<List<Contact>>(iboNum);
		}

		#endregion Contacts

		#region Dreams

		public static List<Dream> GetDreamsUserLevel(string iboNum, string level)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUserLevel");
			NameValueCollection parms = new NameValueCollection() {
					{ "id", iboNum },
					{ "level", level }
				};
			return client.Get<List<Dream>>(parms);
		}

		public static List<Dream> GetDreamsUser(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUser");
			return client.Get<List<Dream>>(iboNum);
		}

		public static DreamsMV GetDreamMV(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "DreamMV", "GetDreamMV");
			return client.Get<DreamsMV>(iboNum);
		}

		public static bool CreateDreamMV(DreamsMV model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "DreamMV", "PostDreamMV");
			return client.Post<DreamsMV>(model);
		}

		public static string UpdateDreamMV(string id, DreamsMV model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "DreamMV", "PutDreamMV");
			return client.Put<DreamsMV>(id, model);
		}

		#endregion Dreams

		#region Goals

		public static List<Goal> GetIBOLevelGoals(string iboNum, string level)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOLevelGoals");
			NameValueCollection parms = new NameValueCollection() {
					{ "id", iboNum },
					{ "level", level }
				};
			return client.Get<List<Goal>>(parms);
		}

		public static List<Goal> GetIBOGoals(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoals");
			return client.Get<List<Goal>>(iboNum);
		}

		public static List<Goal> GetIBOGoalsProgress(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoalsProgress");
			return client.Get<List<Goal>>(iboNum);
		}

		#endregion Goals

		#region Goal Progress

		public static GoalProgress GetProgress(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
			return client.Get<GoalProgress>(id);
		}

		public static bool CreateProgress(GoalProgress model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Progress", "PostProgress");
			return client.Post<GoalProgress>(model);
		}

		public static string UpdateProgress(string id, GoalProgress model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Progress", "PutProgress");
			return client.Put<GoalProgress>(id.ToString(), model);
		}

		#endregion Goal Progress

		#region Followups

		public static List<FollowupView> GetIBOFollowupView(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "ContactFollowups", "GetIBOFollowupView");
			return client.Get<List<FollowupView>>(iboNum);
		}

		#endregion Followups

		#region Books

		public static List<Book> GetBooks()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetBooks");
			return client.Get<List<Book>>();
		}

		public static List<Book> GetIBOBooks(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetIBOBooks");
			return client.Get<List<Book>>(iboNum);
		}

		public static List<Book> GetMyBooks(string iboNum)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetMyBook");
			return client.Get<List<Book>>(iboNum);
		}

		#endregion Books

		#region Issues

		public static List<PriorityLevel> GetPriorityLevels()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Issues", "GetPriorityLevels");
			return client.Get<List<PriorityLevel>>();
		}

		public static bool CreateIssue(Ticket model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Issues", "PostIssue");
			return client.Post<Ticket>(model);
		}

		#endregion Issues
	}
}