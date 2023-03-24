namespace GraphQLAuthorizationTest;

internal static class AuthorizationMessageHandlerFactory
{
    public static HttpMessageHandler Create(string userAgent, string apiKey)
        => HttpClientFactory.CreatePipeline(new HttpClientHandler(), new DelegatingHandler[] { new AuthorizationMessageHandler(apiKey, userAgent) });
}

internal class AuthorizationMessageHandler : DelegatingHandler
{
    public AuthorizationMessageHandler(string apiKey, string userAgent)
    {
        ApiKey = apiKey;
        UserAgent = userAgent;
    }

    private string ApiKey { get; }
    private string UserAgent { get; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(UserAgent))
            request.Headers.Add("User-Agent", UserAgent);
        if (!string.IsNullOrEmpty(ApiKey))
            request.Headers.Add("api-key", ApiKey);
        return base.SendAsync(request, cancellationToken);
    }
}