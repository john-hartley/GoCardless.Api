using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Paging;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public class RefundsClient : ApiClientBase, IRefundsClient
    {
        public RefundsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Refund>> AllAsync()
        {
            return GetAsync<PagedResponse<Refund>>("refunds");
        }

        public Task<PagedResponse<Refund>> AllAsync(AllRefundsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Refund>>("refunds", request.ToReadOnlyDictionary());
        }

        public Task<Response<Refund>> CreateAsync(CreateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            return PostAsync<Response<Refund>>(
                "refunds",
                new { refunds = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<Refund>> ForIdAsync(string refundId)
        {
            if (string.IsNullOrWhiteSpace(refundId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(refundId));
            }

            return GetAsync<Response<Refund>>($"refunds/{refundId}");
        }

        public Task<Response<Refund>> UpdateAsync(UpdateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Refund>>(
                $"refunds/{request.Id}",
                new { refunds = request }
            );
        }
    }
}