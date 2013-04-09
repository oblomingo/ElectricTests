using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers {
    public class DocumentsController : Controller {

        private readonly IDocumentsRepository _repository;

        public DocumentsController(IDocumentsRepository repository) {
            //Get documents repository object (Unity) 
            _repository = repository;
        }

        /// <summary>
        /// Show all formatted documents
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            return View(_repository.GetAllFormattedDocuments());
        }

        /// <summary>
        /// Show add document form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Users = "Aleksandr")]
        public ActionResult Add() {
            return View();
        }

        /// <summary>
        /// Format text to formatted document, save it to the FormattedDocuments table and view saved document 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Users = "Aleksandr")]
        public ActionResult Add(UnformattedDocument document) {
            if (ModelState.IsValid) {
                //Format text to formatted document object
                var formattedDocument = new FormattedDocument(
                    document.Title,
                    document.WithSections,
                    (new STProcessor()).GetParagraphsFromText(document.Text));

                FormattedDocument savedDocument;

                using (var pContext = new ProjectContext()) {
                    //Save to db and get id
                    pContext.FormattedDocuments.Add(formattedDocument);
                    pContext.SaveChanges();
                    
                    //Get new document to show it in Details controller
                    savedDocument = _repository.GetDocumentById(formattedDocument.Id);
                }
                return View("Details", savedDocument);
            }
            return View();
        }

        /// <summary>
        /// View formatted document with all sorted paragraphs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id) {
            Document document = _repository.GetDocumentById(id);
            if (document == null) {
                return RedirectToAction("Index");
            }
            return View(document);
        }
    }
}