using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ElectricTests.Model;
using ElectricTests.Repository;

namespace ElectricTests.Controllers.Api
{
    public class ParagraphsController : ApiController
    {
        private ProjectContext db = new ProjectContext();

        // GET api/Paragraphs
        public IEnumerable<Paragraph> GetParagraphs()
        {
            var paragraphs = db.Paragraphs.Include(p => p.Document);
            return paragraphs.AsEnumerable();
        }

        // GET api/Paragraphs/5
        public Paragraph GetParagraph(int id)
        {
            Paragraph paragraph = db.Paragraphs.Find(id);
            if (paragraph == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return paragraph;
        }

        // PUT api/Paragraphs/5
        public HttpResponseMessage PutParagraph(int id, Paragraph paragraph)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != paragraph.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(paragraph).State = EntityState.Modified;

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

        // POST api/Paragraphs
        public HttpResponseMessage PostParagraph(Paragraph paragraph)
        {
            if (ModelState.IsValid)
            {
                db.Paragraphs.Add(paragraph);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, paragraph);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = paragraph.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Paragraphs/5
        public HttpResponseMessage DeleteParagraph(int id)
        {
            Paragraph paragraph = db.Paragraphs.Find(id);
            if (paragraph == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Paragraphs.Remove(paragraph);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, paragraph);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}