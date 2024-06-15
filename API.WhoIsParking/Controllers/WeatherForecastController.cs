using App.WhoIsParking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.WhoIsParking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<WeatherForecast> GetAsync()
        {
            var b = new Class1(1);
            var a = await _mediator.Send<WeatherModel>(b, default).ConfigureAwait(false);


            var c = new WeatherForecast()
            {
                Date = a.Date,
                TemperatureC = a.TemperatureC,
                Summary = a.Summary
            };

            return c;
        }
    }
}
