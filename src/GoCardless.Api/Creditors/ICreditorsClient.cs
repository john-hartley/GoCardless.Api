using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Creditors
{
    public interface ICreditorsClient : IPageable<GetCreditorsOptions, Creditor>
    {
        Task<Response<Creditor>> ForIdAsync(string id);
        Task<PagedResponse<Creditor>> GetPageAsync();
        Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions options);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorOptions options);
    }
}