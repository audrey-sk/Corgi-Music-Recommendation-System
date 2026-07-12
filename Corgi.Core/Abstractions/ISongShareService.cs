using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface ISongShareService
{
    Task ShareAsync(string fromUserId, string toUserId, Guid songId, string? message, CancellationToken ct = default);

    Task<IReadOnlyList<SongShare>> GetInboxAsync(string userId, CancellationToken ct = default);
}
