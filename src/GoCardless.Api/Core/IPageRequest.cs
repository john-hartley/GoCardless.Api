namespace GoCardless.Api.Core
{
    public interface IPageRequest
    {
        string After { get; set; }
        string Before { get; set; }
        int? Limit { get; set; }
    }
}