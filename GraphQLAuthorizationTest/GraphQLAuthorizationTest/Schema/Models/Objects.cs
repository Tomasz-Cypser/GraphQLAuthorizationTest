namespace GraphQLAuthorizationTest;

public sealed class Object
{
    public Object(string id, DateTime created)
    {
        Id = id;
        Created = created;
    }

    [GraphQLType(typeof(NonNullType<IdType>))]
    public string Id { get; }

    public DateTime Created { get; }
}