using BusinessLMS.ActionFilters;
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
	public class DreamMVController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		// GET api/DreamMV
		public IEnumerable<DreamsMV> GetDreamMVs()
		{
			return db.DreamsMVs.AsEnumerable();
		}

		// GET api/DreamMV/5
		public DreamsMV GetDreamMV(string id)
		{
			DreamsMV dreammv = (from d in db.DreamsMVs where d.IBONum == id select d).FirstOrDefault();
			if (dreammv == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return dreammv;
		}

		// PUT api/DreamMV/5
		public HttpResponseMessage PutDreamMV(int id, DreamsMV dreammv)
		{
			if (ModelState.IsValid && id == dreammv.dreamMVId)
			{
				db.Entry(dreammv).State = EntityState.Modified;

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

		// POST api/DreamMV
		public HttpResponseMessage PostDreamMV(DreamsMV dreammv)
		{
			if (ModelState.IsValid)
			{
				db.DreamsMVs.Add(dreammv);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dreammv);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = dreammv.dreamMVId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// DELETE api/DreamMV/5
		public HttpResponseMessage DeleteDreamMV(int id)
		{
			DreamsMV dreammv = db.DreamsMVs.Find(id);
			if (dreammv == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.DreamsMVs.Remove(dreammv);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dreammv);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}