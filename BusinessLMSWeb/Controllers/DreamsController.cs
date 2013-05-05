﻿using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class DreamsController : BaseWebController
	{

		private List<Area> areas
		{
			get
			{
				return ControllersHelper.GetAreas(baseApiUrl, ibo.languageId);
			}
		}

		public ActionResult Index()
		{
			return View();
		}

		[IsNotPageRefresh]
		public ActionResult DreamList(int id)
		{
			if (ibo != null)
			{
				BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUserLevel");
				NameValueCollection parms = new NameValueCollection() {
					{ "id", ibo.IBONum }, 
					{ "level", id.ToString() } 
				};
				List<Dream> dreams = client.Get<List<Dream>>(parms);
				if ((dreams.Count > 0) || (id == 0))
				{
					ViewBag.dreamLevel = id;
					ViewBag.nextLevel = id + 1;
					parms = new NameValueCollection() { { "id", ibo.IBONum }, { "level", (id + 1).ToString() } };
					List<Dream> dreamsLevel = client.Get<List<Dream>>(parms);
					Dictionary<Timeframe, Dream> timeframeDreams = new Dictionary<Timeframe, Dream>();
					List<Timeframe> timeframes = ControllersHelper.GetTimeFrames(baseApiUrl, id, ibo.languageId);
					Timeframe last = timeframes.Last();
					if (dreamsLevel.Count < 1) ViewBag.lastItem = last;
					foreach (Timeframe time in timeframes)
					{
						Dream dream = (from d in dreams
									   where d.timeframeId == time.timeframeId
									   select d).FirstOrDefault();
						timeframeDreams.Add(time, dream);
					}
					return PartialView(timeframeDreams);
				}
			}
			return null;
		}

		[IsNotPageRefresh]
		public ActionResult DisplayDream(Dream model, bool last)
		{
			ViewBag.area = (from area in areas where area.areaId == model.areaId select area.title).FirstOrDefault();
			ViewBag.completed = model.achieved == true ? "Acieved" : " I'm working on ";
			ViewBag.etaMsg = model.achieved == true ? " Before " : " Until ";
			ViewBag.eta = String.Format("{0:dddd dd MMMM yyyy}", model.datetime);
			ViewBag.last = last;
			return PartialView(model);
		}

		[IsNotPageRefresh]
		public ActionResult NewDream(int timeframeId, int days, string IBONum, int dreamLevel)
		{
			ViewBag.areas = new SelectList(areas, "areaId", "title");
			ViewBag.formName = string.Concat("createIBOForm", dreamLevel.ToString(), timeframeId.ToString());
			ViewBag.uploadName = string.Concat("file_upload", dreamLevel.ToString(), timeframeId.ToString());
			Dream dream = new Dream();
			dream.IBONum = IBONum;
			dream.dreamLevel = dreamLevel;
			dream.timeframeId = timeframeId;
			dream.datetime = DateTime.Now.AddDays(days);
			return PartialView(dream);
		}

		[HttpPost]
		public ActionResult NewDream(Dream model)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Dreams", "PostDream");
				bool result = client.Post<Dream>(model);
			}
			catch { }
			return RedirectToAction("Index");
		}

		public ActionResult MoreDreams(int id)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDream");
				Dream dream = client.Get<Dream>(id.ToString());
				if (dream != null)
				{
					Dream newDream = ModelParser.ParseDream(dream);
					client = new BaseClient(baseApiUrl, "Dreams", "PostDream");
					bool result = client.Post<Dream>(newDream);
				}
			}
			catch { }
			return RedirectToAction("Index");
		}

		[IsNotPageRefresh]
		public ActionResult EditDream(int id)
		{
			ViewBag.areas = new SelectList(areas, "areaId", "title");
			BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDream");
			Dream dream = client.Get<Dream>(id.ToString());
			return PartialView(dream);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult EditDream(Dream model)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Dreams", "PutDream");
				string result = client.Put<Dream>(model.dreamId.ToString(), model);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}

		}

		[IsNotPageRefresh]
		public ActionResult DreamMV()
		{
			BaseClient client = new BaseClient(baseApiUrl, "DreamMV", "GetDreamMV");
			DreamsMV dream = client.Get<DreamsMV>(ibo.IBONum);
			return PartialView(dream);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult DreamMV(DreamsMV dream)
		{
			try
			{
				DreamsMV dreammv = new DreamsMV();
				dreammv.IBONum = ibo.IBONum;
				dreammv.vision = dream.vision;
				dreammv.mission = dream.mission;
				dreammv.purpose = dream.purpose;
				BaseClient client;
				if (dream.dreamMVId == 0)
				{
					client = new BaseClient(baseApiUrl, "DreamMV", "PostDreamMV");
					bool result = client.Post<DreamsMV>(dreammv);
					client = new BaseClient(baseApiUrl, "DreamMV", "GetDreamMV");
					dreammv = client.Get<DreamsMV>(ibo.IBONum);
				}
				else
				{
					dreammv.dreamMVId = dream.dreamMVId;
					client = new BaseClient(baseApiUrl, "DreamMV", "PutDreamMV");
					string result = client.Put<DreamsMV>(dreammv.dreamMVId.ToString(), dreammv);
				}
				return Json(new { success = true, id = dreammv.dreamMVId });
			}
			catch
			{
				return Json(new { success = false, id = 0 });
			}
		}

		[IsNotPageRefresh]
		public ActionResult Details(int id)
		{
			ViewBag.areas = new SelectList(areas, "areaId", "title");
			BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDream");
			Dream dream = client.Get<Dream>(id.ToString());
			return PartialView(dream);
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
