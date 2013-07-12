using System.Runtime.Serialization;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectricTests.Model {
    [DataContract]
	public abstract class Document {
        [DataMember]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

        [Required(ErrorMessage = "Tuščias dokumento pavadinimo laukas")]
        [StringLength(500, ErrorMessage = "Pavadinimas neturi buti ilgesnis 500 simbolių")]
        public string Title { get; set; }
		
        public bool WithSections { get; set; }
	}
}