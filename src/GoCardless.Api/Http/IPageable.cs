namespace GoCardless.Api.Http
{
    public interface IPageable<TOptions, TResponse>
    {
        IPager<TOptions, TResponse> PageFrom(TOptions options);
    }
}