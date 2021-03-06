﻿using System.Collections.Generic;

namespace GoCardlessApi.Exceptions
{
    public class Error
    {
        public string Field { get; set; }
        public IReadOnlyDictionary<string, string> Links { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public string RequestPointer { get; set; }
    }
}