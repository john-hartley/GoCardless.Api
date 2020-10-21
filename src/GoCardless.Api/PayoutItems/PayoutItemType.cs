namespace GoCardlessApi.PayoutItems
{
    public static class PayoutItemType
    {
        public static readonly string AppFee = "app_fee";
        public static readonly string GoCardlessFee = "gocardless_fee";
        public static readonly string PaymentChargedBack = "payment_charged_back";
        public static readonly string PaymentFailed = "payment_failed";
        public static readonly string PaymentPaidOut = "payment_paid_out";
        public static readonly string PaymentRefunded = "payment_refunded";
        public static readonly string Refund = "refund";
        public static readonly string RevenueShare = "revenue_share";
    }
}