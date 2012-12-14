using System.Collections.Generic;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Helpers
{
    public static class ControllersHelper
    {

        public static List<Timeframe> GetTimeFrames(int level, string baseApiUrl)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTimeframesLevel");
            return client.Get<List<Timeframe>>(level.ToString());
        }

        public static List<Area> GetAreas(string baseApiUrl)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetAreas");
            return client.Get<List<Area>>();
        }

        public static List<Tool> GetTools(string baseApiUrl)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTools");
            return client.Get<List<Tool>>();
        }

        public static List<Language> GetLanguages(string baseApiUrl)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetLanguages");
            return client.Get<List<Language>>();
        }

        public static List<ContactType> GetContactTypes(string baseApiUrl)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetContactTypes");
            return client.Get<List<ContactType>>();
        }

    }
}