using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using AbstractConfigurationManager;

namespace ConfigurationManagerWrapper
{
    public class AppConfigConfigurationManager : IConfigurationManager
    {
        public string AppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name]; 
        }

        public NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}
