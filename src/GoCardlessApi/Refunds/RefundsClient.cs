using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Refunds
{
    public class RefundsClient : ApiClientBase
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
    }
}