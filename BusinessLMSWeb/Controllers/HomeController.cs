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

    }
}
