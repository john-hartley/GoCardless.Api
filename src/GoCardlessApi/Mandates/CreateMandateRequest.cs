using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class CreateMandateRequest
    {
        public CreateMandateLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string Reference { get; set; }

        public string Scheme { get; set; }
    }
}