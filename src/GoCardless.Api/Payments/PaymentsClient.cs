﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private readonly ApiClient _apiClient;

        public PaymentsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Payment>> CancelAsync(CancelPaymentOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"payments/{options.Id}/actions/cancel")
                    .PostJsonAsync(new { payments = options })
                    .ReceiveJson<Response<Payment>>();
            });
        }

        public async Task<Response<Payment>> CreateAsync(CreatePaymentOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                async request =>
                {
                    return await request
                        .AppendPathSegment("payments")
                        .PostJsonAsync(new { payments = options })
                        .ReceiveJson<Response<Payment>>();
                });
        }

        public async Task<Response<Payment>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"payments/{id}")
                    .GetJsonAsync<Response<Payment>>();
            });
        }

        public async Task<PagedResponse<Payment>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("payments")
                    .GetJsonAsync<PagedResponse<Payment>>();
            });
        }

        public async Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("payments")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Payment>>();
            });
        }

        public IPager<GetPaymentsOptions, Payment> PageUsing(GetPaymentsOptions options)
        {
            return new Pager<GetPaymentsOptions, Payment>(GetPageAsync, options);
        }

        public async Task<Response<Payment>> RetryAsync(RetryPaymentOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"payments/{options.Id}/actions/retry")
                    .PostJsonAsync(new { payments = options })
                    .ReceiveJson<Response<Payment>>();
            });
        }

        public async Task<Response<Payment>> UpdateAsync(UpdatePaymentOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"payments/{options.Id}")
                    .PutJsonAsync(new { payments = options })
                    .ReceiveJson<Response<Payment>>();
            });
        }
    }
}