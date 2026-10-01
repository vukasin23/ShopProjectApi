using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Searches;
using ShopProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        private readonly UseCaseHandler _handler;

        public StoreController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public IActionResult Get([FromQuery] StoreSearch search, [FromServices] IGetStoresQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StoreController>
        [HttpPost]
        public IActionResult Post([FromServices] ICreateStoreCommand command, [FromBody] StoreDto store)
        {
            _handler.HandleCommand(command, store);
            return Ok();
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteStoreCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
