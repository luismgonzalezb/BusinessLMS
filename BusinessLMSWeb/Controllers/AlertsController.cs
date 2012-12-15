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
            List<Alert> tickets = client.Get<List<Alert>>();
            return View(tickets);
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
    }
}
