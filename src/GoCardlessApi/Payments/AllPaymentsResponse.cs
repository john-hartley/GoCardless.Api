﻿using System.Collections.Generic;

namespace GoCardlessApi.Payments
{
    public class AllPaymentsResponse
    {
        public IEnumerable<Payment> Payments { get; set; }
    }
}