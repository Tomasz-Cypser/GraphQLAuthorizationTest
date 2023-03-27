using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Strategies;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Amazon.XRay.Recorder.Handlers.System.Net;
using GraphQLAuthorizationTest.Configuration;
using HotChocolate.Execution.Batching;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace GraphQLAuthorizationTest;

internal sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private readonly IConfiguration _configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.BuildSchema();
        services.Configure<ProjectSettings>(_configuration);
        services.AddLogging(builder => builder.AddSerilog(LoggingHelper.GetLogger(_configuration), true));
        var settings = _configuration.Get<ProjectSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = settings.IdentityService.BaseUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true
                };
                options.BackchannelHttpHandler = AuthorizationMessageHandlerFactory.Create("test", settings.IdentityService.ApiKey!);
            });

        services.AddHttpContextAccessor();
        services.AddAuthorization();
        services.AddCors(options =>
        {
            options.AddPolicy
            (
                "CorsPolicy",
                builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
            );
        });


        services.AddTransient<HttpClientXRayTracingHandler>();
        AWSXRayRecorder.Instance.ContextMissingStrategy = ContextMissingStrategy.LOG_ERROR;
        AWSSDKHandler.RegisterXRayForAllServices();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
    }
}

public static class StartupExtensions
{
    public static IRequestExecutorBuilder BuildSchema(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddGraphQLServer()
            .AddQueryType()
            .AddTypeExtension<Queries>()
            .AddType<IPagedResult>()
            .AddDirectiveType<ExportDirectiveType>()
            .AddAuthorization();
    }
}
