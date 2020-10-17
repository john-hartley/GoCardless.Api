using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public PaymentsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
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
                request =>
                {
                    return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"payments/{id}")
                    .GetJsonAsync<Response<Payment>>();
            });
        }

        public async Task<PagedResponse<Payment>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("payments")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Payment>>();
            });
        }

        public IPager<GetPaymentsOptions, Payment> PageFrom(GetPaymentsOptions options)
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
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

            return await _apiClient.PutAsync<Response<Payment>>(
                request =>
                {
                    request.AppendPathSegment($"payments/{options.Id}");
                },
                new { payments = options });
        }
    }
}