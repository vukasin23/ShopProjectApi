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
    public class ProductspecificationController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ProductspecificationController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductspecificationController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSpecificationSearch search, [FromServices] IGetProductSpecificationsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ProductspecificationController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProductSpecificationQuery query)
        {
            return Ok(_handler.HandleGetOne(query, id));
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
        public IActionResult Delete(int id, [FromServices] IDeleteProductSpecificationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
