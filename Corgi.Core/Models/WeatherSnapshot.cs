namespace Corgi.Core.Models;

// A simplified, mood-relevant view of the weather. We deliberately don't
// carry the full provider payload here so the rest of the app never has
// to know which weather API produced it.
public record WeatherSnapshot(
    double TemperatureCelsius,
    string Condition,        // e.g. "clear", "rain", "snow", "cloudy"
    bool IsDaytime,
    double WindSpeedKph);
