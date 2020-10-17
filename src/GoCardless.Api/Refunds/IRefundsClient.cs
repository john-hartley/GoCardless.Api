using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public interface IRefundsClient : IPageable<GetRefundsOptions, Refund>
    {
        Task<Response<Refund>> CreateAsync(CreateRefundOptions options);
        Task<Response<Refund>> ForIdAsync(string id);
        Task<PagedResponse<Refund>> GetPageAsync();
        Task<PagedResponse<Refund>> GetPageAsync(GetRefundsOptions options);
        Task<Response<Refund>> UpdateAsync(UpdateRefundOptions options);
    }
}