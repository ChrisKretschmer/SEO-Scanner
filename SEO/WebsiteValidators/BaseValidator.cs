using EventBus;
using SEO.Model;

namespace SEO.WebsiteValidators
{
    public abstract class BaseValidator : IWebsiteValidator
    {
        protected SimpleEventBus _eventbus;
        protected IAnalyzableWebsite _website;

        public virtual void Initialize(IAnalyzableWebsite website, SimpleEventBus eventBus)
        {
            _website = website;
            _eventbus = eventBus;
            _eventbus.Register(this);
        }
    }
}