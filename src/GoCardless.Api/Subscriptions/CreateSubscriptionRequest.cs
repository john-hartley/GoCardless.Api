using GoCardless.Api.Core.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Subscriptions
{
    /// <summary>
    /// For up-to-date documentation of this request, see: https://developer.gocardless.com/api-reference/#subscriptions-create-a-subscription
    /// </summary>
    public class CreateSubscriptionRequest
    {
        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public int Count { get; set; }
        public string Currency { get; set; }
        public int? DayOfMonth { get; set; }

        /// <summary>
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
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? StartDate { get; set; }
    }
}