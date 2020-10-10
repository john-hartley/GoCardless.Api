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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCreditorsOptions, Creditor> BuildPager()
        {
            return new Pager<GetCreditorsOptions, Creditor>(GetPageAsync);
        }

        public async Task<Response<Creditor>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Creditor>>(request =>
            {
                request.AppendPathSegment($"creditors/{id}");
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Creditor>>(request =>
            {
                request.AppendPathSegment("creditors");
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Creditor>>(request =>
            {
                request
                    .AppendPathSegment("creditors")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
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

            return await _apiClient.PutAsync<Response<Creditor>>(
                request =>
                {
                    request.AppendPathSegment($"creditors/{options.Id}");
                },
                new { creditors = options });
        }
    }
}