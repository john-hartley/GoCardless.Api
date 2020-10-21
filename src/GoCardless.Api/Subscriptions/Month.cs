using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoCardlessApi.Subscriptions
{
    public static class Month
    {
        private static IReadOnlyDictionary<int, string> _monthNameLookup;

        static Month()
        {
            var months = new Dictionary<int, string>
            {
                { 1, January },
                { 2, February },
                { 3, March },
                { 4, April },
                { 5, May },
                { 6, June },
                { 7, July },
                { 8, August },
                { 9, September },
                { 10, October },
                { 11, November },
                { 12, December },
            };

            _monthNameLookup = new ReadOnlyDictionary<int, string>(months);
        }

        /// <summary>
        /// Returns the name of the month for the <see cref="DateTime"/> instance.
        /// </summary>
        /// <returns>The lowercased name of the month, as that's what the API expects.</returns>
        public static string NameFrom(DateTime dateTime)
        {
            return _monthNameLookup[dateTime.Month];
        }

        public static readonly string January = "january";
        public static readonly string February = "february";
        public static readonly string March = "march";
        public static readonly string April = "april";
        public static readonly string May = "may";
        public static readonly string June = "june";
        public static readonly string July = "july";
        public static readonly string August = "august";
        public static readonly string September = "september";
        public static readonly string October = "october";
        public static readonly string November = "november";
        public static readonly string December = "december";
    }
}