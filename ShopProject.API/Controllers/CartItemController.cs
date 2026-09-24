using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Implementation;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CartItemController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<CartItemController>
        [HttpPost]
        public IActionResult Post([FromBody] CartItemDto request, [FromServices] ICreateCartItemCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }
    }
}
