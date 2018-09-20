namespace GoCardless.Api.Core.Http
{
    public interface IPagerBuilder<TRequest, TResource>
    {
        IPager<TRequest, TResource> StartFrom(TRequest initialRequest);
    }
}