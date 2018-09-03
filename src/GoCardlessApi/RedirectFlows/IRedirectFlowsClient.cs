using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<CompleteRedirectFlowResponse> CompleteAsync(CompleteRedirectFlowRequest request);
        Task<CreateRedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request);
        Task<RedirectFlowResponse> ForIdAsync(string redirectFlowId);
    }
}