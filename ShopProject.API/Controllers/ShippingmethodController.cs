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
    public class ShippingmethodController : ControllerBase
    {

        private readonly UseCaseHandler _handler;

        public ShippingmethodController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ShippingmethodController>
        [HttpGet]
        public IActionResult Get([FromQuery] ShippingMethodSearch search, [FromServices] IGetShippingMethodsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ShippingmethodController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShippingmethodController>
        [HttpPost]
        public IActionResult Post([FromBody] ShippingMethodDto request, [FromServices] ICreateShippingMethods command)
        {
            _handler.HandleCommand(command,request);
            return Ok();
        }

        // PUT api/<ShippingmethodController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShippingmethodController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
