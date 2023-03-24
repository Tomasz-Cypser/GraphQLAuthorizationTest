using Amazon.Lambda.AspNetCoreServer;

namespace GraphQLAuthorizationTest;

internal class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        RegisterResponseContentEncodingForContentType("font/woff2", ResponseContentEncoding.Base64);
        RegisterResponseContentEncodingForContentType("font/ttf", ResponseContentEncoding.Base64);
        RegisterResponseContentEncodingForContentType("application/font-woff2", ResponseContentEncoding.Base64);
        RegisterResponseContentEncodingForContentType("application/font-woff", ResponseContentEncoding.Base64);
        RegisterResponseContentEncodingForContentType("application/x-font-woff2", ResponseContentEncoding.Base64);
        RegisterResponseContentEncodingForContentType("application/x-font-ttf", ResponseContentEncoding.Base64);
        builder
            .ConfigureAppConfiguration((_, config) =>
            {
                config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .UseStartup<Startup>();
    }
}