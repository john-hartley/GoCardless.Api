using GoCardless.Api.Core;

namespace GoCardless.Api.PayoutItems
{
    public class PayoutItemsRequest : IPageRequest
    {
        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }
        public string Payout { get; set; }
    }
}