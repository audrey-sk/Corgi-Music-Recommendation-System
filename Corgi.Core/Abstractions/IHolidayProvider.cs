using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface IHolidayProvider
{
    Task<HolidayInfo?> GetTodayHolidayAsync(string countryCode, CancellationToken ct = default);
}
