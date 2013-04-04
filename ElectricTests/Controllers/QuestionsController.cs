using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers
{
    public class QuestionsController : Controller
    {
        //
        // GET: /Questions/
        [Authorize]
        public ActionResult Index()
        {
            List<Question> questions = QuestionsRepository.GetAllQuestions();
            return View(questions);
        }

        [HttpPost]
        public bool Add(Question question)
        {
            if (ModelState.IsValid)
            {
                using (var pContext = new ProjectContext())
                {
                    pContext.Questions.Add(question);
                    pContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public ActionResult Edit(int Id)
        {
            Question question = QuestionsRepository.GetQuestionByID(Id);
            return View(question);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                if (QuestionsRepository.saveQuestionChanges(question))
                {
                    return RedirectToAction("Index", "Questions");
                }
            }
            return View(question);
        }

        [Authorize]
        public ActionResult Delete(int Id)
        {
            QuestionsRepository.DeleteQuestion(Id);
            return RedirectToAction("Index", "Questions");
        }
    }
}
