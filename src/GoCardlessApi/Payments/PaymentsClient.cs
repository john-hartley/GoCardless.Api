using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public class PaymentsClient : ApiClientBase, IPaymentsClient
    {
        public PaymentsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreatePaymentRequest, CreatePaymentResponse>(
                "payments",
                new { payments = request },
                idempotencyKey
            );
        }

        public Task<AllPaymentsResponse> AllAsync()
        {
            return GetAsync<AllPaymentsResponse>("payments");
        }

        public Task<PaymentResponse> ForIdAsync(string paymentId)
        {
            return GetAsync<PaymentResponse>($"payments/{paymentId}");
        }

        public Task<UpdatePaymentResponse> UpdateAsync(UpdatePaymentRequest request)
        {
            return PutAsync<UpdatePaymentRequest, UpdatePaymentResponse>(
                $"payments/{request.Id}",
                new { payments = request }
            );
        }

        public Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            return PostAsync<CancelPaymentRequest, CancelPaymentResponse>(
                $"payments/{request.Id}/actions/cancel",
                new { payments = request }
            );
        }

        public Task<RetryPaymentResponse> RetryAsync(RetryPaymentRequest request)
        {
            return PostAsync<RetryPaymentRequest, RetryPaymentResponse>(
                $"payments/{request.Id}/actions/retry",
                new { payments = request }
            );
        }
    }
}