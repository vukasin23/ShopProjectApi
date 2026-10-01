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
    public class CouponController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CouponController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CouponController>
        [HttpGet]
        public IActionResult Get([FromQuery] CouponSearch search, [FromServices] IGetCouponsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<CouponController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CouponController>
        [HttpPost]
        public IActionResult Post([FromBody] CouponDto request, [FromServices] ICreateCouponCommand command)
        {
            _handler.HandleCommand(command, request);
            return Ok();
        }

        // PUT api/<CouponController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCouponCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
