namespace GoCardlessApi.PayoutItems
{
    public class PayoutItem
    {
        public string Amount { get; set; }
        public PayoutLinks Links { get; set; }

        /// <summary>
        /// See <see cref="PayoutItems.PayoutItemType"/> for possible values.
        /// </summary>
        public string Type { get; set; }
    }
}