using System.Collections.Generic;
using System.Linq;
using ElectricTests.Model;

namespace ElectricTests.Repository {

    public interface IQuestionsRepository {
        List<Question> GetAllQuestions();
        Question GetQuestionById(int id);
        Test GetTestAndAllQuestions(int id);
        bool SaveQuestionChanges(Question updatedQuestion);
        void DeleteQuestion(int id);
    }

    public class QuestionsRepository : IQuestionsRepository {
        #region IQuestionsRepository Members

        /// <summary>
        /// Get all questions from db
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAllQuestions() {
            using (var projectContext = new ProjectContext()) {
                return projectContext.Questions
                    .Include("Paragraph")
                    .Include("Paragraph.Document")
                    .Include("Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
                    .ToList();
            }
        }

        /// <summary>
        /// Get question by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetQuestionById(int id) {
            var context = new ProjectContext();
            Question question = context.Questions
                .Include("Paragraph")
                .Include("Paragraph.Document")
                .Include("Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
                .SingleOrDefault(c => c.Id == id);
            return question;
        }


        /// <summary>
        /// Get test and all questions, set bool property InThisTest true to this test questions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Test GetTestAndAllQuestions(int id) {
            using (var context = new ProjectContext()) {
                Test test = context.Tests.SingleOrDefault(c => c.Id == id);

                if (test != null) {
                    ICollection<Question> allQuestions = context.Questions.Include("Tests").ToList();

                    //InThisTest bool property to show as checkbox in view 
                    foreach (Question question in allQuestions) {
                        if (question.Tests == null) continue;

                        foreach (Test questionTest in question.Tests) {
                            if (questionTest.Id == id)
                                question.InThisTest = true;
                        }
                    }
                    test.Questions = allQuestions;
                }
                return test;
            }
        }

        /// <summary>
        /// Save changed question to db
        /// </summary>
        /// <param name="updatedQuestion"></param>
        public bool SaveQuestionChanges(Question updatedQuestion) {
            using (var context = new ProjectContext()) {
                Question question =
                    context.Questions.Include("Paragraph").SingleOrDefault(q => q.Id == updatedQuestion.Id);
                if (question != null) {
                    //Save updated question properties to db
                    question.Title = updatedQuestion.Title;
                    question.Option1 = updatedQuestion.Option1;
                    question.Option2 = updatedQuestion.Option2;
                    question.Option3 = updatedQuestion.Option3;
                    question.Option4 = updatedQuestion.Option4;
                    question.AnswerDescription = updatedQuestion.AnswerDescription;

                    if (updatedQuestion.ParagraphId != null)
                        question.ParagraphId = updatedQuestion.ParagraphId;

                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Delete question by id from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteQuestion(int id) {
            using (var context = new ProjectContext()) {
                Question question = context.Questions.SingleOrDefault(q => q.Id == id);
                context.Questions.Remove(question);
                context.SaveChanges();
            }
        }

        #endregion
    }
}