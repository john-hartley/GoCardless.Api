using System.Collections.Generic;

namespace GoCardlessApi.Tests.Integration.TestHelpers
{
    public static class Metadata
    {
        public static IDictionary<string, string> Initial = new Dictionary<string, string>
        {
            ["Key1"] = "Value1",
            ["Key2"] = "Value2",
            ["Key3"] = "Value3",
        };

        public static IDictionary<string, string> Updated = new Dictionary<string, string>
        {
            ["Key4"] = "Value4",
            ["Key5"] = "Value5",
            ["Key6"] = "Value6",
        };
    }
}
