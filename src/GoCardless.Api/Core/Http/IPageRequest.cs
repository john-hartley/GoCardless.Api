namespace GoCardless.Api.Core.Http
{
    public interface IPageRequest
    {
        string After { get; set; }
        string Before { get; set; }
        int? Limit { get; set; }
    }
}