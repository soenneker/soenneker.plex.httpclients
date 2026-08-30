[![](https://img.shields.io/nuget/v/soenneker.plex.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.plex.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.plex.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.plex.httpclients/actions/workflows/codeql.yml)

# Soenneker.Plex.HttpClients

A cached `HttpClient` configured for a Plex Media Server, including its base URL and token header.

## Installation

```bash
dotnet add package Soenneker.Plex.HttpClients
```

## Configuration

```json
{
  "Plex": {
    "ClientBaseUrl": "http://localhost:32400",
    "ApiKey": "your-plex-token"
  }
}
```

`ClientBaseUrl` defaults to `http://localhost:32400`. Prefer HTTPS for a non-loopback server when it is available; HTTP sends the Plex token without transport encryption.

Plex normally expects the token in `X-Plex-Token`. Override the header only when an intermediary requires a different format:

```json
{
  "Plex": {
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

`{token}` is replaced with `Plex:ApiKey`.

## Registration and usage

```csharp
using Soenneker.Plex.HttpClients.Abstract;
using Soenneker.Plex.HttpClients.Registrars;

services.AddPlexOpenApiHttpClientAsScoped();

IPlexOpenApiHttpClient plexClient =
    serviceProvider.GetRequiredService<IPlexOpenApiHttpClient>();

HttpClient httpClient = await plexClient.Get(cancellationToken);
HttpResponseMessage response = await httpClient.GetAsync("/identity", cancellationToken);
response.EnsureSuccessStatusCode();
```

The wrapper may be scoped or singleton, but the underlying client and transport are cached process-wide. Disposing a scoped wrapper does not destroy the shared client.

Automatic redirects are disabled because Plex's token header must not be forwarded to another origin. Handle an expected redirect explicitly after validating its destination.
