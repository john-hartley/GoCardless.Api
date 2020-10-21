using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.InstalmentSchedules
{
    public interface IInstalmentSchedulesClient : IPageable<GetInstalmentSchedulesOptions, InstalmentSchedule>
    {
        Task<Response<InstalmentSchedule>> CancelAsync(CancelInstalmentScheduleOptions options);
        Task<Response<InstalmentSchedule>> CreateAsync(CreateInstalmentScheduleWithDatesOptions options);
        Task<Response<InstalmentSchedule>> CreateAsync(CreateInstalmentScheduleWithScheduleOptions options);
        Task<Response<InstalmentSchedule>> ForIdAsync(string id);
        Task<PagedResponse<InstalmentSchedule>> GetPageAsync();
        Task<PagedResponse<InstalmentSchedule>> GetPageAsync(GetInstalmentSchedulesOptions options);
        Task<Response<InstalmentSchedule>> UpdateAsync(UpdateInstalmentScheduleOptions options);
    }
}