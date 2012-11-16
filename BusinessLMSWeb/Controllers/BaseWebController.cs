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
                            ViewBag.IBOPicture = ibo.picture != String.Empty ? ibo.picture : "~/Images/noProfilePicture.png";
                            ViewBag.MenuItems = menuItems;
                        }
                        else
                        {
                            if ((actionName != "AddIBO") && (controllerName != "IBO"))
                            {
                                filterContext.Result = RedirectToAction("AddIBO", "IBO");
                            }
                        }
                    }
                }
            }
            catch { }
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
                HttpCookie fbCookie = Request.Cookies[name];
                if (fbCookie != null)  value = fbCookie.Value != null ? crypto.DecryptString(fbCookie.Value) : "";
                return value;
            }
            set
            {
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("fid");
                HttpCookie fbCookie = new HttpCookie(name);
                fbCookie.Value = crypto.EncryptToString(value);
                fbCookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(fbCookie);
            }
        }

        public string AccessToken
        {
            get
            {
                string value = "";
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("at");
                HttpCookie fbCookie = Request.Cookies[name];
                if (fbCookie != null) 
                    value = fbCookie.Value != null ? crypto.DecryptString(fbCookie.Value) : "";
                return value;
            }
            set
            {
                SimpleAES crypto = new SimpleAES();
                string name = crypto.EncryptToString("at");
                HttpCookie fbCookie = new HttpCookie(name);
                fbCookie.Value = crypto.EncryptToString(value);
                fbCookie.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(fbCookie);
            }
        }

        public IBO ibo { 
            get 
            {
                IBO _ibo = (IBO)Session["IBO"];
                if (_ibo == null)
                {
                    BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByUId");
                    _ibo = client.Get<IBO>(WebSecurity.CurrentUserId.ToString());
                    Session["IBO"] = _ibo;
                }
                return _ibo;
            }
        }

        public List<Step> menuItems
        {
            get
            {
                List<Step> _menuItems = (List<Step>)Session["menuItems"];
                if (_menuItems == null)
                {
                    BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
                    _menuItems = client.Get<List<Step>>();
                    Session["menuItems"] = _menuItems;
                }
                return _menuItems;
            }
        }

        public string baseApiUrl
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }

        public string orginizerId { get; }

        public string eventbriteApiKey { get; }

        #endregion

    }
}
