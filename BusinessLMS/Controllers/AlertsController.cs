using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLMS.Models;

namespace BusinessLMS.Controllers
{
	public class AlertsController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		// GET api/Alerts
		public IEnumerable<Alert> GetAlerts()
		{
			return db.Alerts.AsEnumerable();
		}

		// GET api/Alerts/5
		public Alert GetAlert(string id)
		{
			Alert alert = db.Alerts.Find(id);
			if (alert == null)
			{

				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return alert;
		}

        public IEnumerable<Alert>GetAlertsIBO(string id)
        {

            return (from a in db.Alerts
                    join aIBO in db.AlertsIBO on a.AlertId equals aIBO.AlertId
                    where aIBO.IBONum == id
                    select a);
        }

        // PUT api/Alerts/5
        public HttpResponseMessage PutAlert(string id, Alert alert)
		{
			if (ModelState.IsValid && id == alert.AlertId)
			{
				db.Entry(alert).State = EntityState.Modified;

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

		// POST api/Alerts
		public HttpResponseMessage PostAlert(Alert alert)
		{
			if (ModelState.IsValid)
			{
				alert.AlertId = Guid.NewGuid().ToString();
				alert.action = alert.action != null ? alert.action : "";
				db.Alerts.Add(alert);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, alert);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = alert.AlertId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// DELETE api/Alerts/5
		public HttpResponseMessage DeleteAlert(string id)
		{
			Alert alert = db.Alerts.Find(id);
			if (alert == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Alerts.Remove(alert);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, alert);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}