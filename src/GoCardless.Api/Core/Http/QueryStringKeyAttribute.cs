using System;

namespace GoCardless.Api.Core.Http
{
    public class QueryStringKeyAttribute : Attribute
    {
        public QueryStringKeyAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}