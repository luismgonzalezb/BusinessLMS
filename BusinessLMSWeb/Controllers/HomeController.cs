using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using BusinessLMSWeb.ModelsView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

	public class HomeController : Controller
	{

		private List<SearchObject> _userNames
		{
			get
			{
				IBOSearchCookie cookie = new IBOSearchCookie(this.HttpContext);
				return cookie.GetIBOs();
			}
			set
			{
				IBOSearchCookie cookie = new IBOSearchCookie(this.HttpContext);
				if (value != null) cookie.SetIBOs(value); else cookie.Nullify();
			}
		}

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
					ViewBag.iboName = string.Concat(ibo.firstName, " ", ibo.lastName);
					ViewBag.lastName = ibo.lastName;
					ViewBag.IBONum = ibo.IBONum;
					ViewBag.email = ibo.email;
					ViewBag.phone = ibo.phone;
				}
			}
			Contact contact = new Contact();
			contact.contactLevel = "0";
			contact.datetime = DateTime.Now;
			contact.birthday = DateTime.Now;
			contact.isPublic = true;
			contact.contactTypeId = 2;
			contact.languageId = 1;
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
			BaseClient client = new BaseClient(baseApiUrl, "Contacts", "PostContact");
			bool result = client.Post<Contact>(model);
			return Json(new { success = result });
		}

		[HttpGet]
		public ActionResult GetIBO(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetIBOShort");
			IBOShort ibo = client.Get<IBOShort>(id);
			return Json(ibo, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult SearchIBO(string term)
		{
			List<SearchObject> userNames = _userNames;
			if (userNames == null)
			{
				BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetSearchIBO");
				userNames = client.Get<List<SearchObject>>(term);
				_userNames = userNames;
			}
			if ((userNames != null) && (userNames.Count > 0))
			{
				userNames = (from usr in userNames
							 where usr.label.ToUpper().Contains(term.ToUpper())
							 select usr).ToList();
			}
			return Json(userNames, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ReadedAlertAjax(AlertIBO model)
		{
			if (model != null)
			{
				model.datetime = DateTime.Now;
				BaseClient client = new BaseClient(baseApiUrl, "AlertsIBO", "PostAlertIBO");
				bool result = client.Post<AlertIBO>(model);
			}
			return Json(new { success = true });
		}

	}
}
