using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Models;

namespace WeatherService.Controllers
{
    /*

 Написать свой контроллер и методы в нем, которые бы предоставляли следующую функциональность:
    • Возможность сохранить температуру в указанное время
    • Возможность отредактировать показатель температуры в указанное время
    • Возможность удалить показатель температуры в указанный промежуток времени
    • Возможность прочитать список показателей температуры за указанный промежуток времени

 */

    [Route("api/weather")]
    [ApiController]
    public class HomeworkWeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastHolder _holder;

        public HomeworkWeatherForecastController(WeatherForecastHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("add")]
        public IActionResult Add([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            _holder.Add(date, temperatureC);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            return Ok(_holder.Update(date, temperatureC));
        }

        [HttpGet("get")]
        public IEnumerable<WeatherForecast> Get([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            return _holder.Get(dateFrom, dateTo);
        }

        [HttpDelete]

        public IActionResult Delete([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            _holder.Delete(dateFrom, dateTo);
            return Ok();
        }

    }
}
