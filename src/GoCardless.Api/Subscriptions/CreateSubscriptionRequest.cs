using GoCardless.Api.Core.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Subscriptions
{
    public class CreateSubscriptionRequest
    {
        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public int Count { get; set; }
        public string Currency { get; set; }
        public int? DayOfMonth { get; set; }

        /// <summary>
        /// Date on or after which no further payments should be created. If this field is blank 
        /// and count is not specified, the subscription will continue indefinitely.
        /// <para />
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [Obsolete("Deprecated: This field will be removed in a future API version. Use the Count property to specify a number of payments instead.")]
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? EndDate { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public int Interval { get; set; }
        public string IntervalUnit { get; set; }
        public SubscriptionLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Month { get; set; }
        public string Name { get; set; }
        public string PaymentReference { get; set; }

        /// <summary>
        /// The date on which the first payment should be charged. Must be within one year of 
        /// creation and on or after the mandate’s next possible charge date. When blank, this 
        /// will be set as the mandate’s next possible charge date.
        /// <para />
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? StartDate { get; set; }
    }
}