using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLMS.ActionFilters;
using BusinessLMS.Models;

namespace BusinessLMS.Controllers
{
    [BasicAuthentication]
    public class GoalsController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        public IEnumerable<Goal> GetGoals()
        {
            return db.Goals.AsEnumerable();
        }

        public IEnumerable<Goal> GetIBOLevelGoals(string id, int level)
        {
            return (from g in db.Goals join d in db.Dreams on g.dreamId equals d.dreamId where g.goalLevel == level && d.IBONum == id select g);
        }

        public IEnumerable<Goal> GetIBOGoals(string id)
        {
            return (from g in db.Goals join d in db.Dreams on g.dreamId equals d.dreamId where d.IBONum == id select g);
        }

        public IEnumerable<Goal> GetIBOGoalsProgress(string id)
        {
            return (from g in db.Goals join d in db.Dreams on g.dreamId equals d.dreamId where d.IBONum == id && g.completed==false select g);
        }

        public Goal GetGoal(int id)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return goal;
        }

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