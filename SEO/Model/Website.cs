using EventBus;
using Newtonsoft.Json;
using SEO.DomainEvents;
using System;
using System.Collections.Generic;

namespace SEO.Model
{
    public class Website : IAnalyzableWebsite
    {
        public Uri startURL { get; }
        internal List<IPageValidator> PageValidators { get; }
        internal List<IWebsiteValidator> WebsiteValidators { get; }

        [JsonProperty()]
        internal List<Page> ProcessedPages = new List<Page>();
        internal Queue<Page> Pages = new Queue<Page>();
        internal List<string> AllowedDomains;

        [JsonProperty()]
        internal List<IHint> FoundHints = new List<IHint>();

        private List<IHint> allHints = new List<IHint>();
        private SimpleEventBus eventBus = SimpleEventBus.GetDefaultEventBus();

        public Website (string url, List<IPageValidator> pageValidators, List<IWebsiteValidator> websiteValidators, List<string> allowedDomains)
        {
            startURL = new Uri(url);
            AllowedDomains = allowedDomains;
            PageValidators = pageValidators;
            WebsiteValidators = websiteValidators;
            eventBus.Register(this);
        }

        public List<IHint> GetAllHints()
        {

            AddNewURL(startURL);
            InitWebsiteValidators();
            ProcessList();


            return allHints;
        }

        private void InitWebsiteValidators()
        {
            foreach (IWebsiteValidator validator in WebsiteValidators)
            {
                // the Validate Method injects the found hints directly into the Page object
                try
                {
                    validator.Initialize(this, eventBus);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to initialize WebsiteValidator" + ex.Message);
                }
            }
        }

        private void ProcessList()
        {
            while (Pages.Count > 0)
            {
                Page pageToProcess = Pages.Dequeue();
                if (pageToProcess != null)
                {
                    ProcessedPages.Add(pageToProcess);
                    allHints.AddRange(pageToProcess.GetHints());
                }
            }
        }

        private void AddNewURL(Uri url)
        {
            Page page = new Page(this, url);
            if (!Pages.Contains(page) && !ProcessedPages.Contains(page) && AllowedDomains.Contains(url.Host))
            {
                Pages.Enqueue(page);
                Console.WriteLine("Found new Uri: " + url);
            }           
            
        }

        #region Event bus handlers

        [EventSubscriber]
        public void HandleEvent(PageFound pageFoundEvent)
        {
            AddNewURL(pageFoundEvent.Url);
        }

        #endregion

        #region Website Hints

        public void AddHint(IHint hint)
        {
            FoundHints.Add(hint);
        }

        #endregion
    }
}
