﻿using GoCardlessApi.Http;
using GoCardlessApi.Http.Serialisation;
using System;

namespace GoCardlessApi.Subscriptions
{
    public class GetSubscriptionsOptions : IPageOptions, ICloneable
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

        public string Customer { get; set; }
        public string Mandate { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.SubscriptionStatus"/> for possible values.
        /// </summary>
        public string Status { get; set; }
    }
}