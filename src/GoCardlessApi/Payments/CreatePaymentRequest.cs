using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Payments
{
    public class CreatePaymentRequest
    {
        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public string ChargeDate { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreatePaymentLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
    }
}