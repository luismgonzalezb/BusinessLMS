using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class ProgressController : BaseWebController
    {
        //
        // GET: /Progress/
        private List<Tool> tools
        {
            get
            {
                return ControllersHelper.GetTools(baseApiUrl);
            }
        }

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Goals", "GetIBOGoals");
            List<Goal> goals = client.Get<List<Goal>>(ibo.IBONum);
            return View(goals);
        }

        public string GetTool(int Id)
        {
            return (from tool in tools where tool.toolId == Id select tool.name).FirstOrDefault();
        }

        public double GetProgress(int Id)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
            Progress progress = client.Get<Progress>(Id);
            return progress.progress;
        }

        public ActionResult CreateProgress(int id)
        {
            Progress Progress = new Progress();
            Progress.GoalId = id;
            Progress.datetime = DateTime.Now;
            return PartialView(Progress);
        }

        [HttpPost]
        public ActionResult AddProgressAjax(Progress model)
        {
            if (ModelState.IsValid == true)
            {
                //try
                //{
                    BaseClient client = new BaseClient(baseApiUrl, "Progress", "PostProgress");
                    string result = client.Post<Progress>(model);
                    return Json(model);
                //}
                //catch
                //{
                //    return Json(new { success = false });
                //}
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
