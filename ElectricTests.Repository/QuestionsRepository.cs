using System;
using System.Collections.Generic;
using System.Linq;
using ElectricTests.Model;

namespace ElectricTests.Repository
{
	public static class QuestionsRepository {
		
		/// <summary>
		/// Get all tests with only their questions 
		/// </summary>
		/// <returns></returns>
		public static List<Question> GetAllQuestions () {
			using (var projectContext = new ProjectContext()) {
				return projectContext.Questions
					.Include("Paragraph")
					.Include("Paragraph.Document")
					.Include("Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
					.ToList();
			}
		}

		/// <summary>
		/// Get question by id
		/// </summary>
		/// <param name="testId"></param>
		/// <returns></returns>
		public static Question GetQuestionByID(int questionId)
		{
			var context = new ProjectContext();
			var question = context.Questions
				.Include("Paragraph")
				.Include("Paragraph.Document")
				.Include("Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
				.SingleOrDefault(c => c.Id == questionId);
			return question;
		}

		/// <summary>
		/// Set question navigation properties to null to avoid circular reference error
		/// </summary>
		/// <param name="test"></param>
		/// <returns></returns>
		private static Test SetQuestionNavPropToNull(Test test)
		{
			foreach (var question in test.Questions)
			{
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
		private static ICollection<Paragraph> SetParagraphNavPropToNull(ICollection<Paragraph> paragraphs)
		{
			foreach (var paragraph in paragraphs)
			{
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
		/// <param name="testId"></param>
		/// <returns></returns>
		public static Test GetTestAndAllQuestions (int testId) {
			using (var context = new ProjectContext()) {
				Test test = context.Tests.SingleOrDefault(c => c.Id == testId);

				if (test != null) {
					ICollection<Question> allQuestions = context.Questions.Include("Tests").ToList();

					//InThisTest bool property to show as checkbox in view 
					foreach (var question in allQuestions) {
						if (question.Tests != null) {
							foreach (var questionTest in question.Tests) {
								if (questionTest.Id == testId)
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
		public static bool saveQuestionChanges(Question updatedQuestion)
		{
			using (var context = new ProjectContext()) {

				var question = context.Questions.Include("Paragraph").SingleOrDefault(q => q.Id == updatedQuestion.Id);
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

        public static void DeleteQuestion(int Id)
        {
            using (var context = new ProjectContext())
            {
                var question = context.Questions.SingleOrDefault(q => q.Id == Id);
                context.Questions.Remove(question);
                context.SaveChanges();
            }
        }
	}
}
