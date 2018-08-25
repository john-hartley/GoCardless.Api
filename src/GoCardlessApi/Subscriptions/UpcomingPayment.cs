using System;

namespace GoCardlessApi.Subscriptions
{
    public class UpcomingPayment
    {
        public DateTime ChargeDate { get; set; }
        public int Amount { get; set; }
    }
}
