using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using EventbriteNET;
using EventbriteNET.Entities;
using Newtonsoft.Json;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class CalendarController : BaseWebController
    {
        //
        // GET: /Calendar/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetEvents(string types)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();


            EventbriteContext context = new EventbriteContext(eventbriteApiKey, eventbriteUserKey); 
            Organizer organizer = context.GetOrganizer(eventbriteOrginizerId); 
            Dictionary<long,Event> ebevents = organizer.Events;
            
            BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
            List<ContactFollowup> followups = client.Get<List<ContactFollowup>>(ibo.IBONum);


            //events.Add
            return Json(events);
        }

    }

    public class CalendarEvent
    {
        public string title { get; set; }

    }

}
