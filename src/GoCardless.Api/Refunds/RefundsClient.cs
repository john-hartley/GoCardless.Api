using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public class RefundsClient : ApiClientBase, IRefundsClient
    {
        public RefundsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetRefundsRequest, Refund> BuildPager()
        {
            return new Pager<GetRefundsRequest, Refund>(GetPageAsync);
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

        public Task<Response<Refund>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<Refund>>($"refunds/{id}");
        }

        public Task<PagedResponse<Refund>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Refund>>("refunds");
        }

        public Task<PagedResponse<Refund>> GetPageAsync(GetRefundsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Refund>>("refunds", request.ToReadOnlyDictionary());
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