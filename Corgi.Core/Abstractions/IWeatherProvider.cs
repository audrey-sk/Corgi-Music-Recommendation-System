using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface IWeatherProvider
{
    Task<WeatherSnapshot> GetCurrentAsync(double latitude, double longitude, CancellationToken ct = default);
}
