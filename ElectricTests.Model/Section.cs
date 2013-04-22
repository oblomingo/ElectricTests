using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Tuščias skyriaus pavadinimo laukas")]
        [StringLength(500, ErrorMessage = "Skyriaus pavadinimas neturi buti ilgesnis 500 simbolių")]
		public string Title { get; set; }

		// Navigation properties
		public ICollection<Paragraph> Paragraphs { get; set; }
	}
}