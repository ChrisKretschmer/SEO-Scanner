using System;
using EventBus;
using SEO.DomainEvents;
using SEO.Model;

namespace SEO.PageValidators
{
    class TagFoundEmitter : IPageValidator
    {
        public void Validate(IAnalyzablePage page, SimpleEventBus eventBus)
        {
            var content = page.GetHtmlDocument();

            var tags = content.DocumentNode.SelectNodes("//*");

            foreach (var htmlNode in tags)
            {
                eventBus.Post(new TagFound(htmlNode, page), TimeSpan.Zero);
            }
        }
    }
}
