using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Helpers.MobileRedirect;
using BusinessLMSWeb.Models;
using Newtonsoft.Json;
using WebMatrix.WebData;

namespace BusinessLMSWeb.Controllers
{
    public class BaseWebController : Controller
    {

        public BaseWebController()
        {
            if (WebSecurity.Initialized == false)
            {
                SimpleMembershipInitializer();
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            String actionName = filterContext.ActionDescriptor.ActionName;
            String controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            try
            {
                List<string> phoneAgents = new List<string>() { "ipod", "iphone", "android 2", "blackberry", "opera mobi", "windows phone" };
                MobileRedirectUtility mru = new MobileRedirectUtility(phoneAgents);
                if (mru.IsPhone())
                {
                    filterContext.Result = Redirect(ConfigurationManager.AppSettings["MobileUrl"]);
                }
                else
                {
                    if ((WebSecurity.IsAuthenticated) && (actionName != "LogOff"))
                    {
                        if (ibo != null)
                        {
                            ViewBag.IBOName = String.Concat(ibo.firstName, " ", ibo.lastName);
                            ViewBag.IBONum = ibo.IBONum;
                            ViewBag.IBOPicture = ibo.picture != String.Empty ? ibo.picture : Url.Content("~/Images/noProfilePicture.png");
                            ViewBag.MenuItems = menuItems;
                        }
                        else
                        {
                            if ((actionName != "AddIBO") && (controllerName != "IBO"))
                            {
                                ibo = null;
                                filterContext.Result = RedirectToAction("AddIBO", "IBO");
                            }
                        }
                    }
                }
            }
            catch { }
            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;
            base.OnActionExecuted(filterContext);
        }

        public bool SimpleMembershipInitializer()
        {
            Database.SetInitializer<UsersContext>(null);
            try
            {
                using (var context = new UsersContext())
                {
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                }
                WebSecurity.InitializeDatabaseConnection("BusinessLMSContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }

        #region generalProperties

        public string FacebookId
        {
            get
            {
                string value = "";
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("fid");
                HttpCookie cookie = Request.Cookies[name];
                if (cookie != null)  value = cookie.Value != null ? crypto.DecryptString(cookie.Value) : "";
                return value;
            }
            set
            {
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("fid");
                HttpCookie cookie = new HttpCookie(name);
                cookie.Value = crypto.EncryptToString(value);
                cookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(cookie);
            }
        }

        public string AccessToken
        {
            get
            {
                string value = "";
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("at");
                HttpCookie cookie = Request.Cookies[name];
                if (cookie != null) 
                    value = cookie.Value != null ? crypto.DecryptString(cookie.Value) : "";
                return value;
            }
            set
            {
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("at");
                HttpCookie cookie = new HttpCookie(name);
                cookie.Value = crypto.EncryptToString(value);
                cookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(cookie);
            }
        }

        private IBO _ibo {
            get
            {
                IBO value = null;
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("iboinfo");
                HttpCookie cookie = Request.Cookies[name];
                if (cookie != null)
                {
                    if (cookie.Value != null)
                    {
                        string temp = crypto.DecryptString(cookie.Value);
                        value = JsonConvert.DeserializeObject<IBO>(temp);
                    }
                }
                return value;
            }
            set
            {
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("iboinfo");
                HttpCookie cookie = new HttpCookie(name);
                string temp = JsonConvert.SerializeObject(value);
                cookie.Value = crypto.EncryptToString(temp);
                cookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(cookie);
            }
        }

        public IBO ibo { 
            get 
            {
                IBO __ibo = _ibo;
                if (__ibo == null)
                {
                    BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByUId");
                    __ibo = client.Get<IBO>(WebSecurity.CurrentUserId.ToString());
                    _ibo = __ibo;
                }
                return __ibo;
            }
            set
            {
                _ibo = null;
            }
        }

        private List<Step> _menuItems
        {
            get
            {
                List<Step> value = null;
                string name = "menuItems";
                HttpCookie cookie = Request.Cookies[name];
                if (cookie != null)
                {
                    if (cookie.Value != null)
                    {
                        string temp = cookie.Value;
                        value = JsonConvert.DeserializeObject<List<Step>>(temp);
                    }
                }
                return value;
            }
            set
            {
                string name = "menuItems";
                HttpCookie cookie = new HttpCookie(name);
                string temp = JsonConvert.SerializeObject(value);
                cookie.Value = temp;
                cookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(cookie);
            }
        }

        public List<Step> menuItems
        {
            get
            {
                List<Step> __menuItems = _menuItems;
                if (__menuItems == null)
                {
                    BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
                    __menuItems = client.Get<List<Step>>();
                    _menuItems = __menuItems;
                }
                return __menuItems;
            }
        }

        public string baseApiUrl
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }

        public long eventbriteOrginizerId
        {
            get 
            {
                string oId = ConfigurationManager.AppSettings["EventbriteOrginizerId"];
                return long.Parse(oId); 
            }
        }

        public string eventbriteUserKey 
        {
            get { return ConfigurationManager.AppSettings["EventbriteUserKey"]; }
        }

        public string eventbriteApiKey
        {
            get { return ConfigurationManager.AppSettings["EventbriteApiKey"]; }
        }

        #endregion

    }
}
