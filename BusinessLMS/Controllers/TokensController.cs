using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLMS.Models;

namespace BusinessLMS.Controllers
{
    public class TokensController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        public ApiToken GetApiToken(string id)
        {
            ApiToken apitoken = db.ApiTokens.Find(id);
            if (apitoken == null)
            {
                ApiToken IssueToken = new ApiToken();
                IssueToken.apiName = id;
                IssueToken.apiKey = Guid.NewGuid().ToString();

                db.ApiTokens.Add(IssueToken);
                db.SaveChanges();

                return IssueToken;
            }
            else
            {
                return null;                
            }
        }

        public HttpResponseMessage DeleteApiToken(string id)
        {
            ApiToken apitoken = db.ApiTokens.Find(id);
            if (apitoken == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ApiTokens.Remove(apitoken);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, apitoken);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}