namespace Corgi.Core.Models;

// Everything we know about "right now" for a given user, bundled up for
// the mood inference step.
public record MoodContext(
    WeatherSnapshot Weather,
    SeasonInfo Season,
    HolidayInfo? Holiday,
    CalendarSummary? Calendar,
    DateTimeOffset LocalTime);
