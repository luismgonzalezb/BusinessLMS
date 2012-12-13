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
    public class IBOController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        public IEnumerable<IBO> GetIBOes()
        {
            var ibos = db.IBOs;
            return ibos.AsEnumerable();
        }

        public IEnumerable<IBO> GetIBOByTerm(string id)
        {
            List<IBO> ibos = (from ibo in db.IBOs 
                              where ibo.firstName.ToUpper().Contains(id.ToUpper()) 
                              || ibo.lastName.ToUpper().Contains(id.ToUpper()) 
                              || ibo.IBONum.Contains(id) select ibo).ToList();
            return ibos;
        }

        public IBO GetIBO(string id)
        {
            IBO ibo = db.IBOs.Find(id);
            if (ibo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return ibo;
        }

        public IBO GetIBOByUId(int id)
        {
            IBO ibo = (from u in db.IBOs where u.UserId == id select u).FirstOrDefault();
            if (ibo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return ibo;
        }

        public HttpResponseMessage PutIBO(string id, IBO ibo)
        {
            if (ModelState.IsValid && id == ibo.IBONum)
            {
                db.Entry(ibo).State = EntityState.Modified;

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

        public HttpResponseMessage PostIBO(IBO ibo)
        {
            if (ModelState.IsValid)
            {
                db.IBOs.Add(ibo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ibo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ibo.IBONum }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage DeleteIBO(string id)
        {
            IBO ibo = db.IBOs.Find(id);
            if (ibo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.IBOs.Remove(ibo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ibo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}