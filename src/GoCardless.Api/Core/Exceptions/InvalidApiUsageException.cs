using System;

namespace GoCardless.Api.Core.Exceptions
{
    public class InvalidApiUsageException : ApiException
    {
        public InvalidApiUsageException() { }
        public InvalidApiUsageException(string message) : base(message) { }
        public InvalidApiUsageException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidApiUsageException(string message, ApiError apiError) : base(message, apiError) { }
    }
}