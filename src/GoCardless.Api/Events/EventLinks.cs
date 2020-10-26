namespace GoCardlessApi.Events
{
    public class EventLinks
    {
        public string Creditor { get; set; }
        public string Customer { get; set; }
        public string CustomerBankAccount { get; set; }
        public string Mandate { get; set; }
        public string NewCustomerBankAccount { get; set; }
        public string NewMandate { get; set; }
        public string Organisation { get; set; }
        public string ParentEvent { get; set; }
        public string Payment { get; set; }
        public string Payout { get; set; }
        public string PreviousCustomerBankAccount { get; set; }
        public string Refund { get; set; }
        public string Subscription { get; set; }
    }
}