using EventBus;
using SEO.Model;

namespace SEO
{
    public class HeadValidator : IPageValidator
    {
        const int TITLE_TAG_MAX_LENGTH = 60;
        const int TITLE_TAG_MIN_LENGTH = 10;

        public void Validate(IAnalyzableElement page, SimpleEventBus eventBus)
        {
            CheckTitleTag(page);
        }

        private void CheckTitleTag(IAnalyzableElement page)
        {
            var titleElements = page.GetHtmlDocument().DocumentNode.SelectNodes("//title");

            if (titleElements.Count > 1)
            {
                page.AddHint(new Hint("Title-TooMany", "More than one title tag", Severity.Major, titleElements[1].Line, titleElements[1].LinePosition));
            }
            else if (titleElements.Count < 1)
            {
                page.AddHint(new Hint("Title-NoTitle", "No title tag"));
            }
            else
            {
                var textContent = titleElements[0].InnerText;
                if (textContent.Length > TITLE_TAG_MAX_LENGTH)
                {
                    page.AddHint(new Hint("Title-TooLong", $"title tag should not have more than {TITLE_TAG_MAX_LENGTH} characters", Severity.Major, titleElements[0].Line, titleElements[0].LinePosition));
                } else if (textContent.Length < TITLE_TAG_MIN_LENGTH)
                {
                    page.AddHint(new Hint("Title-TooShort", $"title tag should have at least {TITLE_TAG_MIN_LENGTH} characters", Severity.Major, titleElements[0].Line, titleElements[0].LinePosition));
                }
            }

        }
    }
}
