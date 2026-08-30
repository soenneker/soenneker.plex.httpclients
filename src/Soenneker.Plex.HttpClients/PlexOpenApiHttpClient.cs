using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Plex.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Plex.HttpClients;

public sealed class PlexOpenApiHttpClient : IPlexOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private const string _prodBaseUrl = "http://localhost:32400";

    public PlexOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(PlexOpenApiHttpClient), (config: _config, baseUrl: _config["Plex:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            var baseAddress = new Uri(state.baseUrl, UriKind.Absolute);

            var apiKey = state.config.GetValueStrict<string>("Plex:ApiKey");
            string authHeaderName = state.config["Plex:AuthHeaderName"] ?? "X-Plex-Token";
            string authHeaderValueTemplate = state.config["Plex:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = baseAddress,
                AllowAutoRedirect = false,
                DelegatingHandlerFactories = [() => new PlexAuthenticationHandler(authHeaderName, authHeaderValue, baseAddress)]
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
