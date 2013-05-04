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
	public class ProgressController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public IEnumerable<GoalProgress> GetProgresses()
		{
			return db.GoalProgresses.AsEnumerable();
		}

		public GoalProgress GetProgress(int id)
		{
			return (from p in db.GoalProgresses where p.goalId == id select p).FirstOrDefault();
		}

		public HttpResponseMessage PutProgress(long id, GoalProgress progress)
		{
			if (ModelState.IsValid && id == progress.progressId)
			{
				db.Entry(progress).State = EntityState.Modified;

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

		public HttpResponseMessage PostProgress(GoalProgress progress)
		{
			if (ModelState.IsValid)
			{
				db.GoalProgresses.Add(progress);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, progress);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = progress.progressId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteProgress(long id)
		{
			GoalProgress progress = db.GoalProgresses.Find(id);
			if (progress == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.GoalProgresses.Remove(progress);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, progress);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}