using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public UserController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{username}")]
        public IActionResult Get(string username, [FromServices]IGetUserQuery query)
        {
            return Ok(_handler.HandleGetOne(query,username));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto request, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
