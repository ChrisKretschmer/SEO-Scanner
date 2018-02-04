using EventBus;
using SEO.DomainEvents;
using SEO.Model;
using System;

namespace SEO
{
    public class LinkAnalyzer : IPageValidator
    {
        public void Validate(IAnalyzableElement page, SimpleEventBus eventBus)
        {
            var content = page.GetHtmlDocument();

            var links = content.DocumentNode.SelectNodes("//a");

            foreach (var link in links)
            {
                var url = link.GetAttributeValue("href", null);

                if (url != null) {
                    Uri uri = IsAbsoluteUrl(url) ? new Uri(url) : new Uri(page.Uri, url);
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