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
	public class GoalsController : BaseWebController
	{
		private List<Tool> tools
		{
			get
			{
				return IBOVirtualAPI.GetTools(ibo.languageId);
			}
		}

		private List<Dream> dreams
		{
			get
			{
				List<Dream> dreams = IBOVirtualAPI.GetDreamsUser(ibo.IBONum);
				dreams = dreams.GroupBy(d => d.dream1).Select(d => d.FirstOrDefault()).ToList();
				return dreams;
			}
		}

		public ActionResult Index()
		{
			return View();
		}

		[IsNotPageRefresh]
		public ActionResult GoalsList(int id)
		{
			if (ibo != null)
			{
				List<Goal> goals = IBOVirtualAPI.GetIBOLevelGoals(ibo.IBONum, id.ToString());
				if ((goals.Count > 0) || (id == 0))
				{
					ViewBag.goalLevel = id;
					ViewBag.nextLevel = id + 1;
					List<Goal> goalsLevel = IBOVirtualAPI.GetIBOLevelGoals(ibo.IBONum, (id + 1).ToString());
					Dictionary<Timeframe, Goal> timeframeGoals = new Dictionary<Timeframe, Goal>();
					List<Timeframe> timeframes = IBOVirtualAPI.GetTimeFrames(id, ibo.languageId);
					Timeframe last = timeframes.Last();
					if (goalsLevel.Count < 1) ViewBag.lastItem = last;
					foreach (Timeframe time in timeframes)
					{
						Goal dream = (from d in goals
									  where d.timeframeId == time.timeframeId
									  select d).FirstOrDefault();
						timeframeGoals.Add(time, dream);
					}
					return PartialView(timeframeGoals);
				}
			}
			return null;
		}

		[IsNotPageRefresh]
		public ActionResult DisplayGoal(Goal model, bool last)
		{
			ViewBag.tool = (from tool in tools where tool.toolId == model.toolId select tool.name).FirstOrDefault();
			ViewBag.completed = model.completed == true ? "Acieved" : " I'm working on ";
			ViewBag.etaMsg = model.completed == true ? " Before " : " Until ";
			ViewBag.eta = String.Format("{0:dddd dd MMMM yyyy}", model.datetime);
			ViewBag.last = last;
			return PartialView(model);
		}

		[IsNotPageRefresh]
		public ActionResult NewGoal(int timeframeId, int days, int goalLevel)
		{
			ViewBag.tools = new SelectList(tools, "toolId", "name");
			ViewBag.dreams = new SelectList(dreams, "dreamId", "dream1");
			ViewBag.formName = string.Concat("createIBOForm", goalLevel.ToString(), timeframeId.ToString());
			ViewBag.uploadName = string.Concat("file_upload", goalLevel.ToString(), timeframeId.ToString());
			Goal goal = new Goal();
			goal.timeframeId = timeframeId;
			goal.goalLevel = goalLevel;
			goal.datetime = DateTime.Now.AddDays(days);
			goal.goal1 = 1;
			return PartialView(goal);
		}

		[HttpPost]
		public ActionResult NewGoal(Goal model)
		{
			try
			{
				bool result = IBOVirtualAPI.Create<Goal>(model);
			}
			catch { }
			return RedirectToAction("Index");
		}

		public ActionResult MoreGoals(int id)
		{
			try
			{
				Goal goal = IBOVirtualAPI.Get<Goal>(id.ToString());
				if (goal != null)
				{
					Goal newGoal = ModelParser.ParseGoal(goal);
					bool result = IBOVirtualAPI.Create<Goal>(newGoal);
				}
			}
			catch { }
			return RedirectToAction("Index");
		}

		[IsNotPageRefresh]
		public ActionResult EditGoal(int id)
		{
			ViewBag.tools = new SelectList(tools, "toolId", "name");
			ViewBag.dreams = new SelectList(dreams, "dreamId", "dream1");
			Goal goal = IBOVirtualAPI.Get<Goal>(id.ToString());
			return PartialView(goal);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult EditGoal(Goal model)
		{
			try
			{
				string result = IBOVirtualAPI.Update<Goal>(model.goalId.ToString(), model);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}
	}
}