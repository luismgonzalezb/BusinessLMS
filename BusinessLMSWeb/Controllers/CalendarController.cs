using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using EventbriteNET;
using EventbriteNET.Entities;

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

        public ActionResult GetEvents(double start, double end)
        {

            DateTime fromDate = ConvertFromUnixTimestamp(start);
            DateTime toDate = ConvertFromUnixTimestamp(end);

            BaseClient client;
            List<CalendarEvent> events = new List<CalendarEvent>();
            List<CalendarEvent> tempEvents = new List<CalendarEvent>();

            EventbriteContext context = new EventbriteContext(eventbriteApiKey, eventbriteUserKey); 
            Organizer organizer = context.GetOrganizer(eventbriteOrginizerId); 
            Dictionary<long,Event> ebevents = organizer.Events;
            if (ebevents.Count > 0)
            {
                tempEvents = (from e in ebevents where e.Value.StartDateTime >= fromDate && e.Value.EndDateTime <= toDate select new CalendarEvent(e.Value)).ToList();
                events.AddRange(tempEvents);
            }
            /*  TODO: Create API methods that support date filtering */
            client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
            List<ContactFollowup> followups = client.Get<List<ContactFollowup>>(ibo.IBONum);
            if (followups.Count > 0)
            {
                tempEvents = (from e in followups where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
                events.AddRange(tempEvents);
            }
            client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoals");
            List<Goal> goals = client.Get<List<Goal>>(ibo.IBONum);
            if (goals.Count > 0)
            {
                tempEvents = (from e in goals where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
                events.AddRange(tempEvents);
            }
            client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUser");
            List<Dream> dreams = client.Get<List<Dream>>(ibo.IBONum);
            if (dreams.Count > 0)
            {
                tempEvents = (from e in dreams where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
                events.AddRange(tempEvents);
            }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

    }

    public class CalendarEvent
    {

        public CalendarEvent(Event evt)
        {
            this.title = evt.Title;
            this.allDay = false;
            this.start = evt.StartDateTime.ToString("s");
            this.end = evt.EndDateTime.ToString("s");
            this.url = string.Concat("http://www.eventbrite.com/event/", evt.Id.ToString());
            this.editable = false;
        }

        public CalendarEvent(ContactFollowup evt)
        {
            this.title = "Follow up contact";
            this.allDay = true;
            this.start = evt.datetime.ToString("s");
            this.url = string.Concat("/Followup/Details/", evt.followupId);
            this.editable = false;
        }

        public CalendarEvent(Goal evt)
        {
            this.title = "Goal Deadline";
            this.allDay = true;
            this.start = evt.datetime.ToString("s");
            this.url = string.Concat("/Goals/GoalDetail", evt.goalId.ToString());
            this.editable = false;
        }

        public CalendarEvent(Dream evt)
        {
            this.title = "Goal Deadline";
            this.allDay = true;
            this.start = evt.datetime.ToString("s");
            this.url = string.Concat("/Dreams/DreamDetail", evt.dreamId.ToString());
            this.editable = false;
        }

        public string title { get; set; }
        public bool allDay { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public bool editable { get; set; }

    }

}
