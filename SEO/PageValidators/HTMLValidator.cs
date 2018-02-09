using System;
using System.Net;
using EventBus;
using SEO.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SEO.PageValidators
{
    class HTMLValidator : IPageValidator
    {
        const string nuv_link = "http://94.130.181.101:8080/?out=json&doc=";

        public void Validate(IAnalyzablePage page, SimpleEventBus eventBus)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                var json = wc.DownloadString(nuv_link + page.Uri);
                var jsonObject = JObject.Parse(json);
                if (jsonObject["messages"] != null)
                {
                    foreach (var message in jsonObject["messages"])
                    {
                        var hint = new Hint("HTML-Hint", message["message"].ToString());
                        if(message["type"] != null) {
                            switch (message["type"].ToString())
                            {
                                case "error":
                                    hint.Severity = Severity.HtmlError;
                                    break;
                                case "warning":
                                    hint.Severity = Severity.HtmlWarning;
                                    break;
                                default:
                                    hint.Severity = Severity.HtmlHint;
                                    break;
                            }
                        }

                        if (message["lastLine"] != null)
                        {
                            hint.Line = Int32.Parse(message["lastLine"].ToString());
                        }
                        if (message["firstColumn"] != null)
                        {
                            hint.Line = Int32.Parse(message["firstColumn"].ToString());
                        }
                        page.AddHint(hint);
                    }
                }
            }
        }
    }
}
