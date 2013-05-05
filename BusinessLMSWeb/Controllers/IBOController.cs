using BusinessLMS.Models;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System;
using System.Web.Mvc;
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
			ViewBag.languages = languages;
			IBO ibo = ModelParser.ParseIBO(model);
			ibo.UserId = WebSecurity.CurrentUserId;
			ibo.datetime = DateTime.Now;
			ibo.facebookid = FacebookId != null ? FacebookId : "";
			ibo.accesstoken = AccessToken != null ? AccessToken : "";
			ibo.level = model.level;
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "IBO", "PostIBO");
				bool result = client.Post<IBO>(ibo);
				Cookies.iboCookie.Nullify();
				return RedirectToAction("Index", "Dashboard");
			}
			catch
			{
				ModelState.AddModelError(null, "The IBO Number is already been used");
				return View(model);
			}

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
			IBO iboUpdate = ModelParser.ParseIBO(model);
			iboUpdate.UserId = WebSecurity.CurrentUserId;
			iboUpdate.datetime = DateTime.Now;
			iboUpdate.facebookid = FacebookId != null ? FacebookId : "";
			iboUpdate.accesstoken = AccessToken != null ? AccessToken : "";
			iboUpdate.level = model.level;
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "IBO", "PutIBO");
				string result = client.Put<IBO>(model.IBONum, iboUpdate);
				Cookies.iboCookie.Nullify();
			}
			catch { }
			return RedirectToAction("Index", "Dashboard");
		}
	}
}
