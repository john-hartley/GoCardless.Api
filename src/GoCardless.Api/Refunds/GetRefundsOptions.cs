using GoCardlessApi.Http;
using GoCardlessApi.Http.Serialisation;
using System;

namespace GoCardlessApi.Refunds
{
    public class GetRefundsOptions : IPageOptions, ICloneable
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

        public string Mandate { get; set; }
        public string Payment { get; set; }

        /// <summary>
        /// See <see cref="Refunds.RefundType"/> for possible values.
        /// </summary>
        [QueryStringKey("refund_type")]
        public string RefundType { get; set; }
    }
}