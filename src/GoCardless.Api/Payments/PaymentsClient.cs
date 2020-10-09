using Flurl.Http;
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

        public Task<Response<Payment>> CancelAsync(CancelPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Payment>>(
                $"payments/{request.Id}/actions/cancel",
                new { payments = request }
            );
        }

        public Task<Response<Payment>> CreateAsync(CreatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<Payment>>(
                "payments",
                new { payments = request },
                request.IdempotencyKey
            );
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

        public Task<Response<Payment>> RetryAsync(RetryPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Payment>>(
                $"payments/{request.Id}/actions/retry",
                new { payments = request }
            );
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