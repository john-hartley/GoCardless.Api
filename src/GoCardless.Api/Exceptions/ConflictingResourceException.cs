using System;
using System.Linq;

namespace GoCardlessApi.Exceptions
{
    public class ConflictingResourceException : ApiException
    {
        public ConflictingResourceException() { }
        public ConflictingResourceException(string message) : base(message) { }
        public ConflictingResourceException(string message, Exception innerException) : base(message, innerException) { }
        public ConflictingResourceException(string message, ApiError apiError) : base(message, apiError) { }

        public string ResourceId
        {
            get
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
}