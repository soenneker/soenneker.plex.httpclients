using Soenneker.Plex.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Plex.HttpClients.Tests;

[Collection("Collection")]
public sealed class PlexOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IPlexOpenApiHttpClient _httpclient;

    public PlexOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IPlexOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
