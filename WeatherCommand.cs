using System.Text.Json;
using Cocona;

namespace CLIAppsSample;
public class WeatherCommand
{
    private readonly IWeatherService _weatherService;

    public WeatherCommand(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    [Command("weather")]
    public async Task Weather(string city)
    {
        var w = await _weatherService.GetWeatherForCity(city);
        //Console.WriteLine(JsonSerializer.Serialize(w, new JsonSerializerOptions { WriteIndented = true }));

    }



}