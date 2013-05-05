using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class ProgressController : BaseWebController
	{
		private List<Tool> tools
		{
			get
			{
				return ControllersHelper.GetTools(baseApiUrl, ibo.languageId);
			}
		}

		public ActionResult Index()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoalsProgress");
			List<Goal> goals = client.Get<List<Goal>>(ibo.IBONum);
			return View(goals);
		}

		public string GetTool(int Id)
		{
			return (from tool in tools where tool.toolId == Id select tool.name).FirstOrDefault();
		}

		public decimal GetProgress(string id)
		{

			BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
			GoalProgress Goalprogress = client.Get<GoalProgress>(id);
			try
			{
				return Goalprogress.progress;
			}
			catch
			{
				return 0;
			}
		}

		[IsNotPageRefresh]
		public ActionResult CreateProgress(int id)
		{
			GoalProgress Progress = new GoalProgress();
			Progress.goalId = id;
			Progress.datetime = DateTime.Now;
			return PartialView(Progress);
		}

		[IsNotPageRefresh]
		public ActionResult EditProgress(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
			GoalProgress Goalprogress = client.Get<GoalProgress>(id);
			return PartialView(Goalprogress);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult AddProgressAjax(GoalProgress model)
		{
			if (ModelState.IsValid == true)
			{
				try
				{

					BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
					GoalProgress Goalprogress = client.Get<GoalProgress>(model.goalId.ToString());
					if (model.progress == 100)
					{
						client = new BaseClient(baseApiUrl, "Goals", "GetGoal");
						Goal Goal = client.Get<Goal>(model.goalId.ToString());
						Goal.completed = true;
						client = new BaseClient(baseApiUrl, "Goals", "PutGoal");
						string result = client.Put<Goal>(Goal.goalId.ToString(), Goal);
					}
					if (Goalprogress == null)
					{
						client = new BaseClient(baseApiUrl, "Progress", "PostProgress");
						bool result = client.Post<GoalProgress>(model);
					}
					else
					{
						model.progressId = Goalprogress.progressId;
						client = new BaseClient(baseApiUrl, "Progress", "PutProgress");
						string result = client.Put<GoalProgress>(model.progressId.ToString(), model);
					}
					return Json(model);

				}
				catch
				{
					return Json(new { success = false });
				}

			}
			else
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