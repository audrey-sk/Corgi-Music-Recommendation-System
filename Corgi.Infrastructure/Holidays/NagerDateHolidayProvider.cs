using System.Text.Json.Serialization;
using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Holidays;

// Free, keyless public holiday data - https://date.nager.at
public class NagerDateHolidayProvider : IHolidayProvider
{
    private readonly HttpClient _http;

    public NagerDateHolidayProvider(HttpClient http)
    {
        _http = http;
    }

    public async Task<HolidayInfo?> GetTodayHolidayAsync(string countryCode, CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var url = $"api/v3/PublicHolidays/{today.Year}/{countryCode}";

        var holidays = await _http.GetFromJsonAsync<List<NagerHoliday>>(url, ct) ?? [];

        var match = holidays.FirstOrDefault(h => h.Date == today);
        return match is null
            ? null
            : new HolidayInfo(match.LocalName, match.Date, countryCode);
    }

    private sealed record NagerHoliday(
        [property: JsonPropertyName("date")] DateOnly Date,
        [property: JsonPropertyName("localName")] string LocalName);
}
