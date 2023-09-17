# Modularr

## About

.NET application framework for building Modular Monoliths

## Features

- Modular class libraries
- Razor in modules
- Web API's in modules
- Scheduled background tasks

## Quickstart

1. Create a new ASP.NET web project
2. Add `Modularr` to the project

    `Modularr` is not published to NuGet yet, so download manually and add a reference
3. Add `Modularr` to the service collection and request pipeline:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModularr(builder =>
{
    // Add custom modules
    // builder.AddModule<HelloWorldModule>();
})
    // Optionally add multitenancy support
    // .WithMultiTenancy()
;

// .. omitted for brevity

app.UseModularr();
```

## Roadmap

- [ ] Publish to NuGet
- [ ] Scheduled background tasks
- [ ] Multitenancy
- [ ] Build-in support for datastores
  - [ ] SQLite
  - [ ] SQL server
- [ ] Build-in support for authorization/authentication
- [ ] Events
- [ ] Message bus
- [ ] Licensing
