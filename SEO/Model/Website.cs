using EventBus;
using Newtonsoft.Json;
using SEO.DomainEvents;
using System;
using System.Collections.Generic;

namespace SEO.Model
{
    public class Website
    {
        public Uri startURL { get; }
        internal List<IPageValidator> Validators { get; }

        [JsonProperty()]
        internal List<Page> ProcessedPages = new List<Page>();
        internal Queue<Page> Pages = new Queue<Page>();
        internal List<string> AllowedDomains;

        private List<IHint> allHints = new List<IHint>();
        private SimpleEventBus eventBus = SimpleEventBus.GetDefaultEventBus();

        public Website (string url, List<IPageValidator> validators, List<string> allowedDomains)
        {
            startURL = new Uri(url);
            AllowedDomains = allowedDomains;
            Validators = validators;
            eventBus.Register(this);
        }

        public List<IHint> GetAllHints()
        {

            AddNewURL(startURL);
            ProcessList();


            return allHints;
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
    }
}
