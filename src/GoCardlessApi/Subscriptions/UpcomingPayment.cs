using System;

namespace GoCardlessApi.Subscriptions
{
    public class UpcomingPayment
    {
        public int Amount { get; set; }
        public DateTime ChargeDate { get; set; }
    }
}
