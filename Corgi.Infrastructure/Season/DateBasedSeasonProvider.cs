using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Season;

// No API needed - meteorological seasons from the calendar date, mirrored
// for the southern hemisphere based on latitude sign.
public class DateBasedSeasonProvider : ISeasonProvider
{
    public SeasonInfo GetCurrentSeason(double latitude, DateOnly? asOf = null)
    {
        var date = asOf ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var northernSeason = date.Month switch
        {
            12 or 1 or 2 => Core.Models.Season.Winter,
            3 or 4 or 5 => Core.Models.Season.Spring,
            6 or 7 or 8 => Core.Models.Season.Summer,
            _ => Core.Models.Season.Autumn
        };

        var season = latitude >= 0 ? northernSeason : Flip(northernSeason);
        return new SeasonInfo(season, date);
    }

    private static Core.Models.Season Flip(Core.Models.Season season) => season switch
    {
        Core.Models.Season.Winter => Core.Models.Season.Summer,
        Core.Models.Season.Summer => Core.Models.Season.Winter,
        Core.Models.Season.Spring => Core.Models.Season.Autumn,
        _ => Core.Models.Season.Spring
    };
}
