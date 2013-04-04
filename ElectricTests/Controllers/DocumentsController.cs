using System.Collections.Generic;
using System.Web.Mvc;
using ElectricTests.Model;
using ElectricTests.Repository;
using Microsoft.Practices.Unity;

namespace ElectricTests.Controllers
{
	public class DocumentsController : Controller
	{
		//
		// GET: /Documents/

        private IDocumentsRepository _repository;

        public DocumentsController(IDocumentsRepository repository, UnityContainer container)
        {
            container.Resolve<IDocumentsRepository>();
            _repository = repository;
        }

		public ActionResult Index()
		{
			//DocumentsRepository reposytory = new DocumentsRepository();
            List<FormattedDocument> documents = _repository.GetAllFormattedDocuments(); 
			return View(documents);
		}

		/// <summary>
		/// Show add document form
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize]
		public ActionResult Add() {
			return View();
		}

		/// <summary>
		/// Format text to formatted document, save it to the FormattedDocuments table and view saved document 
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public ActionResult Add (UnformattedDocument document) {
			if (ModelState.IsValid) 
			{
				//Format text 
				FormattedDocument formattedDocument = new FormattedDocument(
					document.Title, 
					document.WithSections,
					(new STProcessor()).GetParagraphsFromText(document.Text)
					);

				FormattedDocument savedDocument;

				using (var pContext = new ProjectContext())
				{
					//Save to db and get id
					pContext.FormattedDocuments.Add(formattedDocument);
					pContext.SaveChanges();
					int id = formattedDocument.Id;

					//View saved document
					DocumentsRepository dRepository = new DocumentsRepository();
					savedDocument = dRepository.GetDocumentByID(id);
				}
				return View("Details", savedDocument);
			}
			return View();
		}

		/// <summary>
		/// View formatted document with all sorted paragraphs
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ActionResult Details(int Id) {
			var reposytory = new DocumentsRepository();
			FormattedDocument document = reposytory.GetDocumentByID(Id);
			return View(document);
		}
	}
}
