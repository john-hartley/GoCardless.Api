using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Mandates
{
    public class Mandate
    {
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public MandateLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("next_possible_charge_date")]
        public DateTime? NextPossibleChargeDate { get; set; }

        public bool PaymentsRequireApproval { get; set; }
        public string Reference { get; set; }
        public string Scheme { get; set; }
        public string Status { get; set; }
    }
}