using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SEO
{
    class Program
    {
        private static Model.Website website;

        static void Main(string[] args)
        {
            string url = "http://www.kretschmer-und-kretschmer.de";

            var pageValidators = new List<Model.IPageValidator>
            {
                new PageValidators.HTMLValidator(),
                new PageValidators.LinkAnalyzer(),
                new PageValidators.HeadValidator(),
                new PageValidators.HeadlineValidator(),
                new PageValidators.TagFoundEmitter()
            };

            var websiteValidators = new List<Model.IWebsiteValidator>
            {
                new WebsiteValidators.HeadValidator()
            };

            var allowedPaths = new List<string> { "kretschmer-und-kretschmer.de", "www.kretschmer-und-kretschmer.de" };

            website = new Model.Website(url, pageValidators, websiteValidators, allowedPaths);
            var hints = website.GetAllHints();

            Console.WriteLine(JObject.FromObject(website));
        
            foreach (Model.IHint item in hints)
            {
                Console.WriteLine(item.Message);
            }
            Console.ReadKey();
        }
    }
}