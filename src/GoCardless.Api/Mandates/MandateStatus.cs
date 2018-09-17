namespace GoCardless.Api.Mandates
{
    public static class MandateStatus
    {
        public static readonly string Active = "active";
        public static readonly string Cancelled = "cancelled";
        public static readonly string Expired = "expired";
        public static readonly string Failed = "failed";
        public static readonly string PendingCustomerApproval = "pending_customer_approval";
        public static readonly string PendingSubmission = "pending_submission";
        public static readonly string Submitted = "submitted";
    }
}