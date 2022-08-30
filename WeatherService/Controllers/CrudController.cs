using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Models;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("add")]
        public IActionResult Create(string value)
        {
            _holder.Add(value);
            return Ok();
        }

        [HttpGet("read")]
        public ActionResult<string> Read()
        {
            return Ok(_holder.Get());
        }

    }
}
