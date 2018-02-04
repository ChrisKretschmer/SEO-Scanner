using System;

namespace SEO.DomainEvents
{
    public class PageFound : SimpleEvent
    {
        public Uri Url { get; set; }

        public PageFound(Uri url)
        {
            Url = url;
        }
    }
}
