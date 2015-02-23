using System.Configuration;
using MyNotebook.Domain.Settings;

namespace MyNotebook.Site.Settings.Impl
{
    public class ApplicationSettings : ISiteSettings, IDomainSettings
    {
        public bool EnableCaching
        {
            get
            {
                bool enableCaching;
                if (bool.TryParse(ConfigurationManager.AppSettings["EnableCaching"], out enableCaching))
                    return enableCaching;

                return true;
            }
        }

        public int DefaultCacheExpirationInMin
        {
            get
            {
                int defaultCacheExInMin;
                if (int.TryParse(ConfigurationManager.AppSettings["EnableCaching"], out defaultCacheExInMin))
                    return defaultCacheExInMin;

                return 5;
            }
        }

        public string LogFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFilePath"];
            }
        }
    }
}