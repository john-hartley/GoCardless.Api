using System;
using System.Linq;

namespace GoCardless.Api.Core.Exceptions
{
    public class ResourceAlreadyExistsException : ApiException
    {
        public ResourceAlreadyExistsException() { }
        public ResourceAlreadyExistsException(string message) : base(message) { }
        public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        public ResourceAlreadyExistsException(string message, ApiError apiError) : base(message, apiError)
        {
            ResourceId = ParseResourceId();
        }

        public string ResourceId { get; }

        private string ParseResourceId()
        {
            var bankAccountExists = Errors.SingleOrDefault(x => x.Reason == "bank_account_exists");
            if (bankAccountExists != null)
            {
                if (bankAccountExists.Links.ContainsKey("creditor_bank_account"))
                {
                    return bankAccountExists.Links["creditor_bank_account"];
                }
                if (bankAccountExists.Links.ContainsKey("customer_bank_account"))
                {
                    return bankAccountExists.Links["customer_bank_account"];
                }
            }

            return Errors
                .SingleOrDefault(x => x.Reason == "idempotent_creation_conflict")
                ?.Links
                ?.SingleOrDefault(x => x.Key == "conflicting_resource_id").Value;
        }
    }
}