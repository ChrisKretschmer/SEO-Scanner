using System.Collections.Generic;
using EventBus;
using SEO.DomainEvents;
using SEO.Model;

namespace SEO.WebsiteValidators
{
    class HeadValidator : BaseValidator
    {
        public override void Initialize(IAnalyzableWebsite website, SimpleEventBus eventBus)
        {
            base.Initialize(website, eventBus);

        }

        [EventSubscriber]
        public void HandleEvent(TagFound tagFoundEvent)
        {
            CheckForDuplicateTitleTagContent(tagFoundEvent);
            
        }

        #region Checks

        private List<string> PreviousTitleTagContents = new List<string>();

        private void CheckForDuplicateTitleTagContent(TagFound tagFoundEvent)
        {
            if (tagFoundEvent.TagName == "title")
            {
                var content = tagFoundEvent.Node.InnerText;
                if (PreviousTitleTagContents.Contains(content))
                {
                    _website.AddHint(new WebsiteHint("Title-TextDuplicate", "Some of your pages have the same title.", tagFoundEvent.Page, additionalInfo: content));
                }
                PreviousTitleTagContents.Add(content);
            }
        }

        #endregion

    }
}
