using System.Collections.Generic;

namespace ElectricTests.Model
{
	public class FormattedDocument : Document {
		public FormattedDocument() {}

		public FormattedDocument (string title, bool withSections) {
			Title = title;
			WithSections = withSections;
			Sections = new HashSet<Section>();
			Paragraphs = new HashSet<Paragraph>();
		}

		public FormattedDocument (string title, bool withSections, HashSet<Paragraph> paragraphs) {
			Title = title;
			WithSections = withSections;
			Paragraphs = paragraphs;
			Sections = new HashSet<Section>();
		}

		// Navigation properties
		public ICollection<Section> Sections { get; set; }
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}
