using SEO.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SEO.Validators.LinkAnalyzer
{
    public class LinkAnalyzer : IValidator
    {
        public void Validate(IAnalyzableElement page)
        {
            var content = page.GetHtmlDocument();

            var links = content.DocumentNode.SelectNodes("//a");

            foreach (var link in links)
            {
                var hint = new Hint("Test", link.GetAttributeValue("href", null));
                if (hint != null) {
                    page.AddHint(hint);
                }
            }
        }

    }
}