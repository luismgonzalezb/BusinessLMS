using System;
using System.Web;

namespace BusinessLMSWeb.Helpers.MobileRedirect
{
    public class MobileViewCookie {

        public const string FullSite_Phone_Cookie = "ViewFull_Phone";
        public const string FullSite_Tablet_Cookie = "ViewFull_Tablet";

        #region Set

        public static void SetPhoneCookie() {
            SetCookie(FullSite_Phone_Cookie);
        }
        public static void SetTabletCookie() {
            SetCookie(FullSite_Tablet_Cookie);
        }

        public static void SetPhoneAndTabletCookie() {
            SetCookie(FullSite_Tablet_Cookie);
            SetCookie(FullSite_Phone_Cookie);
        }

        private static void SetCookie(string cookieName) {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values["viewfull"] = "true";
            cookie.Expires = DateTime.Now.AddDays(7);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region HasCookie
        
        public static bool HasPhoneCookie() {
            return HasXCookie(FullSite_Phone_Cookie);
        }
        
        public static bool HasTabletCookie() {
            return HasXCookie(FullSite_Tablet_Cookie);
        }
        
        public static bool HasPhoneOrTabletCookie() {
            return HasTabletCookie() || HasPhoneCookie();
        }

        private static bool HasXCookie(string cookieName) {
            return HttpContext.Current.Request.Cookies[cookieName] != null;
        }

        #endregion

    }
}
