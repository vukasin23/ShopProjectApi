using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ShopProject.API.Core;
using ShopProject.API.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenCreator _jwtTokenCreator;

        public AuthController(JwtTokenCreator jwtTokenCreator)
        {
            _jwtTokenCreator = jwtTokenCreator;
        }

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest request )
        {
            string _token = _jwtTokenCreator.Create(request.Email,request.Password);

            return Ok(new AuthResponse { Token = _token });
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
