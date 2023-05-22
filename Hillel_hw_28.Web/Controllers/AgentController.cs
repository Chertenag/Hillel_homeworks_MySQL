using Microsoft.AspNetCore.Mvc;
using Hillel_hw_23.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hillel_hw_28.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get()
        {
            return Agent.ReadAll(CancellationToken.None).Result;
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get(int id)
        {
            var rez = Agent.Search_ById(id, CancellationToken.None).Result;
            if (rez.Count == 0)
            {
                return base.NotFound();
            }
            return rez;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Agent value)
        {
            Agent.AddNew(value, CancellationToken.None).Wait();
            return base.Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult Put(int id, [FromBody] Agent value)
        {
            if (Agent.Search_ById(id, CancellationToken.None).Result.Count == 0)
            {
                return base.NotFound();
            }
            value.ID = id;
            Agent.Update(value, CancellationToken.None).Wait();
            return base.Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (Agent.Search_ById(id, CancellationToken.None).Result.Count == 0)
            {
                return base.NotFound();
            }
            Agent.Delete_ById(id, CancellationToken.None).Wait();
            return base.Ok();
        }
    }
}
