using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class DashboardController : BaseWebController
    {

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "IBO", "GetMyIBOs");
            List<IBO> ibos = client.Get<List<IBO>>(ibo.IBONum);
            return View(ibos);
        }

    }
}
