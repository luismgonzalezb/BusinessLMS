﻿using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class UniversityController : BaseWebController
	{

		public ActionResult Index()
		{
			return View();
		}

	}
}
