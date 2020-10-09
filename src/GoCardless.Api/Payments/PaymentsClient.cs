﻿using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public class PaymentsClient : ApiClient, IPaymentsClient
    {
        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetPaymentsRequest, Payment> BuildPager()
        {
            return new Pager<GetPaymentsRequest, Payment>(GetPageAsync);
        }

        public async Task<Response<Payment>> CancelAsync(CancelPaymentRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Payment>>(
                "payments",
                new { payments = options },
                request =>
                {
                    request.AppendPathSegment($"payments/{options.Id}/actions/cancel");
                });
        }

        public async Task<Response<Payment>> CreateAsync(CreatePaymentRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Payment>>(
                "payments",
                new { payments = options },
                request =>
                {
                    request
                        .AppendPathSegment("payments")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                });
        }

        public async Task<Response<Payment>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Payment>>(request =>
            {
                request.AppendPathSegment($"payments/{id}");
            });
        }

        public async Task<PagedResponse<Payment>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Payment>>(request =>
            {
                request.AppendPathSegment("payments");
            });
        }

        public async Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Payment>>(request =>
            {
                request
                    .AppendPathSegment("payments")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public async Task<Response<Payment>> RetryAsync(RetryPaymentRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Payment>>(
                "payments",
                new { payments = options },
                request =>
                {
                    request.AppendPathSegment($"payments/{options.Id}/actions/retry");
                });
        }

        public Task<Response<Payment>> UpdateAsync(UpdatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Payment>>(
                $"payments/{request.Id}",
                new { payments = request }
            );
        }
    }
}