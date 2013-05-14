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
	public class ProgressController : BaseWebController
	{
		private List<Tool> tools
		{
			get
			{
				return IBOVirtualAPI.GetTools(ibo.languageId);
			}
		}

		public ActionResult Index()
		{
			List<Goal> goals = IBOVirtualAPI.GetIBOGoalsProgress(ibo.IBONum);
			return View(goals);
		}

		public string GetTool(int Id)
		{
			return (from tool in tools where tool.toolId == Id select tool.name).FirstOrDefault();
		}

		public decimal GetProgress(string id)
		{

			GoalProgress Goalprogress = IBOVirtualAPI.GetProgress(id);
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
			GoalProgress Goalprogress = IBOVirtualAPI.GetProgress(id);
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

					GoalProgress Goalprogress = IBOVirtualAPI.GetProgress(model.goalId.ToString());
					if (model.progress == 100)
					{
						Goal Goal = IBOVirtualAPI.Get<Goal>(model.goalId.ToString());
						Goal.completed = true;
						string result = IBOVirtualAPI.Update<Goal>(Goal.goalId.ToString(), Goal);
					}
					if (Goalprogress == null)
					{
						bool result = IBOVirtualAPI.CreateProgress(model);
					}
					else
					{
						model.progressId = Goalprogress.progressId;
						string result = IBOVirtualAPI.UpdateProgress(model.progressId.ToString(), model);
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