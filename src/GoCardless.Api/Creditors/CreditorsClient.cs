using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public class CreditorsClient : ICreditorsClient
    {
        private readonly IApiClient _apiClient;

        public CreditorsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public CreditorsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<Creditor>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"creditors/{id}")
                    .GetJsonAsync<Response<Creditor>>();
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("creditors")
                    .GetJsonAsync<PagedResponse<Creditor>>();
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("creditors")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Creditor>>();
            });
        }

        public IPager<GetCreditorsOptions, Creditor> PageFrom(GetCreditorsOptions options)
        {
            return new Pager<GetCreditorsOptions, Creditor>(GetPageAsync, options);
        }

        public async Task<Response<Creditor>> UpdateAsync(UpdateCreditorOptions options)
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
                    .AppendPathSegment($"creditors/{options.Id}")
                    .PutJsonAsync(new { creditors = options })
                    .ReceiveJson<Response<Creditor>>();
            });
        }
    }
}