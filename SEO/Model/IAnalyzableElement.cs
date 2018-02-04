﻿using System;
using System.Net;
using HtmlAgilityPack;

namespace SEO.Model
{
    public interface IAnalyzableElement
    {
        WebResponse GetPageRequestObject();
        string GetPageContent();
        HtmlDocument GetHtmlDocument();

        Uri url { get; }

        void AddHint(IHint hint);
    }
}