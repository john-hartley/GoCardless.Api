# GoCardless.Api

[![Build status](https://ci.appveyor.com/api/projects/status/aowcdofcx48csujf/branch/master?svg=true)](https://ci.appveyor.com/project/john-hartley/gocardless-api)
[![Nuget.org](https://img.shields.io/nuget/v/GoCardless.Api.svg?style=flat)](https://www.nuget.org/packages/GoCardless.Api)

This is an unofficial alternative to the existing GoCardless .NET API client.

Currently in alpha, this project aims to improve upon the official client by:

- Being easier to test against
- Not mixing `sync` and `async` calls
- Providing simple, predictable mechanisms for paging

# Road Map

There are still a few endpoints that I've not yet implemented. These are:

- ~~POST /customer_notifications/id/actions/handle~~ [Done](https://github.com/john-hartley/GoCardless.Api/pull/20)
- ~~GET /events~~ [Done](https://github.com/john-hartley/GoCardless.Api/pull/4)
- ~~GET /events/id~~ [Done](https://github.com/john-hartley/GoCardless.Api/pull/4)
- ~~POST /mandate_import_entries~~ [Done](https://github.com/john-hartley/GoCardless.Api/pull/3)
- ~~GET /mandate_import_entries?mandate_import=id~~ [Done](https://github.com/john-hartley/GoCardless.Api/pull/3)

~~The paging mechanisms, mentioned above, also need to be implemented. Expect breaking changes around the `AllAsync` methods for each client that supports them.~~ Done

There is no documentation whatsoever at the moment beyond this readme. 

These are the 3 major areas that need addressing before this client can be considered beta.
