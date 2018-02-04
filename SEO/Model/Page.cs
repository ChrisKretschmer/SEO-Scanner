using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using EventBus;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace SEO.Model
{
    internal class Page : IAnalyzableElement
    {
        private Website website { get; set; }
        public Uri url { get; private set; }

        private WebResponse cachedResponse;
        private HtmlDocument cachedHtmlDocument;
        private SimpleEventBus eventBus = SimpleEventBus.GetDefaultEventBus();

        [JsonProperty()]
        internal List<IHint> FoundHints = new List<IHint>();

        internal Page(Website website, Uri url)
        {
            this.website = website;
            this.url = url;
        }

        public List<IHint> GetHints()
        {
            foreach (IValidator validator in website.Validators)
            {
                // the Validate Method injects the found hints directly into the Page object
                try
                {
                    validator.Validate(this, eventBus);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to process " + url);
                }
            }
            Cleanup();
            return FoundHints;
        }
        
        /// <summary>
        /// Download the page's content
        /// </summary>
        /// <returns>Webresponse containing all the data</returns>
        public WebResponse GetPageRequestObject()
        {
            if (cachedResponse == null)
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponseAsync().Result;
                cachedResponse = response;
            }

            return cachedResponse;
        }

        private void Cleanup()
        {
            if (cachedResponse != null)
            {
                cachedResponse.Close();
                cachedResponse.Dispose();
            }

            cachedResponse = null;
            cachedHtmlDocument = null;

        }

        public string GetPageContent()
        {
            Stream stream = GetPageRequestObject().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            reader.Close();
            stream.Close();

            return content;
        }

        public void AddHint(IHint hint)
        {
            FoundHints.Add(hint);
        }

        public HtmlDocument GetHtmlDocument()
        {
            if(cachedHtmlDocument == null)
            {
                cachedHtmlDocument = new HtmlDocument();
                cachedHtmlDocument.LoadHtml(GetPageContent());
            }
            return cachedHtmlDocument;
        }

        public override bool Equals(Object obj)
        {
            Page pageObj = obj as Page;
            if (pageObj == null)
                return false;
            else
                return url.Equals(pageObj.url);
        }

        public override int GetHashCode()
        {
            return this.url.GetHashCode();
        }
    }
}
