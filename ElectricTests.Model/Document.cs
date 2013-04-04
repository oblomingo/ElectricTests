namespace ElectricTests.Model {

	public abstract class Document {

		public int Id { get; set; }
		public string Title { get; set; }
		public bool WithSections { get; set; }
	}
}