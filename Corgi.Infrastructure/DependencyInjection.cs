using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Corgi.Core.Abstractions;
using Corgi.Infrastructure.Calendar;
using Corgi.Infrastructure.Holidays;
using Corgi.Infrastructure.Mood;
using Corgi.Infrastructure.Season;
using Corgi.Infrastructure.Songs;
using Corgi.Infrastructure.Weather;

namespace Corgi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCorgiInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IWeatherProvider, OpenMeteoWeatherProvider>(client =>
        {
            client.BaseAddress = new Uri("https://api.open-meteo.com/");
        });

        services.AddHttpClient<IHolidayProvider, NagerDateHolidayProvider>(client =>
        {
            client.BaseAddress = new Uri("https://date.nager.at/");
        });

        services.AddHttpClient<IMoodInferenceService, GeminiMoodInferenceService>(client =>
        {
            client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
        });

        services.Configure<GeminiOptions>(configuration.GetSection("Gemini"));

        services.AddSingleton<ISeasonProvider, DateBasedSeasonProvider>();
        services.AddScoped<ICalendarProvider, GoogleCalendarProvider>();
        services.AddSingleton<ISongRecommendationService, CuratedSongRecommendationService>();

        return services;
    }
}
