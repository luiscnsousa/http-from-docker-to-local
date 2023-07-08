using System.Collections.Generic;
using System.Web.Http;

namespace ApiV2.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete]
        [Route("{id}")]
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
