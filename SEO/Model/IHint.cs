using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Model
{
    public enum Severity
    {
        Minor,
        Major,
        Critical
    }

    public interface IHint
    {
        string Message { get; set; }
        int Line { get; set; }
        int Position { get; set; }

        string Code { get; set; }

        Severity Severity { get; set; }


    }
}
