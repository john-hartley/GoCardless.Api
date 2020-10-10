﻿using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        IPagerBuilder<GetPaymentsOptions, Payment> BuildPager();
        Task<Response<Payment>> CancelAsync(CancelPaymentOptions options);
        Task<Response<Payment>> CreateAsync(CreatePaymentOptions options);
        Task<Response<Payment>> ForIdAsync(string id);
        Task<PagedResponse<Payment>> GetPageAsync();
        Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsOptions options);
        Task<Response<Payment>> RetryAsync(RetryPaymentOptions options);
        Task<Response<Payment>> UpdateAsync(UpdatePaymentOptions options);
    }
}