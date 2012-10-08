using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using WebMatrix.WebData;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class DreamsController : BaseWebController
    {

        private List<Area> areas
        {
            get
            {
                BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetAreas");
                return client.Get<List<Area>>();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DreamList(int id)
        {
            if (ibo != null)
            {
                BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDreamsUserLevel");
                NameValueCollection parms = new NameValueCollection(){
                    { "id", ibo.IBONum }, 
                    { "level", id.ToString()} 
                };
                List<Dream> dreams = client.Get<List<Dream>>(parms);
                if ((dreams.Count > 0) || (id == 0))
                {
                    ViewBag.dreamLevel = id;
                    ViewBag.nextLevel = id + 1;
                    Dictionary<Timeframe, Dream> timeframeDreams = new Dictionary<Timeframe, Dream>();
                    List<Timeframe> timeframes = GetTimeFrames(id);
                    Timeframe last = timeframes.Last();
                    ViewBag.lastItem = last;
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

        public ActionResult DisplayDream(Dream model, bool last)
        {
            ViewBag.area = (from area in areas where area.areaId == model.areaId select area.title).FirstOrDefault();
            ViewBag.completed = model.achieved == true ? "Acieved" : " I'm working on ";
            ViewBag.etaMsg = model.achieved == true ? " Before " : " Until ";
            ViewBag.eta = String.Format("{0:dddd MMMM yyyy}", model.datetime);
            ViewBag.last = last;
            return PartialView(model);
        }

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
                string result = client.Post<Dream>(model);
            }
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult MoreDreams(int id)
        {
            try
            {
                BaseClient client = new BaseClient(baseApiUrl, "Dreams", "GetDream");
                Dream dream = client.Get<Dream>(id);
                if (dream != null)
                {
                    Dream newDream = new Dream();
                    newDream.IBONum = dream.IBONum;
                    newDream.dream1 = dream.dream1;
                    newDream.achieved = dream.achieved;
                    newDream.areaId = dream.areaId;
                    newDream.datetime = dream.datetime;
                    newDream.picture = dream.picture;
                    newDream.dreamLevel = dream.dreamLevel + 1;
                    newDream.timeframeId = dream.timeframeId + 1;
                    client = new BaseClient(baseApiUrl, "Dreams", "PostDream");
                    string result = client.Post<Dream>(newDream);
                }
            }
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult EditDream()
        {
            ViewBag.areas = areas;
            return PartialView();
        }

        public List<Timeframe> GetTimeFrames(int level)
        {
            BaseClient client = new BaseClient(baseApiUrl, "Lists", "GetTimeframesLevel");
            return client.Get<List<Timeframe>>(level);
        }

    }
}
