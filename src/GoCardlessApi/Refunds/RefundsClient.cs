using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Refunds
{
    public class RefundsClient : ApiClientBase, IRefundsClient
    {
        public RefundsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateRefundResponse> CreateAsync(CreateRefundRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateRefundRequest, CreateRefundResponse>(
                new { refunds = request },
                idempotencyKey,
                new string[] { "refunds" }
            );
        }

        public Task<AllRefundsResponse> AllAsync()
        {
            return GetAsync<AllRefundsResponse>("refunds");
        }

        public Task<RefundResponse> ForIdAsync(string refundId)
        {
            return GetAsync<RefundResponse>("refunds", refundId);
        }

        public Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request)
        {
            return PutAsync<UpdateRefundRequest, UpdateRefundResponse>(
                new { refunds = request },
                "refunds",
                request.Id
            );
        }
    }
}