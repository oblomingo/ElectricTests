using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers.Api
{
    public class DocumentsController : ApiController
    {
        private readonly IDocumentsRepository _repository;

        public DocumentsController(IDocumentsRepository repository) {
            _repository = repository;
        }
        private ProjectContext db = new ProjectContext();

        //[Authorize(Roles = "Administrator")]
        public IEnumerable<FormattedDocument> GetFormattedDocuments()
        {
            return _repository.GetAllFormattedDocuments();
        }

        // GET api/Documents/5
        public FormattedDocument GetFormattedDocument(int id)
        {
            FormattedDocument formatteddocument = db.FormattedDocuments.Find(id);
            if (formatteddocument == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return formatteddocument;
        }

        // PUT api/Documents/5
        public HttpResponseMessage PutFormattedDocument(int id, FormattedDocument formatteddocument)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != formatteddocument.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(formatteddocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Documents
        public HttpResponseMessage PostFormattedDocument(FormattedDocument formatteddocument)
        {
            if (ModelState.IsValid)
            {
                db.FormattedDocuments.Add(formatteddocument);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, formatteddocument);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = formatteddocument.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Documents/5
        public HttpResponseMessage DeleteFormattedDocument(int id)
        {
            FormattedDocument formatteddocument = db.FormattedDocuments.Find(id);
            if (formatteddocument == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.FormattedDocuments.Remove(formatteddocument);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, formatteddocument);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}