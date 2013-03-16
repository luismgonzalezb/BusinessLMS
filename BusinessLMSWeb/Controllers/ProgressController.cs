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
            Progress Goalprogress = client.Get<Progress>(id);
            try
            {
                return Goalprogress.progress;
            }
            catch 
            {
                return 0;
            }
        }

        public ActionResult CreateProgress(int id)
        {
            Progress Progress = new Progress();
            Progress.GoalId = id;
            Progress.datetime = DateTime.Now;
            return PartialView(Progress);
        }

        public ActionResult EditProgress(string id)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
            Progress Goalprogress = client.Get<Progress>(id);
            return PartialView(Goalprogress);
        }

        [HttpPost]
        public ActionResult AddProgressAjax(Progress model)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    
                    BaseClient client = new BaseClient(baseApiUrl, "Progress", "GetProgress");
                    Progress Goalprogress = client.Get<Progress>(model.GoalId.ToString());
                    if (model.progress == 100)
                    {
                        client = new BaseClient(baseApiUrl, "Goals", "GetGoal");
                        Goal Goal = client.Get<Goal>(model.GoalId.ToString());
                        Goal.completed = true;
                        client = new BaseClient(baseApiUrl, "Goals", "PutGoal");
                        string result = client.Put<Goal>(Goal.goalId.ToString(), Goal);
                    }
                    if (Goalprogress == null)
                    {
                        client = new BaseClient(baseApiUrl, "Progress", "PostProgress");
                        string result = client.Post<Progress>(model);

                    }
                    else
                    {
                        model.ProgressId = Goalprogress.ProgressId;
                        client = new BaseClient(baseApiUrl, "Progress", "PutProgress");
                        string result = client.Put<Progress>(model.ProgressId.ToString(), model);
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
    }
}