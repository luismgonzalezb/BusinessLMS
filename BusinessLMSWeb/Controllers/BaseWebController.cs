using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Helpers.MobileRedirect;
using BusinessLMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BusinessLMSWeb.Controllers
{
	public class BaseWebController : Controller
	{

		internal Helpers.Cookies Cookies;

		public BaseWebController()
		{
			if (WebSecurity.Initialized == false)
			{
				SimpleMembershipInitializer();
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Cookies = new Helpers.Cookies(filterContext.HttpContext);
			base.OnActionExecuting(filterContext);
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

		public string FacebookId
		{
			get
			{
				return Cookies.fidCookie.GetFacebookID();
			}
			set
			{
				if (value != null) Cookies.fidCookie.SetFacebookID(value); else Cookies.fidCookie.Nullify();
			}
		}

		public string AccessToken
		{
			get
			{
				return Cookies.atCookie.GetAccessToken();
			}
			set
			{
				if (value != null) Cookies.atCookie.SetAccessToken(value); else Cookies.atCookie.Nullify();
			}
		}

		public IBO ibo
		{
			get
			{
				IBO _ibo = Cookies.iboCookie.GetIBO();
				if (_ibo == null)
				{
					BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByUId");
					_ibo = client.Get<IBO>(WebSecurity.CurrentUserId.ToString());
					Cookies.iboCookie.SetIBO(_ibo);
				}
				return _ibo;
			}
		}

		public List<Step> menuItems
		{
			get
			{
				List<Step> _menuItems = Cookies.menuItemsCooke.GetMenuItems();
				if (_menuItems == null)
				{
					BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
					_menuItems = client.Get<List<Step>>();
					Cookies.menuItemsCooke.SetMenuItems(_menuItems);
				}
				return _menuItems;
			}
		}

		public List<Alert> listAlerts
		{
			get
			{
				List<Alert> _alerts = Cookies.alertsCookie.GetAlerts();
				if (_alerts == null)
				{
					BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlertsIBO");
					_alerts = client.Get<List<Alert>>(ibo.IBONum);
					Cookies.alertsCookie.SetAlerts(_alerts);
				}
				return _alerts;
			}
		}

		public List<ContactFollowup> Followups
		{
			get
			{
				List<ContactFollowup> _followups = Cookies.followupsCookie.GetFollowups();
				if (_followups == null)
				{
					BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
					_followups = client.Get<List<ContactFollowup>>(ibo.IBONum);
					Cookies.followupsCookie.SetFollowups(_followups);
				}
				return _followups;
			}
		}

		#region From Configuration

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

		#endregion

	}
}
