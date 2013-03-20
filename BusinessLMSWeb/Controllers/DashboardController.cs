using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class DashboardController : BaseWebController
	{
		public ActionResult Index()
		{
			BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOChilds");
			List<IBO> iboChildren = client.Get<List<IBO>>("0101");
			iboChildren.Add(ibo);
			return View(iboChildren);
		}

	}
}
