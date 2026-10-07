using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Searches;
using ShopProject.Implementation;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public OrderController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetOrdersQuery query, [FromQuery] OrderSearch search)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto request, [FromServices] ICreateOrderCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
