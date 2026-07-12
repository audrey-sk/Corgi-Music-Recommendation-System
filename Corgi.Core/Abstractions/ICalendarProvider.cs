using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface ICalendarProvider
{
    Task<CalendarSummary> GetTodaySummaryAsync(string userId, CancellationToken ct = default);
}
