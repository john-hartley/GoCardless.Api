using System;
using System.Collections.Generic;

namespace GoCardlessApi.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base("An unknown API error occurred.") { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        public ApiException(string message, ApiError apiError) : this(message, null, apiError) { }
        public ApiException(string message, Exception innerException, ApiError apiError) : base(message, innerException)
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
    }
}