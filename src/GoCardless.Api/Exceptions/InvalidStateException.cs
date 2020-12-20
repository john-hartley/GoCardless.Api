namespace GoCardlessApi.Exceptions
{
    public class InvalidStateException : ApiException
    {
        public InvalidStateException(ApiError apiError) : base(apiError) { }
    }
}