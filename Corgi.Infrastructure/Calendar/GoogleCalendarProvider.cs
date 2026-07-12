using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Calendar;

// TODO: wire up real Google Calendar access. That needs:
//   1. An OAuth 2.0 client (Google Cloud Console) and the user's consent.
//   2. The Google.Apis.Calendar.v3 NuGet package.
//   3. Per-user token storage (see Corgi.Data) so we don't ask for
//      consent on every visit.
//
// Deliberately kept coarse: only a busy level + count + next event label
// cross this boundary. Nothing else from the calendar (attendees,
// descriptions, locations) should ever reach the mood model.
public class GoogleCalendarProvider : ICalendarProvider
{
    public Task<CalendarSummary> GetTodaySummaryAsync(string userId, CancellationToken ct = default)
    {
        // Placeholder so the rest of the app has something to render
        // against before OAuth is wired up.
        var placeholder = new CalendarSummary(
            BusyLevel: BusyLevel.Light,
            EventCount: 0,
            NextEventLabel: null);

        return Task.FromResult(placeholder);
    }
}
