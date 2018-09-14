using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
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

        public async Task<IEnumerable<Creditor>> AllBeforeAsync(AllCreditorsRequest request)
        {
            var response = await AllAsync(request);

            var results = new List<Creditor>(response.Creditors ?? Enumerable.Empty<Creditor>());
            while (response.Meta.Cursors.Before != null)
            {
                request.Before = response.Meta.Cursors.Before;

                response = await AllAsync(request);
                results.AddRange(response.Creditors ?? Enumerable.Empty<Creditor>());
            }

            return results;
        }

        public async Task<IEnumerable<Creditor>> AllAfterAsync(AllCreditorsRequest request)
        {
            var response = await AllAsync(request);

            var results = new List<Creditor>(response.Creditors ?? Enumerable.Empty<Creditor>());
            while (response.Meta.Cursors.After != null)
            {
                request.After = response.Meta.Cursors.After;

                response = await AllAsync(request);
                results.AddRange(response.Creditors ?? Enumerable.Empty<Creditor>());
            }

            return results;
        }

        public Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            if (string.IsNullOrWhiteSpace(creditorId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(creditorId));
            }

            return GetAsync<CreditorResponse>($"creditors/{creditorId}");
        }

        public Task<CreditorResponse> UpdateAsync(UpdateCreditorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<CreditorResponse>(
                $"creditors/{request.Id}",
                new { creditors = request }
            );
        }
    }
}