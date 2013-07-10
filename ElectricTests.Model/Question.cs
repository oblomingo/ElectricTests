using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//Question can be without paragraph/document
		public int? FormattedDocumentId { get; set; }
		public FormattedDocument FormattedDocument { get; set; }

		public int? ParagraphId { get; set; }
		public Paragraph Paragraph { get; set; }
		
		//Advanced question information (todo later)
		public int? ParagraphsMarkIn { get; set; }
		public int? ParagraphsMarkOut { get; set; }
		public int? VoltageClass { get; set; }
		public string Category { get; set; }
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "Tuščias klausimo teksto laukas")]
		[StringLength(10000, ErrorMessage = "Klausimas neturi buti ilgesnis 10000 simbolių")]
		public string Title { get; set; }
		
		[Required(ErrorMessage = "Tuščias teisingo atsakymo laukas")]
		[StringLength(1000, ErrorMessage = "Atsakymas neturi buti ilgesnis 1000 simbolių")]
		public string Option1 { get; set; }

		[Required(ErrorMessage = "Tuščias antro atsakymo laukas")]
		[StringLength(1000, ErrorMessage = "Antras atsakymas neturi buti ilgesnis 1000 simbolių")]
		public string Option2 { get; set; }

		[StringLength(1000, ErrorMessage = "Trečias atsakymas neturi buti ilgesnis 1000 simbolių")]
		public string Option3 { get; set; }

		[StringLength(1000, ErrorMessage = "Ketvirtas atsakymas neturi buti ilgesnis 1000 simbolių")]
		public string Option4 { get; set; }

		[NotMapped]
		public int OptionSelected { get; set; }

		[NotMapped]
		public bool InThisTest { get; set; }

		[StringLength(1000, ErrorMessage = "Paaiškinimas neturi buti ilgesnis 1000 simbolių")]
		public string AnswerDescription { get; set; }

		// Navigation properties
		public ICollection<Test> Tests { get; set; }
	}
}