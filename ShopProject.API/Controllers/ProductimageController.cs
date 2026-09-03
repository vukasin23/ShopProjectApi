using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductimageController : ControllerBase
    {

        private readonly UseCaseHandler _handler;

        public ProductimageController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductimageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductimageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductimageController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductImageDto request, [FromServices] ICreateProductImageCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<ProductimageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductimageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
