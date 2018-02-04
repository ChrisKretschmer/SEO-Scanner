using HtmlAgilityPack;
using SEO.Model;

namespace SEO.DomainEvents
{
    public class TagFound : SimpleEvent
    {
        public HtmlNode Node { get; set; }
        public string TagName { get; set; }
        public IAnalyzablePage Page { get; set; }

        public TagFound(HtmlNode node, IAnalyzablePage page)
        {
            Node = node;
            TagName = node.Name;
            Page = page;
        }
    }
}
