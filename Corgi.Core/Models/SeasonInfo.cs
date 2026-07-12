namespace Corgi.Core.Models;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

public record SeasonInfo(Season Season, DateOnly AsOf);
