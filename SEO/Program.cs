using System;
using System.Collections.Generic;

namespace SEO
{
    class Program
    {
        static private Model.Website website;

        static void Main(string[] args)
        {
            string url = "http://www.kretschmer-und-kretschmer.de";
            var validators = new List<Model.IValidator>();

            validators.Add(new Validators.LinkAnalyzer.LinkAnalyzer());
            validators.Add(new Validators.HeadValidator.HeadValidator());
            validators.Add(new Validators.HeadlineValidator.HeadlineValidator());

            website = new Model.Website(url, validators);
            var hints = website.GetAllHints();

            foreach (Model.IHint item in hints)
            {
                Console.WriteLine(item.Message);
            }
            Console.ReadKey();
        }
    }
}