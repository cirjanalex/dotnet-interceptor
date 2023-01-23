namespace ASP.NET_Core.DAL
{
    public interface IForecastRepository
    {
        List<WeatherForecast> GetForecast();
        Task<List<WeatherForecast>> GetForecastAsync();
    }
    public class ForecastRepository : IForecastRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public List<WeatherForecast> GetForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }

        public async Task<List<WeatherForecast>> GetForecastAsync()
        {
            await Task.Delay(100);
            return GetForecast();
        }
    }
}
