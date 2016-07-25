using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace CoreWeb.Helpers
{
    /// <summary>
    /// Fingerprinter for files.
    /// </summary>
    /// <remarks>
    /// http://madskristensen.net/post/cache-busting-in-aspnet
    /// </remarks>
    public class Fingerprint
    {
        /// <summary>
        /// Re-build a file name by appending the file's write timestamp before the file extension.
        /// This will give it a unique name to the browser for cache busting and will be resolved using url rewriting to the correct physical file.
        /// </summary>
        /// <param name="rootRelativePath">Root relative path to file resource.</param>
        /// <returns>Rewritten file name with versioning in it.</returns>
        public static string Tag(string rootRelativePath)
        {
            if (HttpRuntime.Cache[rootRelativePath] == null)
            {
                var absolute = HostingEnvironment.MapPath("~" + rootRelativePath);
                if (absolute == null)
                {
                    throw new FileNotFoundException("Could not find absolute path from rootRelativePath.", rootRelativePath);
                }

                var date = File.GetLastWriteTimeUtc(absolute);
                var index = rootRelativePath.LastIndexOf('.');

                var result = rootRelativePath.Insert(index, "-ver-" + date.Ticks);
                HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));
            }

            return HttpRuntime.Cache[rootRelativePath] as string;
        }
    }
}