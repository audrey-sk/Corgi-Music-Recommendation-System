using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Mood;

public class GeminiMoodInferenceService : IMoodInferenceService
{
    private readonly HttpClient _http;
    private readonly GeminiOptions _options;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GeminiMoodInferenceService(HttpClient http, IOptions<GeminiOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<MoodResult> InferMoodAsync(MoodContext context, CancellationToken ct = default)
    {
        var prompt = BuildPrompt(context);

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            },
            generationConfig = new
            {
                responseMimeType = "application/json"
            }
        };

        var url = $"v1beta/models/{_options.Model}:generateContent?key={_options.ApiKey}";

        using var response = await _http.PostAsJsonAsync(url, requestBody, ct);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<GeminiResponse>(cancellationToken: ct)
            ?? throw new InvalidOperationException("Gemini returned an empty response.");

        var rawJson = payload.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text
            ?? throw new InvalidOperationException("Gemini response had no candidate text.");

        var parsed = JsonSerializer.Deserialize<MoodJson>(rawJson, JsonOptions)
            ?? throw new InvalidOperationException("Could not parse Gemini's mood JSON.");

        return new MoodResult(parsed.MoodTags, parsed.Energy, parsed.Valence, parsed.Rationale);
    }

    private static string BuildPrompt(MoodContext ctx)
    {
        var sb = new StringBuilder();
        sb.AppendLine("You pick the mood for a music recommendation app. Given the context below,");
        sb.AppendLine("respond with ONLY a JSON object matching this shape, no other text:");
        sb.AppendLine("""{"moodTags": string[], "energy": number 0-1, "valence": number 0-1, "rationale": string (one sentence)}""");
        sb.AppendLine();
        sb.AppendLine($"Local time: {ctx.LocalTime:f}");
        sb.AppendLine($"Weather: {ctx.Weather.Condition}, {ctx.Weather.TemperatureCelsius:F0}C, " +
                       $"{(ctx.Weather.IsDaytime ? "daytime" : "nighttime")}");
        sb.AppendLine($"Season: {ctx.Season.Season}");

        if (ctx.Holiday is not null)
        {
            sb.AppendLine($"Holiday today: {ctx.Holiday.Name}");
        }

        if (ctx.Calendar is not null)
        {
            sb.AppendLine($"Calendar: {ctx.Calendar.BusyLevel} ({ctx.Calendar.EventCount} events today)");
        }

        return sb.ToString();
    }

    private sealed record GeminiResponse(
        [property: JsonPropertyName("candidates")] List<GeminiCandidate> Candidates);

    private sealed record GeminiCandidate(
        [property: JsonPropertyName("content")] GeminiContent Content);

    private sealed record GeminiContent(
        [property: JsonPropertyName("parts")] List<GeminiPart> Parts);

    private sealed record GeminiPart(
        [property: JsonPropertyName("text")] string Text);

    private sealed record MoodJson(
        List<string> MoodTags,
        double Energy,
        double Valence,
        string Rationale);
}
