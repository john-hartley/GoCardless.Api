﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class UpdateCustomerBankAccountOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}