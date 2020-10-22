namespace GoCardlessApi.Events
{
    public static class Actions
    {
        public static class InstalmentSchedule
        {
            public static readonly string Cancelled = "cancelled";
            public static readonly string Completed = "completed";
            public static readonly string Created = "created";
            public static readonly string CreationFailed = "creation_failed";
            public static readonly string Errored = "errored";
            public static readonly string Resumed = "resumed";
        }

        public static class Mandate
        {
            public static readonly string Active = "active";
            public static readonly string Cancelled = "cancelled";
            public static readonly string Created = "created";
            public static readonly string CustomerApprovalGranted = "customer_approval_granted";
            public static readonly string CustomerApprovalSkipped = "customer_approval_skipped";
            public static readonly string Expired = "expired";
            public static readonly string Failed = "failed";
            public static readonly string Reinstated = "reinstated";
            public static readonly string Replaced = "replaced";
            public static readonly string ResubmissionRequested = "resubmission_requested";
            public static readonly string Submitted = "submitted";
            public static readonly string Transferred = "transferred";
        }

        public static class Payment
        {
            public static readonly string Cancelled = "cancelled";
            public static readonly string ChargedBack = "charged_back";
            public static readonly string ChargedBackCancelled = "chargeback_cancelled";
            public static readonly string ChargedBackSettled = "chargeback_settled";
            public static readonly string Confirmed = "confirmed";
            public static readonly string Created = "created";
            public static readonly string CustomerApprovalDenied = "customer_approval_denied";
            public static readonly string CustomerApprovalGranted = "customer_approval_granted";
            public static readonly string Failed = "failed";
            public static readonly string LateFailureSettled = "late_failure_settled";
            public static readonly string PaidOut = "paid_out";
            public static readonly string ResubmissionRequested = "resubmission_requested";
            public static readonly string Submitted = "submitted";
        }

        public static class Payout
        {
            public static readonly string FxRateConfirmed = "fx_rate_confirmed";
            public static readonly string Paid = "paid";
            public static readonly string TaxExchangeRatesConfirmed = "tax_exchange_rates_confirmed";
        }

        public static class Refund
        {
            public static readonly string Created = "created";
            public static readonly string Failed = "failed";
            public static readonly string FundsReturned = "funds_returned";
            public static readonly string Paid = "paid";
            public static readonly string Settled = "refund_settled";
        }

        public static class Subscription
        {
            public static readonly string Amended = "amended";
            public static readonly string Cancelled = "cancelled";
            public static readonly string Created = "created";
            public static readonly string CustomerApprovalDenied = "customer_approval_denied";
            public static readonly string CustomerApprovalGranted = "customer_approval_granted";
            public static readonly string Finished = "finished";
            public static readonly string Paused = "paused";
            public static readonly string PaymentCreated = "payment_created";
            public static readonly string Resumed = "resumed";
        }
    }
}