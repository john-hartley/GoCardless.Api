using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoCardless.Api.Core.Http
{
    public static class PageRequestExtensions
    {
        public static IReadOnlyDictionary<string, object> ToReadOnlyDictionary(this IPageRequest request)
        {
            return request
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.GetValue(request) != null)
                .ToDictionary(x => {
                    var attr = x.GetCustomAttribute<QueryStringKeyAttribute>();
                    return attr?.Key ?? x.Name.ToLowerInvariant();
                }, x => x.GetValue(request));
        }
    }
}