using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectricTests.Model;
using System.Data.Entity;
using ElectricTests.Repository;

//using System.Web.Http.OData;

namespace ElectricTests.Controllers.Api
{
    public class QuestionsController : /*EntitySetController<Question, int>*/ ApiController
    {
        // GET api/questions
        public IQueryable<Question> Get()
        {
            var repository = new QuestionsRepository();
            return repository.GetAllQuestions().AsQueryable();
            //return new string[] { "value1", "value2" };
        }

        // GET api/questions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/questions
        public void Post([FromBody]string value)
        {
        }

        // PUT api/questions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/questions/5
        public void Delete(int id)
        {
        }
    }
}
