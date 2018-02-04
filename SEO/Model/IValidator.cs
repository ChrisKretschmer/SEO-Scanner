using EventBus;

namespace SEO.Model
{
    public interface IValidator
    {
        void Validate(IAnalyzableElement page, SimpleEventBus eventBus);
    }
}
