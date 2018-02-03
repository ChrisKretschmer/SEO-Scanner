using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SEO.Model
{
    public interface IAnalyzableElement
    {
        WebResponse GetPageRequestObject();
        string GetPageContent();
        HtmlDocument GetHtmlDocument();

        void AddHint(IHint hint);
    }
}
