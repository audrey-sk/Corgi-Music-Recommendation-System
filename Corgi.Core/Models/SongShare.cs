namespace Corgi.Core.Models;

public record SongShare(
    Guid Id,
    string FromUserId,
    string ToUserId,
    Guid SongId,
    string? Message,
    DateTimeOffset SentAtUtc);
