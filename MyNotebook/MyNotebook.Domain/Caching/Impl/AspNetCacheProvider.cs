using System;
using System.Web;
using System.Web.Caching;
using MyNotebook.Domain.Logging;
using MyNotebook.Domain.Settings;

namespace MyNotebook.Domain.Caching.Impl
{
    public class AspNetCacheProvider : ICacheProvider
    {
        private readonly Cache _cache;
        private readonly bool _enableCaching;
        private readonly TimeSpan _defaultExpirationWindow;

        public AspNetCacheProvider(IDomainSettings domainSettings)
        {
            if (System.Web.HttpContext.Current != null)
                _cache = HttpContext.Current.Cache;
            else
                _cache = HttpRuntime.Cache;

            _enableCaching = domainSettings.EnableCaching;
            _defaultExpirationWindow = new TimeSpan(0, domainSettings.DefaultCacheExpirationInMin, 0);
        }

        public void Add(string cacheKey, object dataToAdd)
        {
            _cache.Add(cacheKey, dataToAdd, null, Cache.NoAbsoluteExpiration,
                    _defaultExpirationWindow, CacheItemPriority.BelowNormal, null);
        }

        public T Get<T>(string key) where T : class
        {
            return _enableCaching ? (T)_cache.Get(key) : null;
        }

        public T TryGet<T>(string key, Func<T> fallback) where T : class
        {
            try
            {
                var results = Get<T>(key);

                if (results != null)
                    return results;

                results = fallback();

                if (results != null)
                    Add(key, results);

                return results;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public void InvalidateCacheItem(string key)
        {
            if (_enableCaching && _cache.Get(key) != null)
                _cache.Remove(key);
        }
    }
}
