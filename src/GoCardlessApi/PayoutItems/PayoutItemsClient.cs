﻿using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.PayoutItems
{
    public class PayoutItemsClient : ApiClientBase, IPayoutItemsClient
    {
        public PayoutItemsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PayoutItemsResponse> ForPayoutAsync(PayoutItemsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Payout))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Payout));
            }
            
            return GetAsync<PayoutItemsResponse>(
                "payout_items",
                request.ToReadOnlyDictionary()
            );
        }
    }
}