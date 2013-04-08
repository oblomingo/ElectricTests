using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers {
    public class QuestionsController : Controller {
        private readonly IQuestionsRepository _repository;

        public QuestionsController(IQuestionsRepository repository) {
            //Get question repository object (Unity)
            _repository = repository;
        }

        //
        // GET: /Questions/
        [Authorize(Users="Aleksandr")]
        public ActionResult Index() {
            return View(_repository.GetAllQuestions());
        }

        /// <summary>
        /// Add question to db
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Question question) {
            if (ModelState.IsValid) {
                using (var pContext = new ProjectContext()) {
                    pContext.Questions.Add(question);
                    pContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Edit question by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id) {
            return View(_repository.GetQuestionById(id));
        }

        /// <summary>
        /// Validate question form and save it to db, then redirect to Questions list page
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Users = "Aleksandr")]
        public ActionResult Edit(Question question) {
            if (ModelState.IsValid) {
                if (_repository.SaveQuestionChanges(question))
                    return RedirectToAction("Index", "Questions");
            }
            return View(question);
        }

        /// <summary>
        /// Delete question by id from db, then redirect to Questons list page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Users = "Aleksandr")]
        public ActionResult Delete(int id) {
            _repository.DeleteQuestion(id);
            return RedirectToAction("Index", "Questions");
        }
    }
}