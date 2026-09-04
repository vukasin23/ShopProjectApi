using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductspecificationController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ProductspecificationController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductspecificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductspecificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductspecificationController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductSpecificationDto request, [FromServices] ICreateProductSpecificationCommand command)
        {
            _handler.HandleCommand(command, request);

            return Ok();
        }

        // PUT api/<ProductspecificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductspecificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
