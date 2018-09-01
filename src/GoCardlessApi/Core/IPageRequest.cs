namespace GoCardlessApi.Core
{
    public interface IPageRequest
    {
        string After { get; }
        string Before { get; }
        int? Limit { get; }
    }
}