namespace GoCardlessApi.Exceptions
{
    public class ConflictingResourceException : ApiException
    {
        public ConflictingResourceException(ApiError apiError) : base(apiError) { }
    }
}