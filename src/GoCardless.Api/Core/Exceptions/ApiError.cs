using System.Collections.Generic;

namespace GoCardless.Api.Core.Exceptions
{
    public class ApiError
    {
        public int Code { get; set; }
        public string DocumentationUrl { get; set; }
        public IReadOnlyList<Error> Errors { get; set; }
        public string Message { get; set; }
        public string RawResponse { get; set; }
        public string RequestId { get; set; }
        public string Type { get; set; }
    }
}