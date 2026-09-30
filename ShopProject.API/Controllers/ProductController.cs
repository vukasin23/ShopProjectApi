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
    public class ProductController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ProductController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearch search, [FromServices] IGetAllProductsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto request, [FromServices] ICreateProductCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
