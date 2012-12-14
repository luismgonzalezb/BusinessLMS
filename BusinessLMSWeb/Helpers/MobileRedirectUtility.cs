using System.Collections.Generic;
using System.Linq;

namespace BusinessLMSWeb.Helpers.MobileRedirect
{
    public class MobileRedirectUtility {
        public List<string> PhoneAgents { get; set; }
        public List<string> TabletAgents { get; set; }

        #region Constructors

        #region List Constructors

        /// <summary>
        /// Default Constructor. Will use system user agents for mobile detection.
        /// </summary>
        public MobileRedirectUtility() {
            SetPhoneAgents(new List<string>());
            SetTabletAgents(new List<string>());
        }

        /// <summary>
        /// Constructor to use user defined phone agents, but use system defaults for tablet detection
        /// </summary>
        /// <param name="phoneAgents">list of user agents of phone devices</param>
        public MobileRedirectUtility(List<string> phoneAgents) {
            SetPhoneAgents(phoneAgents);
            SetTabletAgents(new List<string>());
        }

        /// <summary>
        /// Constructor to use, user defined user agents
        /// </summary>
        /// <param name="phoneAgents"></param>
        /// <param name="tabletAgents"></param>
        public MobileRedirectUtility(List<string> phoneAgents, List<string> tabletAgents) {
            SetPhoneAgents(phoneAgents);
            SetTabletAgents(tabletAgents);
        }

        #endregion

        //#region StringConstructors

        //public MobileRedirectUtility(string phoneAgents, char mobileSep) {
        //    SetPhoneAgents(phoneAgents.Split(mobileSep).ToList());
        //    SetTabletAgents(new List<string>());
        //}

        //public MobileRedirectUtility(string phoneAgents, char mobileSep, string tableAgents, char tabletSep) {
        //    SetPhoneAgents(phoneAgents.Split(mobileSep).ToList());
        //    SetTabletAgents(tableAgents.Split(tabletSep).ToList());
        //}

        //#endregion

        #endregion

        /// <summary>
        /// Tests to see if device is a mobile phone.  User defined agents will be used if they exsist.  
        /// </summary>
        /// <returns>Ture or False is a mobile phone</returns>
        public bool IsPhone() {
            if (this.PhoneAgents.Count > 0)
                return IsDeviceByString(this.PhoneAgents);

            return IsDeviceBySystem();
        }

        /// <summary>
        /// Tests to see if device is a tablet device. User defined agents take priority. 
        /// </summary>
        /// <returns>True or False is a tablet device</returns>
        public bool IsTablet() {
            
            if (this.TabletAgents.Count > 0)
                return IsDeviceByString(this.TabletAgents);

            return IsDeviceBySystem();
        }


        /// <summary>
        /// Runs both tests if is a Phone or Table
        /// </summary>
        /// <returns>Returns True if device is Phone OR Tablet</returns>
        public bool IsPhoneOrTablet() {
            return (IsPhone() || IsTablet());
        }

        #region Detection

        /// <summary>
        /// Checks to see if the List of agents contains our current user agent
        /// </summary>
        /// <param name="agents">List of user defined browser agents</param>
        /// <returns>True or False if the current agent is in the list of agents passed in.</returns>
        public bool IsDeviceByString(List<string> agents) {
            //Perform neccassary Null Checks
            if (System.Web.HttpContext.Current.Request == null || string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserAgent)) {
                return false;
            }

            return agents.Any(System.Web.HttpContext.Current.Request.UserAgent.ToLower().Contains);
        }

        /// <summary>
        /// Checks system default agents.
        /// </summary>
        /// <returns></returns>
        public bool IsDeviceBySystem() {
            //C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\Browsers
            return System.Web.HttpContext.Current.Request.Browser.IsMobileDevice;
        }

        #endregion
        
       
        #region Accessory

        public void SetPhoneAgents(List<string> phoneAgents) {
            this.PhoneAgents = phoneAgents;
        }
        public void SetTabletAgents(List<string> tabletAgents) {
            this.TabletAgents = tabletAgents;
        }

        #endregion

    }
}
