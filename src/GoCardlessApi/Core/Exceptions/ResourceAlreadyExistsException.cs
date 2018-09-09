using System;
using System.Linq;

namespace GoCardless.Api.Core.Exceptions
{
    public class ResourceAlreadyExistsException : ApiException
    {
        public ResourceAlreadyExistsException() { }
        public ResourceAlreadyExistsException(string message) : base(message) { }
        public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        public ResourceAlreadyExistsException(string message, ApiError apiError) : base(message, apiError)
        {
            ResourceId = Errors
                .SingleOrDefault(x => x.Reason == "idempotent_creation_conflict")
                ?.Links
                ?.SingleOrDefault(x => x.Key == "conflicting_resource_id").Value;
        }

        public string ResourceId { get; }
    }
}