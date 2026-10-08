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
    public class InventoryController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public InventoryController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<InventoryController>
        [HttpGet]
        public IActionResult Get([FromQuery] InventorySearch search, [FromServices] IGetInventoriesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetInventoryQuery query)
        {
            return Ok(_handler.HandleGetOne(query, id));
        }

        // POST api/<InventoryController>
        [HttpPost]
        public void Post([FromBody] InventoryDto request, [FromServices] ICreateInventoryCommand command)
        {
            _handler.HandleCommand(command, request);
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteInventoryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
