[![](https://img.shields.io/nuget/v/soenneker.plex.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.plex.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.plex.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.httpclients/)

# Soenneker.Plex.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Plex.HttpClients
```

## Quick start

```csharp
using Soenneker.Plex.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddPlexOpenApiHttpClientAsSingleton();
```

Adds `PlexOpenApiHttpClient` as a singleton service.

## What you get

- `IPlexOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `PlexOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `PlexOpenApiHttpClientRegistrar.AddPlexOpenApiHttpClientAsSingleton(services)` | Adds `PlexOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `PlexOpenApiHttpClientRegistrar.AddPlexOpenApiHttpClientAsScoped(services)` | Adds `PlexOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
