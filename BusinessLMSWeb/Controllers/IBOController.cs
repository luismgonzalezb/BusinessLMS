using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using WebMatrix.WebData;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class IBOController : BaseWebController
    {

        private SelectList languages
        {
            get
            {
                return new SelectList(ControllersHelper.GetLanguages(baseApiUrl), "languageId", "language1");
            }
        }

        public ActionResult AddIBO()
        {
            ViewBag.languages = languages;
            return View();
        }

        [HttpPost]
        public ActionResult AddIBO(IBO model)
        {
            IBO ibo = ModelParser.ParseIBO(model);
            ibo.UserId = WebSecurity.CurrentUserId;
            ibo.datetime = DateTime.Now;
            ibo.facebookid = FacebookId != null ? FacebookId : "";
            ibo.accesstoken = AccessToken != null ? AccessToken : "";
            try
            {
                BaseClient client = new BaseClient(baseApiUrl, "IBO", "PostIBO");
                string result = client.Post<IBO>(ibo);
            } catch { }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update()
        {
            BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOByUId");
            IBO ibo = client.Get<IBO>(WebSecurity.CurrentUserId.ToString());
            ViewBag.languages = languages;
            return View(ibo);
        }

        [HttpPost]
        public ActionResult Update(IBO model)
        {
            IBO ibo = ModelParser.ParseIBO(model);
            ibo.UserId = WebSecurity.CurrentUserId;
            ibo.datetime = DateTime.Now;
            ibo.facebookid = FacebookId != null ? FacebookId : "";
            ibo.accesstoken = AccessToken != null ? AccessToken : "";
            try
            {
                BaseClient client = new BaseClient(baseApiUrl, "IBO", "PutIBO");
                string result = client.Put<IBO>(model.IBONum,ibo);
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }
    }
}
