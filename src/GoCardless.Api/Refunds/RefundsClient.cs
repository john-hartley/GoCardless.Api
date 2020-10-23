using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Refunds
{
    public class RefundsClient : IRefundsClient
    {
        private readonly ApiClient _apiClient;

        public RefundsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Refund>> CreateAsync(CreateRefundOptions options)
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
                        .AppendPathSegment("refunds")
                        .PostJsonAsync(new { refunds = options })
                        .ReceiveJson<Response<Refund>>();
                });
        }

        public async Task<Response<Refund>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"refunds/{id}")
                    .GetJsonAsync<Response<Refund>>();
            });
        }

        public async Task<PagedResponse<Refund>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("refunds")
                    .GetJsonAsync<PagedResponse<Refund>>();
            });
        }

        public async Task<PagedResponse<Refund>> GetPageAsync(GetRefundsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("refunds")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Refund>>();
            });
        }

        public IPager<GetRefundsOptions, Refund> PageUsing(GetRefundsOptions options)
        {
            return new Pager<GetRefundsOptions, Refund>(GetPageAsync, options);
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

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"refunds/{options.Id}")
                    .PutJsonAsync(new { refunds = options })
                    .ReceiveJson<Response<Refund>>();
            });
        }
    }
}