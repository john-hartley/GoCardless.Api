using Newtonsoft.Json;

namespace GoCardless.Api.MandateImports
{
    public class SubmitMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}