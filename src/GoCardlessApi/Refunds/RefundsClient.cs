using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Refunds
{
    public class RefundsClient : ApiClientBase, IRefundsClient
    {
        public RefundsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllRefundsResponse> AllAsync()
        {
            return GetAsync<AllRefundsResponse>("refunds");
        }

        public Task<AllRefundsResponse> AllAsync(AllRefundsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllRefundsResponse>("refunds", request.ToReadOnlyDictionary());
        }

        public Task<CreateRefundResponse> CreateAsync(CreateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            return PostAsync<CreateRefundResponse>(
                "refunds",
                new { refunds = request },
                request.IdempotencyKey
            );
        }

        public Task<RefundResponse> ForIdAsync(string refundId)
        {
            if (string.IsNullOrWhiteSpace(refundId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(refundId));
            }

            return GetAsync<RefundResponse>($"refunds/{refundId}");
        }

        public Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateRefundResponse>(
                $"refunds/{request.Id}",
                new { refunds = request }
            );
        }
    }
}