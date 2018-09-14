using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        Task<AllPaymentsResponse> AllAsync();
        Task<AllPaymentsResponse> AllAsync(AllPaymentsRequest request);
        Task<PaymentResponse> CancelAsync(CancelPaymentRequest request);
        Task<PaymentResponse> CreateAsync(CreatePaymentRequest request);
        Task<PaymentResponse> ForIdAsync(string paymentId);
        Task<PaymentResponse> RetryAsync(RetryPaymentRequest request);
        Task<PaymentResponse> UpdateAsync(UpdatePaymentRequest request);
    }
}