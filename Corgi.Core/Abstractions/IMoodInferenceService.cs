using Corgi.Core.Models;

namespace Corgi.Core.Abstractions;

public interface IMoodInferenceService
{
    Task<MoodResult> InferMoodAsync(MoodContext context, CancellationToken ct = default);
}
