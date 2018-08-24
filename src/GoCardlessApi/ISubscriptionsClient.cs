using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public interface ISubscriptionsClient
    {
        Task<AllSubscriptionsResponse> AllAsync();
        Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request);
        Task<SubscriptionResponse> ForIdAsync(string subscriptionId);
        Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request);
        Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request);
    }
}
