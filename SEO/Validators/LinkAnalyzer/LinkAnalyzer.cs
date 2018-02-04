using EventBus;
using SEO.DomainEvents;
using SEO.Model;
using System;

namespace SEO.Validators.LinkAnalyzer
{
    public class LinkAnalyzer : IValidator
    {
        public void Validate(IAnalyzableElement page, SimpleEventBus eventBus)
        {
            var content = page.GetHtmlDocument();

            var links = content.DocumentNode.SelectNodes("//a");

            foreach (var link in links)
            {
                var url = link.GetAttributeValue("href", null);

                if (url != null) {
                    Uri uri;
                    if (IsAbsoluteUrl(url))
                    {
                        uri = new Uri(url);
                    }
                    else
                    {
                        uri = new Uri(page.url, url);
                    }
                    eventBus.Post(new PageFound(uri), TimeSpan.Zero);
                }
            }
        }

        private bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

    }
}