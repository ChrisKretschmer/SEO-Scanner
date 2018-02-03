using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SEO.Model
{
    internal class Page : IAnalyzableElement
    {
        private Website website { get; set; }
        private string url;

        private WebResponse cachedResponse;
        private HtmlDocument cachedHtmlDocument;

        internal List<IHint> FoundHints = new List<IHint>();

        internal Page(Website website, string url)
        {
            this.website = website;
            this.url = url;
        }

        ~Page()
        {
            cachedResponse.Dispose();
        }

        public List<IHint> GetHints()
        {
            foreach (IValidator validator in website.Validators)
            {
                // the Validate Method injects the found hints directly into the Page object
                validator.Validate(this);
            }
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

        public string GetPageContent()
        {
            Stream stream = GetPageRequestObject().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            reader.Dispose();

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

    }
}
