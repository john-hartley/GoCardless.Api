namespace GoCardlessApi.InstalmentSchedules
{
    public class CreateInstalmentScheduleWithScheduleOptions : CreateInstalmentScheduleOptions
    {
        public Schedule Instalments { get; set; }
    }
}