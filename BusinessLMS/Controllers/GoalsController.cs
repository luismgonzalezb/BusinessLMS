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
    public class GoalsController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        // GET api/Goals
        public IEnumerable<Goal> GetGoals()
        {
            return db.Goals.AsEnumerable();
        }

        public IEnumerable<Goal> GetIBOGoals(string id, int level)
        {
            return (from g in db.Goals join d in db.Dreams on g.dreamId equals d.dreamId where g.goalLevel == level && d.IBONum == id select g);
        }

        // GET api/Goals/5
        public Goal GetGoal(int id)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return goal;
        }

        // PUT api/Goals/5
        public HttpResponseMessage PutGoal(int id, Goal goal)
        {
            if (ModelState.IsValid && id == goal.goalId)
            {
                db.Entry(goal).State = EntityState.Modified;

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

        // POST api/Goals
        public HttpResponseMessage PostGoal(Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, goal);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = goal.goalId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Goals/5
        public HttpResponseMessage DeleteGoal(int id)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Goals.Remove(goal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, goal);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}