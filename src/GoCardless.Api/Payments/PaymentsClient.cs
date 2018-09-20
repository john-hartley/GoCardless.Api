using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payments
{
    public class PaymentsClient : ApiClientBase, IPaymentsClient
    {
        public PaymentsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetPaymentsRequest, Payment> BuildPager()
        {
            return new Pager<GetPaymentsRequest, Payment>(GetPageAsync);
        }

        public Task<Response<Payment>> CancelAsync(CancelPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Payment>>(
                $"payments/{request.Id}/actions/cancel",
                new { payments = request }
            );
        }

        public Task<Response<Payment>> CreateAsync(CreatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<Payment>>(
                "payments",
                new { payments = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<Payment>> ForIdAsync(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(paymentId));
            }

            return GetAsync<Response<Payment>>($"payments/{paymentId}");
        }

        public Task<PagedResponse<Payment>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Payment>>("payments");
        }

        public Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Payment>>("payments", request.ToReadOnlyDictionary());
        }

        public Task<Response<Payment>> RetryAsync(RetryPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Payment>>(
                $"payments/{request.Id}/actions/retry",
                new { payments = request }
            );
        }

        public Task<Response<Payment>> UpdateAsync(UpdatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Payment>>(
                $"payments/{request.Id}",
                new { payments = request }
            );
        }
    }
}