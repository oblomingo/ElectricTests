using System.Collections.Generic;
using System.Web.Mvc;

namespace ElectricTests.Model
{
	public class Section {

		public Section() {
			Paragraphs = new HashSet<Paragraph>();
		}
        [HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		public int Document_Id { get; set; }
		public string Title { get; set; }

		// Navigation properties
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}