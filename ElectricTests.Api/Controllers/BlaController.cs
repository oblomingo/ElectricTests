using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElectricTests.Api.Controllers
{
    public class BlaController : ApiController
    {
        // GET api/bla
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/bla/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bla
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bla/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bla/5
        public void Delete(int id)
        {
        }
    }
}
