using System;
using System.Collections.Generic;
using System.Linq;

namespace GoCardlessApi.Exceptions
{
    public class ApiException : Exception
    {
        private const string DefaultMessage = "An unknown API error occurred. Please see the RawResponse property for more information.";

        public ApiException() : base(DefaultMessage) { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        public ApiException(ApiError apiError) : this(MessageFrom(apiError), null, apiError) { }

        public ApiException(string message, Exception innerException, ApiError apiError) 
            : base(message, innerException)
        {
            Code = apiError.Code;
            DocumentationUrl = apiError.DocumentationUrl;
            Errors = apiError.Errors;
            RawResponse = apiError.RawResponse;
            RequestId = apiError.RequestId;
        }

        public int Code { get; }
        public string DocumentationUrl { get; }
        public IReadOnlyList<Error> Errors { get; }
        public string RawResponse { get; }
        public string RequestId { get; }

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

        private static string MessageFrom(ApiError apiError)
        {
            var error = apiError.Errors.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Message));
            if (error == null)
            {
                return !string.IsNullOrWhiteSpace(apiError.Message)
                    ? apiError.Message
                    : DefaultMessage;
            }

            if (string.IsNullOrWhiteSpace(error.Field))
            {
                return error.Message;
            }

            return $"{error.Field}: {error.Message}";
        }
    }
}