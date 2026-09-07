using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WishlistItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WishlistItemController>
        [HttpPost]
        public void Post([FromBody] WishlistitemDto request, [FromServices] ICreateWishlistItemCommand command)
        {
            _handler.HandleCommand(command, request);   
        }

        // PUT api/<WishlistItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WishlistItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
