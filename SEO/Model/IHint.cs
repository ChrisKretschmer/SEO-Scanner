namespace SEO.Model
{
    public enum Severity
    {
        Minor,
        Major,
        Critical,

        HtmlError,
        HtmlWarning,
        HtmlHint
    }

    public interface IHint
    {
        string Message { get; set; }
        string AdditionalInfo { get; set; }

        string Code { get; set; }

        Severity Severity { get; set; }

    }
}
