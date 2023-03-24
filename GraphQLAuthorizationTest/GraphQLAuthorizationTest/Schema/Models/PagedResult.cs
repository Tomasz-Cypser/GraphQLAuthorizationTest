namespace GraphQLAuthorizationTest;

public abstract class PagedResult<T> : IPagedResult
{
    protected PagedResult(IEnumerable<T> items, int pageSize, string? paginationToken)
    {
        PageSize = pageSize;
        PaginationToken = paginationToken;
        Items = items.ToList().AsReadOnly();
    }

    public int PageSize { get; }

    public string? PaginationToken { get; }

    public IReadOnlyCollection<T> Items { get; }
}