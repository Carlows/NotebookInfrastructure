using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotebook.Site.Settings
{
    public interface ISiteSettings
    {
        string LogFilePath { get; }
    }
}