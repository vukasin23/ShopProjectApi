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
    public class AddressController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public AddressController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult Get([FromQuery] AddressSearch search, [FromServices] IGetAddressesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetAddresQuery query)
        {
            return Ok(_handler.HandleGetOne(query, id));
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult Post([FromBody] AddressDto request, [FromServices] ICreateAddressCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAddressDto address, [FromServices] IUpdateAddressCommand command)
        {
            address.Id = id;
            _handler.HandleCommand(command, address);
            return NoContent();
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAddressCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
