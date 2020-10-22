﻿namespace GoCardlessApi.Events
{
    public static class Causes
    {
        public static readonly string AuthorisationDisputed = "authorisation_disputed";
        public static readonly string BankAccountClosed = "bank_account_closed";
        public static readonly string BankAccountTransferred = "bank_account_transferred";
        public static readonly string ChargebackSettled = "chargeback_settled";
        public static readonly string CustomerApprovalDenied = "customer_approval_denied";
        public static readonly string CustomerApprovalGranted = "customer_approval_granted";
        public static readonly string CustomerApprovalSkipped = "customer_approval_skipped";
        public static readonly string DirectDebitNotEnabled = "direct_debit_not_enabled";
        public static readonly string InstalmentScheduleCancelled = "instalment_schedule_cancelled";
        public static readonly string InstalmentScheduleCreated = "instalment_schedule_created";
        public static readonly string InsufficientFunds = "insufficient_funds";
        public static readonly string InvalidBankDetails = "invalid_bank_details";
        public static readonly string LateFailureSettled = "late_failure_settled";
        public static readonly string MandateActivated = "mandate_activated";
        public static readonly string MandateCancelled = "mandate_cancelled";
        public static readonly string MandateCreated = "mandate_created";
        public static readonly string MandateExpired = "mandate_expired";
        public static readonly string MandateReinstated = "mandate_reinstated";
        public static readonly string MandateSubmitted = "mandate_submitted";
        public static readonly string Other = "other";
        public static readonly string PaymentAutoretried = "payment_autoretried";
        public static readonly string PaymentConfirmed = "payment_confirmed";
        public static readonly string PaymentCreated = "payment_created";
        public static readonly string PaymentPaidOut = "payment_paid_out";
        public static readonly string PaymentRetried = "payment_retried";
        public static readonly string PaymentSubmitted = "payment_submitted";
        public static readonly string PayoutFxRateConfirmed = "payout_fx_rate_confirmed";
        public static readonly string PayoutTaxExchangeRatesConfirmed = "payout_tax_exchange_rates_confirmed";
        public static readonly string PayoutPaid = "payout_paid";
        public static readonly string ReferToPayer = "refer_to_payer";
        public static readonly string RefundRequested = "refund_requested";
        public static readonly string ResubmissionRequested = "resubmission_requested";
        public static readonly string SchemeIdentifierChanged = "scheme_identifier_changed";
        public static readonly string TestFailure = "test_failure";
    }
}