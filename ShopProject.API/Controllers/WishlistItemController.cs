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
    public class WishlistItemController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public WishlistItemController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<WishlistItemController>
        [HttpGet]
        public IActionResult Get([FromServices]IGetWishlistitemsQuery query, [FromQuery]WishlistitemSearch search)
        {
            
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<WishlistItemController>/profile
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile([FromServices] IGetProfileWishlistitemsQuery query, [FromQuery] PagedSearch search)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // POST api/<WishlistItemController>
        [HttpPost]
        public void Post([FromBody] WishlistitemDto request, [FromServices] ICreateWishlistItemCommand command)
        {
            _handler.HandleCommand(command, request);   
        }

        // DELETE api/<WishlistItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteWishlistItemCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
