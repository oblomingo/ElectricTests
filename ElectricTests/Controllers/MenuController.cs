using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            ViewBag.ControllerName = controllerName;
            
            //All menu buttons names with their controller names
            Dictionary<string, string> menu = new Dictionary<string, string>();
            menu.Add("Pagrindinis", "Home");
            menu.Add("Dokumentai", "Documents");
            menu.Add("Testavimas", "Tests");
            menu.Add("Pagalba", "Help");
            
            return PartialView(menu);
        }
    }
}
