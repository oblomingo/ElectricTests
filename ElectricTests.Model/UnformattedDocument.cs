using System.ComponentModel.DataAnnotations;

namespace ElectricTests.Model
{
	public class UnformattedDocument : Document {
		public UnformattedDocument () { }
		public UnformattedDocument (string title, string text, bool withSections) {
			Title = title;
			Text = text;
			WithSections = withSections;
		}

		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Tuščias dokumento teksto laukas")]
		public string Text { get; set; }

		[Required(ErrorMessage = "Tuščias dokumento pavadinimo laukas")]
		public string Title { get; set; }
		
		public bool WithSections { get; set; }
	}
}
