namespace GraphQLAuthorizationTest;

[InterfaceType("IPagedResult")]
internal interface IPagedResult
{
    int PageSize { get; }

    string? PaginationToken { get; }
}