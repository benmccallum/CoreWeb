using System;
using System.Configuration;
using System.Globalization;

namespace CoreWeb.Infrastructure.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebConfigurationManager : CoreSystem.Infrastructure.Config.IConfigurationManager
    {
        
    }

    /// <summary>
    /// Configuration manager wrapping WebConfigurationManager.
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