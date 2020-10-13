﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Mandates
{
    public class CreateMandateRequest
    {
        public CreateMandateRequest()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreateMandateLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }

        /// <summary>
        /// See <see cref="Models.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }
    }
}