using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public class RefundsClient : IRefundsClient
    {
        private readonly IApiClient _apiClient;

        public RefundsClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetRefundsOptions, Refund> BuildPager()
        {
            return new Pager<GetRefundsOptions, Refund>(GetPageAsync);
        }

        public async Task<Response<Refund>> CreateAsync(CreateRefundOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Refund>>(
                request =>
                {
                    request
                        .AppendPathSegment("refunds")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { refunds = options });
        }

        public async Task<Response<Refund>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Refund>>(request =>
            {
                request.AppendPathSegment($"refunds/{id}");
            });
        }

        public async Task<PagedResponse<Refund>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Refund>>(request =>
            {
                request.AppendPathSegment("refunds");
            });
        }

        public async Task<PagedResponse<Refund>> GetPageAsync(GetRefundsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Refund>>(request =>
            {
                request
                    .AppendPathSegment("refunds")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public async Task<Response<Refund>> UpdateAsync(UpdateRefundOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PutAsync<Response<Refund>>(
                request =>
                {
                    request.AppendPathSegment($"refunds/{options.Id}");
                },
                new { refunds = options });
        }
    }
}