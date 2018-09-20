using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        IPagerBuilder<GetPaymentsRequest, Payment> BuildPager();
        Task<Response<Payment>> CancelAsync(CancelPaymentRequest request);
        Task<Response<Payment>> CreateAsync(CreatePaymentRequest request);
        Task<Response<Payment>> ForIdAsync(string paymentId);
        Task<PagedResponse<Payment>> GetPageAsync();
        Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsRequest request);
        Task<Response<Payment>> RetryAsync(RetryPaymentRequest request);
        Task<Response<Payment>> UpdateAsync(UpdatePaymentRequest request);
    }
}