using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

    }
}