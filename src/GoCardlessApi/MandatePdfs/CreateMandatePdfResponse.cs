using Newtonsoft.Json;

namespace GoCardless.Api.MandatePdfs
{
    public class CreateMandatePdfResponse
    {
        [JsonProperty("mandate_pdfs")]
        public MandatePdf MandatePdf { get; set; }
    }
}