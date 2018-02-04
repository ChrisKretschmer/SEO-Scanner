using EventBus;
using SEO.Model;

namespace SEO.Validators.HeadlineValidator
{
    class HeadlineValidator : IValidator
    {
        const int MIN_H1_COUNT = 1;
        const int MIN_H2_COUNT = 0;
        const int MIN_H3_COUNT = 0;
        const int MIN_H4_COUNT = 0;
        const int MIN_H5_COUNT = 0;
        const int MIN_H6_COUNT = 0;

        const int MAX_H1_COUNT = 3;
        const int MAX_H2_COUNT = 5;
        const int MAX_H3_COUNT = 10;
        const int MAX_H4_COUNT = 10;
        const int MAX_H5_COUNT = 10;
        const int MAX_H6_COUNT = 10;

        public void Validate(IAnalyzableElement page, SimpleEventBus eventBus)
        {
            CheckHeadlines(page, "h1", MIN_H1_COUNT, MAX_H1_COUNT);
            CheckHeadlines(page, "h2", MIN_H2_COUNT, MAX_H2_COUNT);
            CheckHeadlines(page, "h3", MIN_H3_COUNT, MAX_H3_COUNT);
            CheckHeadlines(page, "h4", MIN_H4_COUNT, MAX_H4_COUNT);
            CheckHeadlines(page, "h5", MIN_H5_COUNT, MAX_H5_COUNT);
            CheckHeadlines(page, "h6", MIN_H6_COUNT, MAX_H6_COUNT);
        }

        public void CheckHeadlines(IAnalyzableElement page, string tag, int min, int max)
        {
            var headlineElements = page.GetHtmlDocument().DocumentNode.SelectNodes("//" + tag);
            if (headlineElements == null)
            {
                if (min > 0)
                {
                    page.AddHint(new Hint("Headline-TooFew", $"Too few <{tag}> tags - should be more than {min}"));
                }
                return;
            }


            if (headlineElements.Count > max)
            {
                page.AddHint(new Hint("Headline-TooMany", $"Too many <{tag}> tags - should be less than {max}"));
            }
            else if (headlineElements.Count < min)
            {
                page.AddHint(new Hint("Headline-TooFew", $"Too few <{tag}> tags - should be more than {min}"));
            }
        }
    }
}
