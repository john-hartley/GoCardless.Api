﻿using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class RefundLinks
    {
        public string Mandate { get; set; }
        
        public string Payment { get; set; }
    }
}