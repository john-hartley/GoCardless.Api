﻿using GoCardlessApi.Core;
using System;

namespace GoCardlessApi.Payouts
{
    public class AllPayoutsRequest : IPageRequest
    {
        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }

        [QueryStringKey("created_at[gt]")]
        public DateTimeOffset? CreatedGreaterThan { get; set; }

        [QueryStringKey("created_at[gte]")]
        public DateTimeOffset? CreatedGreaterThanOrEqual { get; set; }

        [QueryStringKey("created_at[lt]")]
        public DateTimeOffset? CreatedLessThan { get; set; }

        [QueryStringKey("created_at[lte]")]
        public DateTimeOffset? CreatedLessThanOrEqual { get; set; }

        public string Creditor { get; set; }

        [QueryStringKey("creditor_bank_account")]
        public string CreditorBankAccount { get; set; }

        public string Currency { get; set; }

        [QueryStringKey("payout_type")]
        public string PayoutType { get; set; }

        public string Reference { get; set; }
        public string Status { get; set; }
    }
}