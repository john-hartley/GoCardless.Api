namespace GoCardless.Api.PayoutItems
{
    public class PayoutItem
    {
        public string Amount { get; set; }
        public PayoutLinks Links { get; set; }
        public string Type { get; set; }
    }
}