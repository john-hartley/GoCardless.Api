using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class CreateRefundRequest
    {
        public int Amount { get; set; }

        public CreateRefundLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string Reference { get; set; }

        public int TotalAmountConfirmation { get; set; }
    }
}