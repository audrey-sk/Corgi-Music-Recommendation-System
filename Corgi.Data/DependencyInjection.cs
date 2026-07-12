using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Corgi.Core.Abstractions;

namespace Corgi.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddCorgiData(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Corgi")
            ?? "Data Source=corgi.db";

        services.AddDbContext<CorgiDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<ISongShareService, SongShareService>();

        return services;
    }
}
