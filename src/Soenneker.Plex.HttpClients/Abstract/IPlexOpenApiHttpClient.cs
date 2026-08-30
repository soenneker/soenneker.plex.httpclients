using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Plex.HttpClients.Abstract;

/// <summary>
/// Provides the process-wide cached HTTP client used to call a Plex Media Server API.
/// </summary>
public interface IPlexOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client configured with the Plex server base address and token header.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The shared Plex HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
