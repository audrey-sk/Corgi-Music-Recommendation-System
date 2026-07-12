namespace Corgi.Core.Models;

public record Song(
    Guid Id,
    string Title,
    string Artist,
    IReadOnlyList<string> MoodTags,
    string? SpotifyUrl);
