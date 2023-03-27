namespace GraphQLAuthorizationTest;

[InterfaceType("IPagedResult")]
public interface IPagedResult
{
    int PageSize { get; }

    string? PaginationToken { get; }
}