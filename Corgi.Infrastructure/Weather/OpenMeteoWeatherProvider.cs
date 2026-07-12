using System.Globalization;
using System.Text.Json.Serialization;
using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Weather;

// Free, keyless weather data - https://open-meteo.com
// Non-commercial use is free up to ~10,000 requests/day; see their docs
// before scaling this beyond a personal project.
public class OpenMeteoWeatherProvider : IWeatherProvider
{
    private readonly HttpClient _http;

    public OpenMeteoWeatherProvider(HttpClient http)
    {
        _http = http;
    }

    public async Task<WeatherSnapshot> GetCurrentAsync(double latitude, double longitude, CancellationToken ct = default)
    {
        var lat = latitude.ToString(CultureInfo.InvariantCulture);
        var lon = longitude.ToString(CultureInfo.InvariantCulture);

        var url = $"v1/forecast?latitude={lat}&longitude={lon}" +
                  "&current=temperature_2m,weather_code,wind_speed_10m,is_day";

        var response = await _http.GetFromJsonAsync<OpenMeteoResponse>(url, ct)
            ?? throw new InvalidOperationException("Open-Meteo returned an empty response.");

        var current = response.Current;

        return new WeatherSnapshot(
            TemperatureCelsius: current.Temperature2m,
            Condition: MapWeatherCode(current.WeatherCode),
            IsDaytime: current.IsDay == 1,
            WindSpeedKph: current.WindSpeed10m);
    }

    // See https://open-meteo.com/en/docs for the full WMO weather code table.
    private static string MapWeatherCode(int code) => code switch
    {
        0 => "clear",
        1 or 2 or 3 => "cloudy",
        45 or 48 => "fog",
        51 or 53 or 55 or 56 or 57 => "drizzle",
        61 or 63 or 65 or 66 or 67 => "rain",
        71 or 73 or 75 or 77 => "snow",
        80 or 81 or 82 => "showers",
        85 or 86 => "snow showers",
        95 or 96 or 99 => "thunderstorm",
        _ => "unknown"
    };

    private sealed record OpenMeteoResponse(
        [property: JsonPropertyName("current")] OpenMeteoCurrent Current);

    private sealed record OpenMeteoCurrent(
        [property: JsonPropertyName("temperature_2m")] double Temperature2m,
        [property: JsonPropertyName("weather_code")] int WeatherCode,
        [property: JsonPropertyName("wind_speed_10m")] double WindSpeed10m,
        [property: JsonPropertyName("is_day")] int IsDay);
}
