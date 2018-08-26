using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public interface IPaymentsClient
    {
        Task<AllPaymentsResponse> AllAsync();
        Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request);
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request);
        Task<PaymentResponse> ForIdAsync(string paymentId);
        Task<RetryPaymentResponse> RetryAsync(RetryPaymentRequest request);
        Task<UpdatePaymentResponse> UpdateAsync(UpdatePaymentRequest request);
    }
}