# CLI App sample using dotnet

This app uses `cocona` nuget package to setup cli apps. The app gets the weather information for a city passed as a named option

## How to run

```bash

dotnet run weather --city Delhi

```

The app gets the weather information from `open-weather` api. The API keys are stored as parameters in AWS `Systems Manager`

`Systems Manager` is integrated using `Amazon.Extensions.Configuration.SystemsManager` manager

The sample shows how to use dependency injection.