using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace Sample.Seq.WebApi.Core.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder, IConfiguration configuration, string applicationName)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .WriteTo.Seq("https://xxxxxxxxx.azurewebsites.net/", apiKey: "xxxxxxxxxxxxx", controlLevelSwitch: new LoggingLevelSwitch())
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.Debug()
            .CreateLogger();

        builder.UseSerilog(Log.Logger);

        return builder;
    }
}