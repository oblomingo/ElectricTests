using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ElectricTests.Model
{
	public class Paragraph {

		public Paragraph () {
			Paragraphs = new HashSet<Paragraph>();
		}

		public Paragraph (string text, int number, string parentNumber = "") {
			Number = number;
			Text = text;
			ParentNumber = parentNumber;
			Paragraphs = new HashSet<Paragraph>();
		}
		[HiddenInput(DisplayValue=false)]
		public int Id { get; set; }
		public int Number { get; set; }
		public string Text { get; set; }
		public int FormattedDocumentId { get; set; }
        [HiddenInput(DisplayValue = false)]
		public int? SectionId { get; set; }
		public int? ParagraphId { get; set; }

        //Parent paragraph number (not id)
		[NotMapped]
		public string ParentNumber { get; set; }

        //Full path paragraph number (for view)
		[NotMapped]
		public string FullNumber { get; set; }
		
        //Navigation properties
		public FormattedDocument Document { get; set; }
		public ICollection<Question> Questions { get; set; } 
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}