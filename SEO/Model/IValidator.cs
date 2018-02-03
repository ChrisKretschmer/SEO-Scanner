using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Model
{
    public interface IValidator
    {
        void Validate(IAnalyzableElement page);
    }
}
