using System.Collections.Generic;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTest.WCFService
{
    public class Documents : IDocuments
    {

        public ICollection<FormattedDocument> GetDocuments() {
            var repository = new DocumentsRepository();
            return repository.GetAllFormattedDocuments();
        }
        
        public FormattedDocument GetDocumentById(int id) {
            var repository = new DocumentsRepository();
            return repository.GetDocumentById(id);
        }
    }
}
