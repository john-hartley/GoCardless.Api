using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Paging;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public class CreditorsClient : ApiClientBase, ICreditorsClient
    {
        public CreditorsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Creditor>> AllAsync()
        {
            return GetAsync<PagedResponse<Creditor>>("creditors");
        }

        public Task<PagedResponse<Creditor>> AllAsync(AllCreditorsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Creditor>>("creditors", request.ToReadOnlyDictionary());
        }

        public Task<Response<Creditor>> ForIdAsync(string creditorId)
        {
            if (string.IsNullOrWhiteSpace(creditorId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(creditorId));
            }

            return GetAsync<Response<Creditor>>($"creditors/{creditorId}");
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