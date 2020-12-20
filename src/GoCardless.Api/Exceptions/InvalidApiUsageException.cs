namespace GoCardlessApi.Exceptions
{
    public class InvalidApiUsageException : ApiException
    {
        public InvalidApiUsageException(ApiError apiError) : base(apiError) { }
    }
}