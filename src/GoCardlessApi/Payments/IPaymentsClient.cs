using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public interface IPaymentsClient
    {
        Task<AllPaymentsResponse> AllAsync();
        Task<AllPaymentsResponse> AllAsync(AllPaymentsRequest request);
        Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request);
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request);
        Task<PaymentResponse> ForIdAsync(string paymentId);
        Task<RetryPaymentResponse> RetryAsync(RetryPaymentRequest request);
        Task<UpdatePaymentResponse> UpdateAsync(UpdatePaymentRequest request);
    }
}