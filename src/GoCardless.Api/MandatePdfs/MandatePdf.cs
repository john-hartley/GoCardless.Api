using System;

namespace GoCardless.Api.MandatePdfs
{
    public class MandatePdf
    {
        public DateTimeOffset ExpiresAt { get; set; }
        public string Url { get; set; }
    }
}