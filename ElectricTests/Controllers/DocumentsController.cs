using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;
using System.Data.Entity.Validation;
using ElectricTests.Helpers;

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
        [Authorize(Roles = "Administrator")]
        public ActionResult Add() {
            return View();
        }

        /// <summary>
        /// Format text to formatted document, save it to the FormattedDocuments table and view saved document 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(UnformattedDocument document) {

            if (ModelState.IsValid) {

            //Format text to formatted document object
            var formattedDocument = new FormattedDocument(
                document.Title,
                document.WithSections,
                (new STProcessor()).GetParagraphsFromText(document.Text));

                using (var pContext = new ProjectContext()) {
                    
                    pContext.FormattedDocuments.Add(formattedDocument);
                    try {
                        //Save to db
                        pContext.SaveChanges();
                    } catch (DbEntityValidationException e) {

                        //All formattedDocument inner objects entity validation 
                        ModelState.AddModelError("", InnerValidationHelper.GetEntityInnerValidationErrors(e));
                        return View();
                    }
                }
                //Get new document to show it in Details controller
                return RedirectToAction("Details", new {id = formattedDocument.Id });
            }
            return View();
        }

        /// <summary>
        /// View formatted document with all sorted paragraphs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id) {
            FormattedDocument document = _repository.GetDocumentById(id);
            if (document == null) {
                return RedirectToAction("Index");
            }
            return View(document);
        }
    }
}