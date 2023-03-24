using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;

namespace GraphQLAuthorizationTest.Configuration
{
    public static class LoggingHelper
    {
        public static Serilog.ILogger GetLogger(IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.Console(new ElasticsearchJsonFormatter())
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http", LogEventLevel.Warning)
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext();

            Log.Logger = loggerConfiguration.CreateLogger();
            return Log.Logger;
        }
    }
}
