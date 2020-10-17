using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public interface IPayoutsClient : IPageable<GetPayoutsOptions, Payout>
    {
        Task<Response<Payout>> ForIdAsync(string id);
        Task<PagedResponse<Payout>> GetPageAsync();
        Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsOptions options);
    }
}