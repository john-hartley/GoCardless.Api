namespace GoCardlessApi.RedirectFlows
{
    public class CreateRedirectFlowRequest
    {
        public string Description { get; set; }
        public CreateRedirectFlowLinks Links { get; set; }
        public PrefilledCustomer PrefilledCustomer { get; set; }
        public string Scheme { get; set; }
        public string SessionToken { get; set; }
        public string SuccessRedirectUrl { get; set; }
    }
}