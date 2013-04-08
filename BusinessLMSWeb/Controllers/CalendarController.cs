using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using EventbriteNET;
using EventbriteNET.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

	[Authorize]
	public class CalendarController : BaseWebController
	{

		enum CalendarType
		{
			All,
			Dreams,
			Followup,
			Goals,
			Meetings
		}

		private CalendarType CalendarTypes;

		public ActionResult Index(int id)
		{
			ViewBag.CalendarTypes = CalendarTypes.ToSelectList();
			ViewBag.CalendarType = id;
			return View();
		}

		public ActionResult GetEvents(int type, double start, double end)
		{
			CalendarType calendarType = (CalendarType)type;
			DateTime fromDate = ConvertFromUnixTimestamp(start);
			DateTime toDate = ConvertFromUnixTimestamp(end);
			List<CalendarEvent> events = new List<CalendarEvent>();
			List<string> toLoad = new List<string>();
			switch (calendarType)
			{
				case CalendarType.Followup:
				case CalendarType.Meetings:
				case CalendarType.Dreams:
				case CalendarType.Goals:
					toLoad.Add(calendarType.ToString());
					break;
				case CalendarType.All:
				default:
					toLoad.Add("Followup");
					toLoad.Add("Meetings");
					toLoad.Add("Dreams");
					toLoad.Add("Goals");
					break;
			}
			if (toLoad.Count > 0)
			{
				BaseClient client;
				List<CalendarEvent> tempEvents = new List<CalendarEvent>();
				NameValueCollection parms = new NameValueCollection() {
					{ "id", ibo.IBONum }, 
					{ "fromDate", fromDate.ToString() },
					{ "toDate", toDate.ToString() }
				};
				if (toLoad.Contains("Meetings") == true)
				{
					EventbriteContext context = new EventbriteContext(eventbriteApiKey, eventbriteUserKey);
					Organizer organizer = context.GetOrganizer(eventbriteOrginizerId);
					Dictionary<long, Event> ebevents = organizer.Events;
					if (ebevents.Count > 0)
					{
						tempEvents = (from e in ebevents where e.Value.StartDateTime >= fromDate && e.Value.EndDateTime <= toDate select new CalendarEvent(e.Value)).ToList();
						events.AddRange(tempEvents);
					}
				}
				if (toLoad.Contains("Followup") == true)
				{
					client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowup");
					List<ContactFollowup> followups = client.Get<List<ContactFollowup>>(parms);
					if (followups.Count > 0)
					{
						tempEvents = (from e in followups where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
						events.AddRange(tempEvents);
					}
				}
				if (toLoad.Contains("Dreams") == true)
				{
					/*  TODO: Create API methods that support date filtering */
					client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUser");
					List<Dream> dreams = client.Get<List<Dream>>(ibo.IBONum);
					if (dreams.Count > 0)
					{
						tempEvents = (from e in dreams where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
						events.AddRange(tempEvents);
					}
				}
				if (toLoad.Contains("Goals") == true)
				{
					/*  TODO: Create API methods that support date filtering */
					client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoals");
					List<Goal> goals = client.Get<List<Goal>>(ibo.IBONum);
					if (goals.Count > 0)
					{
						tempEvents = (from e in goals where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
						events.AddRange(tempEvents);
					}
				}
			}
			return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
		}

		private static DateTime ConvertFromUnixTimestamp(double timestamp)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return origin.AddSeconds(timestamp);
		}

		public ActionResult _HelpInfo()
		{
			return PartialView();
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
			this.url = string.Concat("/Followups/Details/", evt.followupId);
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
