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
    public class ContactFollowupController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/ContactFollowup
        public IEnumerable<ContactFollowup> GetContactFollowups()
        {
            return db.ContactFollowups.AsEnumerable();
        }

        public IEnumerable<ContactFollowup> GetIBOFollowup(string id)
        {
            return (from cf in db.ContactFollowups join c in db.Contacts on cf.contactId equals c.contactId 
                    where cf.completed == false && c.IBONum == id select cf);
        }

        // GET api/ContactFollowup/5
        public ContactFollowup GetContactFollowup(int id)
        {
            ContactFollowup contactfollowup = db.ContactFollowups.Find(id);
            if (contactfollowup == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contactfollowup;
        }

        // PUT api/ContactFollowup/5
        public HttpResponseMessage PutContactFollowup(int id, ContactFollowup contactfollowup)
        {
            if (ModelState.IsValid && id == contactfollowup.followupId)
            {
                db.Entry(contactfollowup).State = EntityState.Modified;

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

        // POST api/ContactFollowup
        public HttpResponseMessage PostContactFollowup(ContactFollowup contactfollowup)
        {
            if (ModelState.IsValid)
            {
                db.ContactFollowups.Add(contactfollowup);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contactfollowup);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contactfollowup.followupId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/ContactFollowup/5
        public HttpResponseMessage DeleteContactFollowup(int id)
        {
            ContactFollowup contactfollowup = db.ContactFollowups.Find(id);
            if (contactfollowup == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactFollowups.Remove(contactfollowup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contactfollowup);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}