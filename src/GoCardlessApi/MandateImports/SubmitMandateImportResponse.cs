using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class SubmitMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}