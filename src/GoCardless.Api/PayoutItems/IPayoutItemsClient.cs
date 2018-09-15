﻿using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient
    {
        Task<PagedResponse<PayoutItem>> ForPayoutAsync(PayoutItemsRequest request);
    }
}