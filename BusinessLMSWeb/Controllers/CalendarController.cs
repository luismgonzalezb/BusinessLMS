using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using EventbriteNET;
using EventbriteNET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class CalendarController : BaseWebController
	{
		private List<CalendarType> CalendarTypes
		{
			get
			{
				return new List<CalendarType>() {
					new CalendarType { id = 0, value = "All" },
					new CalendarType { id = 1, value = "Dreams" },
					new CalendarType { id = 2, value = "Followup" },
					new CalendarType { id = 3, value = "Goals" },
					new CalendarType { id = 4, value = "Meetings" }
				};
			}
		}

		public ActionResult Index(int id)
		{
			ViewBag.CalendarTypes = new SelectList(CalendarTypes, "id", "value");
			CalendarType calendarType = CalendarTypes.Where(ct => ct.id == id).FirstOrDefault();
			return View(calendarType);
		}

		[IsNotPageRefresh]
		public ActionResult GetEvents(int type, double start, double end)
		{
			CalendarType calendarType = CalendarTypes.Where(ct => ct.id == type).FirstOrDefault();
			DateTime fromDate = ConvertFromUnixTimestamp(start);
			DateTime toDate = ConvertFromUnixTimestamp(end);
			List<CalendarEvent> events = new List<CalendarEvent>();
			List<string> toLoad = new List<string>();
			switch (calendarType.value)
			{
				case "Followup":
				case "Meetings":
				case "Dreams":
				case "Goals":
					toLoad.Add(calendarType.value);
					break;

				case "All":
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
					List<ContactFollowup> followups = IBOVirtualAPI.GetFollowups(ibo.IBONum, fromDate.ToString(), toDate.ToString());
					if (followups.Count > 0)
					{
						tempEvents = (from e in followups where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
						events.AddRange(tempEvents);
					}
				}
				if (toLoad.Contains("Dreams") == true)
				{
					/*  TODO: Create API methods that support date filtering */
					List<Dream> dreams = IBOVirtualAPI.GetDreamsUser(ibo.IBONum);
					if (dreams.Count > 0)
					{
						tempEvents = (from e in dreams where e.datetime >= fromDate select new CalendarEvent(e)).ToList();
						events.AddRange(tempEvents);
					}
				}
				if (toLoad.Contains("Goals") == true)
				{
					/*  TODO: Create API methods that support date filtering */
					List<Goal> goals = IBOVirtualAPI.GetIBOGoals(ibo.IBONum);
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

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}
	}

	public class CalendarType
	{
		public int id { get; set; }

		public string value { get; set; }
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
			this.url = string.Concat("/Goals/EditGoal/", evt.goalId.ToString());
			this.editable = false;
		}

		public CalendarEvent(Dream evt)
		{
			this.title = string.Concat("Dream:  ", evt.dream1.Substring(0, evt.dream1.Length <= 30 ? evt.dream1.Length : 30));
			this.allDay = true;
			this.start = evt.datetime.ToString("s");
			this.url = string.Concat("/Dreams/EditDream/", evt.dreamId.ToString());
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