using BusinessLMSWeb.Helpers;
using EventbriteNET;
using EventbriteNET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class InvitesController : BaseWebController
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            List<InviteEvent> Events = new List<InviteEvent>();
            EventbriteContext context = new EventbriteContext(eventbriteApiKey, eventbriteUserKey);
            Organizer organizer = context.GetOrganizer(eventbriteOrginizerId);
            Dictionary<long, Event> ebevents = organizer.Events;
            if (ebevents.Count > 0)
            {
                Events = (from e in ebevents where e.Value.EndDateTime>=DateTime.Now select new InviteEvent(e.Value)).ToList();
            }
            return View(Events);
        }
    }

    public class InviteEvent
    {
        public InviteEvent(Event evt)
        {
            this.title = evt.Title;
            this.start = evt.StartDateTime.Date.ToString();
            this.end = evt.EndDateTime.Date.ToString();
            this.url = string.Concat("http://www.eventbrite.com/event/", evt.Id.ToString());
        }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
    }

}
