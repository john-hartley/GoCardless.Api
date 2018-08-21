using System.Threading.Tasks;

namespace GoCardlessApi
{
    public interface ICreditorsClient
    {
        Task<CreditorResponse> ForIdAsync(string creditorId);
        Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request);
    }
}
