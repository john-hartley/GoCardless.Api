using System;
using System.Collections.Generic;

namespace GoCardless.Api.Payments
{
    public class Payment
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public int AmountRefunded { get; set; }
        public DateTime ChargeDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public PaymentLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
    }
}