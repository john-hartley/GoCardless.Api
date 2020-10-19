using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class SubmitMandateImportOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}