using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<RedirectFlowResponse> CompleteAsync(CompleteRedirectFlowRequest request);
        Task<RedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request);
        Task<RedirectFlowResponse> ForIdAsync(string redirectFlowId);
    }
}