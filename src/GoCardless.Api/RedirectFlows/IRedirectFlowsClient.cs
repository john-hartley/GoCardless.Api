using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowOptions options);
        Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowOptions options);
        Task<Response<RedirectFlow>> ForIdAsync(string id);
    }
}