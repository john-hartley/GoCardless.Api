using System;
using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class Refund
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public RefundLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
    }
}