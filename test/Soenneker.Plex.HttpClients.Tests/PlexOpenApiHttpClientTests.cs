using Soenneker.Plex.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Plex.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PlexOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPlexOpenApiHttpClient _httpclient;

    public PlexOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IPlexOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
