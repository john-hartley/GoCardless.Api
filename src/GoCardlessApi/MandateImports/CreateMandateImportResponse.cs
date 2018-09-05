using Newtonsoft.Json;

namespace GoCardlessApi.MandateImports
{
    public class CreateMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}