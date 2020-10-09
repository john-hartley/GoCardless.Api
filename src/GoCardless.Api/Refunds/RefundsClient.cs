using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public class RefundsClient : ApiClient, IRefundsClient
    {
        private readonly IApiClient _apiClient;

        public RefundsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetRefundsRequest, Refund> BuildPager()
        {
            return new Pager<GetRefundsRequest, Refund>(GetPageAsync);
        }

        public async Task<Response<Refund>> CreateAsync(CreateRefundRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Refund>>(
                "refunds",
                new { refunds = options },
                request =>
                {
                    request
                        .AppendPathSegment("refunds")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                });
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

        public async Task<PagedResponse<Refund>> GetPageAsync(GetRefundsRequest options)
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

        public Task<Response<Refund>> UpdateAsync(UpdateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Refund>>(
                $"refunds/{request.Id}",
                new { refunds = request }
            );
        }
    }
}