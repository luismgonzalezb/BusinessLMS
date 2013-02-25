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
    public class ProgressController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/Progress
        public IEnumerable<Progress> GetProgresses()
        {
            return db.Progresses.AsEnumerable();
        }

        public Progress GetProgress(int id)
        {
            return (from p in db.Progresses where p.GoalId == id select p).FirstOrDefault();
        }
        // GET api/Progress/5
        public Progress GetProgress(long id)
        {
            Progress progress = db.Progresses.Find(id);
            if (progress == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return progress;
        }

        // PUT api/Progress/5
        public HttpResponseMessage PutProgress(long id, Progress progress)
        {
            if (ModelState.IsValid && id == progress.ProgressId)
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

        // POST api/Progress
        public HttpResponseMessage PostProgress(Progress progress)
        {
            if (ModelState.IsValid)
            {
                db.Progresses.Add(progress);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, progress);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = progress.ProgressId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Progress/5
        public HttpResponseMessage DeleteProgress(long id)
        {
            Progress progress = db.Progresses.Find(id);
            if (progress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Progresses.Remove(progress);

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