using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Plex.HttpClients;

internal sealed class PlexAuthenticationHandler : DelegatingHandler
{
    private readonly string _headerName;
    private readonly string _headerValue;
    private readonly Uri _serverOrigin;

    public PlexAuthenticationHandler(string headerName, string headerValue, Uri serverOrigin)
    {
        _headerName = headerName;
        _headerValue = headerValue;
        _serverOrigin = serverOrigin;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Remove(_headerName);

        if (request.RequestUri is { } requestUri && HasSameOrigin(requestUri, _serverOrigin))
            request.Headers.TryAddWithoutValidation(_headerName, _headerValue);

        return base.SendAsync(request, cancellationToken);
    }

    private static bool HasSameOrigin(Uri left, Uri right)
    {
        return string.Equals(left.Scheme, right.Scheme, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(left.Host, right.Host, StringComparison.OrdinalIgnoreCase) &&
               left.Port == right.Port;
    }
}
