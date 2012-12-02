using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Controllers
{

    public class HomeController : Controller
    {

        private string baseApiUrl
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }

        public ActionResult Index(string id)
        {
            ViewBag.iboName = "";
            if (id != null)
            {
                BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBO");
                IBO ibo = client.Get<IBO>(id);
                if (ibo != null)
                {
                    ViewBag.iboName = string.Concat(ibo.firstName," ",ibo.lastName);
                    ViewBag.lastName = ibo.lastName;

                }
                ViewBag.ibo = ibo;
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _Register(string id)
        {
            
            return PartialView();
        }

    }
}
