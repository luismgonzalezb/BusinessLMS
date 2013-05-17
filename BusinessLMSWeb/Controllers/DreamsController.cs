using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using System;
using System.Collections.Generic;
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
				return IBOVirtualAPI.GetAreas(ibo.languageId);
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
				List<Dream> dreams = IBOVirtualAPI.GetDreamsUserLevel(ibo.IBONum, id.ToString());
				if ((dreams.Count > 0) || (id == 0))
				{
					ViewBag.dreamLevel = id;
					ViewBag.nextLevel = id + 1;
					List<Dream> dreamsLevel = IBOVirtualAPI.GetDreamsUserLevel(ibo.IBONum, (id + 1).ToString());
					Dictionary<Timeframe, Dream> timeframeDreams = new Dictionary<Timeframe, Dream>();
					List<Timeframe> timeframes = IBOVirtualAPI.GetTimeFrames(id, ibo.languageId);
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
			ViewBag.completed = model.achieved == true ? " " + TextResources.Businesslms.achieved + " " : " " + TextResources.Businesslms.WorkingOn + " ";
			ViewBag.etaMsg = model.achieved == true ? " " + TextResources.Businesslms.Before + " " : " " + TextResources.Businesslms.Until + " ";
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
			dream.IBONum = ibo.IBONum;
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
				bool result = IBOVirtualAPI.Create<Dream>(model);
			}
			catch { }
			return RedirectToAction("Index");
		}

		public ActionResult MoreDreams(int id)
		{
			try
			{
				Dream dream = IBOVirtualAPI.Get<Dream>(id.ToString());
				if (dream != null)
				{
					Dream newDream = ModelParser.ParseDream(dream);
					bool result = IBOVirtualAPI.Create<Dream>(newDream);
				}
			}
			catch { }
			return RedirectToAction("Index");
		}

		[IsNotPageRefresh]
		public ActionResult EditDream(int id)
		{
			ViewBag.areas = new SelectList(areas, "areaId", "title");
			Dream dream = IBOVirtualAPI.Get<Dream>(id.ToString());
			return PartialView(dream);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult EditDream(Dream model)
		{
			try
			{
				string result = IBOVirtualAPI.Update<Dream>(model.dreamId.ToString(), model);
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
			DreamsMV dream = IBOVirtualAPI.GetDreamMV(ibo.IBONum);
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
				if (dream.dreamMVId == 0)
				{
					bool result = IBOVirtualAPI.CreateDreamMV(dreammv);
					dreammv = IBOVirtualAPI.GetDreamMV(ibo.IBONum);
				}
				else
				{
					dreammv.dreamMVId = dream.dreamMVId;
					string result = IBOVirtualAPI.UpdateDreamMV(dreammv.dreamMVId.ToString(), dreammv);
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
			Dream dream = IBOVirtualAPI.Get<Dream>(id.ToString());
			return PartialView(dream);
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}
	}
}