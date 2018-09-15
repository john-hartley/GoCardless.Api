﻿using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public interface IRedirectFlowsClient
    {
        Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowRequest request);
        Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowRequest request);
        Task<Response<RedirectFlow>> ForIdAsync(string redirectFlowId);
    }
}