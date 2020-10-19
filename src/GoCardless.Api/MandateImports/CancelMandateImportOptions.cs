using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class CancelMandateImportOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}