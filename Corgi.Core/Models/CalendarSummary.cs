namespace Corgi.Core.Models;

public enum BusyLevel
{
    Free,
    Light,
    Busy,
    Packed
}

// Intentionally coarse. We summarize the calendar into a busy level and a
// count rather than passing raw event titles/descriptions downstream -
// keeps the amount of personal data that reaches the mood model minimal.
public record CalendarSummary(
    BusyLevel BusyLevel,
    int EventCount,
    string? NextEventLabel);
