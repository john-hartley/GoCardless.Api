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
                "refunds",
                new { refunds = request },
                idempotencyKey
            );
        }

        public Task<AllRefundsResponse> AllAsync()
        {
            return GetAsync<AllRefundsResponse>("refunds");
        }

        public Task<RefundResponse> ForIdAsync(string refundId)
        {
            return GetAsync<RefundResponse>($"refunds/{refundId}");
        }

        public Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request)
        {
            return PutAsync<UpdateRefundRequest, UpdateRefundResponse>(
                $"refunds/{request.Id}",
                new { refunds = request }
            );
        }
    }
}