using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        Task<PagedResponse<Creditor>> AllAsync();
        Task<PagedResponse<Creditor>> AllAsync(AllCreditorsRequest request);
        Task<CreditorResponse> ForIdAsync(string creditorId);
        Task<CreditorResponse> UpdateAsync(UpdateCreditorRequest request);
    }
}