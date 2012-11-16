using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            // Create the context object with your API details 
            EventbriteContext context = new EventbriteContext("APP_KEY", "USER_KEY"); 
            // Instantiate Organizer entity with the desired organizer ID 
            Organizer organizer = context.GetOrganizer(ORGANIZER_ID_HERE); // Get all the events that the organizer has created var events = organizer.Events.Values; // Get the first event in the collection var firstEvent = events.First(); // All the attendees in that event var attendees = firstEvent.Attendees; // All the tickets in that event var tickets = firstEvent.Tickets.Values;

            BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
            List<ContactFollowup> followups = client.Get<List<ContactFollowup>>(ibo.IBONum);
            return View();
        }

    }
}
