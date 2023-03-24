#pragma warning disable CS8618
namespace GraphQLAuthorizationTest.Configuration
{
    internal class ProjectSettings
    {
        public IdentityServiceSettings IdentityService { get; init; }
    }

    public sealed class IdentityServiceSettings
    {
        public string ApiKey { get; init; }
        public string BaseUrl { get; init; }
    }
}