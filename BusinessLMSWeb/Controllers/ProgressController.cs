using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class ProgressController : BaseWebController
    {
        //
        // GET: /Progress/

        public ActionResult Index()
        {
            return View();
        }

    }
}
