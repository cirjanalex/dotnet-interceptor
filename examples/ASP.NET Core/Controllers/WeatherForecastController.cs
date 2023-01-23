using ASP.NET_Core.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IForecastBusinessLogic _forecastBusinessLogic;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastBusinessLogic forecastBusinessLogic)
    {
        _logger = logger;
        _forecastBusinessLogic = forecastBusinessLogic;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _forecastBusinessLogic.GetForecast();
    }

    [HttpGet("async")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        return await _forecastBusinessLogic.GetForecastAsync();
    }
}
