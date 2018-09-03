using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<CreateRedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request);
        Task<RedirectFlowResponse> ForIdAsync(string redirectFlowId);
    }
}