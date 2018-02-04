using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Model
{
    class WebsiteHint : IHint
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public Severity Severity { get; set; }
        public IAnalyzablePage[] Pages { get; set; }
        public string AdditionalInfo { get; set; }

        public WebsiteHint(string code, string message, IAnalyzablePage page, Severity severity = Severity.Major, string additionalInfo = "")
        {
            Message = message;
            Severity = severity;
            Code = code;
            Pages = new[] {page};
            AdditionalInfo = additionalInfo;
        }

        public WebsiteHint(string code, string message, IAnalyzablePage[] pages, Severity severity = Severity.Major, string additionalInfo = "")
        {
            Message = message;
            Severity = severity;
            Code = code;
            Pages = pages;
            AdditionalInfo = additionalInfo;
        }
    }
}
