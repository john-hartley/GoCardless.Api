using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
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

        public Task<PaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<PaymentResponse>(
                $"payments/{request.Id}/actions/cancel",
                new { payments = request }
            );
        }

        public Task<PaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<PaymentResponse>(
                "payments",
                new { payments = request },
                request.IdempotencyKey
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

        public Task<PaymentResponse> RetryAsync(RetryPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<PaymentResponse>(
                $"payments/{request.Id}/actions/retry",
                new { payments = request }
            );
        }

        public Task<PaymentResponse> UpdateAsync(UpdatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<PaymentResponse>(
                $"payments/{request.Id}",
                new { payments = request }
            );
        }
    }
}