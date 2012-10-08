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
    public class StepController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/Step
        public IEnumerable<Step> GetSteps()
        {
            return db.Steps.AsEnumerable(); //(from step in db.Steps where step.parentStepId == null orderby step.stepOrder select step);
        }

        // GET api/Step/5
        public Step GetStep(int id)
        {
            Step step = db.Steps.Find(id);
            if (step == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return step;
        }

        // PUT api/Step/5
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

        // POST api/Step
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

        // DELETE api/Step/5
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