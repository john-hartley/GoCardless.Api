using GoCardless.Api.Serialisation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Payments
{
    public class CreatePaymentOptions
    {
        public CreatePaymentOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public int Amount { get; set; }
        public int? AppFee { get; set; }

        /// <summary>
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