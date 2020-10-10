namespace GoCardless.Api.Core.Http
{
    public interface IPageable<TOptions, TResponse>
    {
        IPager<TOptions, TResponse> PageFrom(TOptions options);
    }
}