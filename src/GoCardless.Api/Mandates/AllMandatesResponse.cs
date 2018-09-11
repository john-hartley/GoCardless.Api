using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Mandates
{
    public class AllMandatesResponse
    {
        public IEnumerable<Mandate> Mandates { get; set; }
        public Meta Meta { get; set; }
    }
}