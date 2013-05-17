using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BusinessLMSWeb
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
			/* GLOBALIZATION DATE HANDLER */
			ModelBinders.Binders.Add(typeof(System.DateTime), new BusinessLMSWeb.Helpers.DateTimeBinder());
			ModelBinders.Binders.Add(typeof(System.DateTime?), new BusinessLMSWeb.Helpers.NullableDateTimeBinder());
		}

		/* GLOBALIZATION HANDLER */
		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			MvcHandler handler = Context.Handler as MvcHandler;
			if (handler == null)
				return;

			string cultureName;
			HttpCookie cultureCookie = handler.RequestContext.HttpContext.Request.Cookies["_ibovirtualculture"];
			if (cultureCookie != null)
				cultureName = cultureCookie.Value;
			else
				cultureName = Request.UserLanguages[0];

			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}

	}
}