using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTests.Model
{
	public class Test {
		public Test() {
			Questions = new HashSet<Question>();
		}
		public int Id { get; set; }
		[Required(ErrorMessage = "Tuščias testo pavadinimo laukas")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Tuščias klausymų skaičiaus laukas")]
		public int OneTestQuestionsNumber { get; set; }
		public bool IsAvaible { get; set; }

		[NotMapped]
		public int AllQuestionsNumber { get; set; }

		// Navigation properties
		public ICollection<Question> Questions { get; set; }
	}
}