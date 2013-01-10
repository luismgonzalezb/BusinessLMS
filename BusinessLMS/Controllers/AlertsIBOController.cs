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
    public class AlertsIBOController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/AlertsIBO
        public IEnumerable<AlertIBO> GetAlertIBO()
        {
            return db.AlertsIBO.AsEnumerable();
        }

        // GET api/AlertsIBO/5
        public AlertIBO GetAlertIBO(string id)
        {
            AlertIBO alertibo = db.AlertsIBO.Find(id);
            if (alertibo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return alertibo;
        }

        // PUT api/AlertsIBO/5
        public HttpResponseMessage PutAlertIBO(string id, AlertIBO alertibo)
        {
            if (ModelState.IsValid && id == alertibo.AlertId)
            {
                db.Entry(alertibo).State = EntityState.Modified;

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

        // POST api/AlertsIBO
        public HttpResponseMessage PostAlertIBO(AlertIBO alertibo)
        {
            if (ModelState.IsValid)
            {
                db.AlertsIBO.Add(alertibo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, alertibo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = alertibo.AlertId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/AlertsIBO/5
        public HttpResponseMessage DeleteAlertIBO(string id)
        {
            AlertIBO alertibo = db.AlertsIBO.Find(id);
            if (alertibo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AlertsIBO.Remove(alertibo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, alertibo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}