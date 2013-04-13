using BusinessLMS.ActionFilters;
using BusinessLMS.Helpers;
using BusinessLMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{
	[BasicAuthentication]
	public class ContactsController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public IEnumerable<Contact> GetContacts()
		{
			return (from cnt in db.Contacts where cnt.isPublic == true select cnt);
		}

		public IEnumerable<Contact> GetIBOContacts(string id)
		{
			return (from cnt in db.Contacts where cnt.IBONum == id select cnt);
		}

		public Contact GetContact(int id)
		{
			Contact contact = db.Contacts.Find(id);
			if (contact == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}
			return contact;
		}

		public HttpResponseMessage PutContact(int id, Contact contact)
		{
			if (ModelState.IsValid && id == contact.contactId)
			{
				db.Entry(contact).State = EntityState.Modified;
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

		public HttpResponseMessage PostContact(Contact contact)
		{
			if (ModelState.IsValid)
			{
				var exists = db.Contacts.Where(cnt => cnt.email == contact.email || cnt.phone == contact.phone).FirstOrDefault();
				if (exists == null || exists.contactId == 0)
				{
					db.Contacts.Add(contact);
					db.SaveChanges();
					if (contact.email != string.Empty)
					{
						EmailHelper mail = new EmailHelper();
						mail.SendEmail(string.Concat(contact.firstName, " ", contact.lastName), contact.email, null, EmailHelper.EmailType.subscription);
						if (contact.newsletteroptin == true)
						{
							mail.AddToMailingList(contact.firstName, contact.lastName, contact.email, 1);
						}
					}
					if (contact.IBONum != string.Empty)
					{
						/* Business Loginc to add first followup to new contacts */
						ContactFollowup followup = new ContactFollowup();
						followup.contactId = contact.contactId;
						followup.IBONum = contact.IBONum;
						followup.method = contact.preferred != null ? contact.preferred : "email";
						followup.datetime = DateTime.Now.AddDays(1);
						followup.completed = false;
						db.ContactFollowups.Add(followup);
						db.SaveChanges();
					}
					HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contact);
					response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contact.contactId }));
					return response;
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.Ambiguous);
				}
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteContact(int id)
		{
			Contact contact = db.Contacts.Find(id);
			if (contact == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			List<ContactFollowup> followups = db.ContactFollowups.Where(f => f.contactId == contact.contactId).ToList();
			if (followups.Count > 0)
			{
				foreach (ContactFollowup followup in followups)
				{
					db.ContactFollowups.Remove(followup);
				}
				db.SaveChanges();
			}
			db.Contacts.Remove(contact);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, contact);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

	}
}
