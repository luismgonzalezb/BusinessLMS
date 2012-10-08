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
    public class DeleteController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}