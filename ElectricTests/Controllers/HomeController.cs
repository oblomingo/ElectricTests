using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ElectricTests.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

		public string Test() {
			string result = "";
			//string notParagraph = "blablablalbal";
			string paragraph = "1. Bla bla bla.";
			Regex regex = new Regex(@"(\d+)\.(?:(\d+)\.)?\s(.+)");
			Match match = regex.Match(paragraph);
			if(match.Success) {
				for (int i= 0; i < match.Groups.Count; i++) {
					result += i + ". -" + match.Groups[i] + ".";
				}
			}
			else {
				result = "no matches";
			}
			return result;
		}

	}
}
