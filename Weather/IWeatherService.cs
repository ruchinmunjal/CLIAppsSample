using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

public interface IWeatherService
{
    public Task<WeatherResponse?> GetWeatherForCity(string city);
}

public class WeatherService : IWeatherService
{   

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<WeatherResponse?> GetWeatherForCity(string city)
    {

        var apiKey = _configuration.GetSection("open-weather-api:api:key").Value;
        var baseUrl = _configuration.GetSection("open-weather-api:api:url").Value;
        var url = $"{baseUrl}?q={city}&appid={apiKey}&units=metric";

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<WeatherResponse>();
    }
}

public class WeatherResponse
{
    [JsonPropertyName("main")] public OpenWeatherMapWeather Weather { get; set; }

    [JsonPropertyName("visibility")] public int Visibility { get; set; }

    [JsonPropertyName("dt")] public int Dt { get; set; }

    [JsonPropertyName("timezone")] public int Timezone { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("cod")] public int Cod { get; set; }
}

public class OpenWeatherMapWeather
{
    [JsonPropertyName("temp")] public double Temp { get; set; }

    [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")] public double TempMin { get; set; }

    [JsonPropertyName("temp_max")] public double TempMax { get; set; }

    [JsonPropertyName("pressure")] public int Pressure { get; set; }

    [JsonPropertyName("humidity")] public int Humidity { get; set; }
}