using BC.Problems.Helpers;
using BC.Problems.Helpers.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration));

    var configuration = builder.Configuration;
    var services = builder.Services;

    services.ConfigureSqlContext(configuration);
    services.AddAutoMapper(typeof(Program));
    services.RegisterRepositories();
    services.RegisterServices();
    services.AddControllers().AddNewtonsoftJson();
    services.ConfigureCorsPolicy();
    services.ConfigureSwagger();
    services.AddBCMessaging(configuration, builder.Environment.IsDevelopment());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ExceptionHandler>();

    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
