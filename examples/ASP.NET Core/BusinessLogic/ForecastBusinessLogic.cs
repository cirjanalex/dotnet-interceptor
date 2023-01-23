using ASP.NET_Core.DAL;

namespace ASP.NET_Core.BusinessLogic
{
    public interface IForecastBusinessLogic
    {
        List<WeatherForecast> GetForecast();
        Task<List<WeatherForecast>> GetForecastAsync();
    }

    public class ForecastBusinessLogic : IForecastBusinessLogic
    {
        private readonly IForecastRepository _forecastRepository;

        public ForecastBusinessLogic(IForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        public List<WeatherForecast> GetForecast()
        {
            return _forecastRepository.GetForecast();
        }

        public Task<List<WeatherForecast>> GetForecastAsync()
        {
            return _forecastRepository.GetForecastAsync();
        }
    }
}
