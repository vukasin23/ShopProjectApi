using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using ShopProject.API.Core;
using ShopProject.API.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenStorage _storage;
        private readonly JwtTokenCreator _jwtTokenCreator;

        public AuthController(JwtTokenCreator jwtTokenCreator, ITokenStorage storage)
        {
            _jwtTokenCreator = jwtTokenCreator;
            _storage = storage;
        }

        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest request )
        {
            string _token = _jwtTokenCreator.Create(request.Email,request.Password);

            return Ok(new AuthResponse { Token = _token });
        }


        [Authorize]
        [HttpDelete]
        public IActionResult Logout()
        {

            var tokenId = Request.GetTokenId();

            if (tokenId == null)
            {
                return Unauthorized();
            }

            _storage.Remove(tokenId.Value);

            return NoContent();
        }

    }
}
