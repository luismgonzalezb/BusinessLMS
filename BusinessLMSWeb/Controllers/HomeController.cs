using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            ViewBag.IBONum = "";
            ViewBag.email = "";
            ViewBag.phone = "";
            if (id != null)
            {
                BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBO");
                IBO ibo = client.Get<IBO>(id);
                if (ibo != null)
                {
                    ViewBag.iboName = string.Concat(ibo.firstName," ",ibo.lastName);
                    ViewBag.lastName = ibo.lastName;
                    ViewBag.IBONum = ibo.IBONum;
                    ViewBag.email = ibo.email;
                    ViewBag.phone = ibo.phone;
                }
                ViewBag.ibo = ibo;
            }
            Contact contact = new Contact();
            contact.contactLevel = "";
            contact.datetime = DateTime.Now;
            contact.isPublic = true;
            ViewBag.contact = contact;
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

        [HttpPost]
        public ActionResult RegisterAjax(Contact model)
        {
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult GetIBO(string id)
        {
            BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBO");
            IBO ibo = client.Get<IBO>(id);
            return Json(
                new { 
                    email = ibo.email != null ? ibo.email : ibo.phone, 
                    phone = ibo.phone != null ? ibo.phone : "" }, 
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchIBO(string term)
        {

            BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByTerm");
            List<IBO> ibo = client.Get<List<IBO>>(term);
            List<searchObject> userNames = (from usr in ibo 
                                            select new searchObject { label = string.Concat(usr.firstName," ",usr.lastName), value = usr.IBONum }).ToList();
            return Json(userNames, JsonRequestBehavior.AllowGet);
        }

        [Serializable]
        public class searchObject
        {
            public string label { get; set; }
            public string value { get; set; }
        }

    }
}
