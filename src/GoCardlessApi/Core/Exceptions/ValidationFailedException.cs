using System;

namespace GoCardless.Api.Core.Exceptions
{
    public class ValidationFailedException : ApiException
    {
        public ValidationFailedException() { }
        public ValidationFailedException(string message) : base(message) { }
        public ValidationFailedException(string message, Exception innerException) : base(message, innerException) { }
        public ValidationFailedException(string message, ApiError apiError) : base(message, apiError) { }
    }
}