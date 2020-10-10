using GoCardless.Api.Core.Http;
using System;

namespace GoCardless.Api.Mandates
{
    public class GetMandatesOptions : IPageOptions, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

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
        public string Customer { get; set; }

        [QueryStringKey("customer_bank_account")]
        public string CustomerBankAccount { get; set; }

        public string Reference { get; set; }

        /// <summary>
        /// See <see cref="Mandates.MandateStatus"/> for possible values.
        /// </summary>
        public string Status { get; set; }
    }
}