﻿using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        IPagerBuilder<GetCreditorsOptions, Creditor> BuildPager();
        Task<Response<Creditor>> ForIdAsync(string id);
        Task<PagedResponse<Creditor>> GetPageAsync();
        Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions request);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorOptions request);
    }
}