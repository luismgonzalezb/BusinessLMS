using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class TicketsController : BaseWebController
	{
		//
		// GET: /Tickets/

		public ActionResult CreateTicket()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Issues", "GetPriorityLevels");
			List<PriorityLevel> priorityLevel = client.Get<List<PriorityLevel>>();
			ViewBag.PriorityLevel = new SelectList(priorityLevel, "ID", "Name");
			return PartialView();
		}

		[HttpPost]
		public ActionResult CreateTicket(Ticket model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Issues", "PostIssue");
			bool result = client.Post<Ticket>(model);
			return Json(new { success = result });
		}

	}
}
