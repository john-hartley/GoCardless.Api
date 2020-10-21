using GoCardlessApi.Http.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.InstalmentSchedules
{
    public class Schedule
    {
        public IEnumerable<int> Amounts { get; set; }
        public string IntervalUnit { get; set; }
        public int Interval { get; set; }
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime StartDate { get; set; }
    }
}