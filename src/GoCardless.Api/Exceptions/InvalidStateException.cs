using System;

namespace GoCardlessApi.Exceptions
{
    public class InvalidStateException : ApiException
    {
        public InvalidStateException() { }
        public InvalidStateException(string message) : base(message) { }
        public InvalidStateException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidStateException(string message, ApiError apiError) : base(message, apiError) { }
    }
}