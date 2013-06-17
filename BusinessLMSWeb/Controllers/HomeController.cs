using BusinessLMS.Models;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.ModelsView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

	public class HomeController : Controller
	{

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{

			string cultureName = null;
			HttpCookie cultureCookie = Request.Cookies["_ibovirtualculture"];
			if (cultureCookie != null)
				cultureName = cultureCookie.Value;
			else
				cultureName = Request.UserLanguages[0];

			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

			base.OnActionExecuted(filterContext);
		}

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
			BaseClient client = new BaseClient(baseApiUrl, "IBOs", "GetIBOShort");
			IBOShort ibo = client.Get<IBOShort>(id);
			return Json(ibo, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult SearchIBO(string term)
		{
			List<SearchObject> userNames = _userNames;
			if (userNames == null)
			{
				BaseClient client = new BaseClient(baseApiUrl, "IBOs", "GetSearchIBO");
				userNames = client.Get<List<SearchObject>>();
				_userNames = userNames;
			}
			if ((userNames != null) && (userNames.Count > 0))
			{
				userNames = (from usr in userNames
							 where (usr.label.ToUpper().Contains(term.ToUpper())) ||
							 (usr.value.ToUpper().Contains(term.ToUpper()))
							 select usr).ToList();
			}
			return Json(userNames, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ReadedAlertAjax(AlertsIBO model)
		{
			if (model != null)
			{
				model.datetime = DateTime.Now;
				BaseClient client = new BaseClient(baseApiUrl, "AlertsIBO", "PostAlertIBO");
				bool result = client.Post<AlertsIBO>(model);
			}
			return Json(new { success = true });
		}

		public ActionResult ChangeLanguage(string id)
		{
			HttpCookie cultureCookie = new HttpCookie("_ibovirtualculture", id);
			cultureCookie.Expires = DateTime.Now.AddDays(300);
			Response.Cookies.Add(cultureCookie);
			return RedirectToAction("Index");
		}

		public ActionResult Terms()
		{
			return View();
		}

	}
}
