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
	public class ListsController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		#region Languages

		public IEnumerable<Language> GetLanguages()
		{
			return db.Languages.AsEnumerable();
		}

		public Language GetLanguage(int id)
		{
			Language language = db.Languages.Find(id);
			if (language == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return language;
		}

		public HttpResponseMessage PutLanguage(int id, Language language)
		{
			if (ModelState.IsValid && id == language.languageId)
			{
				db.Entry(language).State = EntityState.Modified;

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

		public HttpResponseMessage PostLanguage(Language language)
		{
			if (ModelState.IsValid)
			{
				db.Languages.Add(language);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, language);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = language.languageId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteLanguage(int id)
		{
			Language language = db.Languages.Find(id);
			if (language == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Languages.Remove(language);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, language);
		}

		#endregion

		#region Timeframes

		public IEnumerable<Timeframe> GetTimeframes()
		{
			return db.Timeframes.AsEnumerable();
		}

		public IEnumerable<Timeframe> GetTimeframesLevel(int level, int languageId)
		{
			return (from tf in db.Timeframes where tf.timeLevel == level && tf.languageId == languageId orderby tf.days descending select tf);
		}

		public Timeframe GetTimeframe(int id)
		{
			Timeframe timeframe = db.Timeframes.Find(id);
			if (timeframe == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return timeframe;
		}

		public HttpResponseMessage PutTimeframe(int id, Timeframe timeframe)
		{
			if (ModelState.IsValid && id == timeframe.timeframeId)
			{
				db.Entry(timeframe).State = EntityState.Modified;

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

		public HttpResponseMessage PostTimeframe(Timeframe timeframe)
		{
			if (ModelState.IsValid)
			{
				db.Timeframes.Add(timeframe);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, timeframe);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = timeframe.timeframeId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteTimeframe(int id)
		{
			Timeframe timeframe = db.Timeframes.Find(id);
			if (timeframe == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Timeframes.Remove(timeframe);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, timeframe);
		}

		#endregion

		#region Areas

		public IEnumerable<Area> GetAreas(int id)
		{
			return db.Areas.Where(a => a.languageId == id).AsEnumerable();
		}

		public Area GetArea(int id)
		{
			Area area = db.Areas.Find(id);
			if (area == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return area;
		}

		public HttpResponseMessage PutArea(int id, Area area)
		{
			if (ModelState.IsValid && id == area.areaId)
			{
				db.Entry(area).State = EntityState.Modified;

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

		public HttpResponseMessage PostArea(Area area)
		{
			if (ModelState.IsValid)
			{
				db.Areas.Add(area);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, area);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = area.areaId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteArea(int id)
		{
			Area area = db.Areas.Find(id);
			if (area == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Areas.Remove(area);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, area);
		}

		#endregion

		#region Tools

		public IEnumerable<Tool> GetTools(int id)
		{
			return db.Tools.Where(t => t.languageId == id).AsEnumerable();
		}

		public Tool GetTool(int id)
		{
			Tool tool = db.Tools.Find(id);
			if (tool == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return tool;
		}

		public HttpResponseMessage PutTool(int id, Tool tool)
		{
			if (ModelState.IsValid && id == tool.toolId)
			{
				db.Entry(tool).State = EntityState.Modified;

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

		public HttpResponseMessage PostTool(Tool tool)
		{
			if (ModelState.IsValid)
			{
				db.Tools.Add(tool);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tool);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tool.toolId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteTool(int id)
		{
			Tool tool = db.Tools.Find(id);
			if (tool == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Tools.Remove(tool);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, tool);
		}

		#endregion

		#region Contact Type

		public IEnumerable<ContactType> GetContactTypes(int id)
		{
			return db.ContactTypes.Where(ct => ct.languageId == id).AsEnumerable();
		}

		public ContactType GetContactType(int id)
		{
			ContactType contacttype = db.ContactTypes.Find(id);
			if (contacttype == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return contacttype;
		}

		public HttpResponseMessage PutContactType(int id, ContactType contacttype)
		{
			if (ModelState.IsValid && id == contacttype.contactTypeId)
			{
				db.Entry(contacttype).State = EntityState.Modified;

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

		public HttpResponseMessage PostContactType(ContactType contacttype)
		{
			if (ModelState.IsValid)
			{
				db.ContactTypes.Add(contacttype);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contacttype);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contacttype.contactTypeId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteContactType(int id)
		{
			ContactType contacttype = db.ContactTypes.Find(id);
			if (contacttype == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.ContactTypes.Remove(contacttype);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, contacttype);
		}

		#endregion

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}