using System.Collections.Generic;
using System.ServiceModel;
using ElectricTests.Model;

namespace ElectricTest.WCFService
{
    [ServiceContract]
    public interface IDocuments
    {
        [OperationContract]
        ICollection<FormattedDocument> GetDocuments();

        [OperationContract]
        FormattedDocument GetDocumentById(int id);
    }
}
