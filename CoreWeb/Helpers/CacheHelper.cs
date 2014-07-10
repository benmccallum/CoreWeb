using CoreSystem.Sys.Linq.Expressions;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace CoreWeb.Helpers
{
    /// <summary>
    /// A cache helper to help management of caches.
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// Generates the cache key based on expression.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        private static string GenerateCacheKey<TResult>(Expression<Func<TResult>> factory, string extraCacheKey)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            var methodCallExpression = factory.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException("factory must contain a single MethodCallExpression.");
            }

            var cacheKeyBuilder = new StringBuilder(100);
            cacheKeyBuilder.Append("|Repository:" + methodCallExpression.Method.DeclaringType.FullName + "|Method:" + methodCallExpression.Method.Name + "|Args");

            foreach (var arg in methodCallExpression.Arguments)
            {
                cacheKeyBuilder.Append(":" + ExpressionHelper.GetValue(arg));
            }
            cacheKeyBuilder.Append("|ExtraCacheKey:").Append(extraCacheKey);

            return cacheKeyBuilder.ToString();
        }

        /// <summary>
        /// Gets the cached object. The method creates the object if not already existed based on expression passed.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="factory"></param>
        /// <param name="durationInSeconds"></param>
        /// <returns></returns>
        public static TResult Get<TResult>(Expression<Func<TResult>> factory, int durationInSeconds, string extraCacheKey = "")
        {
            var cacheKey = GenerateCacheKey<TResult>(factory, extraCacheKey);
            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                TResult result = factory.Compile().Invoke();
                if (result != null)
                {
                    HttpContext.Current.Cache.Add(cacheKey,
                        result,
                        null,
                        DateTime.Now.AddSeconds(durationInSeconds),
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Default,
                        null);
                }
            }
            return (TResult)HttpContext.Current.Cache[cacheKey];
        }

        /// <summary>
        /// Clears the particular cache object based on expression passed.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="factory"></param>
        public static void Clear<TResult>(Expression<Func<TResult>> factory, string extraCacheKey = "")
        {
            var cacheKey = GenerateCacheKey<TResult>(factory, extraCacheKey);
            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                HttpContext.Current.Cache[cacheKey] = null;
            }
        }
    }
}