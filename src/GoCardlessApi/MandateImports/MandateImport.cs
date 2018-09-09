using System;

namespace GoCardless.Api.MandateImports
{
    public class MandateImport
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Scheme { get; set; }
        public string Status { get; set; }
    }
}