# GoCardless.Api

This is an unofficial alternative to the existing GoCardless .NET API client.

Currently in alpha, this project aims to improve upon the official client by:

- Being easier to test against
- Not mixing `sync` and `async` calls
- Providing simple, predictable mechanisms for paging

# Roadmap

There are still a few endpoints that I've not yet implemented. These are:

- POST /customer_notifications/id/actions/handle 
- GET /events
- GET /events/id
- POST /mandate_import_entries
- GET /mandate_import_entries?mandate_import=id

The paging mechanisms, mentioned above, also need to be implemented. Expect breaking changes around the `AllAsync` methods for each client that supports them.

There is no documentation whatsoever at the moment beyond this readme. 

These are the 3 major areas that need addressing.
