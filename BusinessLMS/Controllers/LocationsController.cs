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
    public class LocationsController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/Locations
        public IEnumerable<State> GetStates()
        {
            return (from st in db.States where st.StateCode != String.Empty select st).AsEnumerable();
        }

        // GET api/Locations/5
        public State GetState(string id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return state;
        }

        // PUT api/Locations/5
        public HttpResponseMessage PutState(string id, State state)
        {
            if (ModelState.IsValid && id == state.StateCode)
            {
                db.Entry(state).State = EntityState.Modified;

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

        // POST api/Locations
        public HttpResponseMessage PostState(State state)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(state);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, state);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = state.StateCode }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Locations/5
        public HttpResponseMessage DeleteState(string id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.States.Remove(state);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, state);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}