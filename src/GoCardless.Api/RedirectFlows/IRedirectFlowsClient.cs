using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<CompleteRedirectFlowResponse> CompleteAsync(CompleteRedirectFlowRequest request);
        Task<CreateRedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request);
        Task<RedirectFlowResponse> ForIdAsync(string redirectFlowId);
    }
}