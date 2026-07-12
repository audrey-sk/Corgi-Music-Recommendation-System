namespace Corgi.Infrastructure.Mood;

// Bound from configuration section "Gemini". Keep the API key out of
// source control - use `dotnet user-secrets` locally and app config /
// key vault in any deployed environment.
public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    // gemini-2.5-flash-lite is the cheapest/most generous free-tier model
    // as of mid-2026 and is plenty for this kind of short reasoning task.
    // See https://ai.google.dev/gemini-api/docs/pricing for current limits.
    public string Model { get; set; } = "gemini-2.5-flash-lite";
}
