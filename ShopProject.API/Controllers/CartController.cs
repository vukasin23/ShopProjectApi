using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application;
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
    public class CartController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CartController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CartController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetCartsQuery query, [FromQuery] CartSearch search)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET: api/<CartController>/profile
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile([FromServices] IGetProfileCartQuery query, [FromQuery] PagedSearch search)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] CartDto request, [FromServices] ICreateCartCommand command)
        {
            _handler.HandleCommand(command, request); 
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
