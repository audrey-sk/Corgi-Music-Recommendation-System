using Microsoft.EntityFrameworkCore;
using Corgi.Core.Abstractions;
using Corgi.Core.Models;
using Corgi.Data.Entities;

namespace Corgi.Data;

public class SongShareService(CorgiDbContext db) : ISongShareService
{
    public async Task ShareAsync(string fromUserId, string toUserId, Guid songId, string? message, CancellationToken ct = default)
    {
        db.SongShares.Add(new SongShareEntity
        {
            FromUserId = fromUserId,
            ToUserId = toUserId,
            SongId = songId,
            Message = message
        });

        await db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<SongShare>> GetInboxAsync(string userId, CancellationToken ct = default)
    {
        var entities = await db.SongShares
            .Where(s => s.ToUserId == userId)
            .OrderByDescending(s => s.SentAtUtc)
            .ToListAsync(ct);

        return entities
            .Select(e => new SongShare(e.Id, e.FromUserId, e.ToUserId, e.SongId, e.Message, e.SentAtUtc))
            .ToList();
    }
}
