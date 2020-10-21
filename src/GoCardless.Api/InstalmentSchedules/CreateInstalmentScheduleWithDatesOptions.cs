using System.Collections.Generic;

namespace GoCardlessApi.InstalmentSchedules
{
    public class CreateInstalmentScheduleWithDatesOptions : CreateInstalmentScheduleOptions
    {
        public IEnumerable<InstalmentScheduleDate> Instalments { get; set; }
    }
}