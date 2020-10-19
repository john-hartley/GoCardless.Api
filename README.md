# GoCardless.Api

[![Build status](https://ci.appveyor.com/api/projects/status/aowcdofcx48csujf/branch/master?svg=true)](https://ci.appveyor.com/project/john-hartley/gocardless-api)
[![Nuget.org](https://img.shields.io/nuget/v/GoCardless.Api.svg?style=flat)](https://www.nuget.org/packages/GoCardless.Api)

This is an alternative to the official GoCardless .NET API client.

This project aims to improve upon that client by:

- Not mixing `sync` and `async` calls
- Making heavy use of integration tests to provide stronger guarantees of correctness
- Providing simple, predictable mechanisms for paging

There are currently more than 300 tests, with almost 100 of those being integration tests.

To stay up-to-date with the API, please read the [official documentation](https://developer.gocardless.com/api-reference).

## Features

- Support for all major endpoints, including those which are restricted (thanks to the GoCardless support team for enabling this for me)
- Support for partner integrations
- `async` all the way down

## Versioning

This project respects [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for all public releases that are pushed to [nuget.org](https://nuget.org).

## Getting Started

If you're upgrading from `2.x.x` to `3.x.x`, please read the [list of breaking changes](https://github.com/john-hartley/GoCardless.Api/wiki/Breaking-changes-from-2.x.x-to-3.x.x) to help navigate that upgrade.

### Installation

To install from NuGet, use:

    Install-Package GoCardless.Api

### Initialisation

In order to start using the library, you can create a `GoCardlessClient` instance to make requests with:

```c#
using GoCardlessApi;

var configuration = GoCardlessConfiguration.ForSandbox("your_access_token", throwOnConflict: false);
var client = new GoCardlessClient(configuration);
```

Each resource (e.g. payments, subscriptions, etc.) has its own client. `GoCardlessClient` exposes an instance of each client, and is provided solely as a convenience. As an example, we can construct a `CustomersClient` as follows:

```c#
using GoCardlessApi.Customers;

var customersClient = new CustomersClient(configuration);
```

Each resource is scoped to its own namespace - notice we've imported `GoCardlessApi.Customers`. For example, all types involved in managing payments can be found in the `GoCardlessApi.Payments` namespace.

### throwOnConflict

If you attempt to create a resource that already exists, either from retrying a request with an idempotency key (see below), or trying to create a bank account that already exists, the API will return a `409 Conflict` response. `throwOnConflict` determines what the client will do when this happens:

- When set to `false`, the client will perform a GET request to fetch the conflicting resource
- When set to `true`, the client will fail fast by throwing a `ConflictingResourceException`, and the exception's `ResourceId` property will hold the id of the conflicting resource

### Making Requests

Making requests entails either creating an `options` object, or passing the id of the resource you're interested in. For example, creating a new customer can achieved as follows:

```c#
using GoCardlessApi.Customers;

var options = new CreateCustomerOptions
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

// Using GoCardlessClient
var response = await client.Customers.CreateAsync(options);
var customer = response.Item;

// Using CustomersClient
var response = await customersClient.CreateAsync(options);
var customer = response.Item;
```

### Idempotency Keys and Retries

All `CreateXOptions` types will generate an idempotency key when constructed, using `Guid.NewGuid().ToString()`, though you can set them yourself. Idempotency keys are important, as they prevent duplicate resources from being created. See [the official documentation](https://developer.gocardless.com/api-reference/#making-requests-idempotency-keys) for more information.

Retry logic is currently not built into the client. There are no plans to add this feature as this can be handled by [Polly](https://github.com/App-vNext/Polly), which specialises in resiliency and fault tolerance.

### Paging

The GoCardless API returns lists of results in reverse-chronological order (i.e. the newest items appear first). The API uses cursor-pagination to page through results, where the `Before` and `After` cursors means "newer than" and "older than", respectively.

For each type of resource that supports paging, there are a few different ways in which you can access paged data.

| Method | Description |
|:--------:|-----------|
| `GetPageAsync()` | Returns a single page of the most recently added items. |
| `GetPageAsync(GetXOptions options)` | Where `X` is a collection of resources (e.g. `Customers`, `Subscriptions`, etc.), returns a single page of items, allowing you to provide additional filtering. The filtering capabilities differ per endpoint, so you should check the properties on the options type you're interested in. Please refer to the [official documentation](https://developer.gocardless.com/api-reference/) for more information on what the different properties do.
| `PageFrom(GetXOptions options)` | Provides a simple abstraction that allows you to get all pages in either direction. |

As an example of `PageFrom()`, let's say you wanted to get all payments for a given subscription. You can do that like so:

```c#
using GoCardlessApi.Payments;

// Allows you to specify any additional filters
// just as with GetPageAsync().
var options = new GetPaymentsOptions
{
    Subscription = "SB12345678"
};

var payments = await client.Payments
    .PageFrom(options)
    .AndGetAllAfterAsync(); // Remember "after" means older than.
```

The code above will use `options` to get the first (i.e. newest) page of payments for a subscription with an id of `SB12345678`, and then continue sending requests to get the subsequent (i.e. older) pages, until there are no more left. The results of the initial request will be joined together with all of the subsequent requests, returning the complete list of payments for the subscription.

There is a corresponding `AndGetAllBeforeAsync()` method to page in the opposite direction (i.e. oldest to newest).

As `AndGetAllBeforeAsync()` and `AndGetAllAfterAsync()` can be long-running operations, they have support for [`CancellationToken`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken).

### Advanced Paging

If you find yourself needing to perform more sophisticated paging that the helper methods above do not cater for, each `GetPageAsync` call returns a `PagedResponse<T>` containing the underlying cursors necessary to page through resources. This means you can page over those resources yourself, adding any additional logic as required. A simple way to achieve this would be:

```c#
var results = new List<Payment>();
do
{
    var response = await client.Payments.GetPageAsync(options);
    results.AddRange(response.Items ?? Enumerable.Empty<Payment>());

    options.After = response.Meta.Cursors.After;
} while (options.After != null);
```

### Exception Handling

The GoCardless API can generate several different kinds of error, and so there are a few different types of `Exception` defined in this library.

- `ApiException` - the base exception type from which all others are derived
- `InvalidApiUsageException`
- `InvalidStateException`
- `ValidationFailedException`

These correspond with the [4 error types](https://developer.gocardless.com/api-reference/#api-usage-errors) defined by the API. As explained above, there is also `ConflictingResourceException` which is defined as a convenience.

Each exception type exposes a number of properties, matching with those in the official documentation, but there is also a `RawResponse` property to help diagnose any edge cases.

As each exception type exposes the same properties, you have the choice of catching specific exceptions, or simply catching `ApiException`. The reason the different exception types exist is to make different problems stand out clearly when examining exceptions in application monitoring software such as [Sentry](https://sentry.io) and [Raygun](https://raygun.com/).

## Questions

If you have any questions, I'll do my best to answer them. However, please note that if you're asking about how the API works, as opposed to a question about the client, you should raise a [support ticket](https://support.gocardless.com/hc/en-gb) with the GoCardless team.

## Issues and Feature Requests

If you notice a problem with the client, or have a feature request, please [create an issue](https://github.com/john-hartley/GoCardless.Api/issues/new/choose).
