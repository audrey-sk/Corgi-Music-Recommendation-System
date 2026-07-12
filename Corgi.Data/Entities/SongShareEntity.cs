namespace Corgi.Data.Entities;

public class SongShareEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FromUserId { get; set; } = string.Empty;
    public string ToUserId { get; set; } = string.Empty;
    public Guid SongId { get; set; }
    public string? Message { get; set; }
    public DateTimeOffset SentAtUtc { get; set; } = DateTimeOffset.UtcNow;
}
