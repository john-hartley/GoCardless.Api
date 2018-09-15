﻿using GoCardless.Api.Core;
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