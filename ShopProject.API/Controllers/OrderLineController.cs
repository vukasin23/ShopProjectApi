using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Implementation;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public OrderLineController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<OrderLineController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderLineDto request, [FromServices] ICreateOrderLineCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }
    }
}
