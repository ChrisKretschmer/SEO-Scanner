using EventBus;

namespace SEO.Model
{
    public interface IPageValidator
    {
        void Validate(IAnalyzableElement page, SimpleEventBus eventBus);
    }
}
