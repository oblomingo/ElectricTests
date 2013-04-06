using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTests.Model
{
	public class Question {
		public Question() {}
		public Question(string title, string option1, string option2,
			string option3 = "", string option4 = "", string answerDescription = "") {
			Title = title;
			Option1 = option1;
			Option2 = option2;
			Option3 = option3;
			Option4 = option4;
			AnswerDescription = answerDescription;
		}

		public Question(int documentId, int paragraphId, string title, string option1, string option2, 
			string option3 = "", string option4 = "", string imageUrl = "", string answerDescription = "") {
			FormattedDocumentId = documentId;
			ParagraphId = paragraphId;
			Title = title;
			Option1 = option1;
			Option2 = option2;
			Option3 = option3;
			Option4 = option4;
		    ImageUrl = imageUrl;
		    AnswerDescription = answerDescription;
		}

		public Question(string title, string answerDescription) {
			Title = title;
			AnswerDescription = answerDescription;
		}

		public int Id { get; set; }

		public int? FormattedDocumentId { get; set; }
		public FormattedDocument FormattedDocument { get; set; }

		public int? ParagraphId { get; set; }
		public Paragraph Paragraph { get; set; }
		
		public int? ParagraphsMarkIn { get; set; }
		public int? ParagraphsMarkOut { get; set; }
		public int? VoltageClass { get; set; }
		public string Category { get; set; }

		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public string Option1 { get; set; }
		public string Option2 { get; set; }
		public string Option3 { get; set; }
		public string Option4 { get; set; }

		[NotMapped]
		public int OptionSelected { get; set; }

		[NotMapped]
		public bool InThisTest { get; set; }

		public string AnswerDescription { get; set; }

		// Navigation properties
		public ICollection<Test> Tests { get; set; }
	}
}