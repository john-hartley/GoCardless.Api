using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.InstalmentSchedules
{
    public class InstalmentSchedulesClient : IInstalmentSchedulesClient
    {
        private readonly ApiClient _apiClient;

        public InstalmentSchedulesClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public Task<Response<InstalmentSchedule>> CancelAsync(CancelInstalmentScheduleOptions options)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<InstalmentSchedule>> CreateAsync(CreateInstalmentScheduleWithDatesOptions options)
        {
            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("instalment_schedules")
                        .PostJsonAsync(new { instalment_schedules = options })
                        .ReceiveJson<Response<InstalmentSchedule>>();
                });
        }

        public async Task<Response<InstalmentSchedule>> CreateAsync(CreateInstalmentScheduleWithScheduleOptions options)
        {
            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("instalment_schedules")
                        .PostJsonAsync(new { instalment_schedules = options })
                        .ReceiveJson<Response<InstalmentSchedule>>();
                });
        }

        public Task<Response<InstalmentSchedule>> ForIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResponse<InstalmentSchedule>> GetPageAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResponse<InstalmentSchedule>> GetPageAsync(GetInstalmentSchedulesOptions options)
        {
            throw new System.NotImplementedException();
        }

        public IPager<GetInstalmentSchedulesOptions, InstalmentSchedule> PageUsing(GetInstalmentSchedulesOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<InstalmentSchedule>> UpdateAsync(UpdateInstalmentScheduleOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}