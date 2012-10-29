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
    public class DreamMVController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/DreamMV
        public IEnumerable<DreamMV> GetDreamMVs()
        {
            return db.DreamsMV.AsEnumerable();
        }

        // GET api/DreamMV/5
        public DreamMV GetDreamMV(string id)
        {
            DreamMV dreammv = (from d in db.DreamsMV where d.IBONum == id select d).FirstOrDefault();
            if (dreammv == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return dreammv;
        }

        // PUT api/DreamMV/5
        public HttpResponseMessage PutDreamMV(int id, DreamMV dreammv)
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
        public HttpResponseMessage PostDreamMV(DreamMV dreammv)
        {
            if (ModelState.IsValid)
            {
                db.DreamsMV.Add(dreammv);
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
            DreamMV dreammv = db.DreamsMV.Find(id);
            if (dreammv == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DreamsMV.Remove(dreammv);

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