namespace GoCardlessApi.Events
{
    public class Details
    {
        public string BankAccountId { get; set; }
        public string Cause { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Property { get; set; }
        public string ReasonCode { get; set; }

        /// <summary>
        /// See <see cref="Common.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }
    }
}