using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Model
{
    public class Website
    {
        public string startURL { get; private set; }
        public List<IValidator> Validators { get; private set; }

        internal List<Page> ProcessedPages = new List<Page>();
        internal Queue<Page> Pages = new Queue<Page>();

        private List<IHint> allHints = new List<IHint>();

        public Website (string url, List<IValidator> validators)
        {
            startURL = url;
            Validators = validators;
        }

        public List<IHint> GetAllHints()
        {
            
            Page startPage = new Page(this, startURL);
            Pages.Enqueue(startPage);
            ProcessList();


            return allHints;
        }

        private void ProcessList()
        {
            while (Pages.Count > 0)
            {
                Page pageToProcess = Pages.Dequeue();
                allHints.AddRange(pageToProcess.GetHints());
                ProcessedPages.Add(pageToProcess);
            }
        }
    }
}
