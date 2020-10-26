namespace GoCardlessApi.PayoutItems
{
    public class PayoutItem
    {
        public string Amount { get; set; }
        public PayoutItemLinks Links { get; set; }

        /// <summary>
        /// See <see cref="PayoutItems.PayoutItemType"/> for possible values.
        /// </summary>
        public string Type { get; set; }
    }
}