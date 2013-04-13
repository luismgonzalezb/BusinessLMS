using BusinessLMSWeb.Controllers;
using System;
using System.Web.Mvc;

namespace BusinessLMSWeb.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class IsNotPageRefresh : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = (BaseWebController)filterContext.Controller;
			controller.IsNotPageRefresh = true;
		}
	}
}