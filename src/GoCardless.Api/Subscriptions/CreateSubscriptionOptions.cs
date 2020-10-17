using GoCardless.Api.Http.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Subscriptions
{
    public class CreateSubscriptionOptions
    {
        public CreateSubscriptionOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public int? Count { get; set; }
        public string Currency { get; set; }
        public int? DayOfMonth { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public int Interval { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.IntervalUnit"/> for possible values.
        /// </summary>
        public string IntervalUnit { get; set; }

        public SubscriptionLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.Month"/> for possible values.
        /// <para>Use <see cref="Subscriptions.Month.NameOf(DateTime)"/> to get the month name for a given date.</para>
        /// </summary>
        public string Month { get; set; }

        public string Name { get; set; }
        public string PaymentReference { get; set; }

        /// <summary>
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? StartDate { get; set; }
    }
}