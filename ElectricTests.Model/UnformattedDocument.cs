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
        [StringLength(1000000, ErrorMessage = "Tekstas neturi buti ilgesnis 1 000 000 simbolių")]
		public string Text { get; set; }
	}
}
