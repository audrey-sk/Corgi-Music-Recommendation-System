using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface ISeasonProvider
{
    // No network call - pure date math, hemisphere-aware.
    SeasonInfo GetCurrentSeason(double latitude, DateOnly? asOf = null);
}
