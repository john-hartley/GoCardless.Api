using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public class CreditorsClient : ApiClientBase, ICreditorsClient
    {
        public CreditorsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetCreditorsRequest, Creditor> BuildPager()
        {
            return new Pager<GetCreditorsRequest, Creditor>(GetPageAsync);
        }

        public Task<Response<Creditor>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<Creditor>>($"creditors/{id}");
        }

        public Task<PagedResponse<Creditor>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Creditor>>("creditors");
        }

        public Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Creditor>>("creditors", request.ToReadOnlyDictionary());
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