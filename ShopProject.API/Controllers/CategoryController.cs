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
    public class CategoryController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CategoryController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCategoryQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto category, [FromServices] ICreateCategoryCommand command)
        {
            _handler.HandleCommand(command, category);
            return Ok();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto category, [FromServices] IUpdateCategoryCommand command)
        {
            category.Id = id;
            _handler.HandleCommand(command, category);
            return NoContent();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
