using System.Linq;
using BusinessLMS.Models;

namespace BusinessLMS.Helpers
{
    public class Helper
    {
        private static BusinessLMSContext db = new BusinessLMSContext();
        public static bool Authorize(string apiName, string apiKey)
        {
            try 
            {
                ApiToken auth = (from tk in db.ApiTokens where tk.apiName == apiName && tk.apiKey == apiKey select tk).FirstOrDefault();
                if (auth != null)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}