using System;
using System.Collections.Generic;
using System.Linq;
using ElectricTests.Model;

namespace ElectricTests.Repository
{
	public static class TestsRepository {
		
		/// <summary>
		/// Get all tests with only their questions 
		/// </summary>
		/// <returns></returns>
		public static List<Test> GetAllTests () {
			using (var projectContext = new ProjectContext()) {
				return projectContext.Tests.Include("Questions").ToList();
			}
		}

		/// <summary>
		/// Get test by id and some random questions
		/// </summary>
		/// <param name="testId"></param>
		/// <returns></returns>
		public static Test GetTestByID(int testId)
		{
			var context = new ProjectContext();
			var test = context.Tests
				.Include("Questions")
				.Include("Questions.Paragraph")
				.Include("Questions.Paragraph.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
				.Include("Questions.FormattedDocument")
				.SingleOrDefault(c => c.Id == testId);

			if (test != null)
				test.Questions = context.Questions.
					OrderBy(q => Guid.NewGuid()).
					Where(q => q.Tests.Select(o => o.Id).Contains(test.Id)).
					Take(test.OneTestQuestionsNumber).
					ToList();

			//To avoid javascript error "A circular reference was detected while serializing an object of type"
			test = SetQuestionNavPropToNull(test);
			return test;
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
		public static Test GetTestAndAllQuestions(int testId)
		{
			using (var context = new ProjectContext())
			{
				Test test = context.Tests.SingleOrDefault(c => c.Id == testId);

				if (test != null)
				{
					ICollection<Question> allQuestions = context.Questions.Include("Tests").ToList();

					//InThisTest bool property to show as checkbox in view 
					foreach (var question in allQuestions)
					{
						if (question.Tests != null)
						{
							foreach (var questionTest in question.Tests)
							{
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
		public static void saveTestToDB(Test updatedTest)
		{

			var idArray = (from q in updatedTest.Questions
						   where q.InThisTest
						   select q.Id).ToArray();

			using (var context = new ProjectContext())
			{

				var test = context.Tests.SingleOrDefault(t => t.Id == updatedTest.Id);
				var allQuestions = context.Questions.Include("Tests").ToList();

				foreach (var question in allQuestions)
				{
					if (idArray.Contains(question.Id))
						question.Tests.Add(test);
					else
						if (question.Tests.Contains(test))
							question.Tests.Remove(test);
				}
				context.SaveChanges();
			}
		}
	}
}
