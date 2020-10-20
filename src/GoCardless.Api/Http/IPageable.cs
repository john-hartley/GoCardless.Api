namespace GoCardlessApi.Http
{
    public interface IPageable<TOptions, TResponse>
    {
        IPager<TOptions, TResponse> PageUsing(TOptions options);
    }
}