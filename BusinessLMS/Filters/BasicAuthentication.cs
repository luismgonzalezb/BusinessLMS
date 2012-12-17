using System;
using System.Text;
using BusinessLMS.Helpers;

namespace BusinessLMS.ActionFilters
{
	public class BasicAuthentication : System.Web.Http.Filters.ActionFilterAttribute
	{
		public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			if (actionContext.Request.Headers.Authorization == null)
			{
				actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
			}
			else
			{
				string authToken = actionContext.Request.Headers.Authorization.Parameter;
				string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
				string apiName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
				string apiKey = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
				if (Helper.Authorize(apiName, apiKey) == true)
				{
					base.OnActionExecuting(actionContext);
				}
				else
				{
					actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
				}
			}
		}
	}
}