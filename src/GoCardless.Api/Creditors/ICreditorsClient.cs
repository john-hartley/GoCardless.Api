using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        Task<AllCreditorsResponse> AllAsync();
        Task<AllCreditorsResponse> AllAsync(AllCreditorsRequest request);
        Task<CreditorResponse> ForIdAsync(string creditorId);
        Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request);
    }
}