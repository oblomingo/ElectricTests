using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers
{
	public class TestsController : Controller {

		private readonly ITestRepository _repository;
		public TestsController (ITestRepository repository) {
			//Save TestRepository object (Unity)
			_repository = repository;
		}
		//
		// GET: /Tests/
		public ActionResult Index () {
			return View(_repository.GetAllTests());
		}

		/// <summary>
		/// Add new test form
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        [Authorize(Roles = "Administrator")]
		public ActionResult Add () {
			return View();
		}

		/// <summary>
		/// Add test to db, then redirect and add questions to new test
		/// </summary>
		/// <param name="test"></param>
		/// <returns></returns>
		[HttpPost]
        [Authorize(Roles = "Administrator")]
		public ActionResult Add (Test test) {
			if (ModelState.IsValid) {
				using (var pContext = new ProjectContext()) {
					pContext.Tests.Add(test);
					pContext.SaveChanges();
					return RedirectToAction("AddQuestions", new { id = test.Id});
				}
			}
			return View();
		}

		/// <summary>
		/// Add questions to test form
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
        [Authorize(Roles = "Administrator")]
		public ActionResult AddQuestions (int id) {
			return View(_repository.GetTestAndAllQuestions(id));
		}
																											
		/// <summary>
		/// Add questions to test and save changes to db
		/// </summary>
		/// <param name="test"></param> 
		/// <returns></returns>
		[HttpPost]
        [Authorize(Roles = "Administrator")]
		public ActionResult AddQuestions (Test test) {
			if (ModelState.IsValid) {
				_repository.SaveTestToDb(test);
				ViewBag.ChangedState = true;
			}
			return View(test);
		}


		/// <summary>
		/// Show testing, javascript will get answers, process and show testing result
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Show (int id) {
			Test test = _repository.GetTestById(id);
			if (test == null) {
				return RedirectToAction("Index");
			}
			return View(test);
		}
	}
}
