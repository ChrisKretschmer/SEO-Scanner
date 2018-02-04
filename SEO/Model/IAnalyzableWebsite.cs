using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Model
{
    public interface IAnalyzableWebsite
    {
        void AddHint(IHint hint);
    }
}
