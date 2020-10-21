using GoCardlessApi.Http.Serialisation;
using Newtonsoft.Json;
using System;

namespace GoCardlessApi.InstalmentSchedules
{
    public class InstalmentScheduleDate
    {
        public int Amount { get; set; }
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime ChargeDate { get; set; }
    }
}