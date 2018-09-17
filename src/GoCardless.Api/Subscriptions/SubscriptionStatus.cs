namespace GoCardless.Api.Subscriptions
{
    public static class SubscriptionStatus
    {
        public static readonly string Active = "active";
        public static readonly string Cancelled = "cancelled";
        public static readonly string CustomerApprovalDenied = "customer_approval_denied";
        public static readonly string Finished = "finished";
        public static readonly string PendingCustomerApproval = "pending_customer_approval";
    }
}