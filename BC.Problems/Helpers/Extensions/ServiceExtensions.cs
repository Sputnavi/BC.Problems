using BC.Problems.Repositories;
using BC.Problems.Repositories.Interfaces;
using BC.Problems.Services;
using BC.Problems.Services.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BC.Problems.Helpers.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void AddBCMessaging(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserUpdatedConsumer>()
                .Endpoint(e => e.Name = "UserUpdated.Problems");
            x.AddConsumer<UserDeletedConsumer>()
                .Endpoint(e => e.Name = "UserDeleted.Problems");

            if (isDevelopment)
            {
                var rabbitMqSection = configuration.GetSection("RabbitMQ");
                string host = rabbitMqSection["host"];
                string virtualHost = rabbitMqSection["virtualHost"];
                string username = rabbitMqSection["username"];
                string password = rabbitMqSection["password"];

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(host, virtualHost, h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    config.ConfigureEndpoints(context);
                });

                return;
            }

            string azureServiceBusConnection = configuration.GetConnectionString("AzureServiceBusConnection");
            x.UsingAzureServiceBus((context, config) =>
            {
                config.Host(azureServiceBusConnection);

                config.ConfigureEndpoints(context);
            });
        });
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProblemRepository, ProblemRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProblemService, ProblemService>();
    }

    public static void ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "BC.Problems",
                Description = "Microservice to manage problems",
            });
        });
}
