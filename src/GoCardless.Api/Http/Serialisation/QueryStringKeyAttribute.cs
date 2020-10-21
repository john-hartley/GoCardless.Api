using System;

namespace GoCardlessApi.Http.Serialisation
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