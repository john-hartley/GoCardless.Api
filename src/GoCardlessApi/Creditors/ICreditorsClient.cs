using System.Threading.Tasks;

namespace GoCardlessApi.Creditors
{
    public interface ICreditorsClient
    {
        Task<AllCreditorResponse> AllAsync();
        Task<CreditorResponse> ForIdAsync(string creditorId);
        Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request);
    }
}