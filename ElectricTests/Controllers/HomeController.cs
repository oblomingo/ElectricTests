using System.Web.Mvc;

namespace ElectricTests.Controllers {
    public class HomeController : Controller {
        //
        // GET: /Home/

        public ActionResult Index() {
            return View();
        }
    }
}