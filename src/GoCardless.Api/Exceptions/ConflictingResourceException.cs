using System;

namespace GoCardlessApi.Exceptions
{
    public class ConflictingResourceException : ApiException
    {
        public ConflictingResourceException() { }
        public ConflictingResourceException(string message) : base(message) { }
        public ConflictingResourceException(string message, Exception innerException) : base(message, innerException) { }
        public ConflictingResourceException(string message, ApiError apiError) : base(message, apiError) { }
    }
}