using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class UpdateCustomerBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}