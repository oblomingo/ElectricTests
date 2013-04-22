using System.Collections.Generic;
using System.Web.Mvc;

namespace ElectricTests.Controllers
{
    public class MenuController : Controller
    {
        /// <summary>
        /// Show menu as widget, mark button with current controller
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Show (string controllerName = null) {
            //Controller name to mark current menu button
            ViewBag.ControllerName = controllerName;
            
            //All menu buttons names with their controller names
            var menu = new Dictionary<string, string> {
                {"Pagrindinis", "Home"},
                {"Dokumentai", "Documents"},
                {"Testavimas", "Tests"},
                {"Pagalba", "Help"}
            };
            return PartialView(menu);
        }
    }
}
