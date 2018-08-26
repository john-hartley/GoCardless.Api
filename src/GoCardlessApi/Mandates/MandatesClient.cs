﻿using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Mandates
{
    public class MandatesClient : ApiClientBase
    {
        public MandatesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateMandateResponse> CreateAsync(CreateMandateRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateMandateRequest, CreateMandateResponse>(
                new { mandates = request },
                idempotencyKey,
                new string[] { "mandates" }
            );
        }

        public Task<AllMandatesResponse> AllAsync()
        {
            return GetAsync<AllMandatesResponse>("mandates");
        }

        public Task<MandateResponse> ForIdAsync(string mandateId)
        {
            return GetAsync<MandateResponse>("mandates", mandateId);
        }

        public Task<UpdateMandateResponse> UpdateAsync(UpdateMandateRequest request)
        {
            return PutAsync<UpdateMandateRequest, UpdateMandateResponse>(
                new { mandates = request },
                "mandates",
                request.Id
            );
        }

        public Task<CancelMandateResponse> CancelAsync(CancelMandateRequest request)
        {
            return PostAsync<CancelMandateRequest, CancelMandateResponse>(
                new { data = request },
                new string[] { "mandates", request.Id, "actions", "cancel" }
            );
        }

        public Task<ReinstateMandateResponse> ReinstateAsync(ReinstateMandateRequest request)
        {
            return PostAsync<ReinstateMandateRequest, ReinstateMandateResponse>(
                new { data = request },
                new string[] { "mandates", request.Id, "actions", "reinstate" }
            );
        }
    }
}