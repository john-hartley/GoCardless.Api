using System;

namespace GoCardless.Api.Core.Exceptions
{
    public class GoCardlessException : ApiException
    {
        public GoCardlessException() { }
        public GoCardlessException(string message) : base(message) { }
        public GoCardlessException(string message, Exception innerException) : base(message, innerException) { }
        public GoCardlessException(string message, ApiError apiError) : base(message, apiError) { }
        public GoCardlessException(string message, Exception innerException, ApiError apiError) : base(message, innerException, apiError) { }
    }
}