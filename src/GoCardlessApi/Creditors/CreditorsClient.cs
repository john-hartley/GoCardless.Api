using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Creditors
{
    public class CreditorsClient : ApiClientBase, ICreditorsClient
    {
        public CreditorsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllCreditorsResponse> AllAsync()
        {
            return GetAsync<AllCreditorsResponse>("creditors");
        }

        public Task<AllCreditorsResponse> AllAsync(AllCreditorsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllCreditorsResponse>("creditors", request.ToReadOnlyDictionary());
        }

        public Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            if (string.IsNullOrWhiteSpace(creditorId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(creditorId));
            }

            return GetAsync<CreditorResponse>($"creditors/{creditorId}");
        }

        public Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateCreditorResponse>(
                $"creditors/{request.Id}",
                new { creditors = request }
            );
        }
    }
}