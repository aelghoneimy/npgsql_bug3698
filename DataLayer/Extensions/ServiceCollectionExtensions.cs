using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;
using System.Text.Json;

namespace DataLayer.Extensions;

public static class ServiceCollectionExtensions
{
    // static datasource
    private static NpgsqlDataSource? _dataSource;

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();

            var dataSource = GetDataSource(connectionString);
            options
                .UseNpgsql(dataSource, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", AppDbContext.Schema);
                    npgsqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.FullName);
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                });
        });
    }

    private static NpgsqlDataSource GetDataSource(string connectionString)
    {
        if (_dataSource != null) return _dataSource;

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson();
        _dataSource = dataSourceBuilder.Build();

        return _dataSource;
    }
}