using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using next_mole_server.Models;

namespace next_mole_server.Controllers
{
    public class NodesController : ApiController
    {
        // GET: api/Nodes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Nodes/5
        public string Get(int id)
        {
            return "value";
        }
 
        // POST: api/Nodes
        public void Post([FromBody]List<Node> nodes)
        {
            Node.postNodes(nodes);
        }

        // PUT: api/Nodes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Nodes/5
        public void Delete(int id)
        {
        }
    }
}
