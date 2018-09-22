# GoCardless.Api

[![Build status](https://ci.appveyor.com/api/projects/status/aowcdofcx48csujf/branch/master?svg=true)](https://ci.appveyor.com/project/john-hartley/gocardless-api)
[![Nuget.org](https://img.shields.io/nuget/v/GoCardless.Api.svg?style=flat)](https://www.nuget.org/packages/GoCardless.Api)

This is an unofficial alternative to the existing GoCardless .NET API client.

Currently in beta, this project aims to improve upon the official client by:

- Being easier to test against
- Not mixing `sync` and `async` calls
- Providing simple, predictable mechanisms for paging

## Features

Includes support for:

- All core endpoints, including those currently in closed beta
- Partner integrations


## Installation

    Install-Package GoCardless.Api
    
## Versioning

This project respects [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for all public releases that are pushed to [nuget.org](https://nuget.org).

## Questions

If you have any questions, I'll do my best to answer them. However, please note that if you're asking about _how_ the API works, as opposed to a question about the client, you should raise a [support ticket](https://support.gocardless.com/hc/en-gb) with the GoCardless team.
    
## Built With

- [Flurl](https://github.com/tmenier/Flurl) - Fluent URL builder and testable HTTP client for .NET

## Authors

- *Initial work* - [John Hartley](https://github.com/john-hartley)

## Special Mentions

I'd like to say thank you to the GoCardless support team for answering so many of my questions, allowing me to reuse parts of their documentation, and for granting me access to all API endpoints, which allowed me to write integration tests against them.

I'd also like to thank the GoCardless engineering team. Whilst I don't know how much work they had to go through to help me, I do know some of my support tickets were escalated to them, so thank you for any work you did behind the scenes.
