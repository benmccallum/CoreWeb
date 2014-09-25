using System;
using System.Configuration;
using System.Globalization;
using CoreSystemEx.Infrastructure.Config;

namespace CoreWebEx.Infrastructure.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebConfigurationManager : IConfigurationManager
    {
        
    }

    /// <summary>
    /// Configuration manager wrapping WebConfigurationManager.
    /// </summary>
    public class WebConfigurationManager : CoreSystemEx.Infrastructure.Config.ConfigurationManager, IWebConfigurationManager
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