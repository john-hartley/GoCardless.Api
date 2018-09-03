using Newtonsoft.Json;

namespace GoCardlessApi.MandatePdfs
{
    public class CreateMandatePdfRequest
    {
        [JsonIgnore]
        public string Language { get; set; }

        public MandatePdfLinks Links { get; set; } 
    }
}