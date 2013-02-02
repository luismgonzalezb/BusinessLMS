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
	public class ClearSystemCookiesController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public bool GetClearSystemCookies()
		{
			ClearSystemCookies clear = new ClearSystemCookies();
			clear = db.ClearSystemCookies.AsEnumerable().FirstOrDefault();
			if ((clear != null) && (clear.Clear == true)) {
				return true;
			} else {
				return false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}