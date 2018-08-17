using System;

namespace GoCardlessApi
{
    public class UpcomingPayment
    {
        public DateTime ChargeDate { get; set; }
        public int Amount { get; set; }
    }
}
