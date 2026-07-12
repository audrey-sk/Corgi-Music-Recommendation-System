using Corgi.Core.Abstractions;
using Corgi.Core.Models;

namespace Corgi.Infrastructure.Songs;

// Deliberately NOT calling a recommendation algorithm from a streaming
// API - Spotify retired their recommendations/audio-features endpoints
// for new apps in Nov 2024, and a small hand-picked list gives more
// consistent, explainable results anyway. Swap this out for a database
// or a bigger seed list as the catalog grows; the interface won't change.
public class CuratedSongRecommendationService : ISongRecommendationService
{
    private static readonly List<Song> Catalog =
    [
        new(Guid.NewGuid(), "Here Comes the Sun", "The Beatles", ["upbeat", "warm", "morning"], null),
        new(Guid.NewGuid(), "Rainy Days and Mondays", "The Carpenters", ["rain", "mellow", "reflective"], null),
        new(Guid.NewGuid(), "Party in the U.S.A.", "Miley Cyrus", ["holiday", "energetic", "celebratory"], null),
        new(Guid.NewGuid(), "Holocene", "Bon Iver", ["calm", "reflective", "winter"], null),
        new(Guid.NewGuid(), "Good as Hell", "Lizzo", ["energetic", "confident", "upbeat"], null),
        new(Guid.NewGuid(), "Banana Pancakes", "Jack Johnson", ["lazy", "morning", "calm"], null),
        new(Guid.NewGuid(), "Feeling Good", "Nina Simone", ["hopeful", "warm", "energetic"], null),
        new(Guid.NewGuid(), "Snowman", "Sia", ["winter", "reflective", "cozy"], null),
    ];

    public Task<Song> RecommendAsync(MoodResult mood, CancellationToken ct = default)
    {
        var best = Catalog
            .Select(song => new
            {
                Song = song,
                Score = song.MoodTags.Count(tag =>
                    mood.MoodTags.Contains(tag, StringComparer.OrdinalIgnoreCase))
            })
            .OrderByDescending(x => x.Score)
            .First();

        return Task.FromResult(best.Song);
    }
}
