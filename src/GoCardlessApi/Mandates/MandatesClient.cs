using GoCardlessApi.Core;
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

    }
}