namespace GoCardless.Api.Core.Http
{
    public interface IPagerBuilder<TOptions, TResource>
    {
        IPager<TOptions, TResource> StartFrom(TOptions options);
    }
}