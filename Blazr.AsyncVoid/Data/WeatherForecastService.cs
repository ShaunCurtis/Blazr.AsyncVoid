namespace Blazr.AsyncVoid.Data;

public class WeatherForecastService
{
    public WeatherForecastService()
    {
    }


    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }

    public async Task<IEnumerable<WeatherForecast>> GetException(string? throwMe = null)
    {
        ArgumentNullException.ThrowIfNull(throwMe);
        await Task.Yield();
        return Enumerable.Empty<WeatherForecast>();
    }
}
