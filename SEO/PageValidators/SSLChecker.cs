using EventBus;
using SEO.Model;

namespace SEO.PageValidators
{
    class SSLChecker : IPageValidator
    {
        public void Validate(IAnalyzablePage page, SimpleEventBus eventBus)
        {
            var request = page.GetPageRequestObject();

            if (request.ResponseUri.Scheme == "http")
            {
                page.AddHint(new Hint("Protocol-HTTP", "The page was served using HTTP. Please use HTTPS!"));
            }
        }
    }
}