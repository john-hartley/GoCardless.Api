using Newtonsoft.Json;

namespace GoCardless.Api.MandateImports
{
    public class CreateMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}