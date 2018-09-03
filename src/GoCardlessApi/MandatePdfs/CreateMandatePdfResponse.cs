using Newtonsoft.Json;

namespace GoCardlessApi.MandatePdfs
{
    public class CreateMandatePdfResponse
    {
        [JsonProperty("mandate_pdfs")]
        public MandatePdf MandatePdf { get; set; }
    }
}