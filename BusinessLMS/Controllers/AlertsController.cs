﻿using BusinessLMS.Models;
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
	public class AlertsController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public IEnumerable<Alert> GetAlerts()
		{
			return db.Alerts.AsEnumerable();
		}

		public Alert GetAlert(string id)
		{
			Alert alert = db.Alerts.Find(id);
			if (alert == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return alert;
		}

		public IEnumerable<Alert> GetAlertsIBO(string id)
		{

			List<String> alertibos = new List<string>();
			alertibos = (from alert in db.AlertsIBOes
						 where alert.IBONum == id
						 select alert.AlertId).ToList();
			List<Alert> alerts = new List<Alert>();
			alerts = (from alert in db.Alerts
					  where !alertibos.Contains(alert.AlertId)
					  select alert).ToList();
			return alerts;
		}

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