using System.Collections.Generic;
using System.Linq;
using ElectricTests.Model;

namespace ElectricTests.Repository
{
    public interface IDocumentsRepository
    {
        List<FormattedDocument> GetAllFormattedDocuments();
        FormattedDocument GetDocumentById(int id);
    }

	public class DocumentsRepository : IDocumentsRepository {

		/// <summary>
		/// Get all formatted documents without paragraphs
		/// </summary>
		/// <returns></returns>
		public List<FormattedDocument> GetAllFormattedDocuments() {
			List<FormattedDocument> documents = null;
			using (var projectContext = new ProjectContext()) {
				if (projectContext.FormattedDocuments != null)
					documents = projectContext.FormattedDocuments.ToList();
			}
			return documents;
		}

		/// <summary>
		/// Get document by id with sorted by numbers paragraphs 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FormattedDocument GetDocumentById(int id) {
			using (var context = new ProjectContext()) {
				FormattedDocument document = context.FormattedDocuments
					.Include("Paragraphs")
					.SingleOrDefault(c => c.Id == id);

                //Set navigation properties to null to avoid javascript error 
                //"A circular reference was detected while serializing an object of type"
                if (document != null)
                    document.Sections = null;

                if (document != null)
                {
                    document.Paragraphs = GetParagraphsByDocumentId(id);
                }
				return document;
				
			}
		}
		/// <summary>
		/// Get sorted paragraphs by numbers without navigation properties 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ICollection<Paragraph> GetParagraphsByDocumentId(int id) {
			using (var context = new ProjectContext()) {
				var paragraphs = context.Paragraphs
					//Deepest hierarhic level - 5 (it's depends on regular expression template)
					.Include("Paragraphs.Paragraphs.Paragraphs.Paragraphs.Paragraphs")
					.Where(c => c.FormattedDocumentId == id)
					.Where(c => c.ParagraphId == null)
					.OrderBy(c => c.Number)
					.ToList();

				paragraphs = SortAndPrepareParagraphs(paragraphs);
				return paragraphs;
			}
		}

		/// <summary>
		/// Sort paragraphs by number on each hierarhic level (using recursive method) 
		/// and set navigation properties to null to avoid js error 
		/// (A circular reference was detected while serializing an object of type) 
		/// </summary>
		/// <param name="paragraphs"></param>
		/// <returns></returns>
		private List<Paragraph> SortAndPrepareParagraphs (List<Paragraph> paragraphs) {
			foreach (var paragraph in paragraphs) {
				if (paragraph.Paragraphs != null) {
					paragraph.Paragraphs = paragraph.Paragraphs.OrderBy(p => p.Number).ToList();

					paragraph.Document = null;
					paragraph.Questions = null;

					if(paragraph.Paragraphs.Count > 0) {
						foreach (var childParagraph in paragraph.Paragraphs) {
							if (childParagraph.Paragraphs != null)
								childParagraph.Paragraphs = SortAndPrepareParagraphs(childParagraph.Paragraphs.ToList());
						}
					} else {
						paragraph.Paragraphs = null;
					}
				}
			}
			return paragraphs;
		}
	}
}