using System;
using System.Web;

namespace CoreWeb.Helpers
{
    /// <summary>
    /// This class provides funtionalities to add or retrieve object to and from System.HttpContext.Current.Cache.
    /// </summary>
    public class CacheManager
    {
        /// <summary>
        /// Add object to cache.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheMinutes"></param>
        /// <param name="cacheObject"></param>
        public static void AddToCache(string cacheKey, int cacheMinutes, object cacheObject)
        {
            if (HttpContext.Current != null && cacheObject != null)
            {
                var cache = HttpContext.Current.Cache;
                if (cache[cacheKey] == null)
                {
                    cache.Add(cacheKey,
                              cacheObject,
                              null,
                              DateTime.Now.AddMinutes(cacheMinutes),
                              System.Web.Caching.Cache.NoSlidingExpiration,
                              System.Web.Caching.CacheItemPriority.Normal,
                              null);
                }
            }
        }

        /// <summary>
        /// Returns an object from cache by using the cacheKey. Null must be checked by the calling method.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetFromCache(string cacheKey)
        {
            if (HttpContext.Current != null)
            {
                var cache = HttpContext.Current.Cache;
                return cache[cacheKey];
            }
            return null;
        }
    }
}