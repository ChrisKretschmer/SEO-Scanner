using EventBus;

namespace SEO.Model
{
    public interface IWebsiteValidator
    {
        void Initialize(IAnalyzableWebsite website, SimpleEventBus eventBus);
    }
}
