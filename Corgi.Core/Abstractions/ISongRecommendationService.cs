using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface ISongRecommendationService
{
    Task<Song> RecommendAsync(MoodResult mood, CancellationToken ct = default);
}
