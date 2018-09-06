using GoCardlessApi.Core;

namespace GoCardlessApi.PayoutItems
{
    public class PayoutItemsRequest : IPageRequest
    {
        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }
        public string Payout { get; set; }
    }
}