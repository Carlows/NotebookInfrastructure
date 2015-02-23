using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook.Domain.Settings
{
    public interface IDomainSettings
    {
        bool EnableCaching { get; }

        int DefaultCacheExpirationInMin { get; }
    }
}
