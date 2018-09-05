using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class CancelMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}