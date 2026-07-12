namespace Corgi.Core.Models;

// Output of mood inference. Deliberately a small, structured shape rather
// than free text, so downstream matching logic stays simple and testable.
public record MoodResult(
    IReadOnlyList<string> MoodTags,
    double Energy,   // 0 (calm) .. 1 (energetic)
    double Valence,  // 0 (somber) .. 1 (upbeat)
    string Rationale);
