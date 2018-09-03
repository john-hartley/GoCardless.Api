using System;

namespace GoCardlessApi.MandatePdfs
{
    public class MandatePdf
    {
        public string Url { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}