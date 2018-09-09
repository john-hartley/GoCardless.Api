namespace GoCardless.Api.Core
{
    public interface IPageRequest
    {
        string After { get; }
        string Before { get; }
        int? Limit { get; }
    }
}