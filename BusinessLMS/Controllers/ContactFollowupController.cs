using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLMS.ActionFilters;
using BusinessLMS.Models;
using BusinessLMS.ModelsView;

namespace BusinessLMS.Controllers
{
    [BasicAuthentication]
    public class ContactFollowupController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        public IEnumerable<ContactFollowup> GetContactFollowups()
        {
            return db.ContactFollowups.AsEnumerable();
        }

        public IEnumerable<ContactFollowup> GetIBOFollowup(string id)
        {
            return (from cf in db.ContactFollowups
                    join c in db.Contacts on cf.contactId equals c.contactId
                    where cf.completed == false && c.IBONum == id
                    select cf);
        }

        public IEnumerable<ContactFollowup> GetIBOCompFollowup(string id)
        {
            return (from cf in db.ContactFollowups
                    join c in db.Contacts on cf.contactId equals c.contactId
                    where cf.completed == true && c.IBONum == id
                    select cf);
        }

        public IEnumerable<FollowupView> GetIBOFollowupView(string id)
        {
            List<FollowupView> followups = new List<FollowupView>();
            followups = (from cf in db.ContactFollowups
                         join c in db.Contacts on cf.contactId equals c.contactId
                         where cf.completed == false && c.IBONum == id
                         select new FollowupView
                         {
                             followupId = cf.followupId,
                             ContactName = string.Concat(c.firstName, " ", c.lastName),
                             Method = cf.method,
                             datetime = cf.datetime
                         }).ToList();
            return followups;
        }

        public IEnumerable<FollowupView> GetIBOCompFollowupView(string id)
        {
            List<FollowupView> followups = new List<FollowupView>();
            followups = (from cf in db.ContactFollowups
                         join c in db.Contacts on cf.contactId equals c.contactId
                         where cf.completed == true && c.IBONum == id
                         select new FollowupView
                         {
                             followupId = cf.followupId,
                             ContactName = string.Concat(c.firstName, " ", c.lastName),
                             Method = cf.method,
                             datetime = cf.datetime
                         }).ToList();
            return followups;
        }

        public ContactFollowup GetContactFollowup(int id)
        {
            ContactFollowup contactfollowup = db.ContactFollowups.Find(id);
            if (contactfollowup == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return contactfollowup;
        }

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