using EventBus;

namespace SEO.Model
{
    public interface IPageValidator
    {
        void Validate(IAnalyzablePage page, SimpleEventBus eventBus);
    }
}
