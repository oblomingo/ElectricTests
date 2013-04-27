using System.Runtime.Serialization;
using System.Web.Mvc;

namespace ElectricTests.Model {
    [DataContract]
	public abstract class Document {
        [DataMember]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

        [DataMember]
		public string Title { get; set; }

        [DataMember]
		public bool WithSections { get; set; }
	}
}