using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using BusinessLMS.Helpers;
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
						if (ClearCookies == true)
						{
							CookieHelper cookies = new CookieHelper(_context);
							cookies.CleanCookies();
						}
						if (ibo != null)
						{
							ViewBag.IBOName = String.Concat(ibo.firstName, " ", ibo.lastName);
							ViewBag.IBONum = ibo.IBONum;
							ViewBag.IBOPicture = ibo.picture != String.Empty ? ibo.picture : Url.Content("~/Images/noProfilePicture.png");
							ViewBag.MenuItems = menuItems;
							ViewBag.AlertItems = listAlerts;
							ViewBag.FollowupsCount = Followups.Count;
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

		#region General Properties

		public System.Web.HttpContext _context { get { return System.Web.HttpContext.Current; } }

		public string FacebookId
		{
			get
			{
				string value = "";
				CookieHelper cookie = new CookieHelper(_context, "fid");
				value = cookie.GetCookie<string>();
				return value;
			}
			set
			{
				CookieHelper cookie = new CookieHelper(_context, "fid");
				if (value != null) cookie.SetCookie<string>(value); else cookie.Remove();
			}
		}

		public string AccessToken
		{
			get
			{
				string value = "";
				CookieHelper cookie = new CookieHelper(_context, "at");
				value = cookie.GetCookie<string>();
				return value;
			}
			set
			{
				CookieHelper cookie = new CookieHelper(_context, "at");
				if (value != null) cookie.SetCookie<string>(value); else cookie.Remove();
			}
		}

		public IBO ibo
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByUId");
				IBO _ibo = client.Get<IBO>(WebSecurity.CurrentUserId.ToString());
				return _ibo;
			}
		}

		public List<Step> menuItems
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
				List<Step> _menuItems = client.Get<List<Step>>();
				return _menuItems;
			}
		}

		public List<Alert> listAlerts
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlertsIBO");
				List<Alert> Alerts = client.Get<List<Alert>>(ibo.IBONum);
				return Alerts;
			}
		}

		public List<ContactFollowup> Followups
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
				List<ContactFollowup> followups = client.Get<List<ContactFollowup>>(ibo.IBONum);
				return followups;
			}
		}

		public bool ClearCookies
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "ClearSystemCookies", "GetClearSystemCookies");
				bool clear = client.Get<bool>();
				return clear;
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
