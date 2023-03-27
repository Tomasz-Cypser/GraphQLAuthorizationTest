using HotChocolate.Execution;
using HotChocolate.Execution.Batching;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace GraphQLAuthorizationTest.UnitTests;

[TestFixture]
public class GraphQlSchemaTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly RequestExecutorProxy _executor;
    public GraphQlSchemaTests()
    {
        var services = new ServiceCollection()
            .BuildSchema()
            .Services
            .AddSingleton(sp => new RequestExecutorProxy(sp.GetRequiredService<IRequestExecutorResolver>(), HotChocolate.Schema.DefaultName));
        _serviceProvider = services.BuildServiceProvider();

        _executor = _serviceProvider.GetRequiredService<RequestExecutorProxy>();
    }

    [Test]
    public async Task SchemaChangesTest()
    {
        // act
        var schema = await _executor.GetSchemaAsync(default);

        // assert
        schema.ToString().MatchSnapshot();
    }
}

