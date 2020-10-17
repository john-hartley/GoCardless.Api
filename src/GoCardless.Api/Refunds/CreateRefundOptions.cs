using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class CreateRefundOptions
    {
        public CreateRefundOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public int Amount { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreateRefundLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
        public int TotalAmountConfirmation { get; set; }
    }
}