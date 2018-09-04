using System;

namespace GoCardlessApi.Payouts
{
    public class Payout
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public int DeductedFees { get; set; }
        public PayoutLinks Links { get; set; }
        public string PayoutType { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
    }
}