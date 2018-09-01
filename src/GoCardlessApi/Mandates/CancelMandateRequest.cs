﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class CancelMandateRequest
    {
        public CancelMandateRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}