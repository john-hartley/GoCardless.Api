using GoCardless.Api.Http.Serialisation;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoCardless.Api.Http
{
    public static class PageOptionsExtensions
    {
        public static IReadOnlyDictionary<string, object> ToReadOnlyDictionary(this IPageOptions options)
        {
            return options
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.GetValue(options) != null)
                .ToDictionary(x => 
                {
                    var attr = x.GetCustomAttribute<QueryStringKeyAttribute>();
                    return attr?.Key ?? x.Name.ToLowerInvariant();
                }, 
                x => x.GetValue(options));
        }
    }
}