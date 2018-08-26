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
                new { payments = request },
                idempotencyKey,
                new string[] { "payments" }
            );
        }

        public Task<AllPaymentsResponse> AllAsync()
        {
            return GetAsync<AllPaymentsResponse>("payments");
        }

        public Task<PaymentResponse> ForIdAsync(string paymentId)
        {
            return GetAsync<PaymentResponse>("payments", paymentId);
        }

        public Task<UpdatePaymentResponse> UpdateAsync(UpdatePaymentRequest request)
        {
            return PutAsync<UpdatePaymentRequest, UpdatePaymentResponse>(
                new { payments = request },
                "payments",
                request.Id
            );
        }

        public Task<RetryPaymentResponse> RetryAsync(RetryPaymentRequest request)
        {
            return PostAsync<RetryPaymentRequest, RetryPaymentResponse>(
                new { payments = request },
                new string[] { "payments", request.Id, "actions", "retry" }
            );
        }

        public Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            return PostAsync<CancelPaymentRequest, CancelPaymentResponse>(
                new { payments = request },
                new string[] { "payments", request.Id, "actions", "cancel" }
            );
        }
    }
}