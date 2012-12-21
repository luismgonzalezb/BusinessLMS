using BusinessLMSWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class AlertsController : BaseWebController
    {
        //
        // GET: /Alerts/

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlerts");
            List<Alert> Alerts = client.Get<List<Alert>>();
            return View(Alerts);
        }

        public ActionResult CreateAlert()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Alerts", "GetAlerts");
            List<Alert> AlertList = client.Get<List<Alert>>();
            ViewBag.AlertList = AlertList;
            Alert Alert = new Alert();
            Alert.datetime = DateTime.Now;
            return PartialView(Alert);
        }
        [HttpPost]
        public ActionResult CreateAlert(Alert model)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Alerts", "PostAlert");
            string result = client.Post<Alert>(model);
            //return Json(new { success = true }); //This is when it comes from Ajax (with JQuery)
            //return PartialView(model); //This keeps you on the same page after saving
            return RedirectToAction("Index"); // This Sends you back to the index after saving

        }
        public ActionResult DeleteAlert(String id)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Alerts", "DeleteAlert");
            string result = client.Delete(id);
            return RedirectToAction("Index");
        }
    }
}