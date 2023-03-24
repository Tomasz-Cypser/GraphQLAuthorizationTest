namespace GraphQLAuthorizationTest;

public sealed class ObjectsPagedResult : PagedResult<Object>
{
    public ObjectsPagedResult(IEnumerable<Object> items, int pageSize, string? paginationToken) 
        : base(items, pageSize, paginationToken)
    {
    }
}
