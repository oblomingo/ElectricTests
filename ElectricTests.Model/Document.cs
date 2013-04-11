using System.Web.Mvc;

namespace ElectricTests.Model {

	public abstract class Document {
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		public string Title { get; set; }
		public bool WithSections { get; set; }
	}
}