﻿using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public class RefundsClient : ApiClientBase, IRefundsClient
    {
        public RefundsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Refund>> AllAsync()
        {
            return GetAsync<PagedResponse<Refund>>("refunds");
        }

        public Task<PagedResponse<Refund>> AllAsync(AllRefundsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Refund>>("refunds", request.ToReadOnlyDictionary());
        }

        public Task<RefundResponse> CreateAsync(CreateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            return PostAsync<RefundResponse>(
                "refunds",
                new { refunds = request },
                request.IdempotencyKey
            );
        }

        public Task<RefundResponse> ForIdAsync(string refundId)
        {
            if (string.IsNullOrWhiteSpace(refundId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(refundId));
            }

            return GetAsync<RefundResponse>($"refunds/{refundId}");
        }

        public Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateRefundResponse>(
                $"refunds/{request.Id}",
                new { refunds = request }
            );
        }
    }
}