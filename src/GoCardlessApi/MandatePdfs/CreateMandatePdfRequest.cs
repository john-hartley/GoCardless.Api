using Newtonsoft.Json;

namespace GoCardless.Api.MandatePdfs
{
    public class CreateMandatePdfRequest
    {
        [JsonIgnore]
        public string Language { get; set; }

        public MandatePdfLinks Links { get; set; } 
    }
}