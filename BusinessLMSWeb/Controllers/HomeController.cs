using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class HomeController : BaseWebController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Step", "GetSteps");
            List<Step> steps = client.Get<List<Step>>();
            return PartialView(steps);
        }

    }
}
