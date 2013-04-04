using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;
using HtmlHelpers.BeginCollectionItem;

namespace ElectricTests.Controllers
{
	public class TestsController : Controller
	{
		//
		// GET: /Tests/
		public ActionResult Index () {
			return View(TestsRepository.GetAllTests());
		}

		[HttpGet]
		[Authorize]
		public ActionResult Add () {
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult Add (Test test) {
			if (ModelState.IsValid) {
				using (var pContext = new ProjectContext()) {
					pContext.Tests.Add(test);
					pContext.SaveChanges();
					return RedirectToAction("AddQuestions", new { id = test.Id });
				}
			}
			return View();
		}

		[HttpGet]
		[Authorize]
		public ActionResult AddQuestions (int Id) {
			return View(TestsRepository.GetTestAndAllQuestions(Id));
		}

		[HttpPost]
		[Authorize]
		public ActionResult AddQuestions (Test test) {
			if (ModelState.IsValid) {
				TestsRepository.saveTestToDB(test);
			}
			return RedirectToAction("AddQuestions", new { id = test.Id });
		}



		public ActionResult Show (int Id) {
			Test test = TestsRepository.GetTestByID(Id);
			return View(test);
		}
	}
}
