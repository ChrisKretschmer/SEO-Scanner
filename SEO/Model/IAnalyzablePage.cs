using System;
using System.Net;
using HtmlAgilityPack;

namespace SEO.Model
{
    public interface IAnalyzablePage
    {
        WebResponse GetPageRequestObject();
        string GetPageContent();
        HtmlDocument GetHtmlDocument();

        Uri Uri { get; }

        void AddHint(IHint hint);
    }
}
