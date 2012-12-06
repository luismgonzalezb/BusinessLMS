using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLMS.Models;

namespace BusinessLMS.Controllers
{
    public class TicketsController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/Tickets
        public IEnumerable<Ticket> GetTickets()
        {
            return db.Tickets.AsEnumerable();
        }

        public IEnumerable<Ticket> GetMyTickets(string id)
        {
            List<Ticket> tickets = (from ticket in db.Tickets where ticket.IBONum == id select ticket).ToList();
            return tickets;
        }

        // GET api/Tickets/5
        public Ticket GetTicket(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return ticket;
        }

        // PUT api/Tickets/5
        public HttpResponseMessage PutTicket(int id, Ticket ticket)
        {
            if (ModelState.IsValid && id == ticket.ticketId)
            {
                db.Entry(ticket).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Tickets
        public HttpResponseMessage PostTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ticket);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ticket.ticketId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Tickets/5
        public HttpResponseMessage DeleteTicket(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Tickets.Remove(ticket);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ticket);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}