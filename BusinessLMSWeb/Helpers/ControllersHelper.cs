using BusinessLMS.Models;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BusinessLMSWeb.Helpers
{
	public static class ControllersHelper
	{

		public static List<Timeframe> GetTimeFrames(string baseApiUrl, int level, int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTimeframesLevel");
			NameValueCollection parms = new NameValueCollection() {
				{ "level", level.ToString() },
				{ "languageId", languageId.ToString() } 
			};
			return client.Get<List<Timeframe>>(parms);
		}

		public static List<Area> GetAreas(string baseApiUrl, int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetAreas");
			return client.Get<List<Area>>(languageId.ToString());
		}

		public static List<Tool> GetTools(string baseApiUrl, int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTools");
			return client.Get<List<Tool>>(languageId.ToString());
		}

		public static List<Language> GetLanguages(string baseApiUrl)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetLanguages");
			return client.Get<List<Language>>();
		}

		public static List<ContactType> GetContactTypes(string baseApiUrl, int languageId)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetContactTypes");
			return client.Get<List<ContactType>>(languageId.ToString());
		}

	}
}