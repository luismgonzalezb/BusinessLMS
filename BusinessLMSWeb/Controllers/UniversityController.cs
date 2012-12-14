using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
    [Authorize]
    public class UniversityController : BaseWebController
    {
        //
        // GET: /University/

        public ActionResult Index()
        {
            return View();
        }

    }
}
