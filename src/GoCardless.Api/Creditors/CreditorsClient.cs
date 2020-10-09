using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public class CreditorsClient : ApiClient, ICreditorsClient
    {
        private readonly IApiClient _apiClient;

        public CreditorsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCreditorsRequest, Creditor> BuildPager()
        {
            return new Pager<GetCreditorsRequest, Creditor>(GetPageAsync);
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

        public async Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsRequest options)
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

        public Task<Response<Creditor>> UpdateAsync(UpdateCreditorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Creditor>>(
                $"creditors/{request.Id}",
                new { creditors = request }
            );
        }
    }
}