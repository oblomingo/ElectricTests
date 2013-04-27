using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ElectricTests.Model
{
    [DataContract]
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
        [DataMember]
		public ICollection<Section> Sections { get; set; }
        [DataMember]
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}
