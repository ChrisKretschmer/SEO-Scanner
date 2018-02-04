using System;

namespace SEO.DomainEvents
{
    public class PageFound : SimpleEvent
    {
        public Uri URL { get; set; }

        public PageFound(Uri url)
        {
            URL = url;
        }
    }
}
