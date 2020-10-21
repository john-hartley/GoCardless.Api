using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.InstalmentSchedules
{
    public abstract class CreateInstalmentScheduleOptions
    {
        public CreateInstalmentScheduleOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public string Currency { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public InstalmentScheduleLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }

        public int TotalAmount { get; set; }
    }
}