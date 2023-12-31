using System.Text.Json;
using System.Text.Json.Nodes;
using Cocona;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();
builder.Configuration.AddSystemsManager("/teenbhai/prod/shared/");
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();
app.AddCommand("weather", async (string city,IWeatherService weatherService) =>
{
    var weather = await weatherService.GetWeatherForCity(city);
    Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions { WriteIndented = true }));
});

app.Run();