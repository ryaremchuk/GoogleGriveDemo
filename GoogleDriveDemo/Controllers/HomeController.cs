using System.Web.Mvc;

namespace GoogleDriveDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Google Drive Integration Demo";

            return View();
        }
    }
}