using System.Collections.Generic;

namespace GoCardlessApi.InstalmentSchedules
{
    public class InstalmentSchedule
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> PaymentErrors { get; set; }
        public string Status { get; set; }
        public int TotalAmount { get; set; }
    }
}