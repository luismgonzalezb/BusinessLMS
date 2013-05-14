using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class TicketsController : BaseWebController
	{
		[IsNotPageRefresh]
		public ActionResult CreateTicket()
		{
			List<PriorityLevel> priorityLevel = IBOVirtualAPI.GetPriorityLevels();
			ViewBag.PriorityLevel = new SelectList(priorityLevel, "ID", "Name");
			return PartialView();
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult CreateTicket(Ticket model)
		{
			bool result = IBOVirtualAPI.CreateIssue(model);
			return Json(new { success = result });
		}
	}
}