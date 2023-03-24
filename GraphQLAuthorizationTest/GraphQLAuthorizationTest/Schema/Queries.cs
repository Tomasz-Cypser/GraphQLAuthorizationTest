using HotChocolate.Authorization;

namespace GraphQLAuthorizationTest;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class Queries
{
    public Queries()
    {
    }

    [Authorize(ApplyPolicy.BeforeResolver)]
    public Task<ObjectsPagedResult> GetObjects(
        int? pageSize,
        string? paginationToken,
        [Service] IHttpContextAccessor httpContextAccessor,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ObjectsPagedResult(new List<Object> { new Object("id", DateTime.UtcNow) }, 1, "test"));
    }
}
