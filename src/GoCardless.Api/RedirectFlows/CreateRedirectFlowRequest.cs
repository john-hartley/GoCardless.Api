namespace GoCardless.Api.RedirectFlows
{
    public class CreateRedirectFlowRequest
    {
        public string Description { get; set; }
        public CreateRedirectFlowLinks Links { get; set; }
        public PrefilledCustomer PrefilledCustomer { get; set; }

        /// <summary>
        /// See <see cref="Models.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }

        public string SessionToken { get; set; }
        public string SuccessRedirectUrl { get; set; }
    }
}