using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class DashboardController : BaseWebController
	{
		public ActionResult Index()
		{
            return View();
		}
	}

}