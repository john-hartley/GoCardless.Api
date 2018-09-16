namespace GoCardless.Api.Core.Paging
{
    public interface IPageRequest
    {
        string After { get; set; }
        string Before { get; set; }
        int? Limit { get; set; }
    }
}