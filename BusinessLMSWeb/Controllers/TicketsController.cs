using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class TicketsController : BaseWebController
    {
        //
        // GET: /Tickets/

        Dictionary<int, String> ticketTypes = new Dictionary<int, string>()
        {
            { 0, "Defect" },
            { 1, "Enhancement" }
        };

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Tickets", "GetTickets");
            List<Ticket> tickets = client.Get<List<Ticket>>();
            return View(tickets);
        }

        public ActionResult CreateTicket()
        {
            ViewBag.ticketTypes = new SelectList(ticketTypes, "Key", "Value");
            Ticket ticket = new Ticket();
            ticket.IBONum = ibo.IBONum;
            ticket.datetime = DateTime.Now;
            ticket.developer = "Unasigned";
            ticket.impact = "Uknown";
            ticket.priority = 0;
            ticket.solved = false;
            return PartialView(ticket);
        }

        [HttpPost]
        public ActionResult CreateTicket(Ticket model)
        {
            try
            {
                BaseClient client = new BaseClient(baseApiUrl, "Tickets", "PostTicket");
                string result = client.Post<Ticket>(model);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

    }
}
