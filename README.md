# GoCardless.Api

[![Build status](https://ci.appveyor.com/api/projects/status/aowcdofcx48csujf/branch/master?svg=true)](https://ci.appveyor.com/project/john-hartley/gocardless-api)
[![Nuget.org](https://img.shields.io/nuget/v/GoCardless.Api.svg?style=flat)](https://www.nuget.org/packages/GoCardless.Api)

This is an unofficial alternative to the existing GoCardless .NET API client.

This project aims to improve upon the official client by:

- Being easier to test against
- Not mixing `sync` and `async` calls
- Providing simple, predictable mechanisms for paging

To stay up-to-date with the API, please read the [official documentation](https://developer.gocardless.com/api-reference).

## Features

- Support for all core endpoints, including those currently in closed beta
- Support for partner integrations
- `async` all the way down
- More than 300 tests, with almost 100 of those being integration tests

## Versioning

This project respects [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for all public releases that are pushed to [nuget.org](https://nuget.org).
    
## Getting Started

### Installation

To install from NuGet, use:

    Install-Package GoCardless.Api
    
### Initialisation

In order to start using the library, you can create a `GoCardlessClient` instance to make requests with:

```c#
using GoCardless.Api;
using GoCardless.Api.Core.Configuration;
    
var configuration = ClientConfiguration.ForSandbox("your_access_token");
var client = new GoCardlessClient(configuration);
```

Each resource (e.g. payments, subscriptions, etc.) has its own individual client, and can be constructed in the same manner as above. `GoCardlessClient` exposes an instance of each client, and is provided solely as a convenience.

### Making Requests

Making requests entails either creating a request object, or passing the `id` of the resource you're interested in. For example, creating a new `Customer` can achieved as follows:

```c#
using GoCardless.Api.Customers;

var request = new CreateCustomerRequest
{
    AddressLine1 = "Address Line 1",
    AddressLine2 = "Address Line 2",
    AddressLine3 = "Address Line 3",
    City = "London",
    CountryCode = "GB",
    Email = "email@example.com",
    FamilyName = "Family Name",
    GivenName = "Given Name",
    Language = "en",
    PostalCode = "SW1A 1AA",
};

var response = await client.Customers.CreateAsync(request);
var customer = response.Item;
```

Notice how I have imported the `GoCardless.Api.Customers` namespace. Each resource is scoped to its own namespace. For example, all types involved in managing subscriptions can be found in the `GoCardless.Api.Subscriptions` namespace. Similarly, all payments types are in the corresponding `GoCardless.Api.Payments` namespace.

### Idempotency Keys and Retries

All `CreateXRequest` types will generate an idempotency key when constructed, using `Guid.NewGuid().ToString()`, but it's recommended that you set them yourself. Idempotency keys are important, as they prevent duplicate resources from being created. See [the official documentation](https://developer.gocardless.com/api-reference/#making-requests-idempotency-keys) for more information.

Retry logic is currently not built into the client. For retry logic, I'd highly-recommend using the [Polly](https://github.com/App-vNext/Polly) resilience and transient-fault-handling library.

#### Conflicting Resources

If you attempt to create a resource that already exists, either because of retrying a request with an idempotency key, or from trying to create a bank account that already exists, the client will automatically handle the API error response, and will fetch the existing resource for you.

### Paging

GoCardless' API uses what's called cursor-pagination. From a high level, all you need to know is that "before" means records that are newer, and "after" means records that are older. This sounds counter-intuitive, because results returned from the API are in reverse-chronological order, meaning the newest results are returned first.
    
For each type of resource that supports paging, there are a few different ways in which you can access paged data.

| Method | Description |
|:--------:|-----------|
| GetPageAsync() | Returns a single page of the most recently added items. |
| GetPageAsync(GetXRequest request) | Where `X` is a collection of resources (e.g. `Customers`, `Subscriptions`, etc.), returns a single page of items, allowing you provide additional filtering. The filtering capabilities differ per endpoint. Please refer to the [official documentation](https://developer.gocardless.com/api-reference/) for information on the parameters.
| BuildPager() | Provides a simple abstraction that allows you to get all pages in either direction. |

As an example of `BuildPager()`, let's say you wanted to get all payments for a given subscription. You can do that like so:

```c#
using GoCardless.Api.Payments;

// Allows you to specify any additional filters
// just as with GetPageAsync().
var initialRequest = new GetPaymentsRequest
{
    Subscription = "SB12345678"
};

var payments = await client.Payments
    .BuildPager()
    .StartFrom(initialRequest)
    .AndGetAllAfterAsync(); // Remember "after" means older than.
```

The code above will use `initialRequest` to get the first (i.e. newest) page of payments for a subscription with an id of `SB12345678`, and then continue sending requests to get the subsequent (i.e. older) pages, until there are no more left. The results of the initial request will be joined together with all of the subsequent requests, returning the complete list of payments for the subscription.

There is a corresponding `AndGetAllBeforeAsync()` method to page in the opposite direction (i.e. oldest to newest).

#### Advanced Paging

If you find yourself needing to perform more complicated types of paging, that the helper methods above do not cater for, each `GetPageAsync` call returns a `PagedResponse<T>` containing the underlying cursors necessary to page through resources. This means you can page over those resources yourself, adding any additional logic as required. A simple way to achieve this would be:

```c#
var results = new List<Payment>();
do
{
    var response = await client.Payments.GetPageAsync(request).ConfigureAwait(false);
    results.AddRange(response.Items ?? Enumerable.Empty<Payment>());

    request.After = response.Meta.Cursors.After;
} while (request.After != null);
```

### Exception Handling

The GoCardless API can generate several different kinds of error, and so there are a few different types of `Exception` defined in this library. 

- `ApiException` - the base exception type from which all others are derived
- `InvalidApiUsageException`
- `InvalidStateException`
- `ValidationFailedException`

These correspond with the [4 error types](https://developer.gocardless.com/api-reference/#api-usage-errors) defined by the API. 

Each exception type exposes a number of properties, matching with those in the official documentation, but I've also added a `RawResponse` property to help diagnose any edge cases.

## Questions

If you have any questions, I'll do my best to answer them. However, please note that if you're asking about _how_ the API works, as opposed to a question about the client, you should raise a [support ticket](https://support.gocardless.com/hc/en-gb) with the GoCardless team.

## Issues

If you notice a problem with the client, please [create an issue](https://github.com/john-hartley/GoCardless.Api/issues/new), including, where appropriate, steps to reproduce the problem.
    
## Built With

- [Flurl](https://github.com/tmenier/Flurl) - Fluent URL builder and testable HTTP client for .NET

## Authors

- [John Hartley](https://github.com/john-hartley)

## Special Mentions

I'd like to say thank you to the GoCardless support team for answering so many of my questions, allowing me to reuse parts of their documentation, and for granting me access to all API endpoints, which allowed me to write integration tests against them.

I'd also like to thank the GoCardless engineering team. Whilst I don't know how much work they had to go through to help me, I do know some of my support tickets were escalated to them, so thank you for any work you did behind the scenes.

Finally, I'd like to thank [Workshop2](https://github.com/Workshop2) for pointing me in the right direction when setting up AppVeyor.
