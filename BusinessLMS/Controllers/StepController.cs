using BusinessLMS.ActionFilters;
using BusinessLMS.Models;
using BusinessLMS.ModelsView;
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
	public class StepController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public IEnumerable<MenuItem> GetSteps(int id)
		{
			return (from step in db.Steps
					join st in db.StepsTexts on step.stepId equals st.stepId
					where st.languageId == id
					select new MenuItem
					{
						action = step.action,
						controller = step.controller,
						iconClass = step.iconClass,
						text = st.title
					}).AsEnumerable();
		}

		public Step GetStep(int id)
		{
			Step step = db.Steps.Find(id);
			if (step == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}
			return step;
		}

		public HttpResponseMessage PutStep(int id, Step step)
		{
			if (ModelState.IsValid && id == step.stepId)
			{
				db.Entry(step).State = EntityState.Modified;
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

		public HttpResponseMessage PostStep(Step step)
		{
			if (ModelState.IsValid)
			{
				db.Steps.Add(step);
				db.SaveChanges();
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, step);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = step.stepId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteStep(int id)
		{
			Step step = db.Steps.Find(id);
			if (step == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			db.Steps.Remove(step);
			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			return Request.CreateResponse(HttpStatusCode.OK, step);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}