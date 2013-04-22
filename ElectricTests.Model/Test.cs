using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ElectricTests.Model
{
	public class Test {
		public Test() {
			Questions = new HashSet<Question>();
		}
        [HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		
        [Required(ErrorMessage = "Tuščias testo pavadinimo laukas")]
        [StringLength(500, ErrorMessage = "Pavadinimas neturi buti ilgesnis 500 simbolių")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Tuščias klausymų skaičiaus laukas")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Klausymų skaičius vienam kartui - turi būti skaičius")]
		public int OneTestQuestionsNumber { get; set; }

        public bool IsAvaible { get; set; }

		[NotMapped]
		public int AllQuestionsNumber { get; set; }

		// Navigation properties
		public ICollection<Question> Questions { get; set; }
	}
}