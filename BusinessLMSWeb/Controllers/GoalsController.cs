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
	public class GoalsController : BaseWebController
	{

		private List<Tool> tools
		{
			get
			{
				return ControllersHelper.GetTools(baseApiUrl);
			}
		}

		private List<Dream> dreams
		{
			get
			{
				BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUser");
				List<Dream> dreams = client.Get<List<Dream>>(ibo.IBONum);
				dreams = dreams.GroupBy(d => d.dream1).Select(d => d.FirstOrDefault()).ToList();
				return dreams;
			}
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GoalsList(int id)
		{

			if (ibo != null)
			{
				BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOLevelGoals");
				NameValueCollection parms = new NameValueCollection(){
					{ "id", ibo.IBONum }, 
					{ "level", id.ToString()} 
				};
				List<Goal> goals = client.Get<List<Goal>>(parms);
				if ((goals.Count > 0) || (id == 0))
				{
					ViewBag.goalLevel = id;
					ViewBag.nextLevel = id + 1;
					parms = new NameValueCollection() { { "id", ibo.IBONum }, { "level", (id + 1).ToString() } };
					List<Goal> goalsLevel = client.Get<List<Goal>>(parms);
					Dictionary<Timeframe, Goal> timeframeGoals = new Dictionary<Timeframe, Goal>();
					List<Timeframe> timeframes = ControllersHelper.GetTimeFrames(id, baseApiUrl);
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

		public ActionResult DisplayGoal(Goal model, bool last)
		{
			ViewBag.tool = (from tool in tools where tool.toolId == model.toolId select tool.name).FirstOrDefault();
			ViewBag.completed = model.completed == true ? "Acieved" : " I'm working on ";
			ViewBag.etaMsg = model.completed == true ? " Before " : " Until ";
			ViewBag.eta = String.Format("{0:dddd dd MMMM yyyy}", model.datetime);
			ViewBag.last = last;
			return PartialView(model);
		}

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
				BaseClient client = new BaseClient(baseApiUrl, "Goals", "PostGoal");
				bool result = client.Post<Goal>(model);
			}
			catch { }
			return RedirectToAction("Index");
		}

		public ActionResult MoreGoals(int id)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetGoal");
				Goal goal = client.Get<Goal>(id.ToString());
				if (goal != null)
				{
					Goal newGoal = ModelParser.ParseGoal(goal);
					client = new BaseClient(baseApiUrl, "Goals", "PostGoal");
					bool result = client.Post<Goal>(newGoal);
				}
			}
			catch { }
			return RedirectToAction("Index");
		}

		public ActionResult EditGoal(int id)
		{
			ViewBag.tools = new SelectList(tools, "toolId", "name");
			ViewBag.dreams = new SelectList(dreams, "dreamId", "dream1");
			BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetGoal");
			Goal goal = client.Get<Goal>(id.ToString());
			return PartialView(goal);
		}

		[HttpPost]
		public ActionResult EditGoal(Goal model)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Goals", "PutGoal");
				string result = client.Put<Goal>(model.goalId.ToString(), model);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}

		}

		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
