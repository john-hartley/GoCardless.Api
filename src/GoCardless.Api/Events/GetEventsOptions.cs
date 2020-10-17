using GoCardlessApi.Http;
using GoCardlessApi.Http.Serialisation;
using System;

namespace GoCardlessApi.Events
{
    public class GetEventsOptions : IPageOptions, ICloneable
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

        /// <summary>
        /// See <see cref="Events.Actions"/> for possible values.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// See <see cref="Events.Include"/> for possible values.
        /// </summary>
        public string Include { get; set; }

        public string Mandate { get; set; }

        [QueryStringKey("parent_event")]
        public string ParentEvent { get; set; }

        public string Payment { get; set; }
        public string Payout { get; set; }
        public string Refund { get; set; }

        /// <summary>
        /// See <see cref="Events.ResourceType"/> for possible values.
        /// </summary>
        [QueryStringKey("resource_type")]
        public string ResourceType { get; set; }

        public string Subscription { get; set; }
    }
}