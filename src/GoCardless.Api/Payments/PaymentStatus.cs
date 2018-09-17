namespace GoCardless.Api.Payments
{
    public static class PaymentStatus
    {
        public static readonly string Cancelled = "cancelled";
        public static readonly string ChargedBack = "charged_back";
        public static readonly string Confirmed = "confirmed";
        public static readonly string CustomerApprovalDenied = "customer_approval_denied";
        public static readonly string Failed = "failed";
        public static readonly string PaidOut = "paid_out";
        public static readonly string PendingCustomerApproval = "pending_customer_approval";
        public static readonly string PendingSubmission = "pending_submission";
        public static readonly string Submitted = "submitted";
    }
}