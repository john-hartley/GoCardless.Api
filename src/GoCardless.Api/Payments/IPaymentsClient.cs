using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        Task<PagedResponse<Payment>> AllAsync();
        Task<PagedResponse<Payment>> AllAsync(AllPaymentsRequest request);
        Task<PaymentResponse> CancelAsync(CancelPaymentRequest request);
        Task<PaymentResponse> CreateAsync(CreatePaymentRequest request);
        Task<PaymentResponse> ForIdAsync(string paymentId);
        Task<PaymentResponse> RetryAsync(RetryPaymentRequest request);
        Task<PaymentResponse> UpdateAsync(UpdatePaymentRequest request);
    }
}