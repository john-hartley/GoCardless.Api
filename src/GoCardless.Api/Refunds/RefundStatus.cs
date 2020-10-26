namespace GoCardlessApi.Refunds
{
    public static class RefundStatus
    {
        public static readonly string Bounced = "bounced";
        public static readonly string Cancelled = "cancelled";
        public static readonly string Created = "created";
        public static readonly string FundsReturned = "funds_returned";
        public static readonly string Paid = "paid";
        public static readonly string PendingSubmission = "pending_submission";
        public static readonly string Submitted = "submitted";
    }
}