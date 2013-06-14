using BusinessLMS.Models;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Helpers.MobileRedirect;
using BusinessLMSWeb.Models;
using BusinessLMSWeb.ModelsView;
using NerdDinner.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
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

		public bool SimpleMembershipInitializer()
		{
			Database.SetInitializer<UsersContext>(null);
			try
			{
				using (var context = new UsersContext())
				{
					if (!context.Database.Exists())
					{
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

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Cookies = new Helpers.Cookies(filterContext.HttpContext);
			if (WebSecurity.IsAuthenticated) ibo = _ibo;
			if ((ibo != null) && (ibo.UserId != 0))
			{
				string cultureName = CultureHelper.GetCulture(ibo.languageId);
				HttpCookie cultureCookie = new HttpCookie("_ibovirtualculture", cultureName);
				cultureCookie.Expires = DateTime.Now.AddDays(300);
				Response.Cookies.Add(cultureCookie);
			}
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
							if (!IsNotPageRefresh)
							{
								ViewBag.IBOName = String.Concat(ibo.firstName, " ", ibo.lastName);
								ViewBag.IBONum = ibo.IBONum;
								ViewBag.IBOPicture = ibo.picture != String.Empty ? ibo.picture : Url.Content("~/Images/noProfilePicture.png");
								ViewBag.MenuItems = menuItems;
								ViewBag.AlertItems = listAlerts;
								ViewBag.FollowupsCount = Followups.Count;
							}
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
            ViewBag.FacebookAppId = facebookAppId;
			base.OnActionExecuted(filterContext);
		}

		internal Helpers.Cookies Cookies;

		public bool IsNotPageRefresh { get; set; }

		#region General Properties

		#region Not Cookie Saved

		public List<Alert> listAlerts
		{
			get
			{
				return IBOVirtualAPI.GetAlerts(ibo.IBONum);
			}
		}

		public List<ContactFollowup> Followups
		{
			get
			{
				return IBOVirtualAPI.GetFollowups(ibo.IBONum);
			}
		}

		#endregion Not Cookie Saved

		#region Cookie Saved

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

		public IBO ibo { get; set; }

		private IBO _ibo
		{
			get
			{
				IBO _ibo = Cookies.iboCookie.GetIBO();
				if ((_ibo == null) || (_ibo.UserId == 0))
				{
					_ibo = IBOVirtualAPI.GetIBOByUId(WebSecurity.CurrentUserId.ToString());
					Cookies.iboCookie.SetIBO(_ibo);
				}
				return _ibo;
			}
		}

		public List<MenuItem> menuItems
		{
			get
			{
				List<MenuItem> _menuItems = Cookies.menuItemsCooke.GetMenuItems();
				if ((_menuItems == null) || (_menuItems.Count == 0))
				{
					_menuItems = IBOVirtualAPI.GetMenuItems(ibo.languageId);
					Cookies.menuItemsCooke.SetMenuItems(_menuItems);
				}
				return _menuItems;
			}
		}

		#endregion Cookie Saved

		#region From Configuration

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

		public string facebookAppId
		{
			get { return ConfigurationManager.AppSettings["appId"]; }
		}

		#endregion From Configuration

		#endregion General Properties
	}
}