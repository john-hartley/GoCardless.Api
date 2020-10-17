using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowOptions options);
        Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowOptions options);
        Task<Response<RedirectFlow>> ForIdAsync(string id);
    }
}