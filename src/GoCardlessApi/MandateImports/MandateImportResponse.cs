using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class MandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}