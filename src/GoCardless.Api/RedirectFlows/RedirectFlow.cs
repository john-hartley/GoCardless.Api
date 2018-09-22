using System;

namespace GoCardless.Api.RedirectFlows
{
    public class RedirectFlow
    {
        public string Id { get; set; }
        public string ConfirmationUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Description { get; set; }
        public RedirectFlowLinks Links { get; set; }
        public string RedirectUrl { get; set; }

        /// <summary>
        /// See <see cref="Models.Scheme"/> for possible values.
        /// </summary>
        public string Scheme { get; set; }

        public string SessionToken { get; set; }
        public string SuccessRedirectUrl { get; set; }
    }
}