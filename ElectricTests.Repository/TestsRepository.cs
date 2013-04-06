using System;
using System.Collections.Generic;
using System.Linq;
using ElectricTests.Model;

namespace ElectricTests.Repository {

    public interface ITestRepository {
        List<Test> GetAllTests();
        Test GetTestById(int id);
        Test GetTestAndAllQuestions(int id);
        void SaveTestToDb(Test updatedTest);
    }

    public class TestsRepository : ITestRepository {
        /// <summary>
        /// Get all tests with only their questions 
        /// </summary>
        /// <returns></returns>
        public List<Test> GetAllTests() {
            using (var projectContext = new ProjectContext()) return projectContext.Tests.Include("Questions").ToList();
        }

        /// <summary>
        /// Get test by id and some random questions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Test GetTestById(int id) {
            var context = new ProjectContext();
            Test test = context.Tests
                .Include("Questions")
                .Include("Questions.Paragraph")
                .Include("Questions.Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
                .Include("Questions.FormattedDocument")
                .SingleOrDefault(c => c.Id == id);

            if (test != null) {
                test.Questions = context.Questions.
                    OrderBy(q => Guid.NewGuid()).
                    Where(q => q.Tests.Select(o => o.Id).Contains(test.Id)).
                    Take(test.OneTestQuestionsNumber).
                    ToList();
            }

            //To avoid javascript error "A circular reference was detected while serializing an object of type"
            test = SetQuestionNavPropToNull(test);
            return test;
        }

        /// <summary>
        /// Set question navigation properties to null to avoid circular reference error
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private Test SetQuestionNavPropToNull(Test test) {
            foreach (Question question in test.Questions) {
                //Set question navigation properties to null
                question.Tests = null;
                question.FormattedDocument.Paragraphs = null;
                question.FormattedDocument.Sections = null;

                //we have some questions without paragraphs
                if (question.Paragraph == null)
                    continue;

                //Set question paragraph navigation properties to null
                question.Paragraph.Questions = null;
                //we have some questions without paragraphs

                //Set child paragraphs navigation properties to null 
                if (question.Paragraph.Paragraphs != null)
                    question.Paragraph.Paragraphs = SetParagraphNavPropToNull(question.Paragraph.Paragraphs);
            }
            return test;
        }

        /// <summary>
        /// Set paragraph navigation properties to null to avoid circular reference error
        /// </summary>
        /// <param name="paragraphs"></param>
        /// <returns></returns>
        private ICollection<Paragraph> SetParagraphNavPropToNull(ICollection<Paragraph> paragraphs) {
            foreach (Paragraph paragraph in paragraphs) {
                paragraph.Questions = null;
                paragraph.Document = null;
                if (paragraph.Paragraphs != null)
                    paragraph.Paragraphs = SetParagraphNavPropToNull(paragraph.Paragraphs);
            }
            return paragraphs;
        }

        /// <summary>
        /// Get test and all qeustions
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
                        if (question.Tests != null) {
                            foreach (Test questionTest in question.Tests) {
                                if (questionTest.Id == id)
                                    question.InThisTest = true;
                            }
                        }
                    }
                    test.Questions = allQuestions;
                }
                return test;
            }
        }

        /// <summary>
        /// Save changed test to data base
        /// </summary>
        /// <param name="updatedTest"></param>
        public void SaveTestToDb(Test updatedTest) {
            int[] idArray = (from q in updatedTest.Questions
                             where q.InThisTest
                             select q.Id).ToArray();

            using (var context = new ProjectContext()) {
                Test test = context.Tests.SingleOrDefault(t => t.Id == updatedTest.Id);
                List<Question> allQuestions = context.Questions.Include("Tests").ToList();

                foreach (Question question in allQuestions) {
                    if (idArray.Contains(question.Id))
                        question.Tests.Add(test);
                    else if (question.Tests.Contains(test))
                        question.Tests.Remove(test);
                }
                context.SaveChanges();
            }
        }
    }
}