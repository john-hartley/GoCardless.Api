using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class AllMandatesResponse
    {
        public IEnumerable<Mandate> Mandates { get; set; }

        public Meta Meta { get; set; }
    }
}