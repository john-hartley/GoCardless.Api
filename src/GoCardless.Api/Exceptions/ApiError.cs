using System.Collections.Generic;

namespace GoCardlessApi.Exceptions
{
    public class ApiError
    {
        public ApiError()
        {
            Errors = new List<Error>();
        }

        public int Code { get; set; }
        public string DocumentationUrl { get; set; }
        public IReadOnlyList<Error> Errors { get; set; }
        public string Message { get; set; }
        public string RawResponse { get; set; }
        public string RequestId { get; set; }
        public string Type { get; set; }
    }
}