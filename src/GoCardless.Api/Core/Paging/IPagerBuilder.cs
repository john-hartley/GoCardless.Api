namespace GoCardless.Api.Core.Paging
{
    public interface IPagerBuilder<TRequest, TResource>
    {
        IPager<TRequest, TResource> StartFrom(TRequest initialRequest);
    }
}