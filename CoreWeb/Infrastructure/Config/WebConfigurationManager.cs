using System.Configuration;

namespace CoreWeb.Infrastructure.Config
{
    /// <summary>
    /// Interface for a configuraiton manager wrapping the <see cref="WebConfigurationManager"/>.
    /// </summary>
    public interface IWebConfigurationManager : CoreSystem.Infrastructure.Config.IConfigurationManager
    {

    }

    /// <summary>
    /// Configuration manager wrapping the <see cref="WebConfigurationManager"/>.
    /// </summary>
    public class WebConfigurationManager : CoreSystem.Infrastructure.Config.ConfigurationManager, IWebConfigurationManager
    {
        public override string GetAppSetting(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }

        public override ConnectionStringSettings GetConnectionString(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[key];
        }
    }
}