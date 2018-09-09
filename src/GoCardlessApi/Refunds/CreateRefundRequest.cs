﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Refunds
{
    public class CreateRefundRequest
    {
        public int Amount { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreateRefundLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
        public int TotalAmountConfirmation { get; set; }
    }
}