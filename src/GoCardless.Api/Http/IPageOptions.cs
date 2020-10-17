namespace GoCardless.Api.Http
{
    public interface IPageOptions
    {
        string After { get; set; }
        string Before { get; set; }
        int? Limit { get; set; }
    }
}