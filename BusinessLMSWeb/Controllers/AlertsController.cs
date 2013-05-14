using BusinessLMS.Models;
using BusinessLMSWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class AlertsController : BaseWebController
	{

		public ActionResult Index()
		{
			List<Alert> Alerts = IBOVirtualAPI.GetAllAlerts();
			return View(Alerts);
		}

		public ActionResult CreateAlert()
		{

			List<Alert> AlertList = IBOVirtualAPI.GetAllAlerts();
			ViewBag.AlertList = AlertList;
			Alert Alert = new Alert();
			Alert.datetime = DateTime.Now;
			return PartialView(Alert);
		}

		[HttpPost]
		public ActionResult CreateAlert(Alert model)
		{
			bool result = IBOVirtualAPI.Create<Alert>(model);
			return RedirectToAction("Index"); // This Sends you back to the index after saving

		}
		public ActionResult DeleteAlert(String id)
		{
			string result = IBOVirtualAPI.Delete<Alert>(id);
			return RedirectToAction("Index");
		}
	}
}