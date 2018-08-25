using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class DisableCreditorBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}