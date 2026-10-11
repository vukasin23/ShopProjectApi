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
    public class ProductimageController : ControllerBase
    {

        private readonly UseCaseHandler _handler;

        public ProductimageController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductimageController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductImageSearch search, [FromServices] IGetProductImagesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ProductimageController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProductImageQuery query)
        {
            return Ok(_handler.HandleGetOne(query, id));
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
        public IActionResult Delete(int id, [FromServices] IDeleteProductImageCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
