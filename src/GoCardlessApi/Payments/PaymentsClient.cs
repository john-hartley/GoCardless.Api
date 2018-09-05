using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public class PaymentsClient : ApiClientBase, IPaymentsClient
    {
        public PaymentsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllPaymentsResponse> AllAsync()
        {
            return GetAsync<AllPaymentsResponse>("payments");
        }

        public Task<AllPaymentsResponse> AllAsync(AllPaymentsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllPaymentsResponse>("payments", request.ToReadOnlyDictionary());
        }

        public Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<CancelPaymentResponse>(
                $"payments/{request.Id}/actions/cancel",
                new { payments = request }
            );
        }

        public Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreatePaymentResponse>(
                "payments",
                new { payments = request },
                idempotencyKey
            );
        }

        public Task<PaymentResponse> ForIdAsync(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(paymentId));
            }

            return GetAsync<PaymentResponse>($"payments/{paymentId}");
        }

        public Task<RetryPaymentResponse> RetryAsync(RetryPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<RetryPaymentResponse>(
                $"payments/{request.Id}/actions/retry",
                new { payments = request }
            );
        }

        public Task<UpdatePaymentResponse> UpdateAsync(UpdatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdatePaymentResponse>(
                $"payments/{request.Id}",
                new { payments = request }
            );
        }
    }
}