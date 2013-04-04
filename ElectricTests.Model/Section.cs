using System.Collections.Generic;

namespace ElectricTests.Model
{
	public class Section {

		public Section() {
			Paragraphs = new HashSet<Paragraph>();
		}
		public int Id { get; set; }
		public int Document_Id { get; set; }
		public string Title { get; set; }

		// Navigation properties
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}