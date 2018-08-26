﻿using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public class PaymentsClient : ApiClientBase
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

        public Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            return PostAsync<CancelPaymentRequest, CancelPaymentResponse>(
                new { data = request },
                new string[] { "payments", request.Id, "actions", "cancel" }
            );
        }
    }
}