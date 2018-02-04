using SEO.Model;

namespace SEO.Validators
{
    public class Hint : IHint
    {
        public string Message { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }
        public string Code { get; set; }
        public Severity Severity { get; set; }

        public Hint(string code, string message) : this(code, message, Severity.Minor) { }

        public Hint(string code, string message, Severity severity) : this(code, message, severity, 0, 0) { }

        public Hint(string code, string message, Severity severity, int line, int position)
        {
            this.Message = message;
            this.Line = line;
            this.Position = position;
            this.Severity = severity;
            this.Code = code;
        }
    }
}
