using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class FollowupController : BaseWebController
    {
        //
        // GET: /Followup/

        public ActionResult Index()
        {
            return View();
        }

    }
}
