namespace GoCardlessApi.Exceptions
{
    public class ValidationFailedException : ApiException
    {
        public ValidationFailedException(ApiError apiError) : base(apiError) { }
    }
}