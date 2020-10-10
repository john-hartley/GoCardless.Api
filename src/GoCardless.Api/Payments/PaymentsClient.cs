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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetPaymentsOptions, Payment> BuildPager()
        {
            return new Pager<GetPaymentsOptions, Payment>(GetPageAsync);
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

            return await _apiClient.PostAsync<Response<Payment>>(
                request =>
                {
                    request.AppendPathSegment($"payments/{options.Id}/actions/cancel");
                },
                new { payments = options });
        }

        public async Task<Response<Payment>> CreateAsync(CreatePaymentOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Payment>>(
                request =>
                {
                    request
                        .AppendPathSegment("payments")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { payments = options });
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

        public async Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsOptions options)
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

            return await _apiClient.PostAsync<Response<Payment>>(
                request =>
                {
                    request.AppendPathSegment($"payments/{options.Id}/actions/retry");
                },
                new { payments = options });
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