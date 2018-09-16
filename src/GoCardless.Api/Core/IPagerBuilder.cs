namespace GoCardless.Api.Core
{
    public interface IPagerBuilder<TRequest, TResource>
    {
        IPager<TRequest, TResource> StartFrom(TRequest initialRequest);
    }
}