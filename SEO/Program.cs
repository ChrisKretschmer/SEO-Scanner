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

            var validators = new List<Model.IPageValidator>
            {
                new SEO.LinkAnalyzer(),
                new SEO.HeadValidator(),
                new SEO.HeadlineValidator()
            };

            var allowedPaths = new List<string> { "kretschmer-und-kretschmer.de", "www.kretschmer-und-kretschmer.de" };

            website = new Model.Website(url, validators, allowedPaths);
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