using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        Task<PagedResponse<Payment>> AllAsync();
        Task<PagedResponse<Payment>> AllAsync(AllPaymentsRequest request);
        Task<Response<Payment>> CancelAsync(CancelPaymentRequest request);
        Task<Response<Payment>> CreateAsync(CreatePaymentRequest request);
        Task<Response<Payment>> ForIdAsync(string paymentId);
        Task<Response<Payment>> RetryAsync(RetryPaymentRequest request);
        Task<Response<Payment>> UpdateAsync(UpdatePaymentRequest request);
    }
}