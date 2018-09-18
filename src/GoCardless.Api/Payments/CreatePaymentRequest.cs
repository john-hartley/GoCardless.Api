using GoCardless.Api.Core.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Payments
{
    public class CreatePaymentRequest
    {
        public int Amount { get; set; }
        public int? AppFee { get; set; }

        /// <summary>
        /// A future date on which the payment should be collected. If not specified, 
        /// the payment will be collected as soon as possible. This must be on or after 
        /// the mandate’s next possible charge date, and will be rolled-forwards by 
        /// GoCardless if it is not a working day.
        /// <para />
        /// A calendar date in the ISO-8061 format of yyyy-MM-dd. If a time component is supplied,
        /// it will be discarded (e.g. 2018-09-18T15:05:06.123Z will become 2018-09-18).
        /// </summary>
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? ChargeDate { get; set; }

        public string Currency { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreatePaymentLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
    }
}