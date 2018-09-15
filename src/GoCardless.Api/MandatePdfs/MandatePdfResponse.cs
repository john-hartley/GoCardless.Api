using Newtonsoft.Json;

namespace GoCardless.Api.MandatePdfs
{
    public class MandatePdfResponse
    {
        [JsonProperty("mandate_pdfs")]
        public MandatePdf MandatePdf { get; set; }
    }
}