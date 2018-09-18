using Newtonsoft.Json.Converters;
using System;

namespace GoCardless.Api.Core.Serialisation
{
    public class IsoDateJsonConverter : IsoDateTimeConverter
    {
        public IsoDateJsonConverter(string dateFormat)
        {
            if (string.IsNullOrWhiteSpace(dateFormat))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(dateFormat));
            }

            DateTimeFormat = dateFormat;
        }
    }
}